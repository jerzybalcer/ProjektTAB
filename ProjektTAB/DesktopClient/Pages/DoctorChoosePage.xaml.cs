﻿using Database.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // get all doctors from api
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://tabbackend.azurewebsites.net/");
            HttpResponseMessage response = await client.GetAsync("GetListOfDoctors");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(responseString);
                DoctorsList.ItemsSource = doctors;
            }
            else
            {
                MessageBox.Show("Brak dostępnych doktorów w bazie");
            }
        }
        private  void DoctorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (Doctor)DoctorsList.SelectedItem;
            SelectedDoctorName.Text = item.Surname;
            NextBtn.IsEnabled = true;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // pass doctor object from combobox to page construtor
            this.NavigationService.Navigate(new DateChoosePage((Doctor)DoctorsList.SelectedItem));
        }
    }
}
