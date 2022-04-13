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
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 1. load all appointments registered for this doctor 
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
