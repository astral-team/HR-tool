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

        public static string login = "pag";
        public static string password = "1234";
        //public static HttpClient client = ClientHelper.GetClient(login, password);
        public static RestClient client = new RestClient();
        public static Uri uri = new Uri("http://localhost:8888");



        static void Main(string[] args)
        {
            client.BaseUrl = uri;
            client.Authenticator = new HttpBasicAuthenticator(login, password);
            var request = new RestRequest("resource", Method.POST);
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            
            //Task.Run(async () => { await GetProductAsync(); }).Wait();
            //Task.Run(async () => { await PostProductAsync(); }).Wait();

        }
    }
}
