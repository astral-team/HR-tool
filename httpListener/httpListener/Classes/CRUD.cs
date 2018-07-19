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
    }
}
