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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WordSkills.Classes;
using WordSkills.Models;

namespace WordSkills.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan duration;
        Random random = new Random();
        string symbols = "";
        private int attempts = 0;
        public Login()
        {
            InitializeComponent();
            ConnectDBClass.modelDB = new Models.BlagodatEntities();
        }

        private void LogIn()
        {
            try
            {
                // обратиться к таблице User, чтобы извлечь логин  пароль
                // var - общий тип переменной
                // userObj - имя объектаБ которого вы задаете сами. Информация об агенте - agentObj
                //Сравнить данные из таблицы и назвыния столбцов
                var userObj = ConnectDBClass.modelDB.User.FirstOrDefault(x =>
                x.Login == textboxLogin.Text && x.Password == PassBox.Password);
                if (userObj != null)
                {

                    BlagodatEntities.CurrentUser = userObj;
                    switch (userObj.RoleID)
                    {
                        case 1:
                            NavigationService.Navigate(new Admin());
                            break;
                        case 2:
                            NavigationService.Navigate(new User());
                            break;
                        case 3:
                            NavigationService.Navigate(new Admin());
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Критическая работа приложения",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void LoginButton(object sender, RoutedEventArgs e)
        {
            var userObj = Classes.ConnectDBClass.modelDB.User.FirstOrDefault(x =>
                 x.Login == textboxLogin.Text && x.Password == PassBox.Password);
            if (userObj == null)
            {
                MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                attempts++;
                CheckAttemps();

            }
            else
            {
                LogIn();
            }
        }

        private void BtnUpdateCaptcha_Click(object sender, RoutedEventArgs e)
        {
            UpdateCaptcha();
        }

        private void UpdateCaptcha()
        {
            symbols = "";
            CaptchaTB.Text = "";
            SPanelSymbols.Children.Clear();
            CanvasNoise.Children.Clear();

            GenerateSymbols(4);
            GenerateNoise(32);
        }

        private void GenerateSymbols(int count)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(random.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new TextBlock();

                lbl.Text = symbol;
                lbl.FontSize = random.Next(24, 32);
                lbl.RenderTransform = new RotateTransform(random.Next(-24, 24));
                lbl.Margin = new Thickness(16, 0, 16, 0);

                SPanelSymbols.Children.Add(lbl);

                symbols = symbols + symbol;
            }
        }

        private void GenerateNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Color.FromArgb((byte)random.Next(32, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128)));
                border.Height = random.Next(4, 8);
                border.Width = random.Next(256, 512);

                border.RenderTransform = new RotateTransform(random.Next(0, 360));

                CanvasNoise.Children.Add(border);
                Canvas.SetLeft(border, random.Next(0, 200));
                Canvas.SetTop(border, random.Next(0, 75));


                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)random.Next(32, 64), (byte)random.Next(0, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128)));
                ellipse.Height = ellipse.Width = random.Next(20, 40);

                CanvasNoise.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, random.Next(0, 400));
                Canvas.SetTop(ellipse, random.Next(0, 100));
            }
        }

        private void CheckAttemps()
        {
            if (attempts == 2)
            {
                CaptchaBlock.Visibility = Visibility.Visible;
                CaptchaTbBlock.Visibility = Visibility.Visible;
                UpdateCaptcha();
                MessageBox.Show("Пройдите капчу.", "Не удается войти!", MessageBoxButton.OK, MessageBoxImage.Warning);

                if (CaptchaTB.Text != symbols)
                {
                    textboxLogin.Visibility = Visibility.Hidden;

                }
                else textboxLogin.Visibility = Visibility.Visible;
            }
            else
            {
                if (attempts == 3)
                {
                    duration = TimeSpan.FromSeconds(10);

                    LoginTimerTB.Visibility = Visibility.Visible;
                    LoginBlock.Visibility = Visibility.Collapsed;
                    BlockedTB.Text = "Превышено количество попыток авторизации.\nВозможность входа заблокирована.";
                    textboxLogin.IsEnabled = false;
                }

            }
        }

        private void CheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaTB.Text == symbols)
                textboxLogin.Visibility = Visibility.Visible;
        }


    private void TbxShowPass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxbPassword.Width = PassBox.Width;
            TxbPassword.Visibility = Visibility.Visible;
            PassBox.Visibility = Visibility.Collapsed;
            TxbPassword.Text = PassBox.Password;
        }

        private void TbxShowPass_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TxbPassword.Visibility = Visibility.Collapsed;
            PassBox.Visibility = Visibility.Visible;
        }

        private void BtnUpdateCaptcha_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
