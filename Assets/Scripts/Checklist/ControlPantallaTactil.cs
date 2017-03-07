using UnityEngine;
using System.Collections;

public class ControlPantallaTactil : MonoBehaviour {
	public GameObject botonesSuperiores;
	public GameObject botonesInferioresInterior;
	public enum estadoPantallaTactil {menuD, botonesSuperiores, enOperador, enLog, enOperadorUser, enOperadorLanguages, enOperadorDisplay, enOperadorUnits, enOperadorBrakeTest, enOperadorConvert, enLogModules, enLogLevers, enLogPedals};
	public estadoPantallaTactil estado = estadoPantallaTactil.botonesSuperiores;
	public int indiceBotonSuperiorSeleccionado = 0;
	
	public GameObject menuOperador;
	public GameObject menuMaquina;
	public GameObject menuHidraulico;
	public GameObject menuTemperaturas;
	public GameObject menuLogs;
	public GameObject menuSistemas;
	
	public GameObject menuD1;
	public GameObject menuD2;
	public GameObject menuD3;
	public GameObject menuD4;

	//Menu operador
	public GameObject menuOperadorUser;
	public GameObject menuOperadorLanguages;
	public GameObject menuOperadorDisplay;
	public GameObject menuOperadorUnits;
	public GameObject menuOperadorBrakeTest;
	public GameObject menuOperadorConverterStall;
	//Menu Logs
	public GameObject menuLogModules;
	public GameObject menuLogLevers;
	public GameObject menuLogPedals;

	public Vector3[] posicionesBotonesMenuOperador;
	public Vector3[] posicionesBotonesMenuLog;
	public int indiceBotonOperadorSeleccionado = -1;
	public int indiceBotonLogSeleccionado = -1;
	public Transform cubreBotonOperador;
	public Transform cubreBotonLog;
	// Menu Operador/Convert Stall
	public UILabel op1menuOperadorConvert;
	public UILabel op2menuOperadorConvert;
	public UILabel op3menuOperadorConvert;
	public UILabel op4menuOperadorConvert;

	//Menu Vehicle
	public UILabel batteryVoltage;
	public UILabel vehicleSpeed;

	//Menu Hidraulico
	public UILabel accumPressure;
	public UILabel hidraulicTemp;

	//Menu Temperaturas
	public UILabel percentTorque;
	public UILabel boostPres;
	public UILabel oilTemp;
	public UILabel fuelRate;

	//Menu D1
	public UILabel numPalas;
	public UILabel pesoPala;
	public UILabel pesoPalaAcumulado;
	public UILabel op3menuD1;
	public UILabel porcentajePetroleo;
	public UISlider porcentajePetroleoSlider;

	//Menu D2
	public UILabel op1menuD2;
	public UILabel op2menuD2;
	public UILabel op3menuD2;
	public Transform aguja1;
	public Transform aguja2;
	public Transform aguja3;
	
	//Menu D3
	public UILabel op1menuD3;
	public UILabel op2menuD3;
	public UILabel op3menuD3;
	public Transform aguja1D3;
	public Transform aguja2D3;
	public Transform aguja3D3;

	//Menu Logs
	public UISlider brakeBarra;
	public UISlider throttleBarra;
	public UISlider joyIzqYBarra;
	public UISlider joyDerXBarra;
	public UISlider joyDerYBarra;
	public GameObject boton1Izq;
	public GameObject boton2Izq;
	public GameObject boton3Izq;
	public GameObject boton4Izq;
	public GameObject botonGatilloIzq;
	public GameObject boton1Der;
	public GameObject boton2Der;
	public GameObject boton3Der;
	public GameObject boton4Der;
	public GameObject botonGatilloDer;

	//botones inferior interiores
	public GameObject[] botonMotorEncendido;
	public GameObject[] botonPalaCargada;
	public GameObject[] botonNeutro;
	//public GameObject[] cobertoresBotonesInferior;
	int[] valoresPotenciometro = new int[6];

	ConfiguracionControles configuracionControles;

	public float tiempoMotorFuncionando = 0f;
	bool motorFuncionando = false;

	public UILabel diaLabel;
	public UILabel horaLabel;
	public UILabel segundosLabel;

	//0: freno
	//1: acelerador
	//2: joy izq X
	//3: joy izq Y
	//4: joy der X
	//5: joy der Y
	void potenciometros(int[] valores){
		valoresPotenciometro = valores;
	}

