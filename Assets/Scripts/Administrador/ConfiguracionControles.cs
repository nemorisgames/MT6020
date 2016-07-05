using UnityEngine;
using System.Collections;

public class ConfiguracionControles : MonoBehaviour {
	//[HideInInspector]
	public int COM = 6;

	//[HideInInspector]
	public int idBotonLucesAltasDelanteras = 5;
	//[HideInInspector]
	public int idBotonLucesAltasTraseras = 4;
	//[HideInInspector]
	public int idBotonLucesBajasDelanteras = 3;
	//[HideInInspector]
	public int idBotonLucesBajasTraseras = 2;
	
	//[HideInInspector]
	public int idBotonEncendido = 19;
	//[HideInInspector]
	public int idBotonOverride = 20;
	//[HideInInspector]
	public int idBotonPruebaFrenos = 1;
	//[HideInInspector]
	public int idBotonRRC = 18; //no va
	//[HideInInspector]
	public int idBotonTransmisionAutomatica = 9;
	//[HideInInspector]
	public int idBotonDesembragar = 6; // no va
	//[HideInInspector]
	public int idBotonClaxon = 7;

	//[HideInInspector]
	public int idBotonRideControl = 8;
	
	//[HideInInspector]
	public int idBotonCambio1 = 17;
	//[HideInInspector]
	public int idBotonCambio2 = 15;
	//[HideInInspector]
	public int idBotonCambio3 = 16;
	//[HideInInspector]
	public int idBotonCambio4 = 14;
	
	//[HideInInspector]
	public int idBotonFrenoParqueo = 13;
	//[HideInInspector]
	public int idBotonDisplayON = 11;
	//[HideInInspector]
	public int idBotonDisplayOFF = 12;

	//[HideInInspector]
	public int idBotonAux = 10;

	//Leds
	//[HideInInspector]
	public int idLedLucesAltasDelanteras = 16;
	//[HideInInspector]
	public int idLedLucesAltasTraseras = 14;
	//[HideInInspector]
	public int idLedLucesBajasDelanteras = 17;
	//[HideInInspector]
	public int idLedLucesBajasTraseras = 15;
	
	//[HideInInspector]
	public int idLedEncendido = 39;
	//[HideInInspector]
	public int idLedPruebaFrenos = 38;
	//[HideInInspector]
	public int idLedOverride = 41;
	//[HideInInspector]
	public int idLedRRC = 43;
	//[HideInInspector]
	public int idLedTransmisionAutomatica = 21;
	//[HideInInspector]
	public int idLedDesembragar = 20;
	//[HideInInspector]
	public int idLedClaxon = 23;
	
	//[HideInInspector]
	public int idLedRideControl = 22;
	
	//[HideInInspector]
	public int idLedCambio1 = 45;
	//[HideInInspector]
	public int idLedCambio2 = 42;
	//[HideInInspector]
	public int idLedCambio3 = 44;
	//[HideInInspector]
	public int idLedCambio4 = 47;
	
	public int idLedFrenoParqueo = 0;
	//[HideInInspector]
	public int idLedDisplayON = 19;
	//[HideInInspector]
	public int idLedDisplayOFF = 18;
	
	//int idLedAux = 10;

	//joysticks
	//[HideInInspector]
	public int idJDerechoGatillo = 60;
	//[HideInInspector]
	public int idJDerechoBotonSupIzq = 61;
	//[HideInInspector]
	public int idJDerechoBotonSupDer = 62;
	//[HideInInspector]
	public int idJDerechoBotonInfIzq = 63;
	//[HideInInspector]
	public int idJDerechoBotonInfDer = 64;
	
	//[HideInInspector]
	public int idJIzquierdoGatillo = 65;
	//[HideInInspector]
	public int idJIzquierdoBotonSupIzq = 66;
	//[HideInInspector]
	public int idJIzquierdoBotonSupDer = 67;
	//[HideInInspector]
	public int idJIzquierdoBotonInfIzq = 68;
	//[HideInInspector]
	public int idJIzquierdoBotonInfDer = 69;

	//potenciometros
	//[HideInInspector]
	public int idFreno = 0;
	//[HideInInspector]
	public int idAcelerador = 1;
	//[HideInInspector]
	public int idJoystickIzquierdoX = 2;
	//[HideInInspector]
	public int idJoystickIzquierdoY = 3;
	//[HideInInspector]
	public int idJoystickDerechoX = 4;
	//[HideInInspector]
	public int idJoystickDerechoY = 5;

