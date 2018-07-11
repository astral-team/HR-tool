using System;
using System.Net.Sockets;
using System.IO;

namespace FinanceClient
{
    class Program
    {
        const int PORT = 5006;
        const string ADDRESS = "127.0.0.1";

        static void Main(string[] args)
        {
            TcpClient client = null;
            try
            {
                Console.WriteLine("Для входа в систему введите логин и пароль");
                Console.Write("Логин: ");
                string login = Console.ReadLine();
                Console.Write("Пароль: ");
                string password = Console.ReadLine();

                client = new TcpClient(ADDRESS, PORT);
                NetworkStream stream = client.GetStream();

                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(login);
                writer.Write(password);
                writer.Flush();

                BinaryReader reader = new BinaryReader(stream);
                string hash = reader.ReadString();
                Console.WriteLine("Хэш: " + hash);

                reader.Close();
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
            Console.Read();
        }
    }
}