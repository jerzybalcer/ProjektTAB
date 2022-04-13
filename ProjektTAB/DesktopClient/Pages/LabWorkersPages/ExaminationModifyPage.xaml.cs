using Database.Examinations;
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
    /// Logika interakcji dla klasy ExaminationModifyPage.xaml
    /// </summary>
    public partial class ExaminationModifyPage : Page
    {
        private readonly UserSimplified _labWorker;
        private readonly LabExamination _examination;

        public ExaminationModifyPage(UserSimplified labWorker, LabExamination examination)
        {
            InitializeComponent();
            _labWorker = labWorker;
            _examination = examination;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(_labWorker.Role == Role.LabAssistant)
            {
                LabAssistantPanel.Visibility = Visibility.Visible;
                LabManagerPanel.Visibility = Visibility.Collapsed;
            }
            else if(_labWorker.Role == Role.LabManager)
            {
                LabAssistantPanel.Visibility = Visibility.Collapsed;
                LabManagerPanel.Visibility = Visibility.Visible;
            }
        }

        private void SaveExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            // get status from combobox and then save
        }

        private void ExaminationStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExaminationDescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ExaminationDescriptionTextBox.Text.Length > 0)
            {
                SaveExaminationBtn.IsEnabled = true;
            }
            else
            {
                SaveExaminationBtn.IsEnabled = false;
            }
        }

        private void ExaminationManagerCommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveExaminationManagerBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
