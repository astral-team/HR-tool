using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

            if (userDb == null)
            {
                /*user.Id = userDb.Id;
                var sessionDb = CRUD.GetSession(user);
                sessionDb.SessionKey = user.GetSessionKey();
                CRUD.SetSession(sessionDb);*/
                CRUD.CreateUser(user);
                responseString = $"Пользователь зарегистрирован Логин={user.Login}, Hash={user.Hash}, Session={user.SessionKey}";
            }
            else
            {
                responseString = $"Ошибка регистрации, пользователь уже зарегистрирован Логин={user.Login}, Hash={user.Hash}";
            }

            Console.WriteLine(responseString);


            //Console.WriteLine($"{request.HttpMethod} {sRequest} {request.Headers} {request.UserAgent}");

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}
