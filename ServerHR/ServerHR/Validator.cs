using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHR
{
    public class Validator
    {
        public static bool ConfirmPassword(UserDB userdb, AuthorizedUser user)
        {
            return userdb.Password == user.Password;
        }

        public static bool ConfirmLogIn(UserDB userdb, AuthorizedUser user)
        {
            return ConfirmPassword(userdb,user) &&(userdb.Login == user.Login) && userdb.Hash == user.Hash;
        }
    }
}
