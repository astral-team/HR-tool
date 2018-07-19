using System;

namespace httpListener.Classes
{
    class Validator
    {
        public static bool CheckTimeOfSession(Session session)
        {
            if (session.ExpTime <= DateTime.Now)
            {
                session.SessionKey = Guid.Empty;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}