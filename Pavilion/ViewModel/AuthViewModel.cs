using Pavilion.Commands;
using Pavilion.Model.Repository;
using System.Windows.Input;

namespace Pavilion.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        public AuthRepository AuthRepository;

        private string _login = "Elizor@gmai.com";
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string _password = "yntiRS";
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }


        public ICommand LoginCommand { get; }

        public AuthViewModel()
        {
            LoginCommand = new AuthCommand(this);
            AuthRepository = new AuthRepository();
        }

    }
}
