using GalaSoft.MvvmLight.Messaging;
using LBD.API;
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
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        void NavigationSetup()
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(new View.Pages.CassetesList(this));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(new View.Pages.CreateOrderPage(this));
        }
    }
}
