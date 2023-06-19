using KayanYazı.ViewModels;
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

namespace KayanYazı.Views
{
    /// <summary>
    /// Interaction logic for MetinKaydıPage.xaml
    /// </summary>
    public partial class MetinKaydıPage : Page
    {
        public MetinKaydıPage()
        {
            InitializeComponent();
            this.DataContext = new MetinKaydıViewModel();
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (System.Threading.ThreadStart)delegate ()
            {
                ViewModel.SetErrorTemplate(this.Content as DependencyObject);
            });

        }
    }
}