	//[HideInInspector]
	public int frenoRangoMinimo = 310;
	//[HideInInspector]
	public int frenoRangoMaximo = 830;
	//[HideInInspector]
	public int aceleradorRangoMinimo = 310;
	//[HideInInspector]
	public int aceleradorRangoMaximo = 830;
	//[HideInInspector]
	public int joystickIzquierdoXRangoMinimo = 0;
	//[HideInInspector]
	public int joystickIzquierdoXRangoMaximo = 1023;
	//[HideInInspector]
	public int joystickIzquierdoYRangoMinimo = 0;
	//[HideInInspector]
	public int joystickIzquierdoYRangoMaximo = 1023;
	//[HideInInspector]
	public int joystickDerechoXRangoMinimo = 0;
	//[HideInInspector]
	public int joystickDerechoXRangoMaximo = 1023;
	//[HideInInspector]
	public int joystickDerechoYRangoMinimo = 0;
	//[HideInInspector]
	public int joystickDerechoYRangoMaximo = 1023;


	// Use this for initialization
	void Start () {
		cargarPlayerPrefs ();
		DontDestroyOnLoad (gameObject);
	}

	public void guardarConfiguracion(){
		PlayerPrefs.SetInt ("COM", COM);
		PlayerPrefs.SetInt ("idBotonLucesAltasDelanteras", idBotonLucesAltasDelanteras);
		PlayerPrefs.SetInt ("idBotonLucesAltasTraseras", idBotonLucesAltasTraseras);
		PlayerPrefs.SetInt ("idBotonLucesBajasDelanteras", idBotonLucesBajasDelanteras);
		PlayerPrefs.SetInt ("idBotonLucesBajasTraseras", idBotonLucesBajasTraseras);
		PlayerPrefs.SetInt ("idBotonEncendido", idBotonEncendido);
		PlayerPrefs.SetInt ("idBotonPruebaFrenos", idBotonPruebaFrenos);
		PlayerPrefs.SetInt ("idBotonOverride", idBotonOverride);
		PlayerPrefs.SetInt ("idBotonRRC", idBotonRRC);
		PlayerPrefs.SetInt ("idBotonTransmisionAutomatica", idBotonTransmisionAutomatica);
		PlayerPrefs.SetInt ("idBotonDesembragar", idBotonDesembragar);
		PlayerPrefs.SetInt ("idBotonClaxon", idBotonClaxon);
		PlayerPrefs.SetInt ("idBotonRideControl", idBotonRideControl);
		PlayerPrefs.SetInt ("idBotonCambio1", idBotonCambio1);
		PlayerPrefs.SetInt ("idBotonCambio2", idBotonCambio2);
		PlayerPrefs.SetInt ("idBotonCambio3", idBotonCambio3);
		PlayerPrefs.SetInt ("idBotonCambio4", idBotonCambio4);
		PlayerPrefs.SetInt ("idBotonFrenoParqueo", idBotonFrenoParqueo);
		PlayerPrefs.SetInt ("idBotonDisplayON", idBotonDisplayON);
		PlayerPrefs.SetInt ("idBotonDisplayOFF", idBotonDisplayOFF);
		PlayerPrefs.SetInt ("idBotonAux", idBotonAux);
		PlayerPrefs.SetInt ("idLedLucesAltasDelanteras", idLedLucesAltasDelanteras);
		PlayerPrefs.SetInt ("idLedLucesAltasTraseras", idLedLucesAltasTraseras);
		PlayerPrefs.SetInt ("idLedLucesBajasDelanteras", idLedLucesBajasDelanteras);
		PlayerPrefs.SetInt ("idLedLucesBajasTraseras", idLedLucesBajasTraseras);
		PlayerPrefs.SetInt ("idLedEncendido", idLedEncendido);
		PlayerPrefs.SetInt ("idLedPruebaFrenos", idLedPruebaFrenos);
		PlayerPrefs.SetInt ("idLedOverride", idLedOverride);
		PlayerPrefs.SetInt ("idLedRRC", idLedRRC);
		PlayerPrefs.SetInt ("idLedTransmisionAutomatica", idLedTransmisionAutomatica);
		PlayerPrefs.SetInt ("idLedDesembragar", idLedDesembragar);
		PlayerPrefs.SetInt ("idLedClaxon", idLedClaxon);
		PlayerPrefs.SetInt ("idLedRideControl", idLedRideControl);
		PlayerPrefs.SetInt ("idLedCambio1", idLedCambio1);
		PlayerPrefs.SetInt ("idLedCambio2", idLedCambio2);
		PlayerPrefs.SetInt ("idLedCambio3", idLedCambio3);
		PlayerPrefs.SetInt ("idLedCambio4", idLedCambio4);
		PlayerPrefs.SetInt ("idLedDisplayON", idLedDisplayON);
		PlayerPrefs.SetInt ("idLedDisplayOFF", idLedDisplayOFF);
		PlayerPrefs.SetInt ("idJDerechoGatillo", idJDerechoGatillo);
		PlayerPrefs.SetInt ("idJDerechoBotonSupIzq", idJDerechoBotonSupIzq);
		PlayerPrefs.SetInt ("idJDerechoBotonSupDer", idJDerechoBotonSupDer);
		PlayerPrefs.SetInt ("idJDerechoBotonInfIzq", idJDerechoBotonInfIzq);
		PlayerPrefs.SetInt ("idJDerechoBotonInfDer", idJDerechoBotonInfDer);
		PlayerPrefs.SetInt ("idJIzquierdoGatillo", idJIzquierdoGatillo);
		PlayerPrefs.SetInt ("idJIzquierdoBotonSupIzq", idJIzquierdoBotonSupIzq);
		PlayerPrefs.SetInt ("idJIzquierdoBotonSupDer", idJIzquierdoBotonSupDer);
		PlayerPrefs.SetInt ("idJIzquierdoBotonInfIzq", idJIzquierdoBotonInfIzq);
		PlayerPrefs.SetInt ("idJIzquierdoBotonInfDer", idJIzquierdoBotonInfDer);
		PlayerPrefs.SetInt ("idFreno", idFreno);
		PlayerPrefs.SetInt ("idAcelerador", idAcelerador);
		PlayerPrefs.SetInt ("idJoystickIzquierdoX", idJoystickIzquierdoX);
		PlayerPrefs.SetInt ("idJoystickIzquierdoY", idJoystickIzquierdoY);
		PlayerPrefs.SetInt ("idJoystickDerechoX", idJoystickDerechoX);
		PlayerPrefs.SetInt ("idJoystickDerechoY", idJoystickDerechoY);

		PlayerPrefs.SetInt ("frenoRangoMinimo", frenoRangoMinimo);
		PlayerPrefs.SetInt ("frenoRangoMaximo", frenoRangoMaximo);
		PlayerPrefs.SetInt ("aceleradorRangoMinimo", aceleradorRangoMinimo);
		PlayerPrefs.SetInt ("aceleradorRangoMaximo", aceleradorRangoMaximo);
		PlayerPrefs.SetInt ("joystickIzquierdoXRangoMinimo", joystickIzquierdoXRangoMinimo);
		PlayerPrefs.SetInt ("joystickIzquierdoXRangoMaximo", joystickIzquierdoXRangoMaximo);
		PlayerPrefs.SetInt ("joystickIzquierdoYRangoMinimo", joystickIzquierdoYRangoMinimo);
		PlayerPrefs.SetInt ("joystickIzquierdoYRangoMaximo", joystickIzquierdoYRangoMaximo);
		PlayerPrefs.SetInt (" joystickDerechoXRangoMinimo", joystickDerechoXRangoMinimo);
		PlayerPrefs.SetInt ("joystickDerechoXRangoMaximo", joystickDerechoXRangoMaximo);
		PlayerPrefs.SetInt ("joystickDerechoYRangoMinimo", joystickDerechoYRangoMinimo);
		PlayerPrefs.SetInt ("joystickDerechoYRangoMaximo", joystickDerechoYRangoMaximo);
	}

