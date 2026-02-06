using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.ViewModels
{
    class RequestConfigViewModel : BindableBase
    {
        public RequestConfigViewModel()
        {

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
        public enum ModbusFunctionCode : byte
        {
            [Description("0x01 - 读取线圈")]
            ReadCoils = 0x01,
            [Description("0x02 - 读取离散输入")]
            ReadDiscreteInputs = 0x02,
            [Description("0x03 - 读取保持寄存器")]
            ReadHoldingRegisters = 0x03,
            [Description("0x04 - 读取输入寄存器")]
            ReadInputRegisters = 0x04,
            [Description("0x05 - 写入单个线圈")]
            WriteSingleCoil = 0x05,
            [Description("0x06 - 写入单个寄存器")]
            WriteSingleRegister = 0x06,
            [Description("0x0F - 写入多个线圈")]
            WriteMultipleCoils = 0x0F,
            [Description("0x10 - 写入多个寄存器")]
            WriteMultipleRegister = 0x10
        }
        private ModbusFunctionCode _functionCodeSelected = ModbusFunctionCode.ReadHoldingRegisters;

        public ModbusFunctionCode FunctionCodeSelected
        {
            get { return _functionCodeSelected; }
            set { SetProperty(ref _functionCodeSelected, value); }
        }
        public IEnumerable<KeyValuePair<ModbusFunctionCode, string>> AvailableFunctionCodes =>
        Enum.GetValues<ModbusFunctionCode>()
            .Select(code => new KeyValuePair<ModbusFunctionCode, string>(code, GetEnumDescription(code)));
        private string GetEnumDescription(ModbusFunctionCode value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}

