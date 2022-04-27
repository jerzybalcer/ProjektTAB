using Database;
using Database.Appointments.Simplified;
using Database.Examinations;
using Database.Examinations.Simplified;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for ExaminationOrderPage.xaml
    /// </summary>
    public partial class ExaminationOrderPage : Page
    {
        private readonly AppointmentSimplified _appointment;
        public ExaminationOrderPage(AppointmentSimplified appointment)
        {
            InitializeComponent();
            _appointment = appointment;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadExaminationsList();
        }

        private async void LoadExaminationsList()
        {
            HttpResponseMessage response = await ApiCaller.Get("GetExaminationsTypes");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                List<ExaminationTemplate> types = JsonConvert.DeserializeObject<List<ExaminationTemplate>>(responseString);

                if (types != null && types.Count > 0)
                {
                    foreach (ExaminationTemplate exType in types)
                    {
                        if (exType.ExaminationType == ExaminationType.LabExamination)
                            LabExaminationCodes.Items.Add(exType.ExaminationCode);
                        else
                            PhysicalExaminationCodes.Items.Add(exType.ExaminationCode);
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak dostępnych pacjentów w bazie");
            }
        }

        private async void OrderLabExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = LabExaminationCodes.SelectedItem.ToString();

            if (type != null)
            {
                Tuple<string, int> tuple = new Tuple<string, int>(type, _appointment.AppointmentId);
                HttpResponseMessage response2 = await ApiCaller.Post("/AddLabExamination", tuple);
                if (response2.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pomyślnie zlecono badanie");
                }
                else
                    MessageBox.Show(await response2.Content.ReadAsStringAsync());
            }
        }

        private void LabExaminationCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderLabExaminationBtn.IsEnabled = true;
        }

        private struct dataToSend
        {
            public int AppointmentId;
            public string templateType;
            public string description;
        }

        private async void AddPhysicalExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = PhysicalExaminationCodes.SelectedItem.ToString();
            string desc = PhysicalExaminationResult.Text;

            dataToSend structure = new dataToSend { AppointmentId = _appointment.AppointmentId, templateType = type, description = desc};
            if (type != null)
            {
                HttpResponseMessage response = await ApiCaller.Post("/AddPhysicalExamination", structure);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pomyślnie zapisano badanie");
                }
                else
                    MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
        }

        private void PhysicalExaminationCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddPhysicalExaminationBtn.IsEnabled = true;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
