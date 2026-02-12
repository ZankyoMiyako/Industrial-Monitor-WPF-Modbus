using Industrial_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Services
{
    public interface IRequestConfigService
    {
        //从站地址
        byte SlaveId { get; set; }
        //功能码
        byte FunctionCode { get; set; }
        //起始地址
        int StartAddress { get; set; }
        //数量
        int Count { get; set; }
        //数据项
        List<ModbusDataItem> ModbusDataItems { get; set; }
        //获取数据的方法
        List<ModbusDataItem> GetModbusDataItems();
    }
}
