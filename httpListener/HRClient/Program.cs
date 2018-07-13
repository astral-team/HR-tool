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
            
            Uri uri = new Uri("http://10.254.4.116:8888?ololo=xyu");

            client.BaseAddress = uri;
            Task.Run(async () => { await GetProductAsync(); }).Wait();
            //Task.Run(async () => { await PostProductAsync(); }).Wait();

        }

        static async Task GetProductAsync()
        {
            var responseString = await client.GetStringAsync("http://10.254.4.116:8888?ololo=xyu");
            
        }

        static async Task PostProductAsync()
        {
            var values = new Dictionary<string, string>
            {
             { "thing1", "hello" },
             { "thing2", "world" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://10.254.4.116:8888/", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
