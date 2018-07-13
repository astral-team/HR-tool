using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HRClient
{
    class ClientHelper
    {
        public static HttpClient GetClient(string login, string password)
        {
            var authValue = new AuthenticationHeaderValue("basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}")));
            HttpClient client = new HttpClient() { DefaultRequestHeaders = { Authorization = authValue } };
            return client;
        }
    }
}
