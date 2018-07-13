using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRClient
{
    class Program
    {
        public static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            //Task.Run(async () => { await GetProductAsync(); }).Wait();
            Task.Run(async () => { await PostProductAsync(); }).Wait();
        }

        /*static async Task GetProductAsync()
        {
            var responseString = await client.GetStringAsync("http://10.254.4.213:8888");
            
        }*/

        static async Task PostProductAsync()
        {
            var values = new Dictionary<string, string>
            { 
                { "thing1", "hello" },
                { "thing2", "world" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://localhost:8888",content);

            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
