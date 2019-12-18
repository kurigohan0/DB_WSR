using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBD.ViewModel
{
    class FindHandler
    {
        Model.RentalShopEntities rs;

        public enum FieldType
        {
            Title,
            Director,
            Price,
            Departament
        }
        public ObservableCollection<Model.Cassetes> FindTitle(FieldType type, string str)
        {
            rs = new Model.RentalShopEntities();
            ObservableCollection<Model.Cassetes> cassetes = new ObservableCollection<Model.Cassetes>();
            switch (type)
            {
                case FieldType.Title:
                    foreach (var item in rs.Cassetes)
                    {
                        if(item.Title.ToLower().Contains(str.ToLower()))
                        {
                            cassetes.Add(item);
                        }
                    }
                    break;
                case FieldType.Director:
                    foreach (var item in rs.Cassetes)
                    {
                        if (item.Director.ToLower().Contains(str.ToLower()))
                        {
                            cassetes.Add(item);
                        }
                    }
                    break;
                case FieldType.Price:
                    foreach (var item in rs.Cassetes)
                    {
                        if (item.Price.ToString().ToLower().Contains(str.ToLower()))
                        {
                            cassetes.Add(item);
                        }
                    }
                    break;
                case FieldType.Departament:
                    foreach (var item in rs.Cassetes)
                    {
                        if (item.Departament_Id.ToString().ToLower().Contains(str.ToLower()))
                        {
                            cassetes.Add(item);
                        }
                    }
                    break;
            }
            return cassetes;

        }
    }
}
