using Database.Users;
using Database.Users.Simplified;
using DesktopClient.Authentication;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DesktopClient.Pages.SharedPages
{
    /// <summary>
    /// Interaction logic for LoggedAsPage.xaml
    /// </summary>
    public partial class LoggedAsPage : Page
    {
        private readonly UserSimplified? _loggedUser = null;
        private readonly DispatcherTimer _timer;

        public LoggedAsPage(UserSimplified loggedUser)
        {
            InitializeComponent();
            _loggedUser = loggedUser;
            _timer = new DispatcherTimer();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_loggedUser is not null)
            {
                LoggedUserName.Text = _loggedUser.Name + " " + _loggedUser.Surname;
            }
            else
            {
                LoggedUserName.Text = "nie zalogowano";
            }

            ClockTick(null, null);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += ClockTick;
            _timer.Start();
        }

        private void ClockTick(object? sender, EventArgs e)
        {
            ClockHours.Text = DateTime.Now.Hour.ToString("D2");
            ClockMinutes.Text = DateTime.Now.Minute.ToString("D2");
            ClockSeconds.Text = DateTime.Now.Second.ToString("D2");

            var culture = new CultureInfo("pl-PL");

            WeekDay.Text = ((int)DateTime.Now.DayOfWeek).ToString(culture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek), culture);
            Day.Text = DateTime.Now.Day.ToString();
            Month.Text = ((int)DateTime.Now.Month).ToString(culture.DateTimeFormat.GetMonthName(DateTime.Now.Month), culture);
            Year.Text = DateTime.Now.Year.ToString();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentAccount.Logout();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }
    }
}
