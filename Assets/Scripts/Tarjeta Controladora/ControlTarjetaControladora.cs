using UnityEngine;
using System.Collections;
//using PoKeysDevice_DLL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;



public class PoKeysNativeDevice
{
#if !UNITY_EDITOR
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static int PK_EnumerateUSBDevices();

    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static long PK_ConnectToDevice(Int32 deviceIndex);

    // Connect to a PoKeys device with the specific serial number. Returns pointer to a newly created PoKeys device structure. Returns NULL if the connection is not successfull
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static long PK_ConnectToDeviceWSerial(UInt32 serialNumber, UInt32 checkForNetworkDevicesAndTimeout);

    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    // Same as above, but uses UDP for connection
    public extern static long PK_ConnectToDeviceWSerial_UDP(UInt32 serialNumber, UInt32 checkForNetworkDevicesAndTimeout);


    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static void PK_DisconnectDevice(long devPtr);

    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    // Set single digital output
    public extern static Int32 PK_DigitalIOSetSingle(long device, byte pinID, byte pinValue);

    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    // Get single digital input value
    public extern static Int32 PK_DigitalIOGetSingle(long device, byte pinID, ref byte pinValue);

    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static void PK_SL_SetPinFunction(long device, byte pin, byte function);
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static byte PK_SL_GetPinFunction(long device, byte pin);
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static void PK_SL_DigitalOutputSet(long device, byte pin, byte value);
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static byte PK_SL_DigitalInputGet(long device, byte pin);
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static UInt32 PK_SL_AnalogInputGet(long device, byte pin);

    // Retrieve pin configuration from the devic
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_PinConfigurationGet(long device);
    // Send pin configuration to device
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_PinConfigurationSet(long device);

    // Retrieve encoder configuration from the device
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_EncoderConfigurationGet(long device);
    // Send encoder configuration to device
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_EncoderConfigurationSet(long device);
    // Retrieve encoder values from device
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_EncoderValuesGet(long device);
    // Send encoder values to device
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_EncoderValuesSet(long device);

    // Set digital outputs values
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_DigitalIOSet(long device);
    // Get digital inputs values
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_DigitalIOGet(long device);


    // Get analog input values
    [DllImport(@"Assets\Scripts\PoKeyslib.dll")]
    public extern static Int32 PK_AnalogIOGet(long device);


    public class cPinFunctions
    {
        public long devPtr;

        public byte this[byte index]
        {
            get
            {
                if (devPtr == 0) return 0;
                return PK_SL_GetPinFunction(devPtr, index);
            }

            set
            {
                if (devPtr == 0) return;
                PK_SL_SetPinFunction(devPtr, index, value);
            }
        }
    }

    public class cPinStates
    {
        public long devPtr;

        public byte this[byte index]
        {
            get
            {
                if (devPtr == 0) return 0;
                return PK_SL_DigitalInputGet(devPtr, index);
            }

            set
            {
                if (devPtr == 0) return;
                PK_SL_DigitalOutputSet(devPtr, index, value);
            }
        }
    }

    private long devPtr = 0;

    public int EnumerateDevices()
    {
        return PK_EnumerateUSBDevices();
    }

    void UpdateDeviceReference(long devPtr)
    {
        PinFunctions.devPtr = devPtr;
        PinStates.devPtr = devPtr;
    }

    public void Disconnect()
    {
        if (devPtr > 0)
        {
            PK_DisconnectDevice(devPtr);
        }
        devPtr = 0;
        UpdateDeviceReference(devPtr);
    }

    public bool ConnectToDevice_Index(int index)
    {
        Disconnect();

        devPtr = PK_ConnectToDevice(index);
        UpdateDeviceReference(devPtr);
        return (devPtr > 0);
    }

    public bool ConnectToDevice_Serial(uint serialNumber)
    {
        Disconnect();

        devPtr = PK_ConnectToDeviceWSerial_UDP(serialNumber, 1000);
        UpdateDeviceReference(devPtr);
        return (devPtr > 0);
    }

    public void LoadIOConfig()
    {
        if (devPtr == 0) return;

        PK_PinConfigurationGet(devPtr);
    }

    public void StoreIOConfig()
    {
        if (devPtr == 0) return;

        PK_PinConfigurationSet(devPtr);
    }

    public cPinFunctions PinFunctions = new cPinFunctions();
    public cPinStates PinStates = new cPinStates();

    public void ReadIOStates()
    {
        if (devPtr == 0) return;

        PK_DigitalIOGet(devPtr);
    }

