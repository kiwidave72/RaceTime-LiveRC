using System;
using System.Diagnostics;
using System.IO.Ports;
using RaceTime.Library.Controller.Scoreboard;

namespace RaceTime.Library.Scoreboard
{
    public class SerialScoreboard : IScoreboard
    {
        private bool IsConnected = false;
        private SerialPort _serialPort;

        public void WriteOutput(string line)
        {
            if (IsConnected == false)
                ConnectPort();

            if(_serialPort!=null)
                _serialPort.WriteLine(line);
        }

        private void ConnectPort()
        {
            Interval = 1000;
            _serialPort = new SerialPort();
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                Console.WriteLine("   {0}", s);
            }
            _serialPort.PortName = "COM1";
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            IsConnected = true;
            _serialPort.Open();
        }

        public long Interval { get; set; }
    }
}