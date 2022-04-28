using Database.Appointments.Simplified;
using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace DesktopClient.Pages.ReceptionistPages
{
    /// <summary>
    /// Interaction logic for AppointmentCancelPage.xaml
    /// </summary>
    public partial class AppointmentCancelPage : Page
    {
        public AppointmentCancelPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            HttpResponseMessage response = await ApiCaller.Get("GetAllAppointments");
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
                    MessageBox.Show("Brak dostępnych wizyt w bazie");
                }
            }
            else
            {
                MessageBox.Show("Błąd podczas pobierania wizyt");
            }
        }

        private void Appointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CancelAppointmentBtn.IsEnabled = true;
            AppointmentSimplified selected = (AppointmentSimplified)Appointments.SelectedItems[0];
            SelectedAppointment.Text = string.Format("{0} {1}", selected.Patient.Name, selected.Patient.Surname);
        }

        private async void CancelAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Appointments.SelectedItems[0] != null)
            {
                AppointmentSimplified selected = (AppointmentSimplified)Appointments.SelectedItems[0];

                HttpResponseMessage response = await ApiCaller.Post("CancelAppointment", selected.AppointmentId);

                if (response.IsSuccessStatusCode)
                {
                    this.NavigationService.Navigate(new AppointmentCancelPage());
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas zmiany statusu wizyty");
                }
            }
        }
    }
}
