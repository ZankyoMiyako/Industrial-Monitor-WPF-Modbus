using Industrial_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Services
{
    public interface IModbusMasterService
    {
        //连接参数
        ConnectionConfigParameters ConnectionParameters { get; set; }
        //连接状态
        bool IsConnected { get;}
        //连接方法
        bool Connect(ConnectionConfigParameters Parameters);
    }
}
