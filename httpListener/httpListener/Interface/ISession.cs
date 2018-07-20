using httpListener.БД;
using System;

namespace httpListener
{
    interface ISession
    {
        System.Guid Id { get; set; }
        System.Guid LoginId { get; set; }
        System.Guid SessionKey { get; set; }
        DateTimeOffset ExpTime { get; set; }

        Logins Logins { get; set; }
    }
}
