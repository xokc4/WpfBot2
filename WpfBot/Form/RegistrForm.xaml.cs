using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Xceed.Wpf.Toolkit;

namespace WpfBot.Form
{
    /// <summary>
    /// здесь происходит регистрация пользователся и 2 кнопки одна кнопка для перехода назад и вторая кнопка для перехода к форме отправки соообщения
    /// </summary>



    
    public partial class RegistrForm : Window
    {
        public static List<UsersBot> users = new List<UsersBot>();
        public RegistrForm()
        {
            InitializeComponent();
            
           
        }
        /// <summary>
        /// метод по регистрации пользователя 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void entrance_Click(object sender, RoutedEventArgs e)
        {
            string login = RegistLogin.Text;//логин
            string pass = RegistPassword.Password.ToString();//переменная 1 пароля 
            string pass2 = entePassword.Password.ToString();//переменная 2 пароля
            if(pass == pass2)//условие если пароли совпали
            {
                users.Add(new UsersBot()//добавление пользователя
                {
                    login = login,
                    Password = pass
                });

                MainWindow mainWindow = new MainWindow();// создания формы
                mainWindow.Show();//открытие окна
                Close();//закрытие окна
            }
            else
            {
                System.Windows.MessageBox.Show("не совпадают пароли");// вывод сообщения елси пароли не совпали
            } 
        }
        /// <summary>
        /// метод на кнопку назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();//создания окна в меню
            mainWindow.Show();//открытие
            Close();//закрытие окна
        }
    }
}
