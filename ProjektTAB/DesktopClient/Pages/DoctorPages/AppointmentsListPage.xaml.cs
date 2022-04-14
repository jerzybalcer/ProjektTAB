using Database;
using Database.Appointments.Simplified;
using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for AppointmentsListPage.xaml
    /// </summary>
    public partial class AppointmentsListPage : Page
    {
        // this doctor -> CurrentAccount.CurrentUser
        public AppointmentsListPage()
        {
            InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            HttpResponseMessage response = await ApiCaller.Get("GetDoctorsAppointments/" + CurrentAccount.CurrentUser.AccountId);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                List<AppointmentSimplified> patients = JsonConvert.DeserializeObject<List<AppointmentSimplified>>(responseString);

                if (patients != null && patients.Count > 0)
                {
                    foreach (AppointmentSimplified app in patients)
                    {
                        Appointments.Items.Add(app);
                    }
                }
                else
                {
                    MessageBox.Show("Brak dostępnych pacjentów w bazie");
                }
            }
            else
            {
                MessageBox.Show("Brak dostępnych pacjentów w bazie");
            }
            // 1. load all appointments registered for this doctor +
            // (include started)
            // 2. sort by date - sooner first
            // 3. Appointments.ItemsSource = appointmentsFromApi;
        }

        private void Appointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenAppointmentBtn.IsEnabled = true;
            // SelectedAppointment.Text = ...;
        }

        private void OpenAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            // pass appointment object
            //this.NavigationService.Navigate(new AppointmentPage());
        }
        
    }
}