    public void WriteIOStates()
    {
        if (devPtr == 0) return;

        PK_DigitalIOSet(devPtr);
    }

    public void ReadAnalogInputs()
    {
        if (devPtr == 0) return;

        PK_AnalogIOGet(devPtr);
    }

    public double GetAnalogInput(byte inputPin)
    {
        return (double)PK_SL_AnalogInputGet(devPtr, inputPin) / 4095.0;
    }

#endif
}

public class ControlTarjetaControladora : MonoBehaviour {

    PoKeysNativeDevice dev;
    //PoKeysDevice device = null;
    double analogInputVoltage;
    int analogValue;

    public enum pinFunctionsEnum
    {
        pinFunctionInactive = 0,
        pinFunctionDigitalInput = 2,
        pinFunctionDigitalOutput = 4,
        pinFunctionAnalogInput = 8,
        pinFunctionAnalogOutput = 16,
        pinInvert = 128
    }

    public string mensaje = "";

    public void Start()
    {
#if !UNITY_EDITOR
        dev = new PoKeysNativeDevice();
        dev.ConnectToDevice_Serial(33282);
        //mensaje += ("Devices: " + dev.EnumerateDevices() + "\n");
        //mensaje += ("Connected: " + dev.ConnectToDevice_Serial(33282) + "\n");

        // Device configuration

        // Retrieve IO configuration from device first, update it as needed
        // 4 output?
        // 2 input
        // 128 inverted input
        // 8 analog input
        dev.LoadIOConfig();        

        //Luces
        for(int i = 0; i <= 19; i++)
            dev.PinFunctions[(byte)i] = 4;

        //Selectores
        for (int i = 20; i <= 29; i++)
            dev.PinFunctions[(byte)i] = 2;

        //Velocimetro

        //Decena
        for (int i = 48; i <= 54; i++)
            dev.PinFunctions[(byte)i] = 4;

        //Unidad
        for (int i = 37; i <= 43; i++)
            dev.PinFunctions[(byte)i] = 4;

        //Punto
        dev.PinFunctions[(byte)47] = 4;

        //Unidad
        for (int i = 30; i <= 36; i++)
            dev.PinFunctions[(byte)i] = 4;

        //Pedales
        for (int i = 44; i <= 46; i++)
            dev.PinFunctions[(byte)i] = 8;

        // Send the IO configuration back to device
        dev.StoreIOConfig();

        //reset de todas las luces
        StartCoroutine(resetAll());

        // Update process
        /*bool state = false;
        for (int i = 0; i < 100; i++)
        {
            // Read all digital inputs
            dev.ReadIOStates();
            // Read all analog inputs
            dev.ReadAnalogInputs();

            // Display values (state of pin 4 and analog input 41)
            print("Inputs: " + dev.PinStates[3] + " - analog: " + dev.GetAnalogInput(40));

            // Set the output value
            dev.PinStates[0] = (byte)(state ? 1 : 0);
            
            // Update the output pins
            dev.WriteIOStates();

            state = !state;
            //System.Threading.Thread.Sleep(100);
        }
 */
#endif
    }

