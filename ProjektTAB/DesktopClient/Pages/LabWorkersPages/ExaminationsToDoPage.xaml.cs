using Database.Examinations;
using Database.Users.Simplified;
using DesktopClient.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;


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
            LabExamination examination = Examinations.SelectedItem as LabExamination;
            SelectedExaminationCode.Text = examination?.ExaminationTemplate.ExaminationCode;
            SelectedExaminationSurname.Text = examination?.Appointment.Patient.Surname;
        }
        private void OpenExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            LabExamination examination = Examinations.SelectedItem as LabExamination;
            NavigationService.Navigate(new ExaminationModifyPage(_labWorker, examination));
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(_labWorker.Role == Role.LabAssistant)
            {
                HttpResponseMessage response = await ApiCaller.Get("/GetAllOrderedLabExaminations");
                if (response.IsSuccessStatusCode)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    List<LabExamination> examinations = JsonConvert.DeserializeObject<List<LabExamination>>(responseMessage);
                    Examinations.ItemsSource = examinations;
                }
                else
                    MessageBox.Show("Nie znaleziono żadnych badań"); 
            }
            else if(_labWorker.Role == Role.LabManager)
            {
                HttpResponseMessage response = await ApiCaller.Get("/GetAllExecutedLabExaminations");
                if (response.IsSuccessStatusCode)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    List<LabExamination> examinations = JsonConvert.DeserializeObject<List<LabExamination>>(responseMessage);
                    Examinations.ItemsSource = examinations;
                }
                else
                    MessageBox.Show("Nie znaleziono żadnych badań");
            }
        }
    }
}
