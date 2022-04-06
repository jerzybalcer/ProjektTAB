using Database;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        private readonly Appointment? _appointment;

        public AppointmentPage(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;
        }

        private void OrderExaminationsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExaminationOrderPage(_appointment));
        }

        private void ShowExaminationsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExaminationResultsPage(_appointment));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PatientText.Text = _appointment.Patient.Name + " " + _appointment.Patient.Surname;
            StatusText.Text = _appointment.Status.ToString();
            DescriptionText.Text = _appointment.Description.ToString();
            DiagnosisText.Text = _appointment.Diagnosis.ToString();
        }
    }
}
