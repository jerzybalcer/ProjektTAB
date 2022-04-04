using Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddPatientPage.xaml
    /// </summary>
    public partial class AddPatientPage : Page
    {
        public AddPatientPage()
        {
            InitializeComponent();
        }

        private async void AddPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            // call authentication api
            HttpClient client = new HttpClient();


            var values = new Dictionary<string, string>
            {
                { "name", firstname.Text },
                { "surname", lastname.Text },
                { "pesel", pesel.Text },
                { "city", city.Text },
                { "street", street.Text },
                { "houseNumber", house.Text },
                { "roomNumber", apartment.Text }
            };

            //string content = JsonConvert.SerializeObject(values);
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://tabbackend.azurewebsites.net/", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonSettings = JsonConfiguration.GetJsonSettings();

            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
