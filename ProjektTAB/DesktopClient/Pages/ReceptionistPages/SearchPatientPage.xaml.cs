using Database.Appointments.Simplified;
using Database.Patients;
using Database.Users.Simplified;
using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
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
        private readonly UserSimplified _chosenDoctor;
        private readonly DateTime? _chosenDate; 
        private Regex actualRegex = null;

        public SearchPatientPage()
        {
            InitializeComponent();
        }

        public SearchPatientPage(UserSimplified chosenDoctor, DateTime chosenDate)
        {
            InitializeComponent();
            _chosenDoctor = chosenDoctor;
            _chosenDate = chosenDate;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            foreach (var ch in e.Text)
            {
                if (SearchPhrase.Text == "")
                {
                    if (Char.IsDigit(ch))
                    {
                        Regex regex1 = new Regex("[^0-9]+");
                        actualRegex = regex1;
                        e.Handled = regex1.IsMatch(e.Text);
                        break;
                    }
                    else if (!Char.IsDigit(ch))
                    {
                        Regex regex2 = new Regex("[^a-zA-Z]+");
                        actualRegex = regex2;
                        e.Handled = regex2.IsMatch(e.Text);
                        break;
                    }
                }
                else
                    e.Handled = actualRegex.IsMatch(e.Text);
            }
        }

        private async void SearchPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            // get patients from api
            HttpResponseMessage response = await ApiCaller.Get("SearchPatients/"+SearchPhrase.Text.ToString());
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(responseString);
                Patients.ItemsSource = patients;
            }
            else
            {
                MessageBox.Show("Brak dostępnych pacjentów w bazie");
                Patients.ItemsSource = null;
            }
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var patient = (Patient)Patients.SelectedItem;
            AppointmentSimplified appointment = new AppointmentSimplified((DateTime)_chosenDate, CurrentAccount.CurrentUser, _chosenDoctor, patient);
            // there is some problem probably with appointment model
            HttpResponseMessage response = await ApiCaller.Post("api/Appointments/Add", appointment);

            if (response.IsSuccessStatusCode)   
                MessageBox.Show("Pomyslnie zarejestrowano wizytę!");
            else
            {
                MessageBox.Show("Wystąpił błąd podczas rejestracji wizyty!");
            }
        }
        private void Patients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var patient = (Patient)Patients.SelectedItem;
            ChosenPatient.Text = patient.Name +" "+ patient.Surname;
            RegisterBtn.IsEnabled = true;
        }
    }
}
