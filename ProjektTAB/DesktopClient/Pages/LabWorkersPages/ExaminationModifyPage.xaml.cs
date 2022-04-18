using Database.Examinations;
using Database.Examinations.Simplified;
using Database.Users.Simplified;
using DesktopClient.Helpers;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.LabWorkersPages
{
    /// <summary>
    /// Logika interakcji dla klasy ExaminationModifyPage.xaml
    /// </summary>
    public partial class ExaminationModifyPage : Page
    {
        private readonly UserSimplified _labWorker;
        private readonly LabExamination _examination;
        private readonly DateTime _executionTime;
        private readonly LabExaminationStatus status;

        public ExaminationModifyPage(UserSimplified labWorker, LabExamination examination)
        {
            _executionTime = DateTime.Now;
            _labWorker = labWorker;
            _examination = examination;
            status = _examination.Status;
            InitializeComponent();
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
                ExaminationAssistantName.Text = _examination.LabAssistant.Name +" "+ _examination.LabAssistant.Surname;
                if(status == LabExaminationStatus.SuccessfullyExecuted)
                    ExaminationResultText.Text = "Sukces";
                else if(status == LabExaminationStatus.CancelledByAssistant)
                    ExaminationResultText.Text = "Anulowane";
                ExaminationDescriptionText.Text = _examination.Result;
            }
        }

        private async void SaveExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            _examination.Result = ExaminationDescriptionTextBox.Text;
            LabExaminationSimplified examination = new LabExaminationSimplified(_labWorker,null,_examination,_executionTime);
            HttpResponseMessage response = await ApiCaller.Post("/ChangeLabExaminationStatus",examination);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Pomyślnie zmieniono status badania");
                NavigationService.Navigate(new ExaminationsToDoPage(_labWorker));
            }
            else
                MessageBox.Show(await response.Content.ReadAsStringAsync());
        }

        private void ExaminationStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                var comboBoxItem = (ComboBoxItem)ExaminationStatusComboBox.SelectedItem;
                Enum.TryParse(comboBoxItem.Tag.ToString(), out LabExaminationStatus status);
                _examination.Status = status;
        }
        private void ManagerExaminationStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                var comboBoxItem = (ComboBoxItem)ManagerExaminationStatusComboBox.SelectedItem;
                Enum.TryParse(comboBoxItem.Tag.ToString(), out LabExaminationStatus status);
                _examination.Status = status;
        }

        private void ExaminationDescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ExaminationDescriptionTextBox.Text.Length > 0)
                SaveExaminationBtn.IsEnabled = true;
            else
                SaveExaminationBtn.IsEnabled = false;
        }

        private void ExaminationManagerCommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ExaminationManagerCommentTextBox.Text.Length > 0)
                SaveExaminationManagerBtn.IsEnabled = true;
            else
                SaveExaminationManagerBtn.IsEnabled = false;
        }

        private async void SaveExaminationManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            _examination.LabManagerComment = ExaminationManagerCommentTextBox.Text;
            LabExaminationSimplified examination = new LabExaminationSimplified(null, _labWorker, _examination, _executionTime);
            HttpResponseMessage response = await ApiCaller.Post("/ChangeLabExaminationStatusByManager", examination);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Pomyślnie zmieniono status badania");
                NavigationService.Navigate(new ExaminationsToDoPage(_labWorker));
            }
            else
                MessageBox.Show(await response.Content.ReadAsStringAsync());
        }
    }
}
