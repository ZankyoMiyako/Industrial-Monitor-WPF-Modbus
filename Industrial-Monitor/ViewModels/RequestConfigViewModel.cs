using Industrial_Monitor.Core.Enums;
using Industrial_Monitor.Core.Events;
using Industrial_Monitor.Core.Models;
using Industrial_Monitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    class RequestConfigViewModel : BindableBase
    {
        //事件聚合器引用
        private readonly IEventAggregator _aggregator;
        //IRequestConfigService服务引用
        private readonly IRequestConfigService _requestService;
        //IModbusMasterService服务引用
        private readonly IModbusMasterService _modbusService;
        //生成面板命令
        public DelegateCommand ApplyCommand {  get; set; }
        //暂存寄存器列表
        public ObservableCollection<ModbusDataItem> ModbusDataItems { get; set; } = new ObservableCollection<ModbusDataItem>();

        public RequestConfigViewModel(IRequestConfigService requsetService, IModbusMasterService masterService,IEventAggregator aggregator)
        {
            _aggregator=aggregator;
            _requestService = requsetService;
            _modbusService = masterService;
            ApplyCommand = new DelegateCommand(ApplyConfig);
        }
        //应用配置命令
        public void ApplyConfig()
        {
            if (_modbusService.IsConnected)
            {
                _requestService.SlaveId = SlaveId;
                _requestService.FunctionCode = (byte)FunctionCodeSelected;
                _requestService.StartAddress = StartAddress;
                _requestService.Count = Count;

                ModbusDataItems.Clear();
                var items = _requestService.GetModbusDataItems();

                foreach (var item in items)
                {
                    ModbusDataItems.Add(item);
                }
                _aggregator.GetEvent<ModbusDataItemGeneratedEvent>().Publish(ModbusDataItems);
            }
            else
            {
                Debug.WriteLine("尚未连接主站");
            }
        }

        /// <summary>
        /// Modbus从站地址,默认为1
        /// </summary>
        private byte _SlaveId = 1;

        public byte SlaveId
        {
            get { return _SlaveId; }
            set
            {
                SetProperty(ref _SlaveId, value);
            }
        }
        /// <summary>
        /// 起始地址,默认为0
        /// </summary>
        private int _StartAddress = 0;

        public int StartAddress
        {
            get { return _StartAddress; }
            set { SetProperty(ref _StartAddress, value); }
        }
        /// <summary>
        /// 寄存器数量,默认为10
        /// </summary>
        private int _Count = 10;

        public int Count
        {
            get { return _Count = 10; }
            set { SetProperty(ref _Count, value); }
        }
        /// <summary>
        /// 功能码,默认为03
        /// </summary>

        private ModbusFunctionCode _functionCodeSelected = ModbusFunctionCode.ReadHoldingRegisters;

        public ModbusFunctionCode FunctionCodeSelected
        {
            get { return _functionCodeSelected; }
            set { SetProperty(ref _functionCodeSelected, value); }
        }
        /// <summary>
        /// 生成<枚举成员-成员描述>的键值对
        /// </summary>
        public IEnumerable<KeyValuePair<ModbusFunctionCode, string>> AvailableFunctionCodes =>
        Enum.GetValues<ModbusFunctionCode>()
            .Select(code => new KeyValuePair<ModbusFunctionCode, string>(code, GetEnumDescription(code)));
        /// <summary>
        /// 获取枚举成员的描述属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetEnumDescription(ModbusFunctionCode value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}