    IEnumerator resetAll()
    {
        yield return new WaitForSeconds(0.2f);
#if !UNITY_EDITOR
        resetLucesVelocimetro();
        for (int i = 0; i < 20; i++)
            LuzCircuito(i, true);
        dev.WriteIOStates();
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 20; i++)
            LuzCircuito(i, false);
        dev.WriteIOStates();
#endif
    }

    void OnDisable()
    {
#if !UNITY_EDITOR
        dev.Disconnect();
        dev = null;
#endif
    }

    void Update()
    {
#if !UNITY_EDITOR
        // Read all digital inputs
        dev.ReadIOStates();
        // Read all analog inputs
        dev.ReadAnalogInputs();
#endif
    }

    //Panel de Control Central

    public void IntermitenteIzquierda(bool encendido)
    {
        LuzCircuito(9, encendido);
    }
    public void IntermitenteDerecha(bool encendido)
    {
        LuzCircuito(10, encendido);
    }
    public void LuzEmergenciaBajaPresion(bool encendido)
    {
        LuzCircuito(11, encendido);
    }
    public void LuzEmergenciaAltaPresion(bool encendido)
    {
        LuzCircuito(12, encendido);
    }
    public void luzPresionDropbox(bool encendido)
    {
        LuzCircuito(13, encendido);
    }
    public void LuzFiltroTransmision(bool encendido)
    {
        LuzCircuito(14, encendido);
    }
    public void luzPresionUpbox(bool encendido)
    {
        LuzCircuito(15, encendido);
    }
    public void luzDetenerMotor(bool encendido)
    {
        LuzCircuito(16, encendido);
    }
    public void luzTemperaturaUpboxDropbox(bool encendido)
    {
        LuzCircuito(17, encendido);
    }
    public void luzMantenimientoMotor(bool encendido)
    {
        LuzCircuito(18, encendido);
    }
    public void LuzNivelBajoBombaLubricacion(bool encendido)
    {
        LuzCircuito(19, encendido);
    }

    public int leerSwitch(int pin1, int pin2)
    {
#if !UNITY_EDITOR
        bool a = false;
        bool b = false;
        a = (dev.PinStates[(byte)pin1] == 1);
        b = (dev.PinStates[(byte)pin2] == 1);
        if (a)
        {
            if (b)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            if (b)
            {
                return 0;
            }
               
        }
#endif
        //si no hay lectura
        return -1;
    }
    
    public int ignicion()
    {
        //dev.PinStates[3] + " - analog: " + dev.GetAnalogInput(40)
        return leerSwitch(20, 21);
    }

    public int ignicion1()
    {
#if !UNITY_EDITOR
        return dev.PinStates[20];
#endif
        //si no hay lectura
        return -1;
    }
    public int ignicion2()
    {
#if !UNITY_EDITOR
        return dev.PinStates[21];
#endif
        //si no hay lectura
        return -1;
    }

    public int ControlLucesDelanteras()
    {
        return leerSwitch(22, 23);
    }

    public int ControlLucesDelanteras1()
    {
#if !UNITY_EDITOR
        return dev.PinStates[22];
#endif
        //si no hay lectura
        return -1;
    }
    public int ControlLucesDelanteras2()
    {
#if !UNITY_EDITOR
        return dev.PinStates[23];
#endif
        //si no hay lectura
        return -1;
    }

    public int ControlLucesCarga()
    {
#if !UNITY_EDITOR
        return (dev.PinStates[24] == 0)?0:1;
#endif
        //si no hay lectura
        return -1;
    }
    
    public int ControlLucesTraseras()
    {
#if !UNITY_EDITOR
        return (dev.PinStates[25] == 0)?0:1;
#endif
        //si no hay lectura
        return -1;
    }
    public int controlManualMotor()
    {
#if !UNITY_EDITOR
        return (dev.PinStates[26] == 0)?0:1;
#endif
        //si no hay lectura
        return -1;
    }
    public int PruebaDeFrenos()
    {
        return leerSwitch(27, 28);
    }
    /*
    public int PruebaDeFrenos1()
    {
        return dev.PinStates[27];
    }
    public int PruebaDeFrenos2()
    {
        return dev.PinStates[28];
    }
    */

    public int BotonAccion()
    {
#if !UNITY_EDITOR
        return (dev.PinStates[29] == 0)?0:1;
#endif
        //si no hay lectura
        return Input.GetKey(KeyCode.I)?0:1;
    }
    
    //0 a 8
    public void LuzCircuito(int indice, bool encendido)
    {
#if !UNITY_EDITOR
        dev.PinStates[(byte)indice] = (byte)(encendido ? 0 : 1);

        // Update the output pins
        dev.WriteIOStates();
#endif
        print("luz " + indice);
    }
    

    public float Acelerador() //45 a 255
    {
#if !UNITY_EDITOR
        return Mathf.Clamp(((float)dev.GetAnalogInput(44) - 0.17f) / 0.83f, 0f, 1f);
#else
        return Input.GetAxis("Acelerador");
#endif
    }

    public float Retardador()
    {
#if !UNITY_EDITOR
        return Mathf.Clamp(((float)dev.GetAnalogInput(45) - 0.17f) / 0.83f, 0f, 1f);
#else
        return -Input.GetAxis("Retardador");
#endif
    }

    public float Freno()
    {
#if !UNITY_EDITOR
        return Mathf.Clamp(((float)dev.GetAnalogInput(46) - 0.17f) / 0.83f, 0f, 1f);
#else
        return Input.GetAxis("Freno");
#endif
    }

    //Apaga todas las luces del velocimetro
    public void resetLucesVelocimetro()
    {
#if !UNITY_EDITOR
        for (int i = 30; i <= 43; i++)
            dev.PinStates[(byte)i] = 1;
        for (int i = 48; i <= 54; i++)
            dev.PinStates[(byte)i] = 1;
        dev.WriteIOStates();
#endif
    }

    public void velocimetro(string _velocidad)
    {
        mensaje += "test velocimetro\n";

#if !UNITY_EDITOR
        int _decena, _unidad, _decimal;
        _decimal = int.Parse(_velocidad.ToString().Split(',')[1]);
        _unidad = int.Parse(_velocidad.ToString().Split(',')[0]) % 10;
        _decena = (int.Parse(_velocidad.ToString().Split(',')[0]) - _unidad) / 10;

        resetLucesVelocimetro();

        //Enciende leds

        //Decena
        if (_decena == 0 || _decena == 2 || _decena == 3 || _decena == 5 || _decena == 6 || _decena == 7 || _decena == 8 || _decena == 9) dev.PinStates[48] = 0;
        if (_decena == 0 || _decena == 1 || _decena == 2 || _decena == 3 || _decena == 4 || _decena == 7 || _decena == 8 || _decena == 9) dev.PinStates[49] = 0;
        if (_decena == 0 || _decena == 1 || _decena == 3 || _decena == 4 || _decena == 5 || _decena == 6 || _decena == 7 || _decena == 8 || _decena == 9) dev.PinStates[50] = 0;
        if (_decena == 0 || _decena == 2 || _decena == 3 || _decena == 5 || _decena == 6 || _decena == 8) dev.PinStates[51] = 0;
        if (_decena == 0 || _decena == 2 || _decena == 6 || _decena == 8) dev.PinStates[52] = 0;
        if (_decena == 0 || _decena == 4 || _decena == 5 || _decena == 6 || _decena == 8 || _decena == 9) dev.PinStates[53] = 0;
        if (_decena == 2 || _decena == 3 || _decena == 4 || _decena == 5 || _decena == 6 || _decena == 8 || _decena == 9) dev.PinStates[54] = 0;

        //Unidad
        if (_unidad == 0 || _unidad == 2 || _unidad == 3 || _unidad == 5 || _unidad == 6 || _unidad == 7 || _unidad == 8 || _unidad == 9) dev.PinStates[37] = 0;
        if (_unidad == 0 || _unidad == 1 || _unidad == 2 || _unidad == 3 || _unidad == 4 || _unidad == 7 || _unidad == 8 || _unidad == 9) dev.PinStates[38] = 0;
        if (_unidad == 0 || _unidad == 1 || _unidad == 3 || _unidad == 4 || _unidad == 5 || _unidad == 6 || _unidad == 7 || _unidad == 8 || _unidad == 9) dev.PinStates[39] = 0;
        if (_unidad == 0 || _unidad == 2 || _unidad == 3 || _unidad == 5 || _unidad == 6 || _unidad == 8) dev.PinStates[40] = 0;
        if (_unidad == 0 || _unidad == 2 || _unidad == 6 || _unidad == 8) dev.PinStates[41] = 0;
        if (_unidad == 0 || _unidad == 4 || _unidad == 5 || _unidad == 6 || _unidad == 8 || _unidad == 9) dev.PinStates[42] = 0;
        if (_unidad == 2 || _unidad == 3 || _unidad == 4 || _unidad == 5 || _unidad == 6 || _unidad == 8 || _unidad == 9) dev.PinStates[43] = 0;

        //Punto

        dev.PinStates[47] = 0;

        //Decimal
        if (_decimal == 0 || _decimal == 2 || _decimal == 3 || _decimal == 5 || _decimal == 6 || _decimal == 7 || _decimal == 8 || _decimal == 9) dev.PinStates[30] = 0;
        if (_decimal == 0 || _decimal == 1 || _decimal == 2 || _decimal == 3 || _decimal == 4 || _decimal == 7 || _decimal == 8 || _decimal == 9) dev.PinStates[31] = 0;
        if (_decimal == 0 || _decimal == 1 || _decimal == 3 || _decimal == 4 || _decimal == 5 || _decimal == 6 || _decimal == 7 || _decimal == 8 || _decimal == 9) dev.PinStates[32] = 0;
        if (_decimal == 0 || _decimal == 2 || _decimal == 3 || _decimal == 5 || _decimal == 6 || _decimal == 8) dev.PinStates[33] = 0;
        if (_decimal == 0 || _decimal == 2 || _decimal == 6 || _decimal == 8) dev.PinStates[34] = 0;
        if (_decimal == 0 || _decimal == 4 || _decimal == 5 || _decimal == 6 || _decimal == 8 || _decimal == 9) dev.PinStates[35] = 0;
        if (_decimal == 2 || _decimal == 3 || _decimal == 4 || _decimal == 5 || _decimal == 6 || _decimal == 8 || _decimal == 9) dev.PinStates[36] = 0;

        dev.WriteIOStates();
#endif
        mensaje += "tablero ok\n";
    }
    
}
