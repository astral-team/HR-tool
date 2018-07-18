using System;
using System.IO;
using System.Net;
using System.Text;

namespace httpListener
{
    class ClientReg
    {
        private HttpListenerContext context;



        public ClientReg(HttpListenerContext context)
        {
            this.context = context;
        }

        public void Process()
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            Stream streamBody = request.InputStream;
            Encoding encoding = request.ContentEncoding;
            StreamReader streamReader = new StreamReader(streamBody, encoding);
            var sRequest = streamReader.ReadToEnd();

            AuthorizedUser user = new AuthorizedUser(request.Headers["login"], request.Headers["Authorization"]);
            var userDb = CRUD.GetUser(user);
            string responseString = "";

            //if (userDb == null)
            //{
            //    /*user.Id = userDb.Id;
            //    var sessionDb = CRUD.GetSession(user);
            //    sessionDb.SessionKey = user.GetSessionKey();
            //    CRUD.SetSession(sessionDb);*/
            //    CRUD.CreateUser(user);
            //    responseString = $"Пользователь зарегистрирован Логин={user.Login}, Hash={user.Hash}, Session={user.SessionKey}";
            //}
            //else
            //{
            //    responseString = $"Ошибка регистрации, пользователь уже зарегистрирован Логин={user.Login}, Hash={user.Hash}";
            //}
            switch (request.HttpMethod)
            {
                case "GET":
                    Login(userDb, user, out responseString);
                    break;
                case "POST":
                    Reg(userDb, user, out responseString);
                    break;
                case "DELETE":
                    Delete(userDb, user, out responseString);
                    break;
                default:
                    responseString = $"Ошибка, не распознан HTTP метод, Логин={user.Login}, Hash={user.Hash}";
                    break;
            }

            Console.WriteLine(responseString);
            
            //Console.WriteLine($"{request.HttpMethod} {sRequest} {request.Headers} {request.UserAgent}");

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        private static void Reg(Logins userDb, AuthorizedUser user, out string responseString)
        {
            if (userDb == null)
            {
                CRUD.CreateUser(user);
                responseString = $"Пользователь зарегистрирован Логин={user.Login}, Hash={user.Hash}, Session={user.SessionKey}";
            }
            else
            {
                responseString = $"Ошибка регистрации, пользователь уже зарегистрирован Логин={user.Login}, Hash={user.Hash}";
            }
        }

        private static void Login(Logins userDb, AuthorizedUser user, out string responseString)
        {
            if (userDb != null)
            {
                user.Id = userDb.Id;
                var sessionDb = CRUD.GetSession(user);
                sessionDb.SessionKey = user.GetSessionKey();
                sessionDb.ExpTime = DateTime.Now.AddHours(2);
                CRUD.SetSession(sessionDb);
                responseString = $"Пользователь вошел Логин={user.Login}, Hash={user.Hash}, Session={user.SessionKey}, время={sessionDb.ExpTime}";
            }
            else
            {
                responseString = $"Ошибка авторизации Логин={user.Login}, Hash={user.Hash}";
            }
        }

        private static void Delete(Logins userDb, AuthorizedUser user, out string responseString)
        {
            if (userDb != null)
            {
                userDb.DateOff = DateTime.Now;
                CRUD.RemoveUser(userDb);
                responseString = $"Пользователь удалён Логин={user.Login}, Hash={user.Hash}, Session={user.SessionKey}, Дата удаления= {userDb.DateOff}";
            }
            else
            {
                responseString = $"Ошибка удаления Логин={user.Login}, Hash={user.Hash}";
            }
        }
    }
}
