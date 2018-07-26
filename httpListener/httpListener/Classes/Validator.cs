using httpListener.БД;
using System;

namespace httpListener.Classes
{
    class Validator
    {
        public static bool CheckTimeOfSession(Session session)
        {
            if (session == null)
            {
                return false;
            }
            else if ((session != null) && (session.ExpTime <= DateTime.Now))
            {
                session.SessionKey = Guid.Empty;
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckDateOff<T>(T father)
            where T : Father
        {
            if (father.DateOff != DateTime.MinValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}