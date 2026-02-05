using Industrial_Monitor.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    class CommunicationConfigViewModel : BindableBase
    {
        public CommunicationConfigViewModel(IEventAggregator eventAggregator)
        {
            aggregator = eventAggregator;
            CloseDrawerCommand = new DelegateCommand(() => aggregator.GetEvent<DrawerControlEvent>().Publish(new DrawerControlEventArgs
            {
                IsOpen = false
            }));
        }
        #region 事件聚合器引用
        private readonly IEventAggregator aggregator;
        #endregion
        #region 取消按钮命令
        public DelegateCommand CloseDrawerCommand { get; set; }
        #endregion
    }
}
