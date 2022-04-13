using Database.Users.Simplified;
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

namespace DesktopClient.Pages.LabWorkersPages
{
    /// <summary>
    /// Logika interakcji dla klasy ExaminationsToDoPage.xaml
    /// </summary>
    public partial class ExaminationsToDoPage : Page
    {
        private readonly UserSimplified _labWorker;

        public ExaminationsToDoPage(UserSimplified labWorker)
        {
            InitializeComponent();
            _labWorker = labWorker;
        }

        private void Examinations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenExaminationBtn.IsEnabled = true;
            // code
            SelectedExaminationCode.Text = Examinations.SelectedItem.ToString();
            // surname
            SelectedExaminationSurname.Text = Examinations.SelectedItem.ToString();
        }

        private void OpenExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new ExaminationModifyPage(_labWorker, Examinations.SelectedItem));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(_labWorker.Role == Role.LabAssistant)
            {
                // get examinations to do
            }else if(_labWorker.Role == Role.LabManager)
            {
                // get examinations to check
            }
        }
    }
}
