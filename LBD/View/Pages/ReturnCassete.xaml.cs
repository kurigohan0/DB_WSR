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
    /// Логика взаимодействия для ReturnCassete.xaml
    /// </summary>
    public partial class ReturnCassete : Page
    {
        Window w;
        public ReturnCassete(Window window)
        {
            w = window;
            DataContext = new ViewModel.ReturnCasseteViewModel(this);
            InitializeComponent();
        }
    }
}
