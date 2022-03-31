using System.ComponentModel;
using System.Security;

namespace DesktopClient.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string Email { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
