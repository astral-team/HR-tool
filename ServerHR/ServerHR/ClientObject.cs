using System;
using System.IO;
using System.Net.Sockets;
using ServerHR;


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
            string login = reader.ReadString();
            string password = reader.ReadString();
            // создаем по полученным от клиента данным объект счета

            var dbResponse = CRUD.GetUser(login, password);

            UserDB user;

            if (dbResponse != null)
            {
                Account account = new Account(login, password);
                user = dbResponse;
                user.Hash = account.Hash;
                CRUD.SetHash(user);

                Console.WriteLine("{0} Получил хэш {1}", account.Login, account.Hash);

                // отправляем ответ в виде номера счета
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(account.Hash);
                writer.Flush();

                writer.Close();
                reader.Close();
            }
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
