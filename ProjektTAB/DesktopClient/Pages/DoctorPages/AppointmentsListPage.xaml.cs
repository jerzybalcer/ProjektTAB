using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for AppointmentsListPage.xaml
    /// </summary>
    public partial class AppointmentsListPage : Page
    {
        public AppointmentsListPage()
        {
            InitializeComponent();
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
