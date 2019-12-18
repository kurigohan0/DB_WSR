using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LBD.ViewModel
{
    class AllOrdersViewModel : INotifyPropertyChanged
    {
        private Page p;
        Model.RentalShopEntities rs;
        public AllOrdersViewModel(Page page)
        {
            FindByDateCommand = new RelayCommand(() => FindByDateClick("FindButton"));
            p = page;
            Get();
        }

        
        public async void Get()
        {
            await Task.Run(() => GetOrders());
        }

        private void GetOrders()
        {
            rs = new Model.RentalShopEntities();
            foreach (var item in rs.Cassete_Rentals)
            {
                p.Dispatcher.Invoke(() =>
                {
                    Orders.Add(new OrderInfo
                    {
                        OrderID = item.Order_Id,
                        CopyID = item.Copy_Id,
                        Give_Date = item.Give_Date.Date,
                        Get_Date = item.Get_Date,
                        Client = item.Clients.First_Name + " " + item.Clients.Second_Name + " (" + item.Clients.Client_Id + ")",
                        Departament_ID = item.Departament_Id
                    });
                });
            }
        }

        public ICommand FindByDateCommand { get; set; }

        private void FindByDateClick(object sender)
        {
            Orders.Clear();
            rs = new Model.RentalShopEntities();
            foreach (var item in rs.Cassete_Rentals)
            {
                p.Dispatcher.Invoke(() =>
                {
                    if(item.Give_Date == SelectedDate)
                    Orders.Add(new OrderInfo
                    {
                        OrderID = item.Order_Id,
                        CopyID = item.Copy_Id,
                        Give_Date = item.Give_Date.Date,
                        Get_Date = item.Get_Date,
                        Client = item.Clients.First_Name + " " + item.Clients.Second_Name + " (" + item.Clients.Client_Id + ")",
                        Departament_ID = item.Departament_Id
                    });
                });
            }
        }

        private DateTime _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateTime SelectedDate {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public ObservableCollection<OrderInfo> _orders = new ObservableCollection<OrderInfo>();
        public ObservableCollection<OrderInfo> Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
