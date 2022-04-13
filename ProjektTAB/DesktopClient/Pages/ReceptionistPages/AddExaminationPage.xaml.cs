using Database.Examinations;
using DesktopClient.Helpers;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.ReceptionistPages
{
    /// <summary>
    /// Interaction logic for AddExaminationPage.xaml
    /// </summary>
    public partial class AddExaminationPage : Page
    {
        public AddExaminationPage()
        {
            InitializeComponent();
        }

        private async void AddExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            ExaminationType type = new ExaminationType();
            if (ExaminationTypeItem.Text == "Fizykalne")
                type = ExaminationType.PhysicalExamination;
            else if (ExaminationTypeItem.Text == "Laboratoryjne")
                type = ExaminationType.LabExamination;
            ExaminationTemplate exam = new ExaminationTemplate(ExaminationCode.Text,type,ExaminationName.Text);
            HttpResponseMessage response = await ApiCaller.Post("/AddExaminationTemplate",exam);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Poprawnie dodano do słownika");
                NavigationService.Navigate(new AddExaminationPage());
            }
            else
                MessageBox.Show("Nie udało się dodać do słownika");
        }

        private void ExaminationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ExaminationName.Text != "" && ExaminationCode.Text.Length == 3)
                AddExaminationBtn.IsEnabled = true;
            else
                AddExaminationBtn.IsEnabled = false;
        }

        private void ExaminationCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ExaminationName.Text != "" && ExaminationCode.Text.Length == 3)
                AddExaminationBtn.IsEnabled = true;
            else
                AddExaminationBtn.IsEnabled = false;
        }
    }
}
