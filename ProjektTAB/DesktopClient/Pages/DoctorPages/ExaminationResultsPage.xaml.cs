using Database;
using Database.Appointments.Simplified;
using Database.Examinations;
using Database.Examinations.Simplified;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for ExaminationResultsPage.xaml
    /// </summary>
    public partial class ExaminationResultsPage : Page
    {
        private readonly AppointmentSimplified _appointment;
        private List<PhysicalExaminationSimplified> _phisicalExaminations;
        private List<LabExaminationSimplified> _labExaminations;

        public ExaminationResultsPage(AppointmentSimplified appointment)
        {
            _appointment = appointment;
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadExaminationsList();
        }

        private void Examinations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhysicalExaminationsBtn.IsChecked == true)
            {
                PhysicalExaminationDetails.Visibility = System.Windows.Visibility.Visible;
                LabExaminationDetails.Visibility = System.Windows.Visibility.Collapsed;

                PhysicalExaminationSimplified selected = _phisicalExaminations[PhisicalExaminationsList.SelectedIndex];

                PhysicalExaminationName.Text = selected.ExaminationTemplate.Name;
                PhysicalExaminationDoctorName.Text = _appointment.Doctor.Name + " " + _appointment.Doctor.Surname;
                PhysicalExaminationDate.Text = _appointment.RegistrationDate.ToString();
                PhysicalExaminationResult.Text = selected.Result;
            }
            else if(LabExaminationsBtn.IsChecked == true)
            {
                PhysicalExaminationDetails.Visibility = System.Windows.Visibility.Collapsed;
                LabExaminationDetails.Visibility = System.Windows.Visibility.Visible;

                LabExaminationSimplified selected = _labExaminations[LabExaminationsList.SelectedIndex];

                LabExaminationName.Text = selected.ExaminationTemplate.Name;
                LabExaminationStatusText.Text = StatusDic.getStatusLabel(selected.Status.ToString());
                LabExaminationDoctorName.Text = _appointment.Doctor.Name + " " + _appointment.Doctor.Surname;
                LabExaminationLabAssistantName.Text = selected.LabAssistant == null ? "Nie przypisano" : selected.LabAssistant.Name + " " + selected.LabAssistant.Surname;
                LabExaminationManagerName.Text = selected.LabManager == null ? "Nie przypisano" : selected.LabManager.Name + " " + selected.LabManager.Surname;
                LabExaminationOrderDate.Text = selected.OrderDate == null ? "Nie przypisano" : selected.OrderDate.ToString();
                LabExaminationExecutionDate.Text = selected.ExecutionDate == null ? "Nie przypisano" : selected?.ExecutionDate.ToString();
                LabExaminationClosingDate.Text = selected.ClosingDate == null ? "Nie przypisano" :  selected?.ClosingDate.ToString();
                LabExaminationResult.Text = selected.Result == null ? "Nie przypisano" :  selected?.Result;
                LabExaminationManagerComment.Text = selected.LabManagerComment == null ? "Nie przypisano" :  selected?.LabManagerComment;
                LabExaminationDoctorComment.Text = selected.DoctorComment;
                
            }
        }

        private void PhysicalExaminationsBtn_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (LabExaminationsList != null)
            {
                // load physical examinations to list
                LabExaminationsList.Visibility = System.Windows.Visibility.Collapsed;
                PhisicalExaminationsList.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void LabExaminationsBtn_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            // load lab examinations to list
            PhisicalExaminationsList.Visibility = System.Windows.Visibility.Collapsed;
            LabExaminationsList.Visibility = System.Windows.Visibility.Visible;
        }

        private async void LoadExaminationsList()
        {
                HttpResponseMessage response = await ApiCaller.Get("/GetExaminations/"+ _appointment.AppointmentId);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var examinations = JsonConvert.DeserializeObject<Tuple<List<PhysicalExaminationSimplified>, List<LabExaminationSimplified>>>(responseString);

                    _phisicalExaminations = examinations.Item1;
                    _labExaminations = examinations.Item2;

                if (examinations.Item1 != null && examinations.Item1.Count > 0)
                    {
                        foreach (var exam in examinations.Item1)
                        {
                            PhisicalExaminationsList.Items.Add(exam);
                        }
                    }

                    if (examinations.Item2 != null && examinations.Item2.Count > 0)
                    {
                        foreach (var exam in examinations.Item2)
                        {
                            LabExaminationsList.Items.Add(exam);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(await response.Content.ReadAsStringAsync());
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
