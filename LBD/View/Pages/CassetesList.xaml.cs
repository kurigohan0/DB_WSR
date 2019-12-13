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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LBD.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CassetesList.xaml
    /// </summary>
    public partial class CassetesList : Page
    {
        private Window window;
        public CassetesList(Window w)
        {
            InitializeComponent();
            DataContext = new ViewModel.CassetesListViewControl(this);
            window = w;
        }

    }
}
