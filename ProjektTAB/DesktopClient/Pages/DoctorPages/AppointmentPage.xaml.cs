using Database;
using System;
using System.Collections.Generic;
using System.Linq;
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
