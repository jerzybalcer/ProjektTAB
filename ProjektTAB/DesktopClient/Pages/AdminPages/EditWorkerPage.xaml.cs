using Database.Users.Simplified;
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

namespace DesktopClient.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for EditWorkerPage.xaml
    /// </summary>
    public partial class EditWorkerPage : Page
    {
        private readonly UserSimplified _worker;

        public EditWorkerPage(UserSimplified worker)
        {
            InitializeComponent();
            _worker = worker;
        }
    }
}
