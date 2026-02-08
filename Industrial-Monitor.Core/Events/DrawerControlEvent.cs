using Industrial_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Events
{
    public class DrawerControlEvent : PubSubEvent<DrawerControlEventArgs> { }
    
    public class DrawerControlEventArgs
    {
        public bool IsOpen { get; set; }
        public string ViewName {  get; set; }
        public CommunicationConfigParameters ConfigPayload { get; set; }
    }
}
