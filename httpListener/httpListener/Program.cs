using System;
using System.Net;
using System.Threading.Tasks;


namespace httpListener
{
    class Program
    {
        //public static object AsyncContext { get; private set; }
        public static HttpListener regListener = new HttpListener();
        public static HttpListener loginListener = new HttpListener();
        static void Main(string[] args)
        {
            Task.Run(async () => { await Listen(); }).Wait();

        
            Console.WriteLine("Обработка подключений завершена");
            Console.Read();
        }

        private static Task Listen()
        {
            //HttpListener usersListener = new HttpListener();
            loginListener.Prefixes.Add("http://localhost:8888/login/");
            regListener.Prefixes.Add("http://localhost:8888/reg/");

          

            regListener.Start();
            loginListener.Start();

            Console.WriteLine("Ожидание подключений...");

            while (true)
            {

                Task.WhenAny(Program.AwaitLogin(), Program.AwaitReg());
                //HttpListenerContext context = await usersListener.GetContextAsync();
                //Task.Run(async () => { await Program.AwaitUsers(); }).Wait();
                //Task.Run(async () => { await Program.AwaitLogin(); }).Wait();
                //HttpListenerContext context1 = await loginListener.GetContextAsync();
                /* ClientObject clientObject = new ClientObject(context);

                 // создаем новый поток для обслуживания нового клиента
                 Task clientTask = new Task(clientObject.Process);
                 clientTask.Start();*/

            }
        }

        public static async Task AwaitLogin()
        {
            HttpListenerContext context = await loginListener.GetContextAsync();
            ClientLogin clientObject = new ClientLogin(context);

            // создаем новый поток для обслуживания нового клиента
            Task clientTask = new Task(clientObject.Process);
            clientTask.Start();
        }

        public static async Task AwaitReg()
        {
            HttpListenerContext context = await regListener.GetContextAsync();
            Console.WriteLine("Подключился регистрирующийся");
            ClientReg clientObject = new ClientReg(context);

            // создаем новый поток для обслуживания нового клиента
            Task clientTask = new Task(clientObject.Process);
            clientTask.Start();
        }
    }
}
