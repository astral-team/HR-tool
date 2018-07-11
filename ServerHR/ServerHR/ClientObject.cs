using System;
using System.IO;
using System.Net.Sockets;

public class ClientObject
{
    public TcpClient client;
    public ClientObject(TcpClient tcpClient)
    {
        client = tcpClient;
    }

    public void Process()
    {
        NetworkStream stream = null;
        try
        {
                stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                // считываем данные из потока
                string name = reader.ReadString();
                decimal sum = reader.ReadDecimal();
                int period = reader.ReadInt32();
                // создаем по полученным от клиента данным объект счета
                Account account = new Account(name, sum, period);

                Console.WriteLine("{0} зарегистрировал счет на сумму {1}", account.Name, account.Sum);

                // отправляем ответ в виде номера счета
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(account.Id);
                writer.Flush();

                writer.Close();
                reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
