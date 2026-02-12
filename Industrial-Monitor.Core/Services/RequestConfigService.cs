using Industrial_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Services
{
    public class RequestConfigService : IRequestConfigService
    {
        public byte SlaveId { get; set; } = 1;
        public byte FunctionCode { get; set; }
        public int StartAddress { get; set; }
        public int Count { get; set; }
        public List<ModbusDataItem> ModbusDataItems { get; set; }

        public List<ModbusDataItem> GetModbusDataItems()
        {
            var items = new List<ModbusDataItem>();
            for (int i = 0; i < Count; i++)
            {
                items.Add(new ModbusDataItem()
                {
                    Index = i + 1,
                    Address = i,
                    Name = $"数据项{i + 1}",
                    Value = ""
                });
            }
            ModbusDataItems=items;
            return items;
        }
    }
}
