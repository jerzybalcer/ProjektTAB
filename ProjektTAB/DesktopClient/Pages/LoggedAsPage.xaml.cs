using Database.Users;
using System;
using System.Collections.Generic;
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

namespace DesktopClient.Pages
{
    /// <summary>
    /// Interaction logic for LoggedAsPage.xaml
    /// </summary>
    public partial class LoggedAsPage : Page
    {
        private readonly User? _loggedUser = null;

        public LoggedAsPage()
        {
            InitializeComponent();
        }

        public LoggedAsPage(User loggedUser)
        {
            _loggedUser = loggedUser;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(_loggedUser is not null)
            {
                LoggedUserName.Text = _loggedUser.Name + " " + _loggedUser.Surname;
            }
            else
            {
                LoggedUserName.Text = "nie zalogowano";
            }
        }
    }
}
