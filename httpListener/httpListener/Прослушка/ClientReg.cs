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
            string stateString = "";

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
            stateString = $"Login = {user.Login}\nHash = {user.Hash}\nSession = {user.SessionKey}\nDateOff = {user.DateOff}\nSessionExpTime = {user.ExpTime}\n\n";
            Console.WriteLine(stateString);

            
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
                responseString = "200";
            }
            else
            {
                responseString = "404";
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
                user.ExpTime = sessionDb.ExpTime;
                CRUD.SetSession(sessionDb);
                responseString = $"{user.SessionKey}";
            }
            else
            {
                responseString = "404";
            }
        }

        private static void Delete(Logins userDb, AuthorizedUser user, out string responseString)
        {
            if (userDb != null)
            {
                userDb.DateOff = DateTime.Now;
                CRUD.RemoveUser(userDb);
                responseString = "200";
            }
            else
            {
                responseString = "404";
            }
        }
    }
}