	public void motorEncendido(bool valor){
		motorFuncionando = valor;
		foreach (GameObject g in botonMotorEncendido) {
			g.SetActive(valor);
		}
	}

	public void palaCargada(float valor){
        string tipo = "";
        if(valor > 14.5f)
        {
            if(valor < 15f)
            {
                tipo = "Amarilla";
            }
            else
            {
                tipo = "Roja";
            }
        }
		foreach (GameObject g in botonPalaCargada) {
			g.SetActive(valor > 0.01f);
            g.GetComponent<UISprite>().spriteName = "Palada" + tipo;
		}
	}

	public void neutro(bool valor){
		foreach (GameObject g in botonNeutro) {
			g.SetActive(valor);
		}
	}

	public void mostrarMenuD1(){
		estado = estadoPantallaTactil.menuD;
		esconderTodo ();
		menuD1.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}

	public void mostrarMenuD2(){
		estado = estadoPantallaTactil.menuD;
		esconderTodo ();
		menuD2.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}

	public void mostrarMenuD3(){
		estado = estadoPantallaTactil.menuD;
		esconderTodo ();
		menuD3.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}

	public void mostrarMenuD4(){
		estado = estadoPantallaTactil.menuD;
		esconderTodo ();
		menuD4.SetActive (true);
	}

	public void mostrarMenuOperador(){
		esconderTodo ();
		botonesSuperiores.SetActive (true);
		menuOperador.SetActive (true);
		indiceBotonSuperiorSeleccionado = 0;
		indiceBotonOperadorSeleccionado = -1;
		actualizarBotonOperadorSeleccionado ();
	}

	public void mostrarMenuOperadorUser(){
		esconderTodo ();
		estado = estadoPantallaTactil.enOperadorUser;
		menuOperadorUser.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}
	
	public void mostrarMenuOperadorLanguages(){
		esconderTodo ();
		estado = estadoPantallaTactil.enOperadorLanguages;
		menuOperadorLanguages.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}
	
	public void mostrarMenuOperadorDisplay(){
		esconderTodo ();
		estado = estadoPantallaTactil.enOperadorDisplay;
		menuOperadorDisplay.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}
	
	public void mostrarMenuOperadorUnits(){
		esconderTodo ();
		estado = estadoPantallaTactil.enOperadorUnits;
		menuOperadorUnits.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}
	
	public void mostrarMenuOperadorBrakeTest(){
		esconderTodo ();
		estado = estadoPantallaTactil.enOperadorBrakeTest;
		menuOperadorBrakeTest.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}
	
	public void mostrarMenuOperadorConvert(){
		esconderTodo ();
		estado = estadoPantallaTactil.enOperadorConvert;
		menuOperadorConverterStall.SetActive (true);
		botonesInferioresInterior.SetActive (true);
	}

	public void mostrarMenuLogModules(){
		esconderTodo ();
		estado = estadoPantallaTactil.enLogModules;
		menuLogModules.SetActive (true);
		botonesSuperiores.SetActive (true);
		botonesInferioresInterior.SetActive (false);
	}

	public void mostrarMenuLogLevers(){
		esconderTodo ();
		estado = estadoPantallaTactil.enLogLevers;
		menuLogLevers.SetActive (true);
		botonesSuperiores.SetActive (true);
		botonesInferioresInterior.SetActive (false);
	}

	public void mostrarMenuLogPedals(){
		esconderTodo ();
		estado = estadoPantallaTactil.enLogPedals;
		menuLogPedals.SetActive (true);
		botonesSuperiores.SetActive (true);
		botonesInferioresInterior.SetActive (false);
	}

	public void mostrarMenuHidraulica(){
		menuHidraulico.SetActive (true);
	}

	public void mostrarMenuMaquina(){
		menuMaquina.SetActive (true);
	}

	public void mostrarMenuLogs(){
		esconderTodo ();
		botonesSuperiores.SetActive (true);
		menuLogs.SetActive (true);
		indiceBotonSuperiorSeleccionado = 4;
		indiceBotonLogSeleccionado = -1;
		actualizarBotonLogSeleccionado ();
	}

	public void mostrarMenuModulos(){
		menuSistemas.SetActive (true);
	}

	public void mostrarMenuTemperaturas(){
		menuTemperaturas.SetActive (true);
	}

