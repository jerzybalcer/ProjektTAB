using Database;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for ExaminationResultsPage.xaml
    /// </summary>
    public partial class ExaminationResultsPage : Page
    {
        private readonly Appointment? _appointment;

        public ExaminationResultsPage(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;
        }
    }
}
