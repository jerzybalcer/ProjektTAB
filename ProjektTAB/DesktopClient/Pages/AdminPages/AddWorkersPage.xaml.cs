using Database.Users.Simplified;
using DesktopClient.Helpers;
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

namespace DesktopClient.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for AddWorkersPage.xaml
    /// </summary>
    public partial class AddWorkersPage : Page
    {
        private bool _isTypeSelected = false;
        private string _selectedType = "";

        public AddWorkersPage()
        {
            InitializeComponent();
        }

        private async void AddWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_isTypeSelected)
            {
                MessageBox.Show("Wybierz typ pracownika!");
                return;
            }

            if(_isTypeSelected && FirstName.Text.Length == 0
                || LastName.Text.Length == 0 || Email.Text.Length == 0)
            {
                MessageBox.Show("Uzupełnij dane!");
                return;
            }

            if(_isTypeSelected && _selectedType == "Doctor" && LicenseNumber.Text.Length == 0)
            {
                MessageBox.Show("Uzupełnij numer pozwolenia!");
                return;
            }

            var newWorker = new UserSimplified 
            {
                Name = FirstName.Text,
                Surname = LastName.Text,
                Email = Email.Text,
                IsActive = true,
            };

            Role newWorkerRole;

            Enum.TryParse(_selectedType, out newWorkerRole);

            newWorker.Role = newWorkerRole;

            if(newWorker.Role == Role.Doctor)
            {
                newWorker.LicenseNumber = LicenseNumber.Text;
            }

            var response = await ApiCaller.Post("AddWorker", newWorker);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Poprawnie dodano pracownika!");
                NavigationService.Navigate(new AddWorkersPage());
            }
            else
            {
                MessageBox.Show("Wystąpił błąd podczas dodawania pracownika!");
            }
        }

        private void WorkerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isTypeSelected = true;

            var selected = WorkerType.SelectedItem as ComboBoxItem;

            if (selected.Tag.ToString() == "Doctor")
            {
                LicenseNumber.IsEnabled = true;
            }
            else
            {
                LicenseNumber.IsEnabled = false;
            }

            _selectedType = selected.Tag.ToString();
        }
    }
}
