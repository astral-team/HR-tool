using httpListener.Interface;
using httpListener.БД;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpListener.Classes
{
    class ProfileData:IProfile
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
        public System.DateTimeOffset DateOff { get; set; }
        public bool Responed { get; set; }
        public string ResumeLink { get; set; }
        public string Interviews { get; set; }
        public bool IsReadyToTrips { get; set; }
        public long SalaryTo { get; set; }
        public long SalaryFrom { get; set; }
        public string Position { get; set; }

        public List<Experience> Exp;

        public ProfileData()
        {
            Exp = new List<Experience>();
        }
        public static implicit operator Profile(ProfileData p)
        {
            var prof = new Profile();
            prof.Id = p.Id;
            prof.FullName = p.FullName;
            prof.PhoneNumer = p.PhoneNumer;
            prof.Photo = p.Photo;
            prof.Email = p.Email;
            prof.Sex = p.Sex;
            prof.Education = p.Education;
            prof.MaritalStatus = p.MaritalStatus;
            prof.City = p.City;
            prof.Сitizen = p.Сitizen;
            prof.About = p.About;
            prof.DateOff = p.DateOff;
            prof.Responed = p.Responed;
            prof.ResumeLink = p.ResumeLink;
            prof.Interviews = p.Interviews;
            prof.IsReadyToTrips = p.IsReadyToTrips;
            prof.SalaryFrom = p.SalaryFrom;
            prof.SalaryTo = p.SalaryTo;
            prof.Position = p.Position;

            return prof;
        }
    }
}
