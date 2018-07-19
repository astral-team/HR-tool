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
            string ip = "http://localhost:8888/";
            //HttpListener usersListener = new HttpListener();
            loginListener.Prefixes.Add(ip+"login/");
            regListener.Prefixes.Add(ip+"reg/");

            
            regListener.Start();
            loginListener.Start();

            Console.WriteLine("Ожидание подключений...");

            while (true)
            {

                Task.WaitAny(AwaitReg());
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

        //public static async Task AwaitLogin()
        //{
        //    HttpListenerContext context = await loginListener.GetContextAsync();
        //    ClientLogin clientObject = new ClientLogin(context);

        //    // создаем новый поток для обслуживания нового клиента
        //    Task clientTask = new Task(clientObject.Process);
        //    clientTask.Start();
        //}

        public static async Task AwaitReg()
        {
            HttpListenerContext context = await regListener.GetContextAsync();
            Console.WriteLine("Подключился регистрирующийся");
            DateTime BeginTime = DateTime.Now;
            ClientReg clientObject = new ClientReg(context);

            // создаем новый поток для обслуживания нового клиента
            Task clientTask = new Task(clientObject.Process);
            clientTask.Start();
            Task.WaitAny(clientTask);
            TimeSpan rez = DateTime.Now - BeginTime;
            Console.WriteLine($"Время выполнения = {rez}\n");
        }
    }
}