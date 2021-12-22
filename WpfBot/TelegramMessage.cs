using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace WpfBot
{/// <summary>
/// тут происходит логика телеграмма , отправка сообщение, сохранение информации, запуск телеграм бота
/// </summary>
    class TelegramMessage
    {

        Form.MessageForm w;
        private TelegramBotClient bot;
        public ObservableCollection<MessageLogin> BotMessageLog { get; set; }
         public static long idM = 5;
       public static MessageEventArgs qw;
        /// <summary>
        /// метод по началы работы телеграмма и токен
        /// </summary>
        /// <param name="W"></param>
        /// <param name="PathToken"></param>
        [Obsolete]
        public TelegramMessage(Form.MessageForm W, string PathToken = @"1913383880:AAFS6gdXlpeIOo_XtChZmhrlf7mntGY1NyA")
        {
            this.BotMessageLog = new ObservableCollection<MessageLogin>();
            this.w = W;
            bot = new TelegramBotClient(PathToken);
            bot.OnMessage += MessageListener;
            bot.StartReceiving();//страт для телеграмма 
            
        }
        /// <summary>
        /// метод по принятии сообщения с телеграма, также присуствуют 3 команды. 1 /help :узнать какие команды есть, 
        /// 2 /GetFile : передача документа, 3 /GetAudi : передача музыки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)//
        {
            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";//переменная с текстом
            idM = e.Message.Chat.Id;
            qw = e;
            Debug.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");
            Thread[] thead = new Thread[4];
            
            string information = $"{e.Message.Date.ToLongDateString()}+{e.Message.Text}+{e.Message.Chat.FirstName}+{e.Message.Chat.Id}";
            thead[2] = new Thread(new ParameterizedThreadStart(HistoriFile.HistoriFile.AddHistori));
            thead[2].Start(information);
            var messageText = e.Message.Text;
            if (e.Message.Text == "/help")
            {
                bot.SendTextMessageAsync(e.Message.Chat.Id, "Привет, этот телеграм бот нужен для сохранения документов и музыки, для того чтобы выдать все документы: /GetFile." +
                    " Для выдачи всей музыки: /GetAudi. Чтобы сохранить документы просто переташите и отправьте.");
            }
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document)//условие если сообщение файла
            {
                thead[0] = new Thread(DownFileMessage);
                thead[0].Start();
            }
            if(e.Message.Text == e.Message.Text)//условие еслипользователь отправил сообщение
            {

                w.Dispatcher.Invoke(() =>//показывает сообщения пользователя
                {
                    BotMessageLog.Add(
                    new MessageLogin(
                        DateTime.Now.ToLongTimeString(), messageText,
                        e.Message.Chat.FirstName, e.Message.Chat.Id));
                });
            }
            if(e.Message.Audio != null)//условие если сообщение ауди
            {
                thead[1] = new Thread(DownAudiMessage);
                thead[1].Start();
            }    
            if(e.Message.Text == "/GetFile")//условие при выдачи файлов
            {
               
                GetDownFile(e);//метод по передачи документов
            }
            if(e.Message.Text == "/GetAudi")//условие при выдачи ауди
            {
                GetDownAudi(e);//метод по передачи аудио
            }
           
        }
        /// <summary>
        /// метод по отправленнии сообщении 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Id"></param>
        public void Message(string Text, string Id)//вывод сообщения
        {
            //long id = Convert.ToInt64(Id);//айди пользователя
            bot.SendTextMessageAsync(idM, Text);//отправка соообщения с айди пользователем и текстом
            
        }
        /// <summary>
        /// скачивание документа
        /// </summary>
        /// <param name="filId"></param>
        /// <param name="path"></param>
        public async void DownDocument(string filId, string path)
        {
            var file = await bot.GetFileAsync(filId);// файл
            FileStream fileStream = new FileStream("_"+ path + ".txt", FileMode.Create);//поток для скачивания
            await bot.DownloadFileAsync(file.FilePath, fileStream);//скачивание для телеграмма
            fileStream.Close();// закрытие потока 
            fileStream.Dispose();
        }
        /// <summary>
        /// метод по скачиванию аудио
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="path"></param>
        public async void DownAudi(string fileId, string path)
        {
            var file = await bot.GetFileAsync(fileId);//файл
            FileStream fileStream = new FileStream(  path + ".MP3", FileMode.Create);//поток для скачивания
            await bot.DownloadFileAsync(file.FilePath, fileStream);//скачивание для телеграмма 
            fileStream.Close();//закрытие потока
            fileStream.Dispose();
        }
        /// <summary>
        /// метод по передачи документа
        /// </summary>
        /// <param name="e"></param>
        public async void GetDownFile(MessageEventArgs e)
        {
            string[] allFoundFiles = Directory.GetFiles(@"C:", "*.txt", SearchOption.AllDirectories);//массив с путем к документам 
            if(allFoundFiles == null)
            {
                bot.SendTextMessageAsync(qw.Message.Chat.Id, "нет документов");
            }
            else
            {
                foreach (string file in allFoundFiles)//перебор массива 
                {
                    var minifile = file;// переменная для потока 
                    FileStream fs = new FileStream(minifile, FileMode.Open);// поток с открытием документа 
                    await bot.SendDocumentAsync(e.Message.Chat.Id, fs, fs.Name);// сообщения бота с документом 
                    fs.Close();// закрытие потока 
                    fs.Dispose();
                }
            }
        }
        /// <summary>
        /// метод по передачи музыки
        /// </summary>
        /// <param name="e"></param>
        public async void GetDownAudi(MessageEventArgs e)
        {
            string[] allFoundFiles = Directory.GetFiles(@"C:", "*.MP3", SearchOption.AllDirectories);//массив с путем к аудио 
            if(allFoundFiles == null)
            {
                bot.SendTextMessageAsync(qw.Message.Chat.Id, "нет музыки");
            }
            else
            {
                foreach (string file in allFoundFiles)//перебор массива
                {
                    Console.WriteLine(file);// путь документа
                    var minifile = file;// переменная для потока 
                    FileStream fas = new FileStream(file, FileMode.Open);// поток с открытием аудио 
                    await bot.SendAudioAsync(e.Message.Chat.Id, fas);// сообщения бота с аудио  
                    fas.Close();// закрытие потока 
                    fas.Dispose();
                }
            }
        }
        /// <summary>
        /// Метод по скачиванию документ и показ сообщения
        /// </summary>
        public void DownFileMessage()
        {
            w.Dispatcher.Invoke(() =>//показывает сообщения пользователя
            {

                BotMessageLog.Add(
                    new MessageLogin(
                        DateTime.Now.ToLongTimeString(), qw.Message.Chat.FirstName,
                        qw.Message.Document.FileName, qw.Message.Chat.Id));
            }
              );
            DownDocument(qw.Message.Document.FileId, qw.Message.Document.FileName);//метод по скачиванию файла
        }
        /// <summary>
        /// Метод по скачиванию музыки и показ сообщения
        /// </summary>
        public void DownAudiMessage()
        {
            w.Dispatcher.Invoke(() =>//показывает сообщения пользователя
            {
                BotMessageLog.Add(
                new MessageLogin(DateTime.Now.ToString(), qw.Message.Chat.FirstName,
                qw.Message.Audio.FileName, qw.Message.Chat.Id));
            });
            DownAudi(qw.Message.Audio.FileId, qw.Message.Audio.Title);//метод по скачиванию ауди
        }
    }
}
