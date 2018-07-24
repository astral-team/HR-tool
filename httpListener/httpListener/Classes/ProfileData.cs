using httpListener.Interface;
using httpListener.БД;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpListener.Classes
{
    class ProfileData
    {
        public Profile Prof;
        public Position Pos;

        public List<Experience> Exp;

        public ProfileData()
        {
            Exp = new List<Experience>();
            this.Prof = new Profile();
            this.Pos = new Position();
            
        }

        public TrueProfile Json()
        {
            var t = new TrueProfile();
            t.FullName = Prof.FullName;
            t.About = Prof.About;
            t.BirthDate = Prof.BirthDate;
            t.City = Prof.City;
            t.DateOff = Prof.DateOff;
            t.Education = Prof.Education;
            t.Email = Prof.Email;
            t.Id = Prof.Id;
            t.Interviews = Prof.Interviews;
            t.IsReadyToTrips = Prof.IsReadyToTrips;
            t.MaritalStatus = Prof.MaritalStatus;
            t.PhoneNumer = Prof.PhoneNumer;
            t.Photo = Prof.Photo;
            t.Position = Prof.Position;
            t.Responed = Prof.Responed;
            t.ResumeLink = Prof.ResumeLink;
            t.SalaryFrom = Prof.SalaryFrom;
            t.SalaryTo = Prof.SalaryTo;
            t.Sex = Prof.Sex;
            t.Сitizen = Prof.Сitizen;
            return t;
        }

        public static implicit operator Profile(ProfileData p)
        {
            return p.Prof;
        }

        public static implicit operator Position(ProfileData p)
        {
            return p.Pos;
        }
    }
    public class TrueProfile
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
    }
}
