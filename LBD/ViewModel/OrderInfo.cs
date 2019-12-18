using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBD.ViewModel
{
    class OrderInfo
    {
        public int OrderID { get; set; }
        public int CopyID { get; set; }
        public DateTime Give_Date { get; set; }
        public DateTime Get_Date { get; set; }
        public string Client { get; set; }
        public int Departament_ID { get; set; }
    }
}
