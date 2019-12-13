using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LBD.ViewModel
{
    class CasseteShortInfo
    {
        public string Name { get; set; }
        public BitmapImage Cover { get; set; }
        public int Id { get;set; }
        public int? Copies { get; set; }
    }
}
