using LBD.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LBD.ViewModel
{
    class CasseteAboutViewModel : DefaultViewModel
    {
        private Model.RentalShopEntities database;
        private Model.Cassetes cassetes;
        private int cat_id;

        public CasseteAboutViewModel(int id)
        {
            SaveCommand = new RelayCommand(o => SaveButtonClick("SaveButton"));
            CancelCommand = new RelayCommand(o => CancelCommandClick("CancelButton"));

        ConnectToDB();
            cat_id = id;

            if (database != null)
            {
                try
                {
                    cassetes = database.Cassetes.Find(id);
                    _title = cassetes.Title;
                    _director = cassetes.Director;
                    _departament = cassetes.Departament_Id;
                    _cover = API.Image.ByteArrayToImage(cassetes.Cover);
                    _price = cassetes.Price;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Запись не найдена.");
            }

        }

        private void ConnectToDB()
        {
            database = new Model.RentalShopEntities();
        }

        public string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string _director;
        public string Director
        {
            get
            {
                return _director;
            }
            set
            {
                _director = value;
                OnPropertyChanged("Director");
            }
        }

        public List<Model.Generes> _genre;
        public List<Model.Generes> Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged("Genre");
            }
        }

        public double? _price;
        public double? Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public int? _departament;
        public int? Departament
        {
            get
            {
                return _departament;
            }
            set
            {
                _departament = value;
                OnPropertyChanged("Departament");
            }
        }
        public int _contentId;
        public int ContentId
        {
            get
            {
                return _contentId;
            }
            set
            {
                _contentId = value;
                OnPropertyChanged("ContentId");
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

        public ICommand SaveCommand { get; set; }
        private async void SaveButtonClick(object sender)
        {
            database.Cassetes.Find(cat_id).Cover = API.Image.BitmapToByteArray(_cover);
            database.Cassetes.Find(cat_id).Title = _title;
            database.Cassetes.Find(cat_id).Director = _director;
            //database.Cassetes.Find(cat_id).Genere_Id = _genre;
            database.Cassetes.Find(cat_id).Price = _price;
            database.Cassetes.Find(cat_id).Departament_Id = _departament;

            database.SaveChanges();
            OnClosingRequest();
        }

        public ICommand CancelCommand { get; set; }
        private async void CancelCommandClick(object sender)
        {
            OnClosingRequest();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
