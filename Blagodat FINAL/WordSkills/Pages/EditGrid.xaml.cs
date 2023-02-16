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
    /// Логика взаимодействия для EditGrid.xaml
    /// </summary>
    public partial class EditGrid : Page
    {
        private Clients _currentBlago = new Clients();
        public EditGrid(Clients selectedClient)
        {
            InitializeComponent();
            if (selectedClient != null)
                _currentBlago = selectedClient;

            DataContext = _currentBlago;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentBlago.fio))
                errors.AppendLine("Укажите фамилию");
          

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                BlagodatEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
