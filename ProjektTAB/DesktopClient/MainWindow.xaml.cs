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
            }
        }

        public void ChangeMenuButtonVisibility(Visibility visibility)
        {
            MenuButtons.Visibility = visibility;
        }
    }
}
