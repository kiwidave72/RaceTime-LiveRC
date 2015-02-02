using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq.Expressions;
using RaceTime.Library.Controller.Scoreboard;

namespace RaceTime.Library.Scoreboard
{
    public class SerialScoreboard : IScoreboard
    {
        private bool IsConnected = false;
        private SerialPort _serialPort;

        private long _interval = 1000;

        public SerialScoreboard()
        {
            Errors = new List<Exception>();
            SerialPortNames = SerialPort.GetPortNames();

        }

        public long Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }


        public bool IsSerialPortOpen { get; private set; }
        public string[] SerialPortNames { get; set; }
        public string PortName { get; set; }
        public string FriendlyOutputText { get; set; }
        public string SerialOutputText { get; set; }

        public List<Exception> Errors { get; private set; }

        public void ClearDisplay()
        {
            WriteOutput("[0:0:0:0:0]");
        }

        public void WriteRaceInfor(int round, int heat, string elapsedTime,string name)
        {
            var serialOutputText = string.Format("[{0}:{1}:{2}:0:0]", round, heat, elapsedTime);

            FriendlyOutputText = string.Format("Round:{0} Heat:{1} Time:{2} Name:{3}", round, heat, elapsedTime, name); ;

            WriteOutput(serialOutputText);
        }

        private void WriteOutput(string line)
        {
            SerialOutputText = line;


            Debug.WriteLine(SerialOutputText);

            if (IsConnected == false || _serialPort.IsOpen==false)
                ConnectPort();

            if (_serialPort != null && _serialPort.IsOpen==true)
                _serialPort.WriteLine(line);
        }

        private void ConnectPort()
        {

            try
            {
                _serialPort = new SerialPort();
                IsSerialPortOpen = _serialPort.IsOpen;


                foreach (string s in SerialPort.GetPortNames())
                {
                    Debug.WriteLine("   {0}", s);
                }



                _serialPort.PortName = PortName; //SerialPort.GetPortNames()[1];
                _serialPort.BaudRate = 9600;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Handshake = Handshake.None;
                _serialPort.ReadTimeout = 500;
                _serialPort.WriteTimeout = 500;

                IsConnected = true;
                _serialPort.Open();
                Errors.Clear();

            }
            catch (IndexOutOfRangeException ex)
            {
                GeneralErrorMessage();
            }
            catch (IOException ex)
            {
                GeneralErrorMessage();

            }
            catch (Exception ex)
            {
                Errors.Add(ex);
            }

        }

        private void GeneralErrorMessage()
        {
            Errors.Add(
                new Exception(DateTime.Now.ToShortTimeString() + " -> The port '" + PortName +
                              "' does not exist, or is unable to connect. Try selecting another port."));
        }
    }
}