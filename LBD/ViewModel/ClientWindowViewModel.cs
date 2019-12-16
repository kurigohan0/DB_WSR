using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBD.ViewModel
{
    class ClientWindowViewModel : INotifyPropertyChanged
    {
        public ClientWindowViewModel()
        {
        }

        public string ClientName
        {
            get
            {
                return "Клиент: " + AuthorizationHandler.UserName + " | (" + AuthorizationHandler.Login + ")";
            }
            set
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
