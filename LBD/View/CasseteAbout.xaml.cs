using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LBD.View
{
    /// <summary>
    /// Логика взаимодействия для CasseteAbout.xaml
    /// </summary>
    public partial class CasseteAbout : Window
    {
        public CasseteAbout(int id)
        {
            InitializeComponent();
            DataContext = new ViewModel.CasseteAboutViewModel(id);
        }
    }
}
