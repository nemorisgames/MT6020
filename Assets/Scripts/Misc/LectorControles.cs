using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.IO.Ports; //for access to COM ports

public class LectorControles : MonoBehaviour {
	public GameObject[] listeners;

	int[] botonPress = new int[96];
	bool[] ledEncendido = new bool[96];

	public float UpdatePlatformRate = 0.1f;
	private float nextUpdatePlatform = 0f;
	float tiempoFalso = 0f;

	string mensaje = "";
	bool ledDisponible = true;

	//public ConfiguracionControles configuracionControles;

	public struct InterfaceState
	{
		public int Analog1;
		public int Analog2;
		public int Analog3;
		public int Analog4;
		public int Analog5;
		public int Analog6;
		public int Analog7;
		public int Analog8;
		public int Analog9;
		public int Analog10;
		public int Analog11;
		public int Analog12;
		public byte[] DigitalInput;
		
		public InterfaceState(int MaxDigital) 
		{
			DigitalInput = new byte[MaxDigital];
			Analog1 = 0;
			Analog2 = 0;
			Analog3 = 0;
			Analog4 = 0;
			Analog5 = 0;
			Analog6 = 0;
			Analog7 = 0;
			Analog8 = 0;
			Analog9 = 0;
			Analog10 = 0;
			Analog11 = 0;
			Analog12 = 0;
			
		}
	}

	string aux;
	
	public InterfaceState CurrentInterfaceState = new InterfaceState(96);
#if !UNITY_EDITOR
	SerialPort InterfacePort = new SerialPort( "COM6"
	                                          , 115200
	                                          , Parity.None
	                                          , 8
	                                          , StopBits.One);
	string nombrePuerto = "COM6";


	
	static int IncomingData = 0;
	static int DataFrameBytes = 0;
	static int FrameLength = 0;
	static byte[] DataFrame = new byte[50];
	static bool ReadInputs = false;


	// Use this for initialization
#endif
	void Start () {
#if !UNITY_EDITOR
		InterfacePort.Close ();
		//configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles>();
		nombrePuerto = "COM" + PlayerPrefs.GetInt ("COM", 6);
		InterfacePort.ReadBufferSize = 256;
		abrirPuerto();
		if(InterfacePort.IsOpen) 
			apagarLeds();
#endif
	}
#if !UNITY_EDITOR
	private void GetInputsCmd()
	{
		int i = 0;
		int PlatformTimeOut = 0;
		byte[] Data = new byte[7];
		byte[] inData = new byte[37];
		byte command = 0xC2;
		
		Data[0] = command;
		
		if (InterfacePort.IsOpen) 
		{ 
			InterfacePort.DiscardInBuffer();
			InterfacePort.Write (Data, 0, 1);
			//Debug.Log ("Get Data");
			
			while (PlatformTimeOut < 1000) //waits until data arrives
			{
				PlatformTimeOut++;
			}
			
			InterfacePort.Read (inData,0,37);
			//Debug.Log ("Data Received");
			for(int ind = 0 ;ind < 37; ind ++)
			{
				//Debug.Log(inData[ind].ToString());
			}
			
			ParseCommand(inData,37);
			
		} 
		else 
		{
			InterfacePort.Open();
		}
		
		if (ReadInputs) 
		{
			//Debug.Log("Read");
		}
		
	}
	
