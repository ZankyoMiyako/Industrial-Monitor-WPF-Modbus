using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Industrial_Monitor_WPF_Modbus.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            MinBtn.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            MaxBtn.Click += (s, e) => { this.WindowState = (this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized); };
            CloseBtn.Click += (s, e) => { this.Close(); };
        }

    }
}