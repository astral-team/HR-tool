using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpListener.Interface
{
    public interface IExperience
    {
         System.Guid Id { get; set; }
         string CompanyName { get; set; }
         string Position { get; set; }
         System.Guid ProfileId { get; set; }
         System.DateTimeOffset FromDate { get; set; }
         System.DateTimeOffset ToDate { get; set; }
         string About { get; set; }
         string City { get; set; }
         System.DateTimeOffset DateOff { get; set; }
    }
}
