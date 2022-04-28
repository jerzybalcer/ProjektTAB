using Database;
using Database.Appointments;
using Database.Appointments.Simplified;
using Database.Examinations;
using DesktopClient.Authentication;
using DesktopClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient.Pages.DoctorPages
{
    public partial class AppointmentPage : Page
    {
        private  AppointmentSimplified _appointment;

        public AppointmentPage(AppointmentSimplified appointment)
        {
            InitializeComponent();
            _appointment = appointment;
        }

        private void OrderExaminationsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExaminationOrderPage(_appointment));
        }

        private void ShowExaminationsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExaminationResultsPage(_appointment));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = await ApiCaller.Get("GetSelectedAppointment/" + _appointment.AppointmentId);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                AppointmentSimplified appointment = JsonConvert.DeserializeObject<AppointmentSimplified>(responseString);

                _appointment = appointment;
            }

            PatientText.Text = _appointment.Patient.Name + " " + _appointment.Patient.Surname;
            StatusText.Text = StatusDic.getStatusLabel(_appointment.Status.ToString());

            if (_appointment.Description != null)
                DescriptionText.Text = _appointment.Description.ToString();

            if (_appointment.Diagnosis != null)
                DiagnosisText.Text = _appointment.Diagnosis.ToString();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var comboBoxItem = (ComboBoxItem)AppointmentStatusSelection.SelectedItem;
            if (comboBoxItem != null)
            { 
                Enum.TryParse(comboBoxItem.Tag.ToString(), out AppointmentStatus status);
                _appointment.Status = status;

                if(status == AppointmentStatus.Unattended || status == AppointmentStatus.Finished || status == AppointmentStatus.Failed)
                {
                    _appointment.ClosingDate = DateTime.Now;
                }
            }
            if (DiagnosisText.Text.Length > 0)
                _appointment.Diagnosis = DiagnosisText.Text;
            if (DescriptionText.Text.Length > 0)
                _appointment.Description = DescriptionText.Text;

            _appointment.LabExaminations = new List<LabExamination>();
            _appointment.PhysicalExaminations = new List<PhysicalExamination>();

            HttpResponseMessage response = await ApiCaller.Post("/SaveAppointmentData", _appointment);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Pomyślnie zapisano informacje o wizycie");
            }
            else
                MessageBox.Show(await response.Content.ReadAsStringAsync());
        }

        private void AppointmentStatusSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveBtn.IsEnabled = true;
        }
    }
}
