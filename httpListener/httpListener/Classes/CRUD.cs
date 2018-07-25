using httpListener.Classes;
using httpListener.Interface;
using httpListener.БД;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return dbContext.LoginsSet.AsQueryable().Where(x => x.DateOff == DateTimeOffset.MinValue).Where(x => x.Login == user.Login).Where(x => x.Hash == user.Hash).First();
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
                dbContext.Entry(prof).State = EntityState.Modified;
                dbContext.SaveChanges();
            }

        }

        public static void UpdatePosition(Position pd)
        {
            var pos = GetPosition(pd.FullName);
            if (pos != null)
            {
                dbContext.Entry(pos).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public static void UpdateProfToPos(Profile prof, Position pos)
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
                }

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
                var profToPos = dbContext.ProfileToPositionSet.AsQueryable().Where(x => x.PositionId == posDB.Id).Where(y => y.ProfileId == profDB.Id).FirstOrDefault();
            }
        }

        public static void UpdateExperience(Experience pd)
        {

        }

        public static void UpdateProfileData(ProfileData pd)
        {
            UpdateProfile(pd.Prof);
            UpdatePosition(pd.Pos);
            foreach (var exp in pd.Exp)
            {
                UpdateExperience(exp);
            }
        }
    }
}
