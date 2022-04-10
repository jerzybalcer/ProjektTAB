using Database.Users;
using Database.Users.Simplified;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.ReceptionistPages
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class DateChoosePage : Page
    {
        private readonly UserSimplified _chosenDoctor;
        private bool _isDatePicked = false;
        private bool _isHourPicked = false;

        public DateChoosePage(UserSimplified chosenDoctor)
        {
            InitializeComponent();
            _chosenDoctor = chosenDoctor;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_chosenDoctor is not null)
            {
                ChosenDoctorName.Text = _chosenDoctor.Name + " " + _chosenDoctor.Surname;
            }
            else
            {
                ChosenDoctorName.Text = "nie wybrano";
            }
        }

        private async void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ChosenDate.Text = DatePicker.SelectedDate.ToString();
            _isDatePicked = true;
            FreeDates.IsEnabled = true;

            if (_isHourPicked && _isDatePicked)
            {
                NextBtn.IsEnabled = true;
            }
            // get all free dates from api
            HttpResponseMessage response = await ApiCaller.Get("GetAllAvailablesDates/" + _chosenDoctor.UserId + "/" + DatePicker.SelectedDate);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                List<string> freeDates = JsonConvert.DeserializeObject<List<string>>(responseString);
                FreeDates.ItemsSource = freeDates;
            }
            else
            {
                MessageBox.Show("Brak dostępnych terminów w danym dniu ");
            }
        }

        private void FreeDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChosenHour.Text = FreeDates.SelectedItem.ToString();
            DateTime selectedDate = (DateTime)DatePicker.SelectedDate;
            string[] splitedDate = ChosenHour.Text.Split(":");
            var date = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, Convert.ToInt32(splitedDate[0]), Convert.ToInt32(splitedDate[1]), 0);
            ChosenDate.Text = date.ToString();
            DatePicker.SelectedDate = date;
            _isHourPicked = true;
            if (_isHourPicked && _isDatePicked)
            {
                NextBtn.IsEnabled = true;
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // navigate to patient choose page
            NavigationService.Navigate(new SearchPatientPage(_chosenDoctor, (DateTime)DatePicker.SelectedDate));
        }

    }
}
