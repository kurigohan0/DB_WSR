using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LBD.Model
{
    class CassetesView
    {
        public string Title { get; set; }
        public int Catalog_Id { get; set; }
        public BitmapImage Cover { get; set; }
        public Nullable<double> Price { get; set; }
        public string Director { get; set; }
        public Nullable<int> Genere_Id { get; set; }
        public Nullable<int> Departament_Id { get; set; }
    }
}
