
using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System;

public class TestManubrio : MonoBehaviour {

    //Setup parameters to connect to Arduino
    public static SerialPort sp;
    public static string strIn;
    public UIInput portInput;
    public UIInput signal;
    public string port = "COM4";
    public UILabel messageLabel;

    string message = "";

    public UILabel output;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        output.text = "" + Display.displays.Length;
        Display.displays[0].SetParams(1366, 768, 0, 0);
        Display.displays[1].SetParams(1920, 1080, 0, 0);
        Display.displays[2].SetParams(1920, 1080, 0, 0);
        Display.displays[3].SetParams(1920, 1080, 0, 0);
        Display.displays[4].SetParams(800, 480, 0, 0); //camara posterior
        Display.displays[5].SetParams(1024, 600, 0, 0); //odometro
        /*port = PlayerPrefs.GetString("port", "COM4");
        portInput.value = port;
        sp = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
        OpenConnection();*/
    }
    /*
    public void changePort()
    {
        port = portInput.value;
        PlayerPrefs.SetString("port", port);
        Application.Quit();
    }

    public void sendSignal()
    {
        message += "Signal sended\n";
        sp.Write(signal.value);
    }

    void Update()
    {
        if (sp.IsOpen)
        {
            //Read incoming data
            strIn = sp.ReadLine();
            print(strIn);
            //You can also send data like this
            //sp.Write("1");
            output.text = strIn;
        }
        messageLabel.text = message;
    }

    //Function connecting to Arduino
    public void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                message += "Closing port, because it was already open!\n";
            }
            else
            {
                try
                {
                    sp.Open();  // opens the connection
                    sp.ReadTimeout = 50;  // sets the timeout value before reporting error
                    message += "Port Opened!\n";
                }
                catch (Exception e)
                {
                    message += "Error: " + e.Message + "\n";
                }
                if (sp.IsOpen)
                {
                    message += "Port Opened!\n";
                }
                else
                {
                    message += "Port not Opened!\n";
                }
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                message += ("Port is already open\n");
            }
            else
            {
                message += ("Port == null\n");
            }
        }
    }

    void OnApplicationQuit()
    {
        sp.Close();
    }
    */
}
