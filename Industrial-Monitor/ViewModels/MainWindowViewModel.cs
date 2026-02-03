using Industrial_Monitor.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IEventAggregator eventaggregator)
        {
            aggregator=eventaggregator;
            aggregator.GetEvent<DrawerControlEvent>().Subscribe(IsOpen => IsRightDrawerOpen = IsOpen,ThreadOption.UIThread);
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
                _IsRightDrawerOpen = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
