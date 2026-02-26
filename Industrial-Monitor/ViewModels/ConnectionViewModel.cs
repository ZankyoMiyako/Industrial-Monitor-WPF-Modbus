using Industrial_Monitor.Core.Events;
using Industrial_Monitor.Core.Models;
using Industrial_Monitor.Core.Services;
using Industrial_Monitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    internal class ConnectionViewModel : BindableBase
    {
        public ConnectionViewModel(IEventAggregator eventAggregator,IModbusMasterService masterService)
        {
            _aggregator = eventAggregator;
            _modbusMaster = masterService;
            ConnectionParameters = new ConnectionConfigParameters();
            OpenDrawerCommand = new DelegateCommand(() =>
            {
                _aggregator.GetEvent<DrawerControlEvent>().Publish(new DrawerControlEventArgs
                {
                    IsOpen = true,
                    ViewName = nameof(ConnectionConfigView),
                    ConfigPayload= ConnectionParameters
                });
            });
            _aggregator.GetEvent<DrawerControlEvent>().Subscribe(Args =>
            {
                if(!Args.IsOpen&&Args.ConfigPayload!=null)
                    ConnectionParameters = Args.ConfigPayload;
            });
            ConnectCommand = new DelegateCommand(OnConnect);
        }
        //连接命令
        private void OnConnect()
        {
            if (_modbusMaster.IsConnected == false)
            {
               IsConnected= _modbusMaster.Connect(ConnectionParameters);
            }
        }

        //事件聚合器引用
        private readonly IEventAggregator _aggregator;
        //modbus主站服务引用
        private readonly IModbusMasterService _modbusMaster;
        //右侧边栏发布命令
        public DelegateCommand OpenDrawerCommand { get; set; }
        //连接绑定命令
        public DelegateCommand ConnectCommand { get; set; }
        //连接属性
        private bool _IsConnected;
        public bool IsConnected
        {
            get { return _IsConnected; }
            set { SetProperty(ref _IsConnected, value); }
        }
        //通信配置参数
        private ConnectionConfigParameters _ConnectionParameters;

        public ConnectionConfigParameters ConnectionParameters
        {
            get { return _ConnectionParameters; }
            set { SetProperty(ref _ConnectionParameters, value); }
        }
    }
}
