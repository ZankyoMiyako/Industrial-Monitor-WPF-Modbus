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
    class ConnectionConfigViewModel : BindableBase
    {
        public ConnectionConfigViewModel(IEventAggregator eventAggregator)
        {
            aggregator = eventAggregator;
            ConnectionParameters = new ConnectionConfigParameters();
            aggregator.GetEvent<DrawerControlEvent>().Subscribe(Args =>
            {
                if (Args.IsOpen && Args.ConfigPayload != null)
                {
                    ConnectionParameters = Args.ConfigPayload;
                }
            },ThreadOption.UIThread);
            CloseDrawerCommand = new DelegateCommand(() => aggregator.GetEvent<DrawerControlEvent>().Publish(new DrawerControlEventArgs
            {
                IsOpen = false
            }));
            SaveCommand = new DelegateCommand(() => aggregator.GetEvent<DrawerControlEvent>().Publish(new DrawerControlEventArgs
            {
                IsOpen = false,
                ConfigPayload = ConnectionParameters
            }));
        }
        //事件聚合器引用
        private readonly IEventAggregator aggregator;
        //取消按钮命令
        public DelegateCommand CloseDrawerCommand { get; set; }
        //确定按钮命令
        public DelegateCommand SaveCommand { get; set; }
        //通信配置参数
        private ConnectionConfigParameters _ConnectionParameters;

        public ConnectionConfigParameters ConnectionParameters
        {
            get { return _ConnectionParameters; }
            set { SetProperty(ref _ConnectionParameters, value); }
        }

    }
}
