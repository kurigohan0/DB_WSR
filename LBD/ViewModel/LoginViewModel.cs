using LBD.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LBD.ViewModel
{
    class LoginViewModel : DefaultViewModel
    {
        private Properties.Settings settings = Properties.Settings.Default;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(o => LoginButtonClick("MainButton"));

            _login = "evgeny";
            _securepassword = "1";
        }

        public ICommand LoginCommand { get; set; }
        private async void LoginButtonClick(object sender)
        {
            AuthorizationHandler authorizationHandler = new AuthorizationHandler();
            if(authorizationHandler.Auth(_login, _securepassword) == Position.EmployeePosition.Manager)
            {
                View.ManagerWindow managerWindow = new View.ManagerWindow();
                managerWindow.Show();
                OnClosingRequest();
            }
            if(authorizationHandler.Auth(_login, _securepassword) == Position.EmployeePosition.Client)
            {
                View.ClientWindow clientWindow = new View.ClientWindow();
                clientWindow.Show();
            }
            if (authorizationHandler.Auth(_login, _securepassword) == Position.EmployeePosition.NotFound)
            {
                MessageBox.Show("Аккаунт не найден в базе данных.");
            }
        }

        private string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _securepassword;
        public string SecurePassword
        {
            get
            {
                return _securepassword;
            }
            set
            {
                _securepassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
