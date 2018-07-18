using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HRClient
{
    class Program
    {


        public static RestClient client = new RestClient();

        static void Main(string[] args)
        {
            Console.Write("Регистрация/логин - reg\nКоманда: ");
            while (true)
            {
                try
                {
                    string login = "";
                    string password = "";
                    string command = "";
                    string IP = "http://localhost:8888/";
                    command = Console.ReadLine();

                    switch (command)
                    {
                        case "reg":

                            IP += "reg/";

                            client.BaseUrl = new Uri(IP);

                            client.Authenticator = new HttpBasicAuthenticator(login, password);

                            Console.WriteLine("Данные для входа/регистрации/удаления");

                            Console.Write("Login: ");
                            login = Console.ReadLine();
                            Console.Write("Password: ");
                            password = Console.ReadLine();

                            Console.Write("Регистрация - reg\nАвторизация - login\nУдаление - del\nКоманда: ");

                            command = Console.ReadLine();

                            var request = new RestRequest();

                            switch (command)
                            {
                                case "reg":

                                    request = new RestRequest("resource", Method.POST);
                                    request.AddHeader("login", login);

                                    break;

                                case "login":

                                    request = new RestRequest("resource", Method.GET);
                                    request.AddHeader("login", login);

                                    break;

                                case "del":

                                    request = new RestRequest("resource", Method.DELETE);
                                    request.AddHeader("login", login);

                                    break;

                                default:

                                    Console.WriteLine("Неправильная команда");

                                    break;
                            }
                            
                            var response = client.Execute(request);

                            Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");

                            break;

                        case "login":
                            
                            break;

                        case "del":
                            
                            break;

                        default:
                            Console.WriteLine("Неправильная команда");
                            break;
                    }

                    /*Console.Write("reg, login, logout, delBase, del :");
                    command = Console.ReadLine();

                    if (login == "")
                    {
                        Console.Write("Login: ");
                        login = Console.ReadLine();
                        Console.Write("Password: ");
                        password = Console.ReadLine();
                    }

                    client.Authenticator = new HttpBasicAuthenticator(login, password);
                    var request = new RestRequest("resource", Method.POST);
                    request.AddHeader("command", command);
                    var response = client.Execute(request);
                    Console.WriteLine(response.Content);*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {

                }
            }
        }
    }
}
