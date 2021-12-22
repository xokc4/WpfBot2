using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HistoriFile
{
    public class HistoriFile
    {
        
        public static InformationMSg informationMSg;
        public static string NameFile = @"HistoriDialog.Json";
        /// <summary>
        /// метод по добавлению информации в  informationMSg
        /// </summary>
        /// <param name="information"></param>
        public static void AddHistori(object information)
        {
            string date; string MSG; string FirstName; long Id;
            
            string[] infwords = information.ToString().Split('+');
           
           
            informationMSg = new InformationMSg(infwords[0], infwords[1], infwords[2],Convert.ToInt32(infwords[3]));

            fileHistor();
            //Thread thread = new Thread(fileHistor);
            //thread.Start();
        }
        /// <summary>
        /// метод по добавлении информации в файл
        /// </summary>
        public static void fileHistor()
        {
            string json = JsonConvert.SerializeObject(informationMSg);// создания сериализации
            File.AppendAllText($"{NameFile}", json);// запись
        }

    }
}
