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
    class CommunicationConfigViewModel : BindableBase
    {
        public CommunicationConfigViewModel(IEventAggregator eventAggregator)
        {
            aggregator = eventAggregator;
            ConfigParameters=new CommunicationConfigParameters();
            aggregator.GetEvent<DrawerControlEvent>().Subscribe(Args =>
            {
                if (Args.IsOpen && Args.ConfigPayload != null)
                {
                    ConfigParameters = Args.ConfigPayload;
                }
            },ThreadOption.UIThread);
            CloseDrawerCommand = new DelegateCommand(() => aggregator.GetEvent<DrawerControlEvent>().Publish(new DrawerControlEventArgs
            {
                IsOpen = false
            }));
            SaveCommand = new DelegateCommand(() => aggregator.GetEvent<DrawerControlEvent>().Publish(new DrawerControlEventArgs
            {
                IsOpen = false,
                ConfigPayload = ConfigParameters
            }));
        }
        #region 事件聚合器引用
        private readonly IEventAggregator aggregator;
        #endregion
        #region 取消按钮命令
        public DelegateCommand CloseDrawerCommand { get; set; }
        #endregion
        /// <summary>
        /// 确定按钮命令
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }
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
