using Industrial_Monitor.Core.Events;
using Industrial_Monitor.Core.Models;
using Industrial_Monitor.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IEventAggregator eventaggregator, IRegionManager _regionManager)
        {
            aggregator = eventaggregator;
            aggregator.GetEvent<DrawerControlEvent>().Subscribe(args =>
            {
                IsRightDrawerOpen = args.IsOpen;

                if (args.IsOpen && !string.IsNullOrEmpty(args.ViewName))
                {
                    // 重用缓存的View，而不是每次都创建新的
                    RightDrawerContent = GetOrCreateView(args.ViewName, args.ConfigPayload);
                }
                else
                {
                    RightDrawerContent = null;
                }
            }, ThreadOption.UIThread);
        }
        #region 事件聚合器引用
        private readonly IEventAggregator aggregator;
        #endregion
        #region 右侧边栏的开关属性
        private bool _IsRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return _IsRightDrawerOpen; }
            set
            {
               SetProperty(ref _IsRightDrawerOpen, value);
            }
        }
        #endregion
        #region 导航字段
        private readonly IRegionManager regionManager;
        #endregion
        #region 侧边栏内容绑定
        private object _rightDrawerContent;
        public object RightDrawerContent
        {
            get => _rightDrawerContent;
            set => SetProperty(ref _rightDrawerContent, value);
        }
        private object GetOrCreateView(string viewName, CommunicationConfigParameters parameters = null)
        {

            switch (viewName)
            {
                case nameof(CommunicationConfigView):
                    if (_cachedConfigView == null)
                    {
                        _cachedConfigView = new CommunicationConfigView();
                    }
                    // 如果有参数，设置到ViewModel
                    if (parameters != null && _cachedConfigView.DataContext is CommunicationConfigViewModel vm)
                    {
                        Debug.WriteLine($"设置参数到缓存的ViewModel: IP={parameters.IpAddress}");
                        vm.ConfigParameters.IpAddress = parameters.IpAddress;
                        vm.ConfigParameters.Port = parameters.Port;
                        vm.ConfigParameters.Timeout = parameters.Timeout;
                        vm.ConfigParameters.RetryCount = parameters.RetryCount;
                    }

                    return _cachedConfigView;

                default:
                    return null;
            }
        }
        private CommunicationConfigView _cachedConfigView;
        #endregion
    }
}