	public void enter(){
		switch (indiceBotonSuperiorSeleccionado) {
		case 0:
			if(indiceBotonOperadorSeleccionado == -1){ 
				estado = estadoPantallaTactil.enOperador;
				indiceBotonOperadorSeleccionado = 0;
				actualizarBotonOperadorSeleccionado();
			}
			else{
				switch(indiceBotonOperadorSeleccionado){
				case 0:  
					mostrarMenuOperadorUser();
					break;
				case 1:  
					mostrarMenuOperadorLanguages();
					break;
				case 2:  
					mostrarMenuOperadorDisplay();
					break;
				case 3:  
					mostrarMenuOperadorUnits();
					break;
				case 4:  
					mostrarMenuOperadorBrakeTest();
					break;
				case 5: 
					mostrarMenuOperadorConvert();
					break;
				}
			}
			break;
		case 4:
			if(indiceBotonLogSeleccionado == -1){ 
				estado = estadoPantallaTactil.enLog;
				indiceBotonLogSeleccionado = 0;
				actualizarBotonLogSeleccionado();
			}
			else{
				switch(indiceBotonLogSeleccionado){
				case 0:  
					mostrarMenuLogModules();
					break;
				case 1:  
					mostrarMenuLogLevers();
					break;
				case 2:  
					mostrarMenuLogPedals();
					break;
				}
			}
			break;
		}
	}

	public void esc(){
		esconderTodo ();
		switch (estado) {
		case estadoPantallaTactil.botonesSuperiores:
			indiceBotonSuperiorSeleccionado = 0;
			indiceBotonOperadorSeleccionado = -1;
			actualizarBotonOperadorSeleccionado();
			botonesSuperiores.SetActive (true);
			mostrarMenuOperador ();
			break;
		case estadoPantallaTactil.enOperador:
			estado = estadoPantallaTactil.botonesSuperiores;
			indiceBotonOperadorSeleccionado = -1;
			actualizarBotonOperadorSeleccionado();
			botonesSuperiores.SetActive (true);
			mostrarMenuOperador ();
			break;
		case estadoPantallaTactil.enOperadorConvert: case estadoPantallaTactil.enOperadorUser: case estadoPantallaTactil.enOperadorLanguages: case estadoPantallaTactil.enOperadorDisplay: case estadoPantallaTactil.enOperadorUnits: case estadoPantallaTactil.enOperadorBrakeTest:
			estado = estadoPantallaTactil.botonesSuperiores;
			esconderTodo();
			mostrarMenuOperador ();
			break;
		case estadoPantallaTactil.enLog:
			estado = estadoPantallaTactil.botonesSuperiores;
			indiceBotonLogSeleccionado = -1;
			actualizarBotonLogSeleccionado();
			botonesSuperiores.SetActive (true);
			mostrarMenuLogs ();
			break;
		case estadoPantallaTactil.enLogLevers: case estadoPantallaTactil.enLogModules: case estadoPantallaTactil.enLogPedals:
			estado = estadoPantallaTactil.botonesSuperiores;
			esconderTodo();
			mostrarMenuLogs ();
			break;
		case estadoPantallaTactil.menuD:
			estado = estadoPantallaTactil.botonesSuperiores;
			botonesSuperiores.SetActive (true);
			mostrarMenuOperador ();
			break;
		}
	}

	public void flechaIzq(){
		switch (estado) {
		case estadoPantallaTactil.botonesSuperiores:
			indiceBotonSuperiorSeleccionado--;
			if(indiceBotonSuperiorSeleccionado < 0) indiceBotonSuperiorSeleccionado = 0;
			actualizarBotonSuperiorSeleccionado();
			break;
		case estadoPantallaTactil.enOperador:
			indiceBotonOperadorSeleccionado -= 4;
			if(indiceBotonOperadorSeleccionado < 0) indiceBotonOperadorSeleccionado = 0;
			actualizarBotonOperadorSeleccionado();
			break;
		case estadoPantallaTactil.enLog:
			indiceBotonLogSeleccionado -= 2;
			if(indiceBotonLogSeleccionado < 0) indiceBotonLogSeleccionado = 0;
			actualizarBotonLogSeleccionado();
			break;
		}
	}

