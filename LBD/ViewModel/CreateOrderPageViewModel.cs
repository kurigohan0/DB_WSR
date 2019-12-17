﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LBD.ViewModel
{
    class CreateOrderPageViewModel : INotifyPropertyChanged
    {
        private Page p;
        Model.RentalShopEntities rs;
        public CreateOrderPageViewModel(Page page)
        {
            p = page;

            Get();

            GetAllClientsNames();
            FindCommand = new RelayCommand(() => FindCommandClick("FindButton"));
            CreateOrder = new RelayCommand(() => CrateOrderClick("CreateOrderButton"));

        }

        private void CrateOrderClick(object sender)
        {
            rs = new Model.RentalShopEntities();
            rs.Cassete_Rentals.Add(new Model.Cassete_Rentals
            {
                Order_Id = rs.Cassete_Rentals.Count() + 1,
                Copy_Id = SelectedCassete.CopyId,
                Give_Date = SelectedDate,
                Get_Date = SelectedReturnDate,
                Client_Id = 1,//fix
                Departament_Id = rs.Staff.Find(AuthorizationHandler.CurrentUserID).Departament_Id.GetValueOrDefault()
            });
            rs.Cassete_Copies.Find(SelectedCassete.CopyId, SelectedCassete.Id).Status = "busy";
            rs.SaveChanges();
            MessageBox.Show("Заказ создан.");
            GetFreeCassetes();
        }

        public async void Get()
        {
            await Task.Run(() => GetFreeCassetes());
        }

        public ObservableCollection<CasseteShortInfo> _freecassetes = new ObservableCollection<CasseteShortInfo>();
        public ObservableCollection<CasseteShortInfo> FreeCassetes
        {
            get
            {
                return _freecassetes;
            }
            set
            {
                _freecassetes = value;
                OnPropertyChanged("FreeCassetes");
            }
        }

        private CasseteShortInfo _selectedCassete;
        public CasseteShortInfo SelectedCassete
        {
            get
            {
                return _selectedCassete;
            }
            set
            {
                _selectedCassete = value;
                _selectedTitle = _selectedCassete.Name;
                _price = rs.Cassetes.Find(_selectedCassete.Id).Price.ToString();
                OnPropertyChanged("SelectedCassete");
                OnPropertyChanged("SelectedTitle");
                OnPropertyChanged("Price");
                OnPropertyChanged("TotalPrice");
            }
        }

        private string _selectedTitle;
        public string SelectedTitle
        {
            get
            {
                try
                {
                    return _selectedTitle + " (id " + SelectedCassete.Id + ")";
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                _selectedTitle = value;
                OnPropertyChanged("SelectedTitle");
            }
        }

        private string _price;
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                OnPropertyChanged("Price");
            }
        }

        private string _totalPrice;
        public string TotalPrice
        {
            get
            {
                try
                {
                    return (int.Parse(_price) * (_selectedReturnDate - _selectedDate).TotalDays).ToString();
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                OnPropertyChanged("TotalPrice");

            }
        }

        private DateTime _selectedReturnDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 14);
        public DateTime SelectedReturnDate
        {
            get
            {
                return _selectedReturnDate;
            }
            set
            {
                _selectedReturnDate = value;
                OnPropertyChanged("SelectedDate");
                OnPropertyChanged("TotalPrice");
            }
        }

        private DateTime _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
                OnPropertyChanged("TotalPrice");
            }
        }

        public ObservableCollection<string> _clients = new ObservableCollection<string>();

        public ObservableCollection<string> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                OnPropertyChanged("Clients");
            }
        }
        
        private string _findArg;
        public string FindArg
        {
            get
            {
                return _findArg;
            }
            set
            {
                _findArg = value;
                
                OnPropertyChanged("FindArg");
            }
        }

        private void GetAllClientsNames()
        {

            rs = new Model.RentalShopEntities();

            foreach (var item in rs.Clients)
            {
                Clients.Add(item.First_Name + " " + item.Second_Name + "(id" + item.Client_Id + ")");
            }
            OnPropertyChanged("Clients");

        }
        public ICommand FindCommand { get; set; }
        public ICommand CreateOrder { get; set; }

        public async void FindCommandClick(object sender)
        {
            FindHandler find = new FindHandler();
            rs = new Model.RentalShopEntities();
            List<int> tmp = new List<int>();
            FreeCassetes.Clear();
            foreach (var item in find.FindTitle(FindHandler.FieldType.Title, FindArg))
            {
                foreach (var copyItem in rs.Cassete_Copies)
                {
                    if(item != null && copyItem.Catalog_Id == item.Catalog_Id && copyItem.Status != "busy" && !tmp.Contains(copyItem.Catalog_Id))
                    {
                        tmp.Add(copyItem.Catalog_Id);
                        p.Dispatcher.Invoke(() =>
                        {
                            FreeCassetes.Add(new CasseteShortInfo
                            {
                                Cover = API.Image.ByteArrayToImage(item.Cover),
                                Id = item.Catalog_Id,
                                Name = item.Title
                            });
                        });
                    }
                }

            }
            tmp.Clear();
        }
        public string OrderId
        {
            get
            {
                rs = new Model.RentalShopEntities();
                if (rs.Cassete_Rentals.Count() > 0)
                {
                    return (rs.Cassete_Rentals.Last().Order_Id + 1).ToString();
                }
                else
                {
                    return "1";
                }
            }
            set
            {
                OnPropertyChanged("OrderId");
            }
        }

        public void GetFreeCassetes()
        {
            rs = new Model.RentalShopEntities();
            List<int> tmp = new List<int>();
            Model.Cassetes cassete;
            FreeCassetes.Clear();
            foreach (var item in rs.Cassete_Copies)
            {
                cassete = rs.Cassetes.Find(item.Catalog_Id);
                if (cassete != null && item.Status != "busy" && !tmp.Contains(item.Catalog_Id))
                {
                    tmp.Add(item.Catalog_Id);
                    p.Dispatcher.Invoke(() =>
                    {
                        FreeCassetes.Add(new CasseteShortInfo
                        {
                            Name = cassete.Title,
                            Copies = item.Copy_Id,
                            Cover = API.Image.ByteArrayToImage(cassete.Cover),
                            Id = item.Catalog_Id,
                            CopyId = item.Copy_Id
                        });
                    });

                }
            }
            tmp.Clear();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
