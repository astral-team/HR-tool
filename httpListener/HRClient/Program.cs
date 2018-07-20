using Newtonsoft.Json;
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
            Console.Write("Регистрация - reg\nАвторизация - login\nУдаление - del\nДобавить профиль - addProfile\nЗапросить профиль - getProfile\nУдалить профиль - deleteProfile\n\nКоманда: ");
            string login = "";
            string password = "";
            while (true)
            {
                try
                {
                    
                    string command = "";
                    string IP = "http://10.254.4.116:8888/";
                    command = Console.ReadLine();



                    var request = new RestRequest();

                    switch (command)
                    {
                        case "reg":
                            IP += "reg/";

                            client.BaseUrl = new Uri(IP);

                            
                            Console.WriteLine("Данные для входа/регистрации/удаления");
                           
                                Console.Write("Login: ");
                                login = Console.ReadLine();
                                Console.Write("Password: ");
                                password = Console.ReadLine();



                            client.Authenticator = new HttpBasicAuthenticator(login, password);

                            request = new RestRequest("resource", Method.POST);
                            request.AddHeader("login", login);

                            break;

                        case "login":
                            IP += "reg/";

                            client.BaseUrl = new Uri(IP);

                            

                            if ((login == "") && (password == ""))
                            {
                                Console.WriteLine("Данные для входа/регистрации/удаления");
                                Console.Write("Login: ");
                                login = Console.ReadLine();
                                Console.Write("Password: ");
                                password = Console.ReadLine();
                            }

                            client.Authenticator = new HttpBasicAuthenticator(login, password);
                            request = new RestRequest("resource", Method.GET);
                            request.AddHeader("login", login);

                            break;
                        case "logout":


                            login = "";
                            password = "";
                            Console.WriteLine("logout complete");
                            break;
                        case "del":
                            IP += "reg/";

                            client.BaseUrl = new Uri(IP);

                            if ((login == "") && (password == ""))
                            {
                                Console.WriteLine("Данные для входа/регистрации/удаления");
                                Console.Write("Login: ");
                                login = Console.ReadLine();
                                Console.Write("Password: ");
                                password = Console.ReadLine();
                            }
                            client.Authenticator = new HttpBasicAuthenticator(login, password);
                            request = new RestRequest("resource", Method.DELETE);
                            request.AddHeader("login", login);

                            break;

                        case "addProfile":
                            IP += "profiles/";

                            client.BaseUrl = new Uri(IP);

                            if ((login == "") && (password == ""))
                            {
                                Console.WriteLine("Данные для входа/регистрации/удаления");
                                Console.Write("Login: ");
                                login = Console.ReadLine();
                                Console.Write("Password: ");
                                password = Console.ReadLine();
                            }
                            Console.WriteLine("Заполните профиль");
                            request = new RestRequest("resource", Method.POST);

                            #region это дерьмо лучше скрыть (Имя великого человека)
                            //-----------------------
                            //здесь происходит заполнение профлия и заполнение им тела запроса



                            Dictionary<string, string> openWith =
                               new Dictionary<string, string>();
                            Console.WriteLine("Введите Имя пользователя");
                            openWith.Add("FullName", Console.ReadLine());
                            Console.WriteLine("Введите BirthDate");
                            openWith.Add("BirthDate", DateTimeOffset.Now.ToString());
                            Console.WriteLine("Введите PhoneNumber");
                            openWith.Add("PhoneNumber", Console.ReadLine());
                            Console.WriteLine("Введите Email");
                            openWith.Add("Email", Console.ReadLine());
                            Console.WriteLine("Введите Sex");
                            openWith.Add("Sex", Console.ReadLine());
                            Console.WriteLine("Введите Position");
                            openWith.Add("Position", Console.ReadLine());
                            Console.WriteLine("Введите Education");
                            openWith.Add("Education", Console.ReadLine());
                            Console.WriteLine("Введите MaritalStatus");
                            openWith.Add("MaritalStatus", Console.ReadLine());
                            Console.WriteLine("Введите City");
                            openWith.Add("City", Console.ReadLine());
                            Console.WriteLine("Введите Photo");
                            openWith.Add("Photo", Console.ReadLine());
                            Console.WriteLine("Введите Citizen");
                            openWith.Add("Citizen", Console.ReadLine());
                            Console.WriteLine("Введите About");
                            openWith.Add("About", Console.ReadLine());
                            Console.WriteLine("Введите Experience");
                            openWith.Add("Experience", Console.ReadLine());
                            Console.WriteLine("Введите Responed");
                            openWith.Add("Responed", Console.ReadLine());
                            Console.WriteLine("Введите ResumeLink");
                            openWith.Add("ResumeLink", Console.ReadLine());
                            Console.WriteLine("Введите Interviews");
                            openWith.Add("Interviews", Console.ReadLine());
                            //Console.WriteLine("Введите Имя пользователя");
                            //request.AddHeader("FullName", Console.ReadLine());
                            //Console.WriteLine("Введите BirthDate");
                            //request.AddHeader("BirthDate", Console.ReadLine());
                            //Console.WriteLine("Введите PhoneNumber");
                            //request.AddHeader("PhoneNumber", Console.ReadLine());
                            //Console.WriteLine("Введите Email");
                            //request.AddHeader("Email", Console.ReadLine());
                            //Console.WriteLine("Введите Sex");
                            //request.AddHeader("Sex", Console.ReadLine());
                            //Console.WriteLine("Введите Position");
                            //request.AddHeader("Position", Console.ReadLine());
                            //Console.WriteLine("Введите Education");
                            //request.AddHeader("Education", Console.ReadLine());
                            //Console.WriteLine("Введите MaritalStatus");
                            //request.AddHeader("MaritalStatus", Console.ReadLine());
                            //Console.WriteLine("Введите City");
                            //request.AddHeader("City", Console.ReadLine());
                            //Console.WriteLine("Введите Photo");
                            //request.AddHeader("Photo", Console.ReadLine());
                            //Console.WriteLine("Введите Citizen");
                            //request.AddHeader("Citizen", Console.ReadLine());
                            //Console.WriteLine("Введите About");
                            //request.AddHeader("About", Console.ReadLine());
                            //Console.WriteLine("Введите Experience");
                            //request.AddHeader("Experience", Console.ReadLine());
                            //Console.WriteLine("Введите Responed");
                            //request.AddHeader("Responed", Console.ReadLine());
                            //Console.WriteLine("Введите ResumeLink");
                            //request.AddHeader("ResumeLink", Console.ReadLine());
                            //Console.WriteLine("Введите Interviews");
                            //request.AddHeader("Interviews", Console.ReadLine());

                            //-----------------------
                            #endregion

                            request.RequestFormat = RestSharp.DataFormat.Json;
                            request.AddBody(JsonConvert.SerializeObject(openWith));

                            client.Authenticator = new HttpBasicAuthenticator(login, password);
                           
                            request.AddHeader("login", login);

                            break;

                        case "getProfile":
                            IP += "profiles/";

                            client.BaseUrl = new Uri(IP);

                            if ((login == "") && (password == ""))
                            {
                                Console.WriteLine("Данные для входа/регистрации/удаления");
                                Console.Write("Login: ");
                                login = Console.ReadLine();
                                Console.Write("Password: ");
                                password = Console.ReadLine();
                            }
                            Console.WriteLine("Заполните профиль");
                            Console.WriteLine("Введите Имя пользователя");
                            //-----------------------
                            //здесь происходит заполнение профлия и заполнение им тела запроса

                            //-----------------------

                            client.Authenticator = new HttpBasicAuthenticator(login, password);
                            request = new RestRequest("resource", Method.GET);

                            //вот сюда писать тело
                            request.AddHeader("login", login);

                            break;

                        case "deleteProfile":
                            IP += "profiles/";

                            client.BaseUrl = new Uri(IP);

                            if ((login == "") && (password == ""))
                            {
                                Console.WriteLine("Данные для входа/регистрации/удаления");
                                Console.Write("Login: ");
                                login = Console.ReadLine();
                                Console.Write("Password: ");
                                password = Console.ReadLine();
                            }
                            Console.WriteLine("Заполните профиль");
                            Console.WriteLine("Введите Имя пользователя");
                            //-----------------------
                            //здесь происходит заполнение профлия и заполнение им тела запроса

                            //-----------------------

                            client.Authenticator = new HttpBasicAuthenticator(login, password);
                            request = new RestRequest("resource", Method.DELETE);
                            request.AddHeader("login", login);





                            break;
                        default:


                            Console.WriteLine("Неправильная команда");

                            break;
                    }

                    var response = client.Execute(request);

                    Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");

                   



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
