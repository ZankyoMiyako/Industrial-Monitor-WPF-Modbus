using Industrial_Monitor.ViewModels;
using Industrial_Monitor.Views;
using System.Configuration;
using System.Data;
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
            regionManager.RequestNavigate("Communication",nameof(CommunicationView));

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationBarView,NavigationBarViewModel>();
            containerRegistry.RegisterForNavigation<CommunicationView, CommunicationViewModel>();
            containerRegistry.RegisterForNavigation<CommunicationParametersView, CommunicationParametersViewModel>();
        }
    }

}
