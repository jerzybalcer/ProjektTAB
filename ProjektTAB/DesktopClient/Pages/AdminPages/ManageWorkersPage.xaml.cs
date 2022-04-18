using Database.Users.Simplified;
using DesktopClient.Helpers;
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

namespace DesktopClient.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for ManageWorkersPage.xaml
    /// </summary>
    public partial class ManageWorkersPage : Page
    {
        private readonly UserSimplified _currentAdmin;

        public ManageWorkersPage(UserSimplified currentAdmin)
        {
            InitializeComponent();
            _currentAdmin = currentAdmin;
        }

        private void SaveWorkersListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = await ApiCaller.Get("api/Users/GetWorkers");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                List<UserSimplified> workers = JsonConvert.DeserializeObject<List<UserSimplified>>(responseString);

                if (workers != null && workers.Count > 0)
                {
                    Workers.ItemsSource = workers;
                }
                else
                {
                    MessageBox.Show("Brak dostępnych pracowników w bazie");
                }
            }
            else
            {
                MessageBox.Show("Brak dostępnych pracowników w bazie");
            }
        }
    }
}
