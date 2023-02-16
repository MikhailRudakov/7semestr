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
using WordSkills.Models;

namespace WordSkills.Pages
{
    /// <summary>
    /// Логика взаимодействия для Views.xaml
    /// </summary>
    public partial class Views : Page
    {
        public Views()
        {
            InitializeComponent();

            var allTypes = BlagodatEntities.GetContext().User.ToList();
            allTypes.Insert(0, new User 
            {
                Login = "Все логины"
            });

            ComboType.ItemsSource = allTypes;
            ComboType.SelectedIndex = 0;

            UpdateUsers();
        }

        private void UpdateUsers()
        {
            var currentUsers = BlagodatEntities.GetContext().User.ToList();

            if(ComboType.SelectedIndex != 0)
            {
                User selectedCategory = (User)ComboType.SelectedItem;
                currentUsers = currentUsers.Where(x => x.Login == selectedCategory.Login).ToList();
            }


            currentUsers = currentUsers.OrderByDescending(p => p.date).ToList();

            LVusers.ItemsSource = currentUsers;
        }


        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }
    }
}
