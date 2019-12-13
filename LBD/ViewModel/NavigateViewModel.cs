using GalaSoft.MvvmLight.Messaging;
using LBD.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBD.ViewModel
{
    public class NavigateViewModel : INotifyPropertyChanged
    {
        public NavigateViewModel()
        {

        }

        public string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Navigate(string url)
        {
            Messenger.Default.Send<NavigateArgs>(new NavigateArgs(url));
        }
    }
}
