using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpListener.Interface
{
     public interface IProfile
    {
         System.Guid Id { get; set; }
         string FullName { get; set; }
         System.DateTimeOffset BirthDate { get; set; }
         string PhoneNumer { get; set; }
         string Email { get; set; }
         bool Sex { get; set; }
         string Education { get; set; }
         string MaritalStatus { get; set; }
         string City { get; set; }
         byte[] Photo { get; set; }
         string Сitizen { get; set; }
         string About { get; set; }
         System.DateTimeOffset DateOff { get; set; }
         bool Responed { get; set; }
         string ResumeLink { get; set; }
         string Interviews { get; set; }
         bool IsReadyToTrips { get; set; }
         long SalaryTo { get; set; }
         long SalaryFrom { get; set; }
         string Position { get; set; }
    }
}
