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

    public void SendResponse(string message, string errMessage, BinaryWriter writer, BinaryReader reader)
    {
        writer.Write(message + "\nВозможные ошибки:" + errMessage);
        writer.Flush();
        reader.Close();
        writer.Close();
    }

    public void Process()
    {
        NetworkStream stream = null;
        try
        {
            stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            string message = "";
            string errMessage = "";
            // считываем данные из потока

            string command = reader.ReadString();
            UserDB userdb = new UserDB();
            userdb.Login = reader.ReadString();
            userdb.Password = reader.ReadString();

            switch (command)
            {
                case "Log":
                    
                    var dbResponse = CRUD.GetUser(userdb);

                    if (dbResponse != null) //Проверка авторизации
                    {
                        userdb = dbResponse;
                        AuthorizedUser user = new AuthorizedUser(userdb);
                        CRUD.SetHash(userdb);

                        Console.WriteLine("{0} получил хэш {1}", user.Login, user.Hash);
                        writer = new BinaryWriter(stream);
                        message = "Пользователь вошел, хэш:" + user.Hash;

                    }
                    else
                    {
                        errMessage += "\nНеправильные логин или пароль";
                    }
                    break;

                case "Reg":

                    userdb.Hash = "";
                    userdb.DateOff = "";

                    if (CRUD.CreateUser(userdb))
                    {
                        message = "Пользователь {0} добавлен в базу" + userdb.Login;
                    }
                    else
                    {
                        errMessage += "\nПользователь уже существует";
                    }
                    break;

                case "Del":

                    CRUD.RemoveUserDB();
                    message = "База данных удалена";

                    break;

                default:
                    errMessage += "\nНеизвестная команда";
                    break;
            }

            SendResponse(message, errMessage, writer, reader);
            
            /*UserDB userdb = new UserDB();
            userdb.Login = reader.ReadString();
            userdb.Password = reader.ReadString();
            userdb.Hash = "";
            userdb.DateOff = "";

            var dbResponse = CRUD.GetUser(userdb);
            if (dbResponse != null) //Проверка авторизации
            {
                userdb = dbResponse;
                AuthorizedUser user = new AuthorizedUser(userdb);
                CRUD.SetHash(userdb);

                Console.WriteLine("{0} Получил хэш {1}", user.Login, user.Hash);

                // отправляем ответ в виде номера счета
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(user.Hash);
                writer.Flush();

                writer.Close();
                reader.Close();
            }
            else 
            {
                CRUD.CreateUser(userdb);
                Console.WriteLine("Пользователь {0} добавлен в базу",userdb.Login);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write("Пользователь зарегистрирован");
                writer.Flush();

                writer.Close();
                reader.Close();
            }*/
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
