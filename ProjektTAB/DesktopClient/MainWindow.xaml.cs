using DesktopClient.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            ContentFrame.Navigate(new LoginPage());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name.ToString())
            {
                case "LoadRegisterPageBtn":
                    ContentFrame.Navigate(new RegistrationPage());
                    break;
                case "LoadPatientsListPageBtn":
                    ContentFrame.Navigate(new PatientsListPage());
                    break;
                case "LoadAccountInfoPageBtn":
                    ContentFrame.Navigate(new AccountInfoPage());
                    break;
            }

        }
    }
}
