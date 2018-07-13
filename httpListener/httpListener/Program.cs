using System;
using System.Net;
using System.Threading.Tasks;


namespace httpListener
{
    class Program
    {
        //public static object AsyncContext { get; private set; }

        static void Main(string[] args)
        {
            Task.Run(async () => { await Listen(); }).Wait();

        
            Console.WriteLine("Обработка подключений завершена");
            Console.Read();
        }

        private static async Task Listen()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://10.254.4.116:8888/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                ClientObject clientObject = new ClientObject(context);

                // создаем новый поток для обслуживания нового клиента
                Task clientTask = new Task(clientObject.Process);
                clientTask.Start();
            }
        }
    }
}
