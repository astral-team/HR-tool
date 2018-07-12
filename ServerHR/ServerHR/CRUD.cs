using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ServerHR
{
    public class CRUD
    {
        // В этом поле хранится информация о базе данных
        static UserDBContainer dbContext = new UserDBContainer();

        public static UserDB GetUser(UserDB user)
        {
            //return dbContext.UserDBSet.AsQueryable<UserDB>();
            // Используем LINQ-запрос для извлечения данных
            try
            {
                return dbContext.UserDBSet.AsQueryable().Where(x => x.Login == user.Login).Where(p => p.Password == user.Password).First();
            }
            catch
            {
                return (null);
            }
        }

        public static void SetHash(UserDB user)
        {
            // Обновить данные в БД с помощью Entity Framework
            dbContext.Entry<UserDB>(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
        
        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        public static bool CreateUser(UserDB user)
        {
            if(GetUser(user) == null)
            {
                dbContext.UserDBSet.Add(user);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*public RemoveUserDb()
        {

        }*/

    }
}
