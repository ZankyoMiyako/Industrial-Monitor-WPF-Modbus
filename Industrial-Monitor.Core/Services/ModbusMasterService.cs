using Industrial_Monitor.Core.Enums;
using Industrial_Monitor.Core.Models;
using NModbus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _factory = new ModbusFactory();
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
            catch (Exception ex)
            {
                IsConnected = false;
                _master = null;
                _tcpClient?.Close();
                _tcpClient = null;
                Debug.WriteLine("连接失败");
                return false;
            }
        }
        //断连
        public void Disconnect()
        {
            if (!IsConnected)
                return;
            try
            {
                _tcpClient.Close();
                _tcpClient = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"断联异常:{ex.Message}");
            }
        }
        //取消令牌
        private CancellationTokenSource _cts;
        //从站号
        private byte _slaveId;
        //功能码
        private byte _functionCode;
        //Modbus数据项
        private ObservableCollection<ModbusDataItem> _modbusItems;
        //轮询间隔
        private int _intervalMs;
        //值更新的回调
        private Action<ModbusDataItem, string> _updateCallback;
        //
        private ConnectionConfigParameters _connectionParameters;
        //启动轮询
        public void StartPolling(byte SlaveId, byte FunctionCode, ObservableCollection<ModbusDataItem> modbusDataItems, int intervalMs, Action<ModbusDataItem, string> updateCallback)
        {
            _cts = new CancellationTokenSource();
            _slaveId = SlaveId;
            _functionCode = FunctionCode;
            _modbusItems = modbusDataItems;
            _intervalMs = intervalMs;
            _updateCallback = updateCallback;
            Task.Run(() => Polling(_cts.Token));
        }
        //轮询方法
        private async Task Polling(CancellationToken Token)
        {
            while (!Token.IsCancellationRequested)
            {
                //未连接,进行自动重连
                if (!IsConnected || _master == null)
                {
                    _currentReconnectAttempts++;
                    //重连次数耗尽,放弃
                    if (_currentReconnectAttempts > (_connectionParameters?.RetryCount ?? 3))
                    {
                        StopPolling();
                        return;
                    }
                    bool connect = Connect(ConnectionParameters);
                    //重连失败
                    if (!connect)
                    {
                        await Task.Delay(_connectionParameters?.Timeout ?? 2000, Token);
                        continue;
                    }
                    //重连成功
                    else
                    {
                        _currentReconnectAttempts = 0;
                    }
                }
                //无数据要读取,等待间隔
                if (_modbusItems == null || _modbusItems.Count == 0)
                {
                    await Task.Delay(_intervalMs, Token);
                    continue;
                }
                //读取数据
                try
                {
                    var startAddress = (ushort)_modbusItems.Min(x => x.Address);
                    var endAddress = (ushort)_modbusItems.Max(x => x.Address);
                    var count = (ushort)(endAddress - startAddress + 1);
                    switch (_functionCode)
                    {
                        case (byte)ModbusFunctionCode.ReadCoils:
                            bool[] coils = _master.ReadCoils(_slaveId, startAddress, count);
                            foreach (var item in _modbusItems)
                            {
                                int address = item.Address - startAddress;
                                _updateCallback.Invoke(item, (address >= 0 && address < coils.Length) ? (coils[address] ? "1" : "0") : "越界");
                            }
                            break;
                        case (byte)ModbusFunctionCode.ReadDiscreteInputs:
                            bool[] inputs = _master.ReadInputs(_slaveId, startAddress, count);
                            foreach (var item in _modbusItems)
                            {
                                int address = item.Address - startAddress;
                                _updateCallback.Invoke(item, (address >= 0 && address < inputs.Length) ? (inputs[address] ? "1" : "0") : "越界");
                            }
                            break;
                        case (byte)ModbusFunctionCode.ReadHoldingRegisters:
                            ushort[] holding = _master.ReadHoldingRegisters(_slaveId, startAddress, count);
                            foreach (var item in _modbusItems)
                            {
                                int address = item.Address - startAddress;
                                _updateCallback.Invoke(item, (address >= 0 && address < holding.Length) ? holding[address].ToString() : "越界");
                            }
                            break;
                        case (byte)ModbusFunctionCode.ReadInputRegisters:
                            ushort[] inputRegisters = _master.ReadInputRegisters(_slaveId, startAddress, count);
                            foreach (var item in _modbusItems)
                            {
                                int address = item.Address - startAddress;
                                _updateCallback.Invoke(item, (address >= 0 && address < inputRegisters.Length) ? inputRegisters[address].ToString() : "越界");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    foreach (var item in _modbusItems)
                    {
                        _updateCallback.Invoke(item, $"Err:{ex.Message}");
                    }
                    Disconnect();
                }
                await Task.Delay(_intervalMs, Token);
            }
        }
        /// <summary>
        /// 停止轮询
        /// </summary>
        public void StopPolling()
        {
            _cts?.Cancel();
        }
    }
}
