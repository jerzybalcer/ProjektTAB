using Database.Users;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopClient.Pages.ReceptionistPages
{
    /// <summary>
    /// Interaction logic for PatientsList.xaml
    /// </summary>
    public partial class SearchPatientPage : Page
    {
        private readonly Doctor? _chosenDoctor;
        private readonly DateTime? _chosenDate;

        public SearchPatientPage()
        {
            InitializeComponent();
        }

        public SearchPatientPage(Doctor chosenDoctor, DateTime chosenDate)
        {
            InitializeComponent();
            _chosenDoctor = chosenDoctor;
            _chosenDate = chosenDate;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SearchPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            // get patients from api
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            // register visit
        }

        private void Patients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ChosenPatient.Text = Patients.SelectedItem.Name + " " + Patients.SelectedItem.Surname;
            RegisterBtn.IsEnabled = true;
        }
    }
}
