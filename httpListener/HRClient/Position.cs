using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRClient
{
    public class Position
    {
        public System.Guid Id { get; set; }
        public string FullName { get; set; }
        public long SalaryFrom { get; set; }
        public string Schedule { get; set; }
        public bool Trips { get; set; }
        public string About { get; set; }
        public double Rate { get; set; }
        public System.DateTimeOffset DateOff { get; set; }
        public long SalaryTo { get; set; }
        public bool IsOwn { get; set; }
    }
}
