using Industrial_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Events
{
    public class ModbusDataItemGeneratedEvent : PubSubEvent<ObservableCollection<ModbusDataItem>> { }
}
