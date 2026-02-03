using Industrial_Monitor.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    internal class CommunicationViewModel
    {
        public CommunicationViewModel(IEventAggregator eventAggregator)
        {
            aggregator = eventAggregator;
            OpenDrawerCommand = new DelegateCommand(() => aggregator.GetEvent<DrawerControlEvent>().Publish(true));
        }
        #region 事件聚合器引用
        private IEventAggregator aggregator;
        #endregion
        #region 右侧边栏发布命令
        public DelegateCommand OpenDrawerCommand { get; }
        #endregion

    }
}
