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

        public static UserDB GetUser(string login, string password)
        {
            //return dbContext.UserDBSet.AsQueryable<UserDB>();
            // Используем LINQ-запрос для извлечения данных
            return dbContext.UserDBSet.AsQueryable().Where(x => x.Login == login).Where(p => p.Password == password).First();
        }

        public static void SetHash(UserDB user)
        {
            // Обновить данные в БД с помощью Entity Framework
            dbContext.Entry<UserDB>(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
