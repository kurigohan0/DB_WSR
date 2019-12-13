using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBD.ViewModel
{
    class DefaultViewModel : INotifyPropertyChanged
    {
        public DefaultViewModel()
        {
        }

        

        public event EventHandler ClosingRequest;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnClosingRequest()
        {
            if (this.ClosingRequest != null)
            {
                this.ClosingRequest(this, EventArgs.Empty);
            }
        }

    }
}
