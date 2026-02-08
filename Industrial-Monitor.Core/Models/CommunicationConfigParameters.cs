using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Models
{
    /// <summary>
    /// Tcp的通信配置参数
    /// </summary>
    public class CommunicationConfigParameters
    {
        /// <summary>
        /// ip地址
        /// </summary>
        public string IpAddress { get; set; } = "127.0.0.1";
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 502;
        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; } = 2000;
        /// <summary>
        /// 尝试重连次数
        /// </summary>
        public int RetryCount { get; set; } = 3;
    }
}
