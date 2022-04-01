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
    /// Interaction logic for DoctorChoosePage.xaml
    /// </summary>
    public partial class DoctorChoosePage : Page
    {
        public DoctorChoosePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // get all doctors from api
            //DoctorsList.ItemsSource = doctors;

            // testing
            DoctorsList.ItemsSource = new List<string> {"1", "2", "3" };
        }
        private void DoctorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDoctorName.Text = DoctorsList.SelectedItem.ToString();
            NextBtn.IsEnabled = true;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // pass doctor object from combobox to page construtor
            // (cast to doctor from comboboxitem)
            this.NavigationService.Navigate(new DateChoosePage());
        }
    }
}