	public void cargarPlayerPrefs(){
		COM = PlayerPrefs.GetInt ("COM", COM);
		idBotonLucesAltasDelanteras = PlayerPrefs.GetInt ("idBotonLucesAltasDelanteras", idBotonLucesAltasDelanteras);
		idBotonLucesAltasTraseras = PlayerPrefs.GetInt ("idBotonLucesAltasTraseras", idBotonLucesAltasTraseras);
		idBotonLucesBajasDelanteras = PlayerPrefs.GetInt ("idBotonLucesBajasDelanteras", idBotonLucesBajasDelanteras);
		idBotonLucesBajasTraseras = PlayerPrefs.GetInt ("idBotonLucesBajasTraseras", idBotonLucesBajasTraseras);
		idBotonEncendido = PlayerPrefs.GetInt ("idBotonEncendido", idBotonEncendido);
		idBotonPruebaFrenos = PlayerPrefs.GetInt ("idBotonPruebaFrenos", idBotonPruebaFrenos);
		idBotonOverride = PlayerPrefs.GetInt ("idBotonOverride", idBotonOverride);
		idBotonRRC = PlayerPrefs.GetInt ("idBotonRRC", idBotonRRC);
		idBotonTransmisionAutomatica = PlayerPrefs.GetInt ("idBotonTransmisionAutomatica", idBotonTransmisionAutomatica);
		idBotonDesembragar = PlayerPrefs.GetInt ("idBotonDesembragar", idBotonDesembragar);
		idBotonClaxon = PlayerPrefs.GetInt ("idBotonClaxon", idBotonClaxon);
		idBotonRideControl = PlayerPrefs.GetInt ("idBotonRideControl", idBotonRideControl);
		idBotonCambio1 = PlayerPrefs.GetInt ("idBotonCambio1", idBotonCambio1);
		idBotonCambio2 = PlayerPrefs.GetInt ("idBotonCambio2", idBotonCambio2);
		idBotonCambio3 = PlayerPrefs.GetInt ("idBotonCambio3", idBotonCambio3);
		idBotonCambio4 = PlayerPrefs.GetInt ("idBotonCambio4", idBotonCambio4);
		idBotonFrenoParqueo = PlayerPrefs.GetInt ("idBotonFrenoParqueo", idBotonFrenoParqueo);
		idBotonDisplayON = PlayerPrefs.GetInt ("idBotonDisplayON", idBotonDisplayON);
		idBotonDisplayOFF = PlayerPrefs.GetInt ("idBotonDisplayOFF", idBotonDisplayOFF);
		idBotonAux = PlayerPrefs.GetInt ("idBotonAux", idBotonAux);
		idLedLucesAltasDelanteras = PlayerPrefs.GetInt ("idLedLucesAltasDelanteras", idLedLucesAltasDelanteras);
		idLedLucesAltasTraseras = PlayerPrefs.GetInt ("idLedLucesAltasTraseras", idLedLucesAltasTraseras);
		idLedLucesBajasDelanteras = PlayerPrefs.GetInt ("idLedLucesBajasDelanteras", idLedLucesBajasDelanteras);
		idLedLucesBajasTraseras = PlayerPrefs.GetInt ("idLedLucesBajasTraseras", idLedLucesBajasTraseras);
		idLedEncendido = PlayerPrefs.GetInt ("idLedEncendido", idLedEncendido);
		idLedPruebaFrenos = PlayerPrefs.GetInt ("idLedPruebaFrenos", idLedPruebaFrenos);
		idLedOverride = PlayerPrefs.GetInt ("idLedOverride", idLedOverride);
		idLedRRC = PlayerPrefs.GetInt ("idLedRRC", idLedRRC);
		idLedTransmisionAutomatica = PlayerPrefs.GetInt ("idLedTransmisionAutomatica", idLedTransmisionAutomatica);
		idLedDesembragar = PlayerPrefs.GetInt ("idLedDesembragar", idLedDesembragar);
		idLedClaxon = PlayerPrefs.GetInt ("idLedClaxon", idLedClaxon);
		idLedRideControl = PlayerPrefs.GetInt ("idLedRideControl", idLedRideControl);
		idLedCambio1 = PlayerPrefs.GetInt ("idLedCambio1", idLedCambio1);
		idLedCambio2 = PlayerPrefs.GetInt ("idLedCambio2", idLedCambio2);
		idLedCambio3 = PlayerPrefs.GetInt ("idLedCambio3", idLedCambio3);
		idLedCambio4 = PlayerPrefs.GetInt ("idLedCambio4", idLedCambio4);
		idLedDisplayON = PlayerPrefs.GetInt ("idLedDisplayON", idLedDisplayON);
		idLedDisplayOFF = PlayerPrefs.GetInt ("idLedDisplayOFF", idLedDisplayOFF);
		idJDerechoGatillo = PlayerPrefs.GetInt ("idJDerechoGatillo", idJDerechoGatillo);
		idJDerechoBotonSupIzq = PlayerPrefs.GetInt ("idJDerechoBotonSupIzq", idJDerechoBotonSupIzq);
		idJDerechoBotonSupDer = PlayerPrefs.GetInt ("idJDerechoBotonSupDer", idJDerechoBotonSupDer);
		idJDerechoBotonInfIzq = PlayerPrefs.GetInt ("idJDerechoBotonInfIzq", idJDerechoBotonInfIzq);
		idJDerechoBotonInfDer = PlayerPrefs.GetInt ("idJDerechoBotonInfDer", idJDerechoBotonInfDer);
		idJIzquierdoGatillo = PlayerPrefs.GetInt ("idJIzquierdoGatillo", idJIzquierdoGatillo);
		idJIzquierdoBotonSupIzq = PlayerPrefs.GetInt ("idJIzquierdoBotonSupIzq", idJIzquierdoBotonSupIzq);
		idJIzquierdoBotonSupDer = PlayerPrefs.GetInt ("idJIzquierdoBotonSupDer", idJIzquierdoBotonSupDer);
		idJIzquierdoBotonInfIzq = PlayerPrefs.GetInt ("idJIzquierdoBotonInfIzq", idJIzquierdoBotonInfIzq);
		idJIzquierdoBotonInfDer = PlayerPrefs.GetInt ("idJIzquierdoBotonInfDer", idJIzquierdoBotonInfDer);
		idFreno = PlayerPrefs.GetInt ("idFreno", idFreno);
		idAcelerador = PlayerPrefs.GetInt ("idAcelerador", idAcelerador);
		idJoystickIzquierdoX = PlayerPrefs.GetInt ("idJoystickIzquierdoX", idJoystickIzquierdoX);
		idJoystickIzquierdoY = PlayerPrefs.GetInt ("idJoystickIzquierdoY", idJoystickIzquierdoY);
		idJoystickDerechoX = PlayerPrefs.GetInt ("idJoystickDerechoX", idJoystickDerechoX);
		idJoystickDerechoY = PlayerPrefs.GetInt ("idJoystickDerechoY", idJoystickDerechoY);

		frenoRangoMinimo = PlayerPrefs.GetInt ("frenoRangoMinimo", frenoRangoMinimo);
		frenoRangoMaximo = PlayerPrefs.GetInt ("frenoRangoMaximo", frenoRangoMaximo);
		aceleradorRangoMinimo = PlayerPrefs.GetInt ("aceleradorRangoMinimo", aceleradorRangoMinimo);
		aceleradorRangoMaximo = PlayerPrefs.GetInt ("aceleradorRangoMaximo", aceleradorRangoMaximo);
		joystickIzquierdoXRangoMinimo = PlayerPrefs.GetInt ("joystickIzquierdoXRangoMinimo", joystickIzquierdoXRangoMinimo);
		joystickIzquierdoXRangoMaximo = PlayerPrefs.GetInt ("joystickIzquierdoXRangoMaximo", joystickIzquierdoXRangoMaximo);
		joystickIzquierdoYRangoMinimo = PlayerPrefs.GetInt ("joystickIzquierdoYRangoMinimo", joystickIzquierdoYRangoMinimo);
		joystickIzquierdoYRangoMaximo = PlayerPrefs.GetInt ("joystickIzquierdoYRangoMaximo", joystickIzquierdoYRangoMaximo);
		joystickDerechoXRangoMinimo = PlayerPrefs.GetInt (" joystickDerechoXRangoMinimo", joystickDerechoXRangoMinimo);
		joystickDerechoXRangoMaximo = PlayerPrefs.GetInt ("joystickDerechoXRangoMaximo", joystickDerechoXRangoMaximo);
		joystickDerechoYRangoMinimo = PlayerPrefs.GetInt ("joystickDerechoYRangoMinimo", joystickDerechoYRangoMinimo);
		joystickDerechoYRangoMaximo = PlayerPrefs.GetInt ("joystickDerechoYRangoMaximo", joystickDerechoYRangoMaximo);

	}

	void OnLevelWasLoaded(int level){
		cargarPlayerPrefs ();
	}

	/*void OnGUI(){
		if (GUI.Button (new Rect (Screen.width * 7f / 10f, 200f, 100, 50), "invertir")) {
			int auxX = idJoystickIzquierdoX;
			int auxY = idJoystickIzquierdoY;
			idJoystickIzquierdoX = idJoystickDerechoX;
			idJoystickIzquierdoY = idJoystickDerechoY;
			idJoystickDerechoX = auxX;
			idJoystickDerechoY = auxY;
		}
	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
}
