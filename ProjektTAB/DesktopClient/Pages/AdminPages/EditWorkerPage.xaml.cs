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
    /// Interaction logic for EditWorkerPage.xaml
    /// </summary>
    public partial class EditWorkerPage : Page
    {
        private readonly UserSimplified _worker;

        public EditWorkerPage(UserSimplified worker)
        {
            InitializeComponent();
            _worker = worker;
        }

        private void WorkerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ComboBoxItem)WorkerType.SelectedItem;

            if(selected.Tag.ToString() == "Doctor")
            {
                LicenseNumber.IsEnabled = true;
            }
            else
            {
                LicenseNumber.IsEnabled = false;
                LicenseNumber.Text = string.Empty;
            }
        }

        private async void UpdateWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedType = ((ComboBoxItem)WorkerType.SelectedItem).Tag.ToString();

            Enum.TryParse(selectedType, out Role role);

            var updatedWorker = new UserSimplified
            {
                UserId = _worker.UserId,
                Name = FirstName.Text,
                Surname = LastName.Text,
                Email = Email.Text,
                IsActive = (bool)IsActive.IsChecked,
                AccountId = _worker.AccountId,
                Role = role,
                LicenseNumber = role == Role.Doctor ? LicenseNumber.Text : null
            };

            var response = await ApiCaller.Post("/api/Users/UpdateWorker", updatedWorker);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Poprawnie zaktualizowano pracownika!");
                NavigationService.Navigate(new AddWorkersPage());
            }
            else
            {
                MessageBox.Show("Wystąpił błąd podczas aktualizacji pracownika!");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(ComboBoxItem item in WorkerType.Items)
            {
                if(item.Tag.ToString() == _worker.Role.ToString())
                {
                    item.IsSelected = true;
                }
            }

            FirstName.Text = _worker.Name;
            LastName.Text = _worker.Surname;
            Email.Text = _worker.Email;
            LicenseNumber.Text = _worker.LicenseNumber;
            IsActive.IsChecked = _worker.IsActive;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ManageWorkersPage());
        }
    }
}