	private void UpdateInterfaceState()
	{
		
		CurrentInterfaceState.Analog1 = (DataFrame[2] << 8 | DataFrame[1]);
		
		CurrentInterfaceState.Analog2 = (DataFrame[4] << 8 | DataFrame[3]);
		
		CurrentInterfaceState.Analog3 = (DataFrame[6] << 8 | DataFrame[5]);
		
		CurrentInterfaceState.Analog4 = (DataFrame[8] << 8 | DataFrame[7]);
		
		CurrentInterfaceState.Analog5 = (DataFrame[10] << 8 | DataFrame[9]);
		
		CurrentInterfaceState.Analog6 = (DataFrame[12] << 8 | DataFrame[11]);
		
		CurrentInterfaceState.Analog7 = (DataFrame[14] << 8 | DataFrame[13]);
		
		CurrentInterfaceState.Analog8 = (DataFrame[16] << 8 | DataFrame[15]);
		
		CurrentInterfaceState.Analog9 = (DataFrame[18] << 8 | DataFrame[17]);
		
		CurrentInterfaceState.Analog10 = (DataFrame[20] << 8 | DataFrame[19]);
		
		CurrentInterfaceState.Analog11 = (DataFrame[22] << 8 | DataFrame[21]);
		
		CurrentInterfaceState.Analog12 = (DataFrame[24] << 8 | DataFrame[23]);

		for (int j = 0; j < listeners.Length; j++) { 
			listeners [j].SendMessage ("potenciometros", new int[12]{CurrentInterfaceState.Analog1, CurrentInterfaceState.Analog2, CurrentInterfaceState.Analog3, CurrentInterfaceState.Analog4, CurrentInterfaceState.Analog5, CurrentInterfaceState.Analog6, CurrentInterfaceState.Analog7, CurrentInterfaceState.Analog8, CurrentInterfaceState.Analog9, CurrentInterfaceState.Analog10, CurrentInterfaceState.Analog11, CurrentInterfaceState.Analog12}, SendMessageOptions.DontRequireReceiver);
		}

		mensaje = "" + CurrentInterfaceState.Analog1 + "\n" + CurrentInterfaceState.Analog2 + "\n" + CurrentInterfaceState.Analog3 + "\n" + CurrentInterfaceState.Analog4 + "\n" + CurrentInterfaceState.Analog5 + "\n" + CurrentInterfaceState.Analog6;

		BitArray InBits = new BitArray(DataFrame);
		for (int i = 200; i <= 295; i+=8)
		{
			for (int j = 7; j >= 0; j-- )
			{
				//LstInputs.SetItemCheckState(i - 200 + (7-j), (InBits[i+j]) ? CheckState.Unchecked : CheckState.Checked);
				CurrentInterfaceState.DigitalInput[i - 200 + (7-j)] = (InBits[i+j]) ? (byte)0 : (byte)1;
			}
		}
		
	}
	
	private void ParseCommand(byte[] indata, int BytesCount)
	{
		int i;
		if (indata [0] == 0xDD && IncomingData == 0) {
			IncomingData = 1;
			ReadInputs = false;
			FrameLength = 37;
			DataFrameBytes = 0;
		}
	
		for (i = 0; i < BytesCount; i++) {
			if (IncomingData == 1)
				DataFrame [DataFrameBytes + i] = indata [i];
		}
	
	
		DataFrameBytes += BytesCount;
	
		if (IncomingData == 1 && DataFrameBytes == FrameLength) {
			FrameLength = 0;
			DataFrameBytes = 0;
			ReadInputs = true;
			IncomingData = 0;
		}
	}
	#endif

	IEnumerator operarLed(byte outPin, bool ON){
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 1000; i++) {
			if (ledDisponible) {
				ledDisponible = false;
				#if !UNITY_EDITOR
				if (InterfacePort != null && InterfacePort.IsOpen){
					byte[] Data = new byte[3];
					byte command = 0xCD;
					
					Data[0] = command;
					if (ON) {
						Data [1] = 0xff;
						//mensaje += "LED " + outPin + " ON\n";
						print ( "LED " + outPin + " ON\n");
					} else {
						//mensaje += "LED " + outPin + " OFF\n";
						print ("LED " + outPin + " OFF\n");
						Data [1] = 0;
					}
					Data[2] = outPin;

					InterfacePort.Write(Data, 0, 3);

				}
				#endif
				//print ("ocupa led");
				yield return new WaitForSeconds(0.5f);
				//print ("desocupa led");
				ledDisponible = true;
				break;
			}
			else{
				yield return new WaitForSeconds(0.1f);
			}
		}
	}

	public void OutCmd(byte outPin, bool ON)
	{
		//print ("llamada a outCmd");
		StartCoroutine (operarLed(outPin, ON));
		/*
		#if !UNITY_EDITOR
		if (InterfacePort != null && InterfacePort.IsOpen){
			byte[] Data = new byte[3];
			byte command = 0xCD;
			
			Data[0] = command;
			if (ON) {
				Data [1] = 0xff;
				mensaje += "LED " + outPin + " ON\n";
			} else {
				mensaje += "LED " + outPin + " OFF\n";
				Data [1] = 0;
			}
			Data[2] = outPin;
			InterfacePort.Write(Data, 0, 3);
		}
		#endif
*/
	}

	public void apagarTodo()
	{
		
		#if !UNITY_EDITOR
		if (InterfacePort != null && InterfacePort.IsOpen){
			byte[] Data = new byte[3];
			byte command = 0xC1;
			
			Data[0] = command;

			InterfacePort.Write(Data, 0, 1);
		}
		#endif
		
	}
	#if !UNITY_EDITOR
	void abrirPuerto(){
		if (InterfacePort.IsOpen) return;
		byte[] Data = new byte[7]; 
		byte command = 0x35;
		
		Data[0] = command;
		
		InterfacePort.Open ();
		
		if (InterfacePort.IsOpen) {
			print("puerto abierto");
			InterfacePort.Write (Data, 0, 1);
		//	estado += "Puerto " + nombrePuerto + " abierto\n";
			//StartCoroutine(apagarLedsRutina());
		}
	}

	IEnumerator apagarLedsRutina(){
		yield return new WaitForSeconds (1f);
		apagarLeds();
	}
	
	void FixedUpdate()
	{
		
		/*if (Time.time > nextUpdatePlatform) {
			nextUpdatePlatform = Time.time + UpdatePlatformRate;
			//Send angles to platform
			//Debug.Log ("Angulo Original" + rigidbody.rotation.eulerAngles.z.ToString ());
			//Debug.Log ("Angulo trasladado" + TranslateAngle (rigidbody.rotation.eulerAngles.z));
			if(Time.timeSinceLevelLoad > 1f)
				GetInputsCmd(); 
		}*/
		if (tiempoFalso > nextUpdatePlatform) {
			nextUpdatePlatform = tiempoFalso + UpdatePlatformRate;
			//Send angles to platform
			//Debug.Log ("Angulo Original" + rigidbody.rotation.eulerAngles.z.ToString ());
			//Debug.Log ("Angulo trasladado" + TranslateAngle (rigidbody.rotation.eulerAngles.z));
			if(Time.timeSinceLevelLoad > 1f)
				GetInputsCmd(); 
		}
		tiempoFalso += 0.01f;
	}
