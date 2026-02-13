using Industrial_Monitor.Core.Events;
using Industrial_Monitor.Core.Models;
using Industrial_Monitor.Core.Services;
using MaterialDesignThemes.Wpf.Converters.Internal;
using NModbus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    internal class ModbusDataViewModel:BindableBase
    {
        //事件聚合器引用
         private readonly IEventAggregator _aggregator;
        //引用IModbusMasterService服务
        private readonly IModbusMasterService _masterService;
        //数据集合,可用于绑定
        private ObservableCollection<ModbusDataItem> _ModbusDataItems;
        public ObservableCollection<ModbusDataItem> ModbusDataItems
        {
            get { return _ModbusDataItems; }
            set { SetProperty(ref _ModbusDataItems, value); }
        }
        public ModbusDataViewModel(IEventAggregator aggregator,IModbusMasterService masterService)
        {
            _aggregator=aggregator;
            _masterService=masterService;
            ModbusDataItems=new ObservableCollection<ModbusDataItem>();
            _aggregator.GetEvent<ModbusDataItemGeneratedEvent>().Subscribe(OnRequestConfigUpdated);
        }
        private void OnRequestConfigUpdated(ObservableCollection<ModbusDataItem> collection)
        {
            ModbusDataItems=collection;
            throw new NotImplementedException();
        }
    }
}
