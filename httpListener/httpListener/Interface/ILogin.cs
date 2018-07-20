using httpListener.БД;
using System;
using System.Collections.Generic;

namespace httpListener
{
    interface ILogin
    {
        System.Guid Id { get; set; }
        string Login { get; set; }
        DateTimeOffset DateOff { get; set; }
        string Hash { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        ICollection<Session> Session { get; set; }
    }
}
