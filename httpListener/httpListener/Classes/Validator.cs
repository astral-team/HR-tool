using System;

namespace httpListener.Classes
{
    class Validator
    {
        public static void CheckTimeOfSession(Session session)
        {
            if (session.ExpTime <= DateTime.Now)
            {
                session.SessionKey = Guid.Empty;
            }
            else
            {
                if ((session.ExpTime - DateTime.Now) <= TimeSpan.FromMinutes(5))
                {
                    Console.WriteLine("Продлить?Y/N");
                    string extension = Console.ReadLine();
                    if (extension == "Y")
                    {
                        session.ExpTime = session.ExpTime.AddHours(2);
                    }
                }
            }
        }
    }
}