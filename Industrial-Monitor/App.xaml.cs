using Industrial_Monitor.Core.Services;
using Industrial_Monitor.ViewModels;
using Industrial_Monitor.Views;
using System.Configuration;
using System.Windows;

namespace Industrial_Monitor
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
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("NavigationBar", nameof(NavigationBarView));
            regionManager.RequestNavigate("Connection",nameof(ConnectionView));
            regionManager.RequestNavigate("RequestConfig", nameof(RequestConfigView));
            regionManager.RequestNavigate("ModbusData",nameof(ModbusDataView));
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationBarView,NavigationBarViewModel>();
            containerRegistry.RegisterForNavigation<ConnectionView, ConnectionViewModel>();
            containerRegistry.RegisterForNavigation<ConnectionConfigView, ConnectionConfigViewModel>();
            containerRegistry.RegisterForNavigation<RequestConfigView, RequestConfigViewModel>();
            containerRegistry.RegisterForNavigation<ModbusDataView,ModbusDataViewModel>();

            containerRegistry.RegisterSingleton<IModbusMasterService,ModbusMasterService>();
            containerRegistry.RegisterSingleton<IRequestConfigService, RequestConfigService>();
        }
    }

}
