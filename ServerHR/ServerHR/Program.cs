using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace FinanceServer
{
    class Program
    {
        const int PORT = 5006; // порт для прослушивания подключений
        static TcpListener listener;
        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, PORT);
                listener.Start();
                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);

                    // создаем новый поток для обслуживания нового клиента
                    Task clientTask = new Task(clientObject.Process);
                    clientTask.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}