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
        private List<UserSimplified> _workers;

        public ManageWorkersPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = await ApiCaller.Get("api/Users/GetWorkers");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                _workers = JsonConvert.DeserializeObject<List<UserSimplified>>(responseString);

                LoadWorkers(Role.Doctor);
            }
            else
            {
                MessageBox.Show("Błąd podczas pobierania pracowników z bazy");
            }
        }

        private void RoleSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ComboBoxItem)RoleSelection.SelectedItem;

            Enum.TryParse(selected.Tag.ToString(), out Role role);

            LoadWorkers(role);
        }

        private void LoadWorkers(Role role)
        {
            if(_workers == null)
            {
                return;
            }

            var workers = _workers.Where(w => w.Role == role).ToList();

            if (workers != null && workers.Count > 0)
            {
                Workers.ItemsSource = workers;
            }
            else
            {
                Workers.ItemsSource = null;
                MessageBox.Show("Brak dostępnych pracowników w bazie");
            }
        }

        private void Workers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Workers.SelectedItem != null)
            {
                this.NavigationService.Navigate(new EditWorkerPage((UserSimplified)Workers.SelectedItem));
            }
        }
    }
}
