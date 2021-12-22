using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoriFile
{
    public class InformationMSg
    {
        //время
        public string Time { get; set; }//время
        //айди
        public long Id { get; set; }//айди
        //сообщение
        public string Msg { get; set; }//сообщение
        //имя пользователя
        public string FirstName { get; set; }//имя пользователя
        //айди файла
        public long FileId { get; set; }//айди файла
        //имя файла
        public string FileName { get; set; }//имя файла
        /// <summary>
        /// метод по создании объекта
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Msg"></param>
        /// <param name="FirstName"></param>
        /// <param name="Id"></param>
        public InformationMSg(string Time, string Msg, string FirstName, long Id)//вывод сообщения
        {
            this.Time = Time;
            this.Msg = Msg;
            this.FirstName = FirstName;
            this.Id = Id;
        }

    }
}
