using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Enums
{
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
}
