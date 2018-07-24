using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRClient
{
    public class Profile
    {
        public System.Guid Id { get; set; }
        public string FullName { get; set; }
        public System.DateTimeOffset BirthDate { get; set; }
        public string PhoneNumer { get; set; }
        public string Email { get; set; }
        public bool Sex { get; set; }
        public string Education { get; set; }
        public string MaritalStatus { get; set; }
        public string City { get; set; }
        public byte[] Photo { get; set; }
        public string Сitizen { get; set; }
        public string About { get; set; }
        public new System.DateTimeOffset DateOff { get; set; }
        public bool Responed { get; set; }
        public string ResumeLink { get; set; }
        public string Interviews { get; set; }
        public bool IsReadyToTrips { get; set; }
        public long SalaryTo { get; set; }
        public long SalaryFrom { get; set; }
        public string Position { get; set; }
    }
}
