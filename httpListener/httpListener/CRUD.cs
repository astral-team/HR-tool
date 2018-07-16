﻿using System;
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
                return dbContext.LoginsSet.AsQueryable().Where(x => x.Hash == user.Hash).First();
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
            dbContext.Entry<Session>(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
       /* public static bool CreateUser(AuthorizedUser user)
        {
            if (GetUser(user) == null)
            {
                UserDB userdb = new UserDB();
                userdb.Login = user.Login;
                userdb.Password = user.Password;
                userdb.Id = 100;
                userdb.Hash = "";
                userdb.DateOff = "";
                userdb.Root = "";

                //dbContext.UserDBSet.Add(user.ToUserDB());
                dbContext.UserDBSet.Add(userdb);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Удаление базы данных
        /// </summary>
        public static void RemoveUserDB()
        {
            dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [UserDBSet]");
            dbContext.SaveChanges();
        }*/

    }
}
