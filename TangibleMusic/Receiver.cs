using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;


public class Receiver : MonoBehaviour
{

    public string portName = "COM3";
    public int baudRate = 9600;

    private SerialPort serial;
    private bool isRunning = false;

    public int sensorVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 3000;
        serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (serial.IsOpen)
        {
            string str = null;
            try
            {
                str = serial.ReadLine();
                if (Int32.TryParse(str, out int val))
                {
                    sensorVal = val;
                }
            }
            catch (System.Exception e)
            {

            }
            Debug.Log(sensorVal.ToString());
        }
    }

    void OnDestroy()
    {
        serial.Close();
        serial.Dispose();
    }
}
