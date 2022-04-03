using Database;
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

namespace DesktopClient.Pages.DoctorPages
{
    /// <summary>
    /// Interaction logic for ExaminationOrderPage.xaml
    /// </summary>
    public partial class ExaminationOrderPage : Page
    {
        private readonly Appointment? _appointment;
        public ExaminationOrderPage(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;
        }
    }
}
