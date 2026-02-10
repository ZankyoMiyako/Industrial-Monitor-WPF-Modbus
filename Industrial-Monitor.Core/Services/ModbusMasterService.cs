using Industrial_Monitor.Core.Models;
using NModbus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Monitor.Core.Services
{
    public class ModbusMasterService : IModbusMasterService
    {
        //tcp客户端
        private TcpClient _tcpClient;
        //主站
        private IModbusMaster _master;
        //工厂
        private IModbusFactory _factory;
        //重连次数
        private int _currentReconnectAttempts;
        //连接参数
        public ConnectionConfigParameters ConnectionParameters { get; set; }
        //连接状态
        public bool IsConnected { get; private set; }
        public ModbusMasterService()
        {
            _factory=new ModbusFactory();
        }
        //连接方法
        public bool Connect(ConnectionConfigParameters Parameters)
        {
            ConnectionParameters = Parameters;
            if (IsConnected)
                return true;
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(Parameters.IpAddress, Parameters.Port);
                _master = _factory.CreateMaster(_tcpClient);
                IsConnected = true;
                _currentReconnectAttempts = 0;
                Debug.WriteLine("连接成功");
                return true;
            }
            catch(Exception ex)
            {
                IsConnected = false;
                _master = null;
                _tcpClient?.Close();
                _tcpClient = null;
                Debug.WriteLine("连接失败");
                return false;
            }
        }
    }
}
