using GalaSoft.MvvmLight.Command;
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
using System.Windows.Media.Imaging;

namespace LBD.ViewModel
{
    class ReturnCasseteViewModel : INotifyPropertyChanged
    {
        Page p;
        Model.RentalShopEntities rs;
        public ReturnCasseteViewModel(Page page)
        {
            p = page;
            ReturnCommand = new RelayCommand(() => ReturnCassete("ReturnButton"));
        }

        private void ReturnCassete(object sender)
        {
            if (copy != null)
            {
                rs = new Model.RentalShopEntities();
                rs.Cassete_Copies.Find(copy.Copy_Id, copy.Catalog_Id).Status = "free";
                rs.SaveChanges();
                MessageBox.Show("Касета возвращена. Заказ выполнен.");
            }
        }

        public string _orderID;
        public string OrderID
        {
            get
            {
                

                return _orderID;
            }
            set
            {
                _orderID = value;
                GetTotalInfo();
                OnPropertyChanged("OrderID");
            }
        }

        public BitmapImage _cover;
        public BitmapImage Cover
        {
            get
            {
                return _cover;
            }
            set
            {
                _cover = value;
                OnPropertyChanged("Cover");
            }
        }

        private Model.Cassete_Copies copy;
        private void GetTotalInfo()
        {
            try
            {
                ReturnTotal.Clear();
                rs = new Model.RentalShopEntities();
                OrderInfo orderInfo;
                Model.Cassete_Rentals cassete_Rentals = rs.Cassete_Rentals.Find(int.Parse(OrderID));
                if(cassete_Rentals != null)
                {
                    
                    Model.Cassetes cassete = rs.Cassete_Copies.Where(s => s.Copy_Id == cassete_Rentals.Copy_Id).FirstOrDefault<Model.Cassete_Copies>().Cassetes;
                    if (rs.Cassete_Copies.Where(s => s.Copy_Id == cassete_Rentals.Copy_Id).FirstOrDefault<Model.Cassete_Copies>().Status == "busy")
                    {
                        Cover = API.Image.ByteArrayToImage(cassete.Cover);
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Клиент",
                            Value = cassete_Rentals.Clients.First_Name + " " + cassete_Rentals.Clients.Second_Name
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Номер клиента",
                            Value = cassete_Rentals.Clients.Client_Id.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Логин клиента",
                            Value = cassete_Rentals.Clients.Login
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Номер копии",
                            Value = cassete_Rentals.Copy_Id.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Кассета",
                            Value = cassete.Title
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Номер кассеты",
                            Value = cassete.Catalog_Id.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Дата получения",
                            Value = cassete_Rentals.Give_Date.Date.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Дата возврата",
                            Value = cassete_Rentals.Get_Date.Date.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Утеряна",
                            Value = IsLose.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Номер отдела",
                            Value = cassete_Rentals.Departament_Id.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Адрес отдела",
                            Value = cassete_Rentals.Departaments.State + ", " + cassete_Rentals.Departaments.City + ", " + cassete_Rentals.Departaments.Street + ", " + cassete_Rentals.Departaments.Zip
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Цена за кассету в сутки",
                            Value = cassete.Price.ToString()
                        });
                        ReturnTotal.Add(new ReturnInfo
                        {
                            Title = "Цена по договору",
                            Value = (((cassete_Rentals.Get_Date - cassete_Rentals.Give_Date).Days) * Convert.ToInt32(cassete.Price)).ToString()
                        });
                        copy = rs.Cassete_Copies.Where(s => s.Copy_Id == cassete_Rentals.Copy_Id).FirstOrDefault<Model.Cassete_Copies>();
                    }
                    else
                    {
                        MessageBox.Show("Заказ уже завершен");
                        ReturnTotal.Clear();
                        Cover = null;
                        IsLose = false;
                    }

                }
                else
                {
                    MessageBox.Show("Заказ не найден");
                    Cover = null;
                    IsLose = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public ICommand ReturnCommand { get; set; }

        public bool _isLose;
        public bool IsLose
        {
            get
            {
                return _isLose;
            }
            set
            {
                _isLose = value;
                OnPropertyChanged("IsLose");
            }
        }

        public ObservableCollection<ReturnInfo> _returnTotal = new ObservableCollection<ReturnInfo>();
        public ObservableCollection<ReturnInfo> ReturnTotal
        {
            get
            {
                return _returnTotal;
            }
            set
            {
                _returnTotal = value;
                OnPropertyChanged("ReturnTotal");
                
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