	public void flechaDer(){
		switch (estado) {
		case estadoPantallaTactil.botonesSuperiores:
			indiceBotonSuperiorSeleccionado++;
			if(indiceBotonSuperiorSeleccionado > 5) indiceBotonSuperiorSeleccionado = 5;
			actualizarBotonSuperiorSeleccionado();
			break;
		case estadoPantallaTactil.enOperador:
			indiceBotonOperadorSeleccionado += 4;
			if(indiceBotonOperadorSeleccionado > 5) indiceBotonOperadorSeleccionado = 5;
			actualizarBotonOperadorSeleccionado();
			break;
		case estadoPantallaTactil.enLog:
			indiceBotonLogSeleccionado += 2;
			if(indiceBotonLogSeleccionado > 2) indiceBotonLogSeleccionado = 2;
			actualizarBotonLogSeleccionado();
			break;
		}
	}

	public void flechaArriba(){
		switch (estado) {
		case estadoPantallaTactil.botonesSuperiores:
			break;
		case estadoPantallaTactil.enOperador:
			indiceBotonOperadorSeleccionado--;
			if(indiceBotonOperadorSeleccionado < 0) indiceBotonOperadorSeleccionado = 0;
			actualizarBotonOperadorSeleccionado();
			break;
		case estadoPantallaTactil.enLog:
			indiceBotonLogSeleccionado--;
			if(indiceBotonLogSeleccionado < 0) indiceBotonLogSeleccionado = 0;
			actualizarBotonLogSeleccionado();
			break;
		}
	}

	public void flechaAbajo(){
		switch (estado) {
		case estadoPantallaTactil.botonesSuperiores:
			break;
		case estadoPantallaTactil.enOperador:
			indiceBotonOperadorSeleccionado++;
			if(indiceBotonOperadorSeleccionado > 5) indiceBotonOperadorSeleccionado = 5;
			actualizarBotonOperadorSeleccionado();
			break;
			
		case estadoPantallaTactil.enLog:
			indiceBotonLogSeleccionado++;
			if(indiceBotonLogSeleccionado > 2) indiceBotonLogSeleccionado = 2;
			actualizarBotonLogSeleccionado();
			break;
		}
	}

	void esconderTodo(){
		if(botonesSuperiores != null) botonesSuperiores.SetActive (false);
		if(menuOperador != null) menuOperador.SetActive (false);
		if(menuMaquina != null) menuMaquina.SetActive (false);
		if(menuHidraulico != null) menuHidraulico.SetActive (false);
		if(menuTemperaturas != null) menuTemperaturas.SetActive (false);
		if(menuLogs != null) menuLogs.SetActive (false);
		if(menuSistemas != null) menuSistemas.SetActive (false);

		if(menuD1 != null) menuD1.SetActive (false);
		if(menuD2 != null) menuD2.SetActive (false);
		if(menuD3 != null) menuD3.SetActive (false);
		if(menuD4 != null) menuD4.SetActive (false);
		
		if(menuOperadorUser != null) menuOperadorUser.SetActive (false);
		if(menuOperadorLanguages != null) menuOperadorLanguages.SetActive (false);
		if(menuOperadorDisplay != null) menuOperadorDisplay.SetActive (false);
		if(menuOperadorUnits != null) menuOperadorUnits.SetActive (false);
		if(menuOperadorBrakeTest != null) menuOperadorBrakeTest.SetActive (false);
		if(menuOperadorConverterStall != null) menuOperadorConverterStall.SetActive (false);

		if(menuLogModules != null) menuLogModules.SetActive (false);
		if(menuLogLevers != null) menuLogLevers.SetActive (false);
		if(menuLogPedals != null) menuLogPedals.SetActive (false);
		
		botonesInferioresInterior.SetActive (false);
	}

	void actualizarBotonSuperiorSeleccionado(){
		esconderTodo ();
		switch (indiceBotonSuperiorSeleccionado) {
		case 0:
			botonesSuperiores.SetActive(true);
			mostrarMenuOperador();
			break;
		case 1:
			botonesSuperiores.SetActive(true);
			mostrarMenuMaquina();
			break;
		case 2:
			botonesSuperiores.SetActive(true);
			mostrarMenuHidraulica();
			break;
		case 3:
			botonesSuperiores.SetActive(true);
			mostrarMenuTemperaturas();
			break;
		case 5:
			botonesSuperiores.SetActive(true);
			mostrarMenuModulos();
			break;
		case 4:
			botonesSuperiores.SetActive(true);
			mostrarMenuLogs();
			break;
		}
	}

