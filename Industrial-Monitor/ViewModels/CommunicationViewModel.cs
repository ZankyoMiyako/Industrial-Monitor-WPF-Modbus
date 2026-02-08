using Industrial_Monitor.Core.Events;
using Industrial_Monitor.Core.Models;
using Industrial_Monitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    internal class CommunicationViewModel : BindableBase
    {
        public CommunicationViewModel(IEventAggregator eventAggregator)
        {
            aggregator = eventAggregator;
            ConfigParameters = new CommunicationConfigParameters();
            OpenDrawerCommand = new DelegateCommand(() =>
            {
                aggregator.GetEvent<DrawerControlEvent>().Publish(new DrawerControlEventArgs
                {
                    IsOpen = true,
                    ViewName = nameof(CommunicationConfigView),
                    ConfigPayload=ConfigParameters
                });
            });
            aggregator.GetEvent<DrawerControlEvent>().Subscribe(Args =>
            {
                if(!Args.IsOpen&&Args.ConfigPayload!=null)
                    ConfigParameters=Args.ConfigPayload;
            });
        }
        #region 事件聚合器引用
        private readonly IEventAggregator aggregator;
        #endregion
        #region 右侧边栏发布命令
        public DelegateCommand OpenDrawerCommand { get; set; }
        #endregion
        /// <summary>
        /// 通信配置参数
        /// </summary>
        private CommunicationConfigParameters _ConfigParameters;

        public CommunicationConfigParameters ConfigParameters
        {
            get { return _ConfigParameters; }
            set { SetProperty(ref _ConfigParameters, value); }
        }
    }
}
