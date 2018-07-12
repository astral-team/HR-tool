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
                bool exit = true;
                while (exit)
                {
                    Console.WriteLine("Log - войти в систему\nReg - регистрация\nDel - удаление базы\nExit - закрыть программу");
                    Console.Write("Команда: ");
                    string command = Console.ReadLine();
                    Console.Write("Логин: ");
                    string Login = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string Password = Console.ReadLine();

                    if (command == "Exit")
                    {
                        exit = false;
                        break;
                    }

                    client = new TcpClient(ADDRESS, PORT);
                    NetworkStream stream = client.GetStream();

                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(command);
                    writer.Write(Login);
                    writer.Write(Password);
                    writer.Flush();

                    BinaryReader reader = new BinaryReader(stream);
                    string message = reader.ReadString();
                    Console.WriteLine("Message: " + message);

                    reader.Close();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}