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
                List<UserSimplified> doctors = JsonConvert.DeserializeObject<List<UserSimplified>>(responseString);
                DoctorsList.ItemsSource = doctors;
            }
            else
            {
                MessageBox.Show("Brak dostępnych doktorów w bazie");
            }
        }
        private void DoctorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var doctor = (UserSimplified)DoctorsList.SelectedItem;
            SelectedDoctorName.Text = doctor.Name + " " + doctor.Surname;
            NextBtn.IsEnabled = true;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // pass doctor object from combobox to page construtor
            NavigationService.Navigate(new DateChoosePage((UserSimplified)DoctorsList.SelectedItem));
        }
    }
}
