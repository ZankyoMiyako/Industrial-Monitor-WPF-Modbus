using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Models
{
    public class ModbusDataItem : BindableBase
    {
        //索引项
        public int Index { get; set; }
        //地址
        public int Address { get; set; }
        //名称
        public string Name { get; set; }
        //数值
        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }

    }
}
