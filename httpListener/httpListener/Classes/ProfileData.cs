using httpListener.БД;
using System.Collections.Generic;

namespace httpListener.Classes
{
    class ProfileData
    {
        public Profile Prof;
        public Position Pos;
        public List<ProfileToPosition> profToPos;
        public List<Experience> Exp;

        public ProfileData()
        {
            Exp = new List<Experience>();
            this.Prof = new Profile();
            this.Pos = new Position();
            this.profToPos = new List<ProfileToPosition>();
        }

        public static implicit operator Profile(ProfileData p)
        {
            return p.Prof;
        }

        public static implicit operator ProfileMov(ProfileData p)
        {
            var t = new ProfileMov();
            t.FullName = p.Prof.FullName;
            t.About = p.Prof.About;
            t.BirthDate = p.Prof.BirthDate;
            t.City = p.Prof.City;
            t.DateOff = p.Prof.DateOff;
            t.Education = p.Prof.Education;
            t.Email = p.Prof.Email;
            t.Id = p.Prof.Id;
            t.Interviews = p.Prof.Interviews;
            t.IsReadyToTrips = p.Prof.IsReadyToTrips;
            t.MaritalStatus = p.Prof.MaritalStatus;
            t.PhoneNumer = p.Prof.PhoneNumer;
            t.Photo = p.Prof.Photo;
            t.Position = p.Prof.Position;
            t.Responed = p.Prof.Responed;
            t.ResumeLink = p.Prof.ResumeLink;
            t.SalaryFrom = p.Prof.SalaryFrom;
            t.SalaryTo = p.Prof.SalaryTo;
            t.Sex = p.Prof.Sex;
            t.Сitizen = p.Prof.Сitizen;
            return t;
        }

        public static implicit operator Position(ProfileData p)
        {
            return p.Pos;
        }

        public static implicit operator PositionMov(ProfileData p)
        {
            var t = new PositionMov();
            t.FullName = p.Pos.FullName;
            t.About = p.Pos.About;
            t.DateOff = p.Pos.DateOff;
            t.Id = p.Pos.Id;
            t.SalaryFrom = p.Pos.SalaryFrom;
            t.SalaryTo = p.Pos.SalaryTo;
            t.IsOwn = p.Pos.IsOwn;
            t.Rate = p.Pos.Rate;
            t.Schedule = p.Pos.Schedule;
            t.Trips = p.Pos.Trips;
            return t;
        }

        public static implicit operator List<ExperienceMov>(ProfileData p)
        {
            var t = new List<ExperienceMov>();
            foreach(var l in p.Exp)
            {
                var exp = new ExperienceMov();
                exp.About = l.About;
                exp.City = l.City;
                exp.CompanyName = l.CompanyName;
                exp.DateOff = l.DateOff;
                exp.FromDate = l.FromDate;
                exp.Id = l.Id;
                exp.Position = l.Position;
                exp.ProfileId = l.ProfileId;
                exp.ToDate = l.ToDate;
                t.Add(exp);
            }
           
            return t;
        }

        public static implicit operator ProfileDataMov(ProfileData p)
        {
            var t = new ProfileDataMov();
            t.Prof = p;
            t.Pos = p;
            t.Exp = p;
            return t;
        }
    }
}
