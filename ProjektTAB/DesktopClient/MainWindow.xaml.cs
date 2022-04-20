using DesktopClient.Authentication;
using DesktopClient.Pages.AdminPages;
using DesktopClient.Pages.DoctorPages;
using DesktopClient.Pages.LabWorkersPages;
using DesktopClient.Pages.ReceptionistPages;
using DesktopClient.Pages.SharedPages;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new LoginPage());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name.ToString())
            {
                case "LoadRegisterPageBtn":
                    ContentFrame.Navigate(new DoctorChoosePage());
                    break;
                case "AddPatientPageBtn":
                    ContentFrame.Navigate(new AddPatientPage());
                    break;
                case "AddExaminationPage":
                    ContentFrame.Navigate(new AddExaminationPage());
                    break;
                case "AppointmentsListPageBtn":
                    ContentFrame.Navigate(new AppointmentsListPage());
                    break;
                case "ExaminationsListPageBtn":
                    ContentFrame.Navigate(new ExaminationsToDoPage(CurrentAccount.CurrentUser));
                    break;
                case "ManageWorkersPageBtn":
                    ContentFrame.Navigate(new ManageWorkersPage());
                    break;
                case "AddWorkersPageBtn":
                    ContentFrame.Navigate(new AddWorkersPage());
                    break;
                case "MyAccountPageBtn":
                    ContentFrame.Navigate(new MyAccountPage());
                    break;
            }
        }

        public void HideMenuButtons()
        {
            ReceptionistMenu.Visibility = Visibility.Collapsed;
            DoctorMenu.Visibility = Visibility.Collapsed;
            LabWorkerMenu.Visibility = Visibility.Collapsed;
            AdminMenu.Visibility = Visibility.Collapsed;
            MyAccountPageBtn.Visibility = Visibility.Collapsed;
        }

        public void ShowMenuButtons(string type)
        {
            switch (type)
            {
                case "Receptionist":
                    ReceptionistMenu.Visibility = Visibility.Visible;
                    break;
                case "Doctor":
                    DoctorMenu.Visibility = Visibility.Visible;
                    break;
                case "LabAssistant":
                    LabWorkerMenu.Visibility = Visibility.Visible;
                    break;
                case "LabManager":
                    LabWorkerMenu.Visibility = Visibility.Visible;
                    break;
                case "Admin":
                    AdminMenu.Visibility = Visibility.Visible;
                    break;
            }

            MyAccountPageBtn.Visibility=Visibility.Visible;
        }

        private void UserLoggedIn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new LoggedAsPage(CurrentAccount.CurrentUser));
        }
    }
}