#endif
	void Update()
	{
#if !UNITY_EDITOR
		/*if (CurrentInterfaceState.DigitalInput [0] == 1) {
			OutCmd (14,true); //LED 1st On
			OutCmd (47,true); //LED 20th On
			OutCmd (14,false); //LED 1st Off
			OutCmd (47,false); //LED 20th Off
		}*/
		if(Time.timeScale <= 0f){
			if (tiempoFalso > nextUpdatePlatform) {
				nextUpdatePlatform = tiempoFalso + UpdatePlatformRate;
				//Send angles to platform
				//Debug.Log ("Angulo Original" + rigidbody.rotation.eulerAngles.z.ToString ());
				//Debug.Log ("Angulo trasladado" + TranslateAngle (rigidbody.rotation.eulerAngles.z));
				if(Time.timeSinceLevelLoad > 1f)
					GetInputsCmd(); 
			}
			tiempoFalso += 0.01f;
		}
		if (ReadInputs) 
		{
			//Debug.Log ("Update");
			UpdateInterfaceState();
			ReadInputs = false;
			
		}
		aux = "";
		for (int i = 0; i < CurrentInterfaceState.DigitalInput.Length; i++) {
			if (CurrentInterfaceState.DigitalInput [i] != botonPress[i]) {
				if(botonPress[i] == 1){
					enviarMensaje(i, true);
				}
				else
					enviarMensaje(i, false);

				aux += "boton"+i+" : " + CurrentInterfaceState.DigitalInput [i] + "\n";
				botonPress[i] = CurrentInterfaceState.DigitalInput [i];
			}
		}
#else
		if(Input.GetKeyUp(KeyCode.R)) enviarMensaje(4, true);
		if(Input.GetKeyUp(KeyCode.T)) enviarMensaje(3, true);
		if(Input.GetKeyDown(KeyCode.R)) enviarMensaje(4, false);
		if(Input.GetKeyDown(KeyCode.T)) enviarMensaje(3, false);
		if(Input.GetKeyDown(KeyCode.Y)) enviarMensaje(18, false);
		if(Input.GetKeyUp(KeyCode.Y)) enviarMensaje(18, true);
		if(Input.GetKeyUp(KeyCode.U)) enviarMensaje(19, true);

		if(Input.GetKeyUp(KeyCode.F)) enviarMensaje(2, true);
		if(Input.GetKeyUp(KeyCode.G)) enviarMensaje(1, true);
		if(Input.GetKeyDown(KeyCode.F)) enviarMensaje(2, false);
		if(Input.GetKeyDown(KeyCode.G)) enviarMensaje(1, false);
		if(Input.GetKeyUp(KeyCode.H)) enviarMensaje(0, true);
		if(Input.GetKeyUp(KeyCode.J)) enviarMensaje(17, true);

		if(Input.GetKeyUp(KeyCode.K)) enviarMensaje(8, true);
		if(Input.GetKeyUp(KeyCode.L)) enviarMensaje(5, true);
		
		if(Input.GetKeyUp(KeyCode.V)) enviarMensaje(6, true);
		if(Input.GetKeyDown(KeyCode.V)) enviarMensaje(6, false);
		if(Input.GetKeyUp(KeyCode.B)) enviarMensaje(7, true);
		if(Input.GetKeyUp(KeyCode.N)) enviarMensaje(16, true);
		if(Input.GetKeyUp(KeyCode.M)) enviarMensaje(14, true);
		if(Input.GetKeyUp(KeyCode.Comma)) enviarMensaje(15, true);
		if(Input.GetKeyUp(KeyCode.Period)) enviarMensaje(13, true);

		if(Input.GetKeyUp(KeyCode.I)) enviarMensaje(12, true);
		if(Input.GetKeyDown(KeyCode.I)) enviarMensaje(12, false);
		if(Input.GetKeyUp(KeyCode.O)) enviarMensaje(10, true);
		if(Input.GetKeyUp(KeyCode.P)) enviarMensaje(11, true);

		
		if(Input.GetButtonUp("Fire1")){ enviarMensaje(9, true); enviarMensaje(59, true); }
		if(Input.GetButtonDown("Fire1")){ enviarMensaje(9, false); enviarMensaje(59, false); }
		if(Input.GetButtonUp("Jump")){ enviarMensaje(64, true); }
		if(Input.GetButtonDown("Jump")){ enviarMensaje(64, false); }
		if (Input.GetKeyUp (KeyCode.JoystickButton3)) enviarMensaje(60, true);
		if (Input.GetKeyDown (KeyCode.JoystickButton3)) enviarMensaje(60, false);
		if (Input.GetKeyUp (KeyCode.JoystickButton4)) enviarMensaje(61, true);

		/*for (int j = 0; j < listeners.Length; j++) { 
			listeners [j].SendMessage ("potenciometros", new int[6]{Mathf.RoundToInt(Input.GetAxis("Horizontal") * 10), 0, 0, 0, 0, 0}, SendMessageOptions.DontRequireReceiver);
		}*/
#endif
	}

	void OnGUI(){
		//GUI.Box (new Rect (10f, 10f, 600f, Screen.height - 20f), aux);
		//GUI.Box (new Rect (Screen.width * 7f / 10f, 0f, 400f, 800f), mensaje);
		/*if (GUI.Button (new Rect (Screen.width * 7f / 10f - 100f, 100f, 90f, 40f), "encender Leds")) {
			OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), true);
			OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), true);
			OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), true);
			OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), true);
		}
		if (GUI.Button (new Rect (Screen.width * 7f / 10f - 100f, 20f, 90f, 40f), "apagar Leds")) {
			apagarLeds();
		}
		if (GUI.Button (new Rect (Screen.width * 7f / 10f - 100f, 180f, 90f, 40f), "siguiente")) {
			if(SceneManager.GetActiveScene().name == "TestTarjeta")
				SceneManager.LoadScene("TestTarjeta2");
			else
				SceneManager.LoadScene("TestTarjeta");
		}*/
	}

	public void apagarLeds(){
		apagarTodo ();
		/*
		#if !UNITY_EDITOR
		if (InterfacePort != null && InterfacePort.IsOpen){
			mensaje = "";
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedLucesAltasDelanteras")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedLucesAltasTraseras")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedLucesBajasDelanteras")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedLucesBajasTraseras")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedEncendido")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedPruebaFrenos")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedOverride")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedRRC")), false);
			OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedTransmisionAutomatica")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedDesembragar")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedClaxon")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedRideControl")), false);
			OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedCambio1")), false);
			OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedCambio2")), false);
			OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedCambio3")), false);
			OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedCambio4")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedDisplayON")), false);
			//OutCmd(byte.Parse("" + PlayerPrefs.GetInt ("idLedDisplayOFF")), false);
		}
		#endif
		*/
	}

	void OnLevelWasLoaded(int level){
		//apagarLeds ();
	}

	/*public void encenderLeds(bool encender){
		print ("encender leds " + encender);
		#if !UNITY_EDITOR
		OutCmd(16, ((ledEncendido[16])?encender:false));
		OutCmd(14, ((ledEncendido[14])?encender:false));
		OutCmd(39, ((ledEncendido[39])?encender:false));
		OutCmd(38, ((ledEncendido[38])?encender:false));
		OutCmd(17, ((ledEncendido[17])?encender:false));
		OutCmd(15, ((ledEncendido[15])?encender:false));
		OutCmd(41, ((ledEncendido[41])?encender:false));
		OutCmd(43, ((ledEncendido[43])?encender:false));
		OutCmd(21, ((ledEncendido[21])?encender:false));
		OutCmd(20, ((ledEncendido[20])?encender:false));
		OutCmd(23, ((ledEncendido[23])?encender:false));
		OutCmd(22, ((ledEncendido[22])?encender:false));
		OutCmd(45, ((ledEncendido[45])?encender:false));
		OutCmd(42, ((ledEncendido[42])?encender:false));
		OutCmd(44, ((ledEncendido[44])?encender:false));
		OutCmd(47, ((ledEncendido[47])?encender:false));
		OutCmd(19, ((ledEncendido[19])?encender:false));
		OutCmd(18, ((ledEncendido[18])?encender:false));
		#endif
	}*/


	void enviarMensaje(int indice, bool isUp){
		for (int j = 0; j < listeners.Length; j++) { 
			if(listeners [j].activeSelf){
				if(isUp) listeners [j].SendMessage ("BotonUp", indice, SendMessageOptions.DontRequireReceiver);
				else listeners [j].SendMessage ("BotonDown", indice, SendMessageOptions.DontRequireReceiver);
			}
		}
#if !UNITY_EDITOR
		//enciende los leds
		/*if(SceneManager.GetActiveScene().name == "Modulo4" ||
		   SceneManager.GetActiveScene().name == "Modulo5" ||
		   SceneManager.GetActiveScene().name == "Modulo6" ||
		   SceneManager.GetActiveScene().name == "Modulo7" ||
		   SceneManager.GetActiveScene().name == "Modulo8" ||
		   SceneManager.GetActiveScene().name == "Modulo9" ||
		   SceneManager.GetActiveScene().name == "Modulo10" ||
		   SceneManager.GetActiveScene().name == "Modulo11" ||
		   SceneManager.GetActiveScene().name == "Modulo13" ||
		   SceneManager.GetActiveScene().name == "Modulo14" ||
		   SceneManager.GetActiveScene().name == "Modulo16" ||
		   SceneManager.GetActiveScene().name == "Modulo17" ||
		   SceneManager.GetActiveScene().name == "Modulo18")
		if(isUp)
		switch(indice){
		case 4:
			OutCmd(16, !ledEncendido[16]);
			ledEncendido[16] = !ledEncendido[16];
			break;
		case 3:
			OutCmd(14, !ledEncendido[14]);
			ledEncendido[14] = !ledEncendido[14];
			break;
		case 18:
			OutCmd(39, !ledEncendido[39]);
			ledEncendido[39] = !ledEncendido[39];
			break;
		case 19:
			OutCmd(38, !ledEncendido[38]);
			ledEncendido[38] = !ledEncendido[38];
			break;

		case 2:
			OutCmd(17, !ledEncendido[17]);
			ledEncendido[17] = !ledEncendido[17];
			break;
		case 1:
			OutCmd(15, !ledEncendido[15]);
			ledEncendido[15] = !ledEncendido[15];
			break;
		case 0:
			OutCmd(41, !ledEncendido[41]);
			ledEncendido[41] = !ledEncendido[41];
			break;
		case 17:
			OutCmd(43, !ledEncendido[43]);
			ledEncendido[43] = !ledEncendido[43];
			break;

		case 8:
			OutCmd(21, !ledEncendido[21]);
			ledEncendido[21] = !ledEncendido[21];
			break;
		case 5:
			OutCmd(20, !ledEncendido[20]);
			ledEncendido[20] = !ledEncendido[20];
			break;

		case 6:
			OutCmd(23, !ledEncendido[23]);
			ledEncendido[23] = !ledEncendido[23];
			break;
		case 7:
			OutCmd(22, !ledEncendido[22]);
			ledEncendido[22] = !ledEncendido[22];
			break;
		case 16:
			OutCmd(45, !ledEncendido[45]);
			ledEncendido[45] = !ledEncendido[45];
			break;
		case 14:
			OutCmd(42, !ledEncendido[42]);
			ledEncendido[42] = !ledEncendido[42];
			break;
		case 15:
			OutCmd(44, !ledEncendido[44]);
			ledEncendido[44] = !ledEncendido[44];
			break;
		case 13:
			OutCmd(47, !ledEncendido[47]);
			ledEncendido[47] = !ledEncendido[47];
			break;

		//case 12:
		//	OutCmd(41, botonPress[indice] == 1);
		//	break;
		case 10:
			OutCmd(19, !ledEncendido[19]);
			ledEncendido[19] = !ledEncendido[19];
			break;
		case 11:
			OutCmd(18, !ledEncendido[18]);
			ledEncendido[18] = !ledEncendido[18];
			break;
		}*/
#endif
		//Debug.Log ("Button " + (indice + 1) + " Up: " + isUp);
	}

}
