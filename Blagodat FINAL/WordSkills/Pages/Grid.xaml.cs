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
using WordSkills.Classes;
using WordSkills.Models;

namespace WordSkills.Pages
{
    /// <summary>
    /// Логика взаимодействия для Grid.xaml
    /// </summary>
    public partial class Grid : Page
    {
        public Grid()
        {
            InitializeComponent();

            GridBlago.ItemsSource = BlagodatEntities.GetContext().User.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditGrid((sender as Button).DataContext as Clients));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditGrid(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = GridBlago.SelectedItems.Cast<Clients>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие{hotelsForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BlagodatEntities.GetContext().Clients.RemoveRange(hotelsForRemoving);
                    BlagodatEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    GridBlago.ItemsSource = BlagodatEntities.GetContext().Clients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void PdfSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog dialog = new PrintDialog();

                if (dialog.ShowDialog() != true)
                    return;
                dialog.PrintVisual(GridBlago, "Печать таблицы");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Печать таблицы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
