using LBD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LBD.API;
using System.Windows.Threading;

namespace LBD.ViewModel
{
    class ManagerWindowViewModel : NavigateViewModel
    {
        public ManagerWindowViewModel()
        {

        }

        public string ManagerName
        {
            get
            {
                return "Менеджер: " + AuthorizationHandler.UserName + " | (" + AuthorizationHandler.Login + ")";
            }
            set
            {

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
