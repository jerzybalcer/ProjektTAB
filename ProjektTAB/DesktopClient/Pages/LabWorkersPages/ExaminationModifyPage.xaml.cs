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

namespace DesktopClient.Pages.LabWorkersPages
{
    /// <summary>
    /// Logika interakcji dla klasy ExaminationModifyPage.xaml
    /// </summary>
    public partial class ExaminationModifyPage : Page
    {
        private readonly UserSimplified _labWorker;

        public ExaminationModifyPage(UserSimplified labWorker)
        {
            InitializeComponent();
            _labWorker = labWorker;
        }
    }
}
