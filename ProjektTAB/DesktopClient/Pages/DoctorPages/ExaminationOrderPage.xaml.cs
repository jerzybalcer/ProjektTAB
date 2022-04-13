using Database;
using Database.Appointments.Simplified;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for ExaminationOrderPage.xaml
    /// </summary>
    public partial class ExaminationOrderPage : Page
    {
        private readonly AppointmentSimplified _appointment;
        public ExaminationOrderPage(AppointmentSimplified appointment)
        {
            InitializeComponent();
            _appointment = appointment;
        }

        private void OrderLabExaminationBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LabExaminationCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddPhysicalExaminationBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PhysicalExaminationCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
