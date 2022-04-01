using Database.Users;
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

namespace DesktopClient.Pages
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class DateChoosePage : Page
    {
        private readonly Doctor? _chosenDoctor = null;
        private bool _isDatePicked = false;
        private bool _isHourPicked = false;

        public DateChoosePage()
        {
            InitializeComponent();
        }

        public DateChoosePage(Doctor chosenDoctor)
        {
            InitializeComponent();
            _chosenDoctor = chosenDoctor;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(_chosenDoctor is not null)
            {
                ChosenDoctorName.Text = _chosenDoctor.Name;
            }
            else
            {
                ChosenDoctorName.Text = "nie wybrano";
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ChosenDate.Text = DatePicker.SelectedDate.ToString();
            _isDatePicked = true;

            if(_isHourPicked && _isDatePicked)
            {
                NextBtn.IsEnabled = true;
            }

            // get all free dates from api
            //FreeDates.ItemsSource = freedates;
        }

        private void FreeDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChosenHour.Text = FreeDates.SelectedItem.ToString();
            _isHourPicked = true;

            if (_isHourPicked && _isDatePicked)
            {
                NextBtn.IsEnabled = true;
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // navigate to patient choose page
            // this.NavigationService.Navigate();
        }

    }
}
