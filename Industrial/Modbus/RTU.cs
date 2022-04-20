using Communication;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Modbus
{
    public class RTU
    {
        private static RTU _instance;
        private static SerialInfo _serialInfo;

        SerialPort _serialPort;

        private RTU(SerialInfo serialInfo)
        {
            _serialInfo = serialInfo;
        }

        public static RTU GetInstance(SerialInfo serialInfo)
        {
            lock ("rtu")//上锁
            {
                if (_instance == null)
                {
                    _instance = new RTU(serialInfo);
                }
                return _instance;
            }
        }

        public bool Connection()
        {
            try
            {
                if (_serialPort.IsOpen) { _serialPort.Close(); }

                _serialPort.PortName = _serialPort.PortName;
                _serialPort.BaudRate = _serialPort.BaudRate;
                _serialPort.Parity = _serialPort.Parity;
                _serialPort.DataBits = _serialPort.DataBits;
                _serialPort.StopBits = _serialPort.StopBits;

                _serialPort.ReceivedBytesThreshold = 1;
                _serialPort.DataReceived += _serialPort_DataReceived;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
