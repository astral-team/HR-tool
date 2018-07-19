using System;
using System.Collections.Generic;

namespace httpListener
{
    public class AuthorizedUser:ILogin, ISession
    {
        public AuthorizedUser(Logins user)
        {
        }

        public AuthorizedUser(string login, string hash)
        {
            this.Login = login;
            this.Hash = hash;
        }

        public Guid GetSessionKey()
        {
            return this.SessionKey = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public DateTimeOffset DateOff { get; set; }
        public string Hash { get; set; }
        public ICollection<Session> Session { get; set; }
        public Guid LoginId { get; set; }
        public Guid SessionKey { get; set; }
        public DateTimeOffset ExpTime { get; set; }
        public Logins Logins { get; set; }

        /*public UserDB ToUserDB()
        {
            var user = new UserDB();
            user.Login = this.Login;
            user.Password = this.Password;
            user.Hash = this.Hash;
            return user;
        }*/
    }
}
