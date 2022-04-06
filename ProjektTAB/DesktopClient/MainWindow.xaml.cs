﻿using DesktopClient.Authentication;
using DesktopClient.Pages.DoctorPages;
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
            }
        }

        public void HideMenuButtons()
        {
            ReceptionistMenu.Visibility = System.Windows.Visibility.Collapsed;
            DoctorMenu.Visibility = System.Windows.Visibility.Collapsed;
            LabAssistantMenu.Visibility = System.Windows.Visibility.Collapsed;
            LabManagerMenu.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void ShowMenuButtons(string type)
        {
            switch (type)
            {
                case "Receptionist":
                    ReceptionistMenu.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "Doctor":
                    DoctorMenu.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "LabAssistant":
                    LabAssistantMenu.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "LabManager":
                    LabManagerMenu.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
            //MenuButtons.Visibility = visibility;
        }

        private void UserLoggedIn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new LoggedAsPage(CurrentAccount.CurrentUser));
        }
    }
}
