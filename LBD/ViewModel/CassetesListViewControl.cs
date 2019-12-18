using LBD.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LBD.ViewModel
{

    class CassetesListViewControl : INotifyPropertyChanged
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token;
        Page p;
        public CassetesListViewControl(Page page)
        {
            token = cancelTokenSource.Token;
            p = page;
            _findargs.Add("Название");
            _findargs.Add("Режиссер");
            _findargs.Add("Цена");
            _findargs.Add("Отдел");
            Get();
            FindCommand = new RelayCommand(o => FindCommandClick("FindButton"));
            SelectedCasseteCommand = new RelayCommand(o => SelectedCasseteClick("SelectButton"));
            UpdateTable = new RelayCommand(p => UpdateTableClick("UpdateButton"));
        }

        #region Methods and Properties

        public async void Get()
        {
            IsUpdateAllows = false;
            await Task.Run(() => GetCassetes());
        }

        public ICommand FindCommand { get; set; }
        private async void FindCommandClick(object sender)
        {
            Cassetes.Clear();
            if(_inputtext != null && _inputtext != "")
            {
                FindHandler find = new FindHandler();
                switch (SelectedArg)
                {
                    case "Название":
                        foreach (var item in find.FindTitle(FindHandler.FieldType.Title, _inputtext))
                        {
                            Cassetes.Add(new CasseteShortInfo
                            {
                                Cover = API.Image.ByteArrayToImage(item.Cover),
                                Id = item.Catalog_Id,
                                Name = item.Title
                            });
                        }
                        break;
                    case "Режиссер":
                        foreach (var item in find.FindTitle(FindHandler.FieldType.Director, _inputtext))
                        {
                            Cassetes.Add(new CasseteShortInfo
                            {
                                Cover = API.Image.ByteArrayToImage(item.Cover),
                                Id = item.Catalog_Id,
                                Name = item.Title
                            });
                        }
                        break;
                    case "Цена":
                        foreach (var item in find.FindTitle(FindHandler.FieldType.Price, _inputtext))
                        {
                            Cassetes.Add(new CasseteShortInfo
                            {
                                Cover = API.Image.ByteArrayToImage(item.Cover),
                                Id = item.Catalog_Id,
                                Name = item.Title
                            });
                        }
                        break;
                    case "Отдел":
                        foreach (var item in find.FindTitle(FindHandler.FieldType.Departament, _inputtext))
                        {
                            Cassetes.Add(new CasseteShortInfo
                            {
                                Cover = API.Image.ByteArrayToImage(item.Cover),
                                Id = item.Catalog_Id,
                                Name = item.Title
                            });
                        }
                        break;
                }
            }
        }

        public async void GetCassetes()
        {
            Model.RentalShopEntities rs = new Model.RentalShopEntities();

            foreach (var item in rs.Cassetes)
            {
                p.Dispatcher.Invoke(() =>
                {
                    if (token.IsCancellationRequested)
                        return;
                    else
                        Cassetes.Add(new CasseteShortInfo
                        {
                            Cover = API.Image.ByteArrayToImage(item.Cover),
                            Id = item.Catalog_Id,
                            Name = item.Title
                        });
                });

            }
            IsUpdateAllows = true;

        }

        public ICommand UpdateTable { get; set; }

        private async void UpdateTableClick(object sender)
        {
            
            Cassetes.Clear();
            _cassetes.Clear();
            Get();
        }

        public ICommand SelectedCasseteCommand { get; set; }
        private async void SelectedCasseteClick(object sender)
        {
            if (SelectedCassete!= null)
            {
                View.CasseteAbout casseteAbout = new View.CasseteAbout(SelectedCassete.Id);
                casseteAbout.Show();
            }
        }
        #endregion

        #region Properies


        private string _inputtext;
        public string InputText
        {
            get
            {
                return _inputtext;
            }
            set
            {
                _inputtext = value;
                OnPropertyChanged("InputText");
            }
        }
        private string _selectedarg;

        public string SelectedArg
        {
            get
            {
                return _selectedarg;
            }
            set
            {
                _selectedarg = value;
                OnPropertyChanged("SelectedArg");
            }
        }

        private ObservableCollection<string> _findargs = new ObservableCollection<string>();

        public ObservableCollection<string> FindArgs
        {
            get
            {
                return _findargs;
            }
            set
            {
                _findargs = value;
                OnPropertyChanged("FindArgs");
            }
        }

        public CasseteShortInfo SelectedCassete { get; set; }

        public ObservableCollection<CasseteShortInfo> _cassetes = new ObservableCollection<CasseteShortInfo>();

        public ObservableCollection<CasseteShortInfo> Cassetes
        {
            get
            {
                return _cassetes;
            }
            set
            {
                _cassetes = value;
                OnPropertyChanged("Cassetes");
            }
        }
        
        private bool _isUpdateAllows;

        public bool IsUpdateAllows
        {
            get
            {
                return _isUpdateAllows;
            }
            set
            {
                _isUpdateAllows = value;
                OnPropertyChanged("IsUpdateAllows");
            }
        }
        #endregion

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
