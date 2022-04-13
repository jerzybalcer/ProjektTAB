using Database;
using Database.Appointments.Simplified;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for ExaminationResultsPage.xaml
    /// </summary>
    public partial class ExaminationResultsPage : Page
    {
        private readonly AppointmentSimplified _appointment;

        public ExaminationResultsPage(AppointmentSimplified appointment)
        {
            InitializeComponent();
            _appointment = appointment;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadExaminationsList((bool)PhysicalExaminationsBtn.IsChecked);
        }

        private void Examinations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhysicalExaminationsBtn.IsChecked == true)
            {
                PhysicalExaminationDetails.Visibility = System.Windows.Visibility.Visible;
                LabExaminationDetails.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if(LabExaminationsBtn.IsChecked == true)
            {
                PhysicalExaminationDetails.Visibility = System.Windows.Visibility.Collapsed;
                LabExaminationDetails.Visibility = System.Windows.Visibility.Visible;
            }

            // show examination details
        }

        private void PhysicalExaminationsBtn_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            // load physical examinations to list
            LoadExaminationsList(true);
        }

        private void LabExaminationsBtn_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            // load lab examinations to list
            LoadExaminationsList(false);
        }

        private async void LoadExaminationsList(bool isPhysicalChecked)
        {
            if (isPhysicalChecked)
            {

            }
            else
            {

            }

            //Examinations.ItemsSource = examinationsFromApi;
        }

        private void BackBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();

            // save doctor's comment to the lab examination
        }
    }
}
