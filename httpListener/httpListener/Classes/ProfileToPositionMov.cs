using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpListener.Classes
{
    class ProfileToPositionMov
    {
        public System.Guid Id { get; set; }
        public System.Guid ProfileId { get; set; }
        public System.Guid PositionId { get; set; }
        public System.DateTimeOffset DateOff { get; set; }
    }
}
