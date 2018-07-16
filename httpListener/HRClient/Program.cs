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

        public static string login = "";
        public static string password = "";
        //public static HttpClient client = ClientHelper.GetClient(login, password);
        public static RestClient client = new RestClient();
        public static Uri uri = new Uri("http://localhost:8888/");



        static void Main(string[] args)
        {
            string command = "";
            client.BaseUrl = uri;
            while (true)
            {
                try
                {
                    Console.Write("reg, login, logout, delBase, del :");
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
                    Console.WriteLine(response.Content);
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
