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

            var response = await ApiCaller.Post("AddPatient", newPatient);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Poprawnie dodano pacjenta!");
                NavigationService.Navigate(new AddPatientPage());
            }
            else
            {
                MessageBox.Show("Wystąpił błąd podczas dodawania pacjenta!");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddPatientBtn.IsEnabled = true;

            foreach (var element in InputPanel.Children)
            {
                if (element is TextBox)
                {
                    TextBox textBox = (TextBox)element;

                    if ((textBox.Name != "apartment"
                        && textBox.Text.Length == 0)
                        || (textBox.Name == "pesel" && textBox.Text.Length < 11)
                        )
                    {
                        AddPatientBtn.IsEnabled = false;
                    }
                }
            }
        }
    }
}
