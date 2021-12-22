using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfBot.Form
{
    /// <summary>
    /// происходит просмотр сообщения с телеграмма и отправка. Есть 2 кнопки , 1 кнопка которая преходит назад к авторизации и 2 кнопка к отправке сообщения
    /// </summary>
    public partial class MessageForm : Window
    {
        TelegramMessage client;
        public MessageForm()
        {
            InitializeComponent();
            client = new TelegramMessage(this);

            logList.ItemsSource = client.BotMessageLog;
        }

        /// <summary>
        /// кнопка назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entrance_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();//создания окна в меню
            mainWindow.Show();//открытие окна
            Close();//закрытие окна
        }
        /// <summary>
        /// кнопка с отправкой сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageClick_Click(object sender, RoutedEventArgs e)
        {
           
            client.Message(txtMsgSend.Text, TargetSend.Text);//метод по отправки сообщения
            txtMsgSend.Clear();//стирает текст при отправки сообщения 
        }
        
    }
}