	void actualizarBotonOperadorSeleccionado(){
		if (indiceBotonOperadorSeleccionado == -1) {
			cubreBotonOperador.localPosition = new Vector3(0f, -2000f, 0f);
		} else {
			cubreBotonOperador.localPosition = posicionesBotonesMenuOperador[indiceBotonOperadorSeleccionado];
		}
	}
	void actualizarBotonLogSeleccionado(){
		if (indiceBotonLogSeleccionado == -1) {
			cubreBotonLog.localPosition = new Vector3(0f, -2000f, 0f);
		} else {
			cubreBotonLog.localPosition = posicionesBotonesMenuLog[indiceBotonLogSeleccionado];
		}
	}

	public void setOp1OperadorConvert(string valor){
		op1menuOperadorConvert.text = valor;
	}
	public void setOp2OperadorConvert(string valor){
		op2menuOperadorConvert.text = valor;
	}
	public void setOp3OperadorConvert(string valor){
		op3menuOperadorConvert.text = valor;
	}
	public void setOp4OperadorConvert(string valor){
		op4menuOperadorConvert.text = valor;
	}
	//Menu Hidraulico
	public void setAccumPressure(string valor){
		accumPressure.text = valor;
	}
	public void setHidraulicTemp(string valor){
		hidraulicTemp.text = valor;
	}
	//Menu Vehicle
	public void setBatteryVoltage(string valor){
		batteryVoltage.text = valor;
	}
	public void setVehicleSpeed(string valor){
		vehicleSpeed.text = valor;
	}
	//Menu Temperaturas
	public void setPercentTorque(string valor){
		percentTorque.text = valor;
	}
	public void setBoost(string valor){
		if(boostPres == null)
			boostPres = percentTorque.transform.parent.FindChild ("LabelBoost").GetComponent<UILabel> ();
		boostPres.text = valor;
	}
	public void setOilTemp(string valor){
		oilTemp.text = valor;
	}
	public void setFuelRate(string valor){
		fuelRate.text = valor;
	}
	//Menu Logs
	public void setBrake(float valor){
		brakeBarra.value = valor;
	}
	public void setThrottle(float valor){
		throttleBarra.value = valor;
	}
	public void setJoyYIzq(float valor){
		joyIzqYBarra.transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, (2f * valor < 0?180f:0f)));
		joyIzqYBarra.value = Mathf.Clamp(Mathf.Abs(2f * valor), -1f, 1f);
	}
	public void setJoyXDer(float valor){
		joyDerXBarra.transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, (2f * valor < 0?-90f:90f)));
		joyDerXBarra.value = Mathf.Clamp(Mathf.Abs(2f * valor), -1f, 1f);
	}
	public void setJoyYDer(float valor){
		joyDerYBarra.transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, (2f * valor < 0?180f:0f)));
		joyDerYBarra.value = Mathf.Clamp(Mathf.Abs(2f * valor), -1f, 1f);
	}
	public void setBoton1Izq(bool valor){
		boton1Izq.SetActive(valor);
	}
	public void setBoton2Izq(bool valor){
		boton2Izq.SetActive(valor);
	}
	public void setBoton3Izq(bool valor){
		boton3Izq.SetActive(valor);
	}
	public void setBoton4Izq(bool valor){
		boton4Izq.SetActive(valor);
	}
	public void setBotonGatilloIzq(bool valor){
		botonGatilloIzq.SetActive(valor);
	}
	public void setBoton1Der(bool valor){
		boton1Der.SetActive(valor);
	}
	public void setBoton2Der(bool valor){
		boton2Der.SetActive(valor);
	}
	public void setBoton3Der(bool valor){
		boton3Der.SetActive(valor);
	}
	public void setBoton4Der(bool valor){
		boton4Der.SetActive(valor);
	}
	public void setBotonGatilloDer(bool valor){
		print ("recibido gatillo " + valor);
		botonGatilloDer.SetActive(valor);
	}
	//Menu D1
	public void setNumPalas(string valor){
		numPalas.text = "" + Mathf.Clamp(int.Parse(valor), 0f, 10000000f);
	}
	public void setPesoPala(string valor){
		pesoPala.text = valor;
		palaCargada (float.Parse (valor));
	}
	public void setPesoPalaAcumulado(string valor){
		float peso = float.Parse(pesoPalaAcumulado.text);
		pesoPalaAcumulado.text = "" + (Mathf.Clamp(peso + float.Parse(valor), 0f, 10000000f));
	}
	public void setOp3menuD1(string valor){
		op3menuD1.text = valor;
	}
	public void setPorcentajePetroleo(string valor){
		porcentajePetroleo.text = valor;
		float porcentaje = float.Parse (valor) / 100f;
		porcentajePetroleoSlider.value = porcentaje;
	}
	//Menu D2
	public void setOp1menuD2(string valor){
		op1menuD2.text = valor;
		aguja1.rotation = Quaternion.Euler(new Vector3 (0f, 0f, -360f * float.Parse (valor) / 4000f));
	}
	public void setOp2menuD2(string valor){
		op2menuD2.text = valor;
		aguja2.rotation = Quaternion.Euler(new Vector3 (0f, 0f, -360f * float.Parse (valor) / 200f)); 
	}
	public void setOp3menuD2(string valor){
		op3menuD2.text = valor;
		aguja3.rotation = Quaternion.Euler(new Vector3 (0f, 0f, -360f * float.Parse (valor) / 8f)); 
	}
	//Menu D3
	public void setOp1menuD3(string valor){
		op1menuD3.text = valor;
			aguja1D3.rotation = Quaternion.Euler(new Vector3 (0f, 0f, -360f * float.Parse (valor) / 200f)); 
	}
	public void setOp2menuD3(string valor){
		op2menuD3.text = valor;
		aguja2D3.rotation = Quaternion.Euler(new Vector3 (0f, 0f, -360f * float.Parse (valor) / 40f)); 
	}
	public void setOp3menuD3(string valor){
		op3menuD3.text = valor;
		aguja3D3.rotation = Quaternion.Euler(new Vector3 (0f, 0f, -360f * float.Parse (valor) / 400f)); 
	}

	// Use this for initialization
	void Start () {
		configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles>();
		mostrarMenuOperador ();
		boostPres = percentTorque.transform.parent.FindChild ("LabelBoost").GetComponent<UILabel> ();
		setBatteryVoltage ("25");
	}

	void BotonDown(int indice){
		//if (estado == EstadoMaquina.apagadaTotal)
		//	return;
		print ("recibido nuevo indice " + indice);
		indice = indice + 1;
		if(configuracionControles == null) configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles>();
		if(indice == configuracionControles.idJIzquierdoBotonSupIzq) setBoton1Izq(true);
		if(indice == configuracionControles.idJIzquierdoBotonSupDer) setBoton2Izq(true);
		if(indice == configuracionControles.idJIzquierdoBotonInfIzq) setBoton3Izq(true);
		if(indice == configuracionControles.idJIzquierdoBotonInfDer) setBoton4Izq(true);
		if(indice == configuracionControles.idJIzquierdoGatillo) setBotonGatilloIzq(true);
		if(indice == configuracionControles.idJDerechoBotonSupIzq) setBoton1Der(true);
		if(indice == configuracionControles.idJDerechoBotonSupDer) setBoton2Der(true);
		if(indice == configuracionControles.idJDerechoBotonInfIzq) setBoton3Der(true);
		if(indice == configuracionControles.idJDerechoBotonInfDer) setBoton4Der(true);
		if(indice == configuracionControles.idJDerechoGatillo) setBotonGatilloDer(true);

	}
	void BotonUp(int indice){
		//if (estado == EstadoMaquina.apagadaTotal)
		//	return;
		print ("recibido nuevo indice up " + indice);
		indice = indice + 1;
		if(configuracionControles == null) configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles>();
		if(indice == configuracionControles.idJIzquierdoBotonSupIzq) setBoton1Izq(false);
		if(indice == configuracionControles.idJIzquierdoBotonSupDer) setBoton2Izq(false);
		if(indice == configuracionControles.idJIzquierdoBotonInfIzq) setBoton3Izq(false);
		if(indice == configuracionControles.idJIzquierdoBotonInfDer) setBoton4Izq(false);
		if(indice == configuracionControles.idJIzquierdoGatillo) setBotonGatilloIzq(false);
		if(indice == configuracionControles.idJDerechoBotonSupIzq) setBoton1Der(false);
		if(indice == configuracionControles.idJDerechoBotonSupDer) setBoton2Der(false);
		if(indice == configuracionControles.idJDerechoBotonInfIzq) setBoton3Der(false);
		if(indice == configuracionControles.idJDerechoBotonInfDer) setBoton4Der(false);
		if(indice == configuracionControles.idJDerechoGatillo) setBotonGatilloDer(false);
	}
	
	// Update is called once per frame
	void Update () {

		horaLabel.text = "" + System.DateTime.Now.ToString("hh:mm"); 
		segundosLabel.text = "" + System.DateTime.Now.ToString(":ss"); 
		string dia = "Lunes";
		switch (System.DateTime.Now.DayOfWeek) {
		case System.DayOfWeek.Tuesday:
			dia = "Martes"; 
			break;
		case System.DayOfWeek.Wednesday:
			dia = "Miércoles"; 
			break;
		case System.DayOfWeek.Thursday:
			dia = "Jueves"; 
			break;
		case System.DayOfWeek.Friday:
			dia = "Viernes"; 
			break;
		case System.DayOfWeek.Saturday:
			dia = "Sábado"; 
			break;
		case System.DayOfWeek.Sunday:
			dia = "Domingo"; 
			break;
		}
		diaLabel.text = dia;

		tiempoMotorFuncionando += motorFuncionando ? Time.deltaTime : 0f;
		float tiempoActual=tiempoMotorFuncionando;
		int minutos=(int)tiempoActual/60;
		
		int mseg=(int)(tiempoActual-tiempoActual*100);
		op3menuD1.text=""+(0+(int)minutos/60)+":"+((minutos%60)<10?"0":"") + (minutos%60);
		//op3menuD1.text = "" + (Mathf.CeilToInt(tiempoMotorFuncionando * 100 / 3600f) * 0.01f);

		float throttle = Input.GetAxis ("Joy1 Axis 5");
		float brake = Input.GetAxis ("Joy1 Axis 6");
		#if !UNITY_EDITOR
		//throttle = ((valoresPotenciometro[0] * 1f) - 310f) / 520f;
		//brake = ((valoresPotenciometro[1] * 1f) - 310f) / 520f;
		throttle = Mathf.Clamp(((valoresPotenciometro[0] * 1f) - configuracionControles.aceleradorRangoMinimo), 0f, configuracionControles.aceleradorRangoMaximo - configuracionControles.aceleradorRangoMinimo) / ((configuracionControles.aceleradorRangoMaximo - configuracionControles.aceleradorRangoMinimo) / 2f);
		brake = Mathf.Clamp(((valoresPotenciometro[1] * 1f) - configuracionControles.frenoRangoMinimo), 0f, configuracionControles.frenoRangoMaximo - configuracionControles.frenoRangoMinimo) / ((configuracionControles.frenoRangoMaximo - configuracionControles.frenoRangoMinimo) / 2f);
		#endif
		float pala = 0f;
		float brazo = 0f;
		float direccion = 0f;
		#if UNITY_EDITOR
		pala = Input.GetAxis ("CucharaEditor");
		brazo = Input.GetAxis ("ArmEditor");
		direccion = Input.GetAxis ("Joy1 Axis 2");
		#else
		pala = ((valoresPotenciometro[4] * 1f) - ((configuracionControles.joystickDerechoXRangoMaximo - configuracionControles.joystickDerechoXRangoMinimo)/2f)) / ((configuracionControles.joystickDerechoXRangoMaximo - configuracionControles.joystickDerechoXRangoMinimo) / 2f);
		brazo = ((valoresPotenciometro[5] * 1f)  - ((configuracionControles.joystickDerechoYRangoMaximo - configuracionControles.joystickDerechoYRangoMinimo)/2f)) / ((configuracionControles.joystickDerechoYRangoMaximo - configuracionControles.joystickDerechoYRangoMinimo) / 2f);

		direccion = ((valoresPotenciometro[3] * 1f) - ((configuracionControles.joystickIzquierdoYRangoMaximo - configuracionControles.joystickIzquierdoYRangoMinimo)/2f)) / ((configuracionControles.joystickIzquierdoYRangoMaximo - configuracionControles.joystickIzquierdoYRangoMinimo) / 2f);

		pala = VariablesGlobales.calcularPresicion(pala);
		brazo = VariablesGlobales.calcularPresicion(brazo);
		direccion = VariablesGlobales.calcularPresicion(direccion);
		#endif
		
		setOp3menuD3 ("" + Mathf.RoundToInt(100f * brake * 100f) * 0.01f);

		setThrottle (throttle);
		setBrake (brake);
		//print (throttle + " " + brake);
		setJoyYIzq (-direccion);
		setJoyXDer (pala);
		setJoyYDer (-brazo);
	}
}
