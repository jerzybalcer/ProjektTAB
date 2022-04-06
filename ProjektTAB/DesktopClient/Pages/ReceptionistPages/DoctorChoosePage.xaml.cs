using Database.Users;
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
    /// Interaction logic for DoctorChoosePage.xaml
    /// </summary>
    public partial class DoctorChoosePage : Page
    {
        public DoctorChoosePage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // get all doctors from api
            HttpResponseMessage response = await ApiCaller.Get("GetListOfDoctors");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(responseString);
                DoctorsList.ItemsSource = doctors;
            }
            else
            {
                MessageBox.Show("Brak dostępnych doktorów w bazie");
            }
        }
        private void DoctorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var doctor = (Doctor)DoctorsList.SelectedItem;
            SelectedDoctorName.Text = doctor.Name + " " + doctor.Surname;
            NextBtn.IsEnabled = true;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // pass doctor object from combobox to page construtor
            NavigationService.Navigate(new DateChoosePage((Doctor)DoctorsList.SelectedItem));
        }
    }
}
