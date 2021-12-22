using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBot
{
   public class UsersBot
    {
        //айди сотрудника
        public int Id { get; set; }
        //логин
        public string login { get; set;}
        //пароль
        public string Password { get; set; }
        /// <summary>
        /// конструктор бота 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        public void usersBot(string login, string password, int id)// вывод пароля и логина
        {
            this.Id = id;
            this.login = login;
            this.Password = password;
        }
    }
}