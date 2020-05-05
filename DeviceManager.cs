using UnityEngine;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;

public class DeviceManager : MonoBehaviour
{
    // Arduino variables
    static public SerialPort serial1;
    static public string ArduinoCom;
    private bool SerialIsOpen = false;

    // Tactor variables
    Thread TactorThread;
    private Process ProcessTactors = new Process();

    // UDP Stuff
    private const int listenPort = 8888;
    public static Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    public static IPAddress send_to_address = IPAddress.Parse("127.0.0.1");
    public static IPEndPoint sending_end_point = new IPEndPoint(send_to_address, listenPort);
	
    // Use this for initialization
    void Start()
    {
        init();
        StartArduino();
    }

    private void init()
    {
        TactorThread = new Thread(
        new ThreadStart(RunTactor));

        TactorThread.IsBackground = true;
        TactorThread.Start();
    }

    private void RunTactor()
    {
        ProcessTactors.StartInfo.FileName = Directory.GetCurrentDirectory() + "/executables/MultiplePerception";
        UnityEngine.Debug.Log(ProcessTactors.StartInfo.FileName);
        ProcessTactors.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        ProcessTactors.Start();
        ProcessTactors.WaitForExit();
    }

    private void StartArduino()
    {
        string[] ComPorts = System.IO.File.ReadAllLines(@"Executables\ComPorts.txt"); // FILE MAY NEED TO BE CHANGED BASED ON DEVICE
        ArduinoCom = ComPorts[1];

        if (!SerialIsOpen)
        {
            serial1 = new SerialPort();
            serial1.PortName = ArduinoCom; 
            serial1.BaudRate = 9600;
            serial1.DataBits = 8;
            serial1.StopBits = StopBits.One;
            try
            {
                serial1.Open();
                SerialIsOpen = true;
                UnityEngine.Debug.Log("Serial port is open.");
            }
            catch (IOException)
            {
				UnityEngine.Debug.Log(ArduinoCom + " did not open arduino");
				// If the arduino is not plugged in, nothing will happen
                // However, if a person then tries to use the arduino actuators, the game will crash
            }
        }
    }

    public void OnApplicationQuit()
    {
        if (!ProcessTactors.HasExited)
        {
            ProcessTactors.CloseMainWindow();
            ProcessTactors.Close();
        }
        // close serial port on application close
        if (SerialIsOpen)
        {
            serial1.Close();
        }
    }
}
