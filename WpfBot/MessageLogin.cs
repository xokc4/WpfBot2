using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBot
{
    class MessageLogin
    {
        //время
        public string Time { get; set; }
        //айди
        public long Id { get; set; }
        //сообщение
        public string Msg { get; set; }
        //имя пользователя
        public string FirstName { get; set; }
        //айди файла
        public long FileId { get; set; }
        //имя файла
        public string FileName { get; set; }
        /// <summary>
        /// конструктор для вывода сообщения
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Msg"></param>
        /// <param name="FirstName"></param>
        /// <param name="Id"></param>
        public MessageLogin(string Time, string Msg, string FirstName, long Id)//вывод сообщения
        {
            this.Time = Time;
            this.Msg = Msg;
            this.FirstName = FirstName;
            this.Id = Id;
        }

       
    }
}
