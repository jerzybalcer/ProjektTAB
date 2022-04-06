using Database;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for ExaminationOrderPage.xaml
    /// </summary>
    public partial class ExaminationOrderPage : Page
    {
        private readonly Appointment? _appointment;
        public ExaminationOrderPage(Appointment appointment)
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
    }
}
