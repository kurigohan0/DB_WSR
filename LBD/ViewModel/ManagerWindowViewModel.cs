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
            //GetCassetes();

        }

        //private ObservableCollection<CassetesView> _cassetes = new ObservableCollection<CassetesView>();
        //public ObservableCollection<CassetesView> Cassetes
        //{
        //    get
        //    {

        //        return _cassetes;
        //    }
        //    set
        //    {
        //        _cassetes = value;
        //        OnPropertyChanged("Cassetes");
        //    }
        //}

        //private void GetCassetes()
        //{
        //    Model.RentalShopEntities rs = new Model.RentalShopEntities();
        //    foreach (var item in rs.Cassetes)
        //    {
        //        _cassetes.Add(new CassetesView
        //        {
        //            Title = item.Title,
        //            Price = item.Price,
        //            Genere_Id = item.Genere_Id,
        //            Catalog_Id = item.Catalog_Id,
        //            Departament_Id = item.Departament_Id,
        //            Director = item.Director
        //        });
        //    }            
        //}

        public static List<CassetesView> GetCassetes()
        {
            Model.RentalShopEntities rs = new RentalShopEntities();
            List<CassetesView> cassetes = new List<CassetesView>();
            foreach (var item in rs.Cassetes)
            {
                cassetes.Add(new CassetesView
                {
                    Catalog_Id = item.Catalog_Id,
                    Departament_Id = item.Departament_Id,
                    Director = item.Director,
                    Genere_Id = item.Genere_Id,
                    Price = item.Price,
                    Title = item.Title,
                    Cover = API.Image.ByteArrayToImage(item.Cover)

                });
            }
            return cassetes;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
