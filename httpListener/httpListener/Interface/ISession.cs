﻿using httpListener.БД;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
