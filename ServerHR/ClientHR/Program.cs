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
                Console.WriteLine("Для регистрации вклада введите данные!");
                Console.Write("Укажите ваше имя: ");
                string userName = Console.ReadLine();

                Console.Write("Укажите сумму вклада: ");
                decimal sum = Decimal.Parse(Console.ReadLine());

                Console.Write("Укажите период вклада в месяцах: ");
                int period = Int32.Parse(Console.ReadLine());

                client = new TcpClient(ADDRESS, PORT);
                NetworkStream stream = client.GetStream();

                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(userName);
                writer.Write(sum);
                writer.Write(period);
                writer.Flush();

                BinaryReader reader = new BinaryReader(stream);
                string accountNumber = reader.ReadString();
                Console.WriteLine("Номер вашего счета " + accountNumber);

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