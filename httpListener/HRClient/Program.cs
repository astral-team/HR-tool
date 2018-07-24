using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;

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
            string SessionKey = "";
            while (true)
            {
                try
                {
                    
                    string command = "";
                    string IP = "http://10.254.4.116:8888/";
                    command = Console.ReadLine();


                    
                    var request = new RestRequest();
                    request.RequestFormat = DataFormat.Json;
                    var openWith = new Person();
                    IRestResponse response = null;
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

                            response = client.Execute<Person>(request);
                            Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");

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

                            response = client.Execute<Person>(request);
                            Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");
                            if ((response.Content!="404")&&(response.Content !=SessionKey))
                            {
                                SessionKey = response.Content;
                            }

                            break;
                        case "logout":


                            login = "";
                            password = "";
                            Console.WriteLine("logout complete");

                            SessionKey = "";

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
                            if (SessionKey != "")
                            {
                                request.AddHeader("SessionKey", SessionKey);
                            }

                            response = client.Execute<Person>(request);
                            Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");

                            break;

                        case "addProfile":
                            IP += "profiles/";

                            client.BaseUrl = new Uri(IP);
                            
                            Console.WriteLine("Заполните профиль");
                            request = new RestRequest("resource", Method.POST);
                            List<Person> closeWith = new List<Person>();


                             #region это дерьмо лучше скрыть (Имя великого человека)
                             //-----------------------
                             //здесь происходит заполнение профлия и заполнение им тела запроса



                            
                             Console.WriteLine("Введите Имя пользователя");
                             openWith.Prof.FullName=Console.ReadLine();
                             Console.WriteLine("Введите BirthDate");
                             openWith.Prof.BirthDate =DateTimeOffset.Now;
                            Console.WriteLine("Введите PhoneNumber");
                             openWith.Prof.PhoneNumer = Console.ReadLine();
                            Console.WriteLine("Введите Email");
                             openWith.Prof.Email = Console.ReadLine();
                            Console.WriteLine("Введите Sex");
                             openWith.Prof.Sex =true;
                            Console.WriteLine("Введите Position");
                             openWith.Prof.Position = Console.ReadLine();
                             Console.WriteLine("Введите Education");
                             openWith.Prof.Education = Console.ReadLine();
                            Console.WriteLine("Введите MaritalStatus");
                             openWith.Prof.MaritalStatus = Console.ReadLine();
                            Console.WriteLine("Введите City");
                             openWith.Prof.City = Console.ReadLine();
                            Console.WriteLine("Введите Photo");
                            openWith.Prof.Photo = new byte[0x10];
                             Console.WriteLine("Введите Citizen");
                             openWith.Prof.Сitizen = Console.ReadLine();
                            Console.WriteLine("Введите About");
                             openWith.Prof.About = Console.ReadLine();


                            //Console.WriteLine("Введите Experience");
                            Experience ex = new Experience();
                            ex.CompanyName = "";
                            ex.Position = "asd";
                            ex.FromDate = DateTimeOffset.Now;
                            ex.ToDate = DateTimeOffset.Now;
                            ex.About = "";
                            ex.City = "";
                            openWith.Exp.Add(ex);

                            /*Console.WriteLine("Введите Experience");
                             openWith.Experience = Console.ReadLine();*/
                             openWith.Prof.Responed = false;
                            Console.WriteLine("Введите ResumeLink");
                             openWith.Prof.ResumeLink = Console.ReadLine();
                            Console.WriteLine("Введите Interviews");
                            openWith.Prof.Interviews = Console.ReadLine();

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
                            closeWith.Add(openWith);
                            openWith = new Person();
                            openWith.Prof.FullName = "sdfbd";
                            openWith.Prof.Position = "sdfbd";
                            closeWith.Add(openWith);

                            request.RequestFormat = RestSharp.DataFormat.Json;
                            request.AddBody(closeWith);


                            client.Authenticator = new HttpBasicAuthenticator(login, password);
                            request.AddHeader("login", login);
                            if (SessionKey != "")
                                request.AddHeader("SessionKey", SessionKey);

                            response = client.Execute<List<Person>>(request);
                            Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");


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
                            if (SessionKey != "")
                                request.AddHeader("SessionKey", SessionKey);

                            response = client.Execute<Person>(request);
                            Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");

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
                            if (SessionKey != "")
                                request.AddHeader("SessionKey", SessionKey);


                            response = client.Execute<Person>(request);
                            Console.Write($"Ответ сервера: {response.Content}\n\n\nКоманда: ");


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
