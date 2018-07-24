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
            dbContext.ProfileSet.Add(p);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {

            }
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

    }
}
