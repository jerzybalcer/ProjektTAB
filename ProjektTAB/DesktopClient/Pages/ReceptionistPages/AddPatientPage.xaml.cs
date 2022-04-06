using Database;
using Database.Patients;
using DesktopClient.Helpers;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopClient.Pages.ReceptionistPages
{
    /// <summary>
    /// Interaction logic for AddPatientPage.xaml
    /// </summary>
    public partial class AddPatientPage : Page
    {
        public AddPatientPage()
        {
            InitializeComponent();
        }

        private async void AddPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient newPatient = new Patient(firstname.Text, lastname.Text, pesel.Text, new Address(city.Text, street.Text, house.Text, apartment.Text));

            var response = await ApiCaller.Post("api/Patients/Add", newPatient);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonSettings = JsonConfiguration.GetJsonSettings();
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
