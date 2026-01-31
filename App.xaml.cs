using Industrial_Monitor_WPF_Modbus.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Industrial_Monitor_WPF_Modbus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }

}
