﻿using httpListener.Classes;
using httpListener.БД;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace httpListener
{
    class CRUD
    {
        // В этом поле хранится информация о базе данных
        static UserDBContainer dbContext = new UserDBContainer();

        #region LoginsReg
        /// <summary>
        /// Получение пользователя из базы данных, если его нет возвращается null
        /// </summary>
        public static Logins GetUser(AuthorizedUser user)
        {
            //return dbContext.UserDBSet.AsQueryable<UserDB>();
            // Используем LINQ-запрос для извлечения данных
            try
            {
                return dbContext.LoginsSet.AsQueryable().Where(x => x.DateOff == DateTimeOffset.MinValue).Where(x => x.Login == user.Login).First();
            }
            catch
            {
                return (null);
            }
        }

        public static Session GetSession(AuthorizedUser user)
        {
            //return dbContext.UserDBSet.AsQueryable<UserDB>();
            // Используем LINQ-запрос для извлечения данных
            try
            {
                return dbContext.SessionSet.AsQueryable().Where(x => x.LoginId == user.Id).First();
            }
            catch
            {
                return (null);
            }
        }

        /// <summary>
        /// Получить сессию по ключу
        /// </summary>
        public static Session GetSession(Guid key)
        {
            try
            {
                return dbContext.SessionSet.AsQueryable().Where(x => x.SessionKey == key).First();
            }
            catch
            {
                return (null);
            }
        }

        /// <summary>
        /// Занесение кэша в базу данных
        /// </summary>
        public static void SetSession(Session user)
        {
            // Обновить данные в БД с помощью Entity Framework
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        public static bool CreateUser(AuthorizedUser user)
        {
            Logins userdb = new Logins()
            {
                Id = Guid.NewGuid()
            };
            CreateSession(userdb.Id);
            userdb.Login = user.Login;
            userdb.Hash = user.Hash;

            //dbContext.UserDBSet.Add(user.ToUserDB());
            dbContext.LoginsSet.Add(userdb);
            dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Начало сессии
        /// </summary>
        public static void CreateSession(Guid logId)
        {
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                LoginId = logId
            };
            dbContext.SessionSet.Add(session);
            //dbContext.SaveChanges();
        }

        /// <summary>
        /// Смена пароля
        /// </summary>
        public static void ChangePassword(AuthorizedUser user)
        {
            var userDB = GetUser(user);
            if (userDB != null)
            {
                dbContext.Entry(userDB).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Удаление из базы данных
        /// </summary>
        public static void RemoveUser(Logins user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Удаление базы данных
        /// </summary>
        public static void RemoveDB()
        {
            dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [UserDBSet]");
            dbContext.SaveChanges();
        }

        #endregion

        public static Profile GetProfile(Profile profile)
        {
            try
            {
                return dbContext.ProfileSet.AsQueryable().Where(x => x.PhoneNumer == profile.PhoneNumer).First();
            }
            catch
            {
                return null;
            }
        }

        public static Profile GetProfile(Guid Id)
        {
            try
            {
                return dbContext.ProfileSet.AsQueryable().Where(x => x.Id == Id).First();
            }
            catch
            {
                return null;
            }
        }

        public static void CreateProfile(ProfileData profile)
        {
            Profile p = new Profile();
            p = (Profile)profile;
            var ProfToPos = new ProfileToPosition();

            p.Id = Guid.NewGuid();
            ProfToPos.Id = Guid.NewGuid();

            ProfToPos.ProfileId = p.Id;
            ProfToPos.Profile = p;

            var pos = GetPosition(p.Position);

            if (pos == null)
            {
                pos = new Position();
                pos.FullName = p.Position;
                CreatePosition(pos);
            }

            
            CreateExperience(profile.Exp, p);
            
            ProfToPos.PositionId = pos.Id;

            ProfToPos.Position = pos;

            dbContext.ProfileToPositionSet.Add(ProfToPos);
            //dbContext.ProfileSet.Add(ProfToPos.Profile);

            /*try
            {
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }*/
        }

        public static void CreatePosition(Position pos)
        {
            pos.Id = Guid.NewGuid();
            dbContext.PositionSet.Add(pos);
            dbContext.SaveChanges();
        }

        public static Position GetPosition(string positionName)
        {
            return dbContext.PositionSet.AsQueryable().Where(x => x.FullName == positionName).FirstOrDefault();
        }

        public static Position GetPosition(Guid Id)
        {
            return dbContext.PositionSet.AsQueryable().Where(x => x.Id == Id).FirstOrDefault();
        }

        public static ProfileToPosition GetProfileToPosition(Guid Id)
        {
            return dbContext.ProfileToPositionSet.AsQueryable().Where(x => x.Id == Id).FirstOrDefault();
        }

        public static Experience GetExperience(Guid Id)
        {
            return dbContext.ExperienceSet.AsQueryable().Where(x => x.Id == Id).FirstOrDefault();
        }

        public static void CreateExperience(List<Experience> exList, Profile profile)
        {
            var exp = new Experience();
            foreach(var ex in exList)
            {
                ex.Profile = profile;
                ex.Id = Guid.NewGuid();
                ex.ProfileId = profile.Id;
                ex.DateOff = DateTimeOffset.MinValue;
                exp = ex;
                dbContext.ExperienceSet.Add(exp);
            }
            try
            {
                dbContext.SaveChanges();
            }
            catch { };
        }

        public static List<ProfileData> GetProfiles()
        {
            var profileDataList = new List<ProfileData>();
            var profileList = dbContext.ProfileSet.AsQueryable().ToList();
            var profToPosList = dbContext.ProfileToPositionSet.AsQueryable().ToList();
            foreach (var p in profToPosList)
            {
                ProfileData r = new ProfileData();
                r.Pos = p.Position;
                r.Prof = p.Profile;
                r.Exp = dbContext.ExperienceSet.AsQueryable().Where(x => x.Id == p.ProfileId).ToList();
                profileDataList.Add(r);
            }
            return profileDataList;
        }

        public static void UpdateProfile(Profile pd)
        {
            var prof = GetProfile(pd);
            if (prof!=null)
            {
                prof.FullName = pd.FullName;
                prof.About = pd.About;
                prof.BirthDate = pd.BirthDate;
                prof.Email = pd.Email;
                prof.City = pd.City;
                prof.DateOff = pd.DateOff;
                prof.Education = pd.Education;
                prof.Experience = pd.Experience;
                prof.Interviews = pd.Interviews;
                prof.IsReadyToTrips = pd.IsReadyToTrips;
                prof.MaritalStatus = pd.MaritalStatus;
                prof.PhoneNumer = pd.PhoneNumer;
                prof.Photo = pd.Photo;
                prof.Position = pd.Position;
                prof.ProfileToPosition = pd.ProfileToPosition;
                prof.Responed = pd.Responed;
                prof.ResumeLink = pd.ResumeLink;
                prof.SalaryFrom = pd.SalaryFrom;
                prof.SalaryTo = pd.SalaryTo;
                prof.Sex = pd.Sex;
                prof.Сitizen = pd.Сitizen;
                dbContext.Entry(prof).State = EntityState.Modified;
                dbContext.SaveChanges();
            }

        }

        public static void UpdatePosition(Position pd)
        {
            var pos = GetPosition(pd.Id);
            if (pos != null)
            {
                pos.About = pd.About;
                pos.DateOff = pd.DateOff;
                pos.FullName = pd.FullName;
                pos.IsOwn = pd.IsOwn;
                pos.ProfileToPosition = pd.ProfileToPosition;
                pos.Rate = pd.Rate;
                pos.SalaryFrom = pd.SalaryFrom;
                pos.SalaryTo = pd.SalaryTo;
                pos.Schedule = pd.Schedule;
                pos.Trips = pd.Trips;
                dbContext.Entry(pos).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static void UpdateExperience(Experience pd)
        {
            var exp = GetExperience(pd.Id);
            if (exp != null)
            {
                exp.About = pd.About;
                exp.City = pd.City;
                exp.CompanyName = pd.CompanyName;
                exp.DateOff = pd.DateOff;
                exp.FromDate = pd.FromDate;
                exp.Position = pd.Position;
                exp.Profile = pd.Profile;
                exp.ProfileId = pd.ProfileId;
                exp.ToDate = pd.ToDate;
                dbContext.Entry(exp).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static void UpdateProfToPos(Profile prof, Position pos, ProfileToPosition PToP)
        {
            var profDB = GetProfile(prof.Id);

            var posDB = GetPosition(pos.Id);

            if (profDB != null)
            {

                if (posDB == null)
                {
                    posDB = new Position();
                    posDB.FullName = pos.FullName;
                    CreatePosition(posDB);


                    var ProfToPos = new ProfileToPosition();
                    ProfToPos.Id = Guid.NewGuid();

                    ProfToPos.ProfileId = profDB.Id;
                    ProfToPos.Profile = profDB;

                    ProfToPos.PositionId = posDB.Id;

                    ProfToPos.Position = posDB;

                    dbContext.ProfileToPositionSet.Add(ProfToPos);
                }
                else
                {
                    var Ptp = GetProfileToPosition(PToP.Id);
                    Ptp.PositionId = posDB.Id;
                    Ptp.Position = posDB;
                    dbContext.Entry(Ptp).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
        }

        public static void UpdateProfileData(ProfileData pd)
        {
            UpdateProfile(pd.Prof);
            UpdatePosition(pd.Pos);
            foreach (var ptp in pd.profToPos)
            {
                UpdateProfToPos(pd.Prof, pd.Pos, ptp);
            }
            foreach (var exp in pd.Exp)
            {
                UpdateExperience(exp);
            }
        }

        public static void RemoveProfile(ProfileData pd)
        {
            var profDB = GetProfile(pd.Prof.Id);
            foreach (var exp in profDB.Experience) 
            {
                exp.DateOff = DateTime.Now;
            }
            foreach (var pToP in profDB.ProfileToPosition)
            {
                pToP.DateOff = DateTime.Now;
            }
            profDB.DateOff = DateTime.Now;
        }
    }
}
