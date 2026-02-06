using Industrial_Monitor.Core.Events;
using Industrial_Monitor.Views;
using System;
using System.Collections.Generic;
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
            this.regionManager = _regionManager;
            aggregator.GetEvent<DrawerControlEvent>().Subscribe(Args =>
            {
                IsRightDrawerOpen = Args.IsOpen;
                if (Args.IsOpen && !string.IsNullOrEmpty(Args.ViewName))
                {
                    // 根据ViewName创建对应的视图
                    RightDrawerContent = CreateView(Args.ViewName);
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
        private object CreateView(string viewName)
        {
            switch (viewName)
            {
                case nameof(CommunicationConfigView):
                    return new CommunicationConfigView();
                // 添加其他视图...
                default:
                    return null;
            }
        }
        #endregion
    }
}
