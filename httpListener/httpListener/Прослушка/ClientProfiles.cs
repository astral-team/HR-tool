using httpListener.БД;
using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using httpListener.Classes;
using System.Collections.Generic;

namespace httpListener
{
    class ClientProfiles
    {
        private HttpListenerContext context;

        public ClientProfiles(HttpListenerContext context)
        {
            this.context = context;
        }
        
        public void Process()
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            
            Stream streamBody = request.InputStream;

            Encoding encoding = Encoding.UTF8;
            StreamReader streamReader = new StreamReader(streamBody, encoding);
            var sRequest = streamReader.ReadToEnd();

            var profile = JsonConvert.DeserializeObject<List<ProfileData>>(sRequest);

            AuthorizedUser user = new AuthorizedUser(request.Headers["login"], request.Headers["Authorization"]);
            var userDb = CRUD.GetUser(user);
            string responseString = "";
            string stateString = "";
            if(userDb != null)
            {
                user.Id = userDb.Id;
            }

            var sessionDb = CRUD.GetSession(user);
            switch (request.HttpMethod)
            {
                case "GET":
                    if (Validator.CheckTimeOfSession(sessionDb))
                    {
                        GetProf(out responseString);
                    }
                    else
                    {
                        responseString = $"Ошибка, неверный сессионный ключ, Логин={user.Login}, Hash={user.Hash}";
                    }
                    break;
                case "POST":
                    if (Validator.CheckTimeOfSession(sessionDb))
                    {
                        AddProfile(profile, out responseString);
                    }
                    else
                    {
                        responseString = $"Ошибка, неверный сессионный ключ, Логин={user.Login}, Hash={user.Hash}";
                    }
                    break;
                case "DELETE":
                    if (Validator.CheckTimeOfSession(sessionDb))
                    {
                       Delete(profile, out responseString);
                    }
                    else
                    {
                        responseString = $"Ошибка, неверный сессионный ключ";
                    }
                    break;
                case "PUT":

                    if (Validator.CheckTimeOfSession(sessionDb))
                    {
                        Update(profile, out responseString);
                    }
                    else
                    {
                        responseString = $"Ошибка, неверный сессионный ключ";
                    }
                    break;
                default:
                    responseString = $"Ошибка, не распознан HTTP метод";
                    break;
            }
            stateString = $"Login = {user.Login}\nHash = {user.Hash}\n\n";
            Console.WriteLine(stateString);
            
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        private static void AddProfile(List<ProfileData> profile, out string responseString)
        {
            responseString = "";
            foreach (var u in profile)
            {
                if (CRUD.GetProfile(u) == null)
                {
                    CRUD.CreateProfile(u);
                    responseString += $"Профиль {u.Prof.FullName} добавлен в базу данных\n";
                }
                else
                {
                    responseString += $"Профиль {u.Prof.FullName} найден в базе данных\n";
                }
            }
        }

        private static void GetProf(out string responseString)
        {
            var list = CRUD.GetProfiles();
            var newList = new List<ProfileDataMov>();
            foreach(var l in list)
            {
                newList.Add(l);
            }
            responseString = JsonConvert.SerializeObject(newList, Formatting.Indented);
        }

        private static void Delete(List<ProfileData> list, out string responseString)
        {
            responseString = "";
            try
            {
                foreach (var l in list)
                {
                    if (Validator.CheckDateOff<ProfileMov>(l))
                    {
                        CRUD.RemoveProfile(l);
                    }
                    else
                    {
                        responseString = "Резюме уже удалено";
                    }
                }
                responseString = "200";
            }
            catch
            {
                responseString = "404";
            }
        }

        private static void Update(List<ProfileData> list, out string responseString)
        {
            responseString = "";
            try
            {
                foreach (var l in list)
                {
                    if (Validator.CheckDateOff<ProfileMov>(l))
                    {
                        CRUD.UpdateProfileData(l);
                    }
                    else
                    {
                        responseString = "Резюме уже удалено";
                    }
                }
                responseString = "200";
            }
            catch
            {
                responseString = "404";
            }
        }
    }
}
