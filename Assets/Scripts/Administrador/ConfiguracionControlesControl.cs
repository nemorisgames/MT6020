using UnityEngine;
using System.Collections;

public class ConfiguracionControlesControl : MonoBehaviour {
	public bool esperandoEntrada = false;
	public UIInput COMLabel;
	public ConfiguracionControlesEntradaControl[] entradas;
	public ConfiguracionControlesPotenciometroControl[] potenciometros;
	public ConfiguracionControlesEntradaControl entradaActual;
	ConfiguracionControles configuracionControles;
	float tiempoDelay;

	// Use this for initialization
	void Start () {
		configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles>();
		setValores ();

	}

	void setValores(){
		COMLabel.text = "" + configuracionControles.COM;
		for (int i = 0; i < entradas.Length; i++) {
			entradas [i].indice = i;
			switch (entradas [i].indice) {
			case 0:
				entradas [i].setID (configuracionControles.idBotonLucesAltasDelanteras);
				entradas [i].setIDLed (configuracionControles.idLedLucesAltasDelanteras);
				break;
			case 1:
				entradas [i].setID (configuracionControles.idBotonLucesAltasTraseras);
				entradas [i].setIDLed (configuracionControles.idLedLucesAltasTraseras);
				break;
			case 2:
				entradas [i].setID (configuracionControles.idBotonLucesBajasDelanteras);
				entradas [i].setIDLed (configuracionControles.idLedLucesBajasDelanteras);
				break;
			case 3:
				entradas [i].setID (configuracionControles.idBotonLucesBajasTraseras);
				entradas [i].setIDLed (configuracionControles.idLedLucesBajasTraseras);
				break;
			case 4:
				entradas [i].setID (configuracionControles.idBotonEncendido);
				entradas [i].setIDLed (configuracionControles.idLedEncendido);
				break;
			case 5:
				entradas [i].setID (configuracionControles.idBotonOverride);
				entradas [i].setIDLed (configuracionControles.idLedOverride);
				break;
			case 6:
				entradas [i].setID (configuracionControles.idBotonPruebaFrenos);
				entradas [i].setIDLed (configuracionControles.idLedPruebaFrenos);
				break;
			case 7:
				entradas [i].setID (configuracionControles.idBotonRRC);
				entradas [i].setIDLed (configuracionControles.idLedRRC);
				break;
			case 8:
				entradas [i].setID (configuracionControles.idBotonTransmisionAutomatica);
				entradas [i].setIDLed (configuracionControles.idLedTransmisionAutomatica);
				break;
			case 9:
				entradas [i].setID (configuracionControles.idBotonDesembragar);
				entradas [i].setIDLed (configuracionControles.idLedDesembragar);
				break;
			case 10:
				entradas [i].setID (configuracionControles.idBotonClaxon);
				entradas [i].setIDLed (configuracionControles.idLedClaxon);
				break;
			case 11:
				entradas [i].setID (configuracionControles.idBotonRideControl);
				entradas [i].setIDLed (configuracionControles.idLedRideControl);
				break;
			case 12:
				entradas [i].setID (configuracionControles.idBotonCambio1);
				entradas [i].setIDLed (configuracionControles.idLedCambio1);
				break;
			case 13:
				entradas [i].setID (configuracionControles.idBotonCambio2);
				entradas [i].setIDLed (configuracionControles.idLedCambio2);
				break;
			case 14:
				entradas [i].setID (configuracionControles.idBotonCambio3);
				entradas [i].setIDLed (configuracionControles.idLedCambio3);
				break;
			case 15:
				entradas [i].setID (configuracionControles.idBotonCambio4);
				entradas [i].setIDLed (configuracionControles.idLedCambio4);
				break;
			case 16:
				entradas [i].setID (configuracionControles.idBotonFrenoParqueo);
				entradas [i].setIDLed (configuracionControles.idLedFrenoParqueo);
				break;
			case 17:
				entradas [i].setID (configuracionControles.idBotonDisplayON);
				entradas [i].setIDLed (configuracionControles.idLedDisplayON);
				break;
			case 18:
				entradas [i].setID (configuracionControles.idBotonDisplayOFF);
				entradas [i].setIDLed (configuracionControles.idLedDisplayOFF);
				break;
			case 19:
				entradas [i].setID (configuracionControles.idJDerechoBotonSupIzq);
				break;
			case 20:
				entradas [i].setID (configuracionControles.idJDerechoBotonSupDer);
				break;
			case 21:
				entradas [i].setID (configuracionControles.idJDerechoBotonInfIzq);
				break;
			case 22:
				entradas [i].setID (configuracionControles.idJDerechoBotonInfDer);
				break;
			case 23:
				entradas [i].setID (configuracionControles.idJDerechoGatillo);
				break;
			case 24:
				entradas [i].setID (configuracionControles.idJIzquierdoBotonSupIzq);
				break;
			case 25:
				entradas [i].setID (configuracionControles.idJIzquierdoBotonSupDer);
				break;
			case 26:
				entradas [i].setID (configuracionControles.idJIzquierdoBotonInfIzq);
				break;
			case 27:
				entradas [i].setID (configuracionControles.idJIzquierdoBotonInfDer);
				break;
			case 28:
				entradas [i].setID (configuracionControles.idJIzquierdoGatillo);
				break;
			}

		}

		for (int j = 0; j < potenciometros.Length; j++) {
			potenciometros[j].cargarValores();
		}
	}

	public void cambiarLED(int indice, int idLed){
		switch (indice) {
		case 0: configuracionControles.idLedLucesAltasDelanteras = idLed; break;
		case 1: configuracionControles.idLedLucesAltasTraseras = idLed; break;
		case 2: configuracionControles.idLedLucesBajasDelanteras = idLed; break;
		case 3: configuracionControles.idLedLucesBajasTraseras = idLed; break;
		case 4: configuracionControles.idLedEncendido = idLed; break;
		case 5: configuracionControles.idLedOverride = idLed; break;
		case 6: configuracionControles.idLedPruebaFrenos = idLed; break;
		case 7: configuracionControles.idLedRRC = idLed; break;
		case 8: configuracionControles.idLedTransmisionAutomatica = idLed; break;
		case 9: configuracionControles.idLedDesembragar = idLed; break;
		case 10: configuracionControles.idLedClaxon = idLed; break;
		case 11: configuracionControles.idLedRideControl = idLed; break;
		case 12: configuracionControles.idLedCambio1 = idLed; break;
		case 13: configuracionControles.idLedCambio2 = idLed; break;
		case 14: configuracionControles.idLedCambio3 = idLed; break;
		case 15: configuracionControles.idLedCambio4 = idLed; break;
		case 16: configuracionControles.idLedFrenoParqueo = idLed; break;
		case 17: configuracionControles.idLedDisplayON = idLed; break;
		case 18: configuracionControles.idLedDisplayOFF = idLed; break;
		}
	}


	public void configurarBoton(ConfiguracionControlesEntradaControl entrada){
		esperandoEntrada = true;
		tiempoDelay = Time.time + 0.3f;
		entradaActual = entrada;
	}

	void BotonUp(int indice){
		if (Time.time < tiempoDelay)
			return;
		if (esperandoEntrada) {
			indice = indice + 1;
			entradaActual.setID(indice);
			switch(entradaActual.indice){
			case 0: configuracionControles.idBotonLucesAltasDelanteras = indice; break;
			case 1: configuracionControles.idBotonLucesAltasTraseras = indice; break;
			case 2: configuracionControles.idBotonLucesBajasDelanteras = indice; break;
			case 3: configuracionControles.idBotonLucesBajasTraseras = indice; break;
			case 4: configuracionControles.idBotonEncendido = indice; break;
			case 5: configuracionControles.idBotonOverride = indice; break;
			case 6: configuracionControles.idBotonPruebaFrenos = indice; break;
			case 7: configuracionControles.idBotonRRC = indice; break;
			case 8: configuracionControles.idBotonTransmisionAutomatica = indice; break;
			case 9: configuracionControles.idBotonDesembragar = indice; break;
			case 10: configuracionControles.idBotonClaxon = indice; break;
			case 11: configuracionControles.idBotonRideControl = indice; break;
			case 12: configuracionControles.idBotonCambio1 = indice; break;
			case 13: configuracionControles.idBotonCambio2 = indice; break;
			case 14: configuracionControles.idBotonCambio3 = indice; break;
			case 15: configuracionControles.idBotonCambio4 = indice; break;
			case 16: configuracionControles.idBotonFrenoParqueo = indice; break;
			case 17: configuracionControles.idBotonDisplayON = indice; break;
			case 18: configuracionControles.idBotonDisplayOFF = indice; break;
			case 19: configuracionControles.idJDerechoBotonSupIzq = indice; break;
			case 20: configuracionControles.idJDerechoBotonSupDer = indice; break;
			case 21: configuracionControles.idJDerechoBotonInfIzq = indice; break;
			case 22: configuracionControles.idJDerechoBotonInfDer = indice; break;
			case 23: configuracionControles.idJDerechoGatillo = indice; break;
			case 24: configuracionControles.idJIzquierdoBotonSupIzq = indice; break;
			case 25: configuracionControles.idJIzquierdoBotonSupDer = indice; break;
			case 26: configuracionControles.idJIzquierdoBotonInfIzq = indice; break;
			case 27: configuracionControles.idJIzquierdoBotonInfDer = indice; break;
			case 28: configuracionControles.idJIzquierdoGatillo = indice; break;
			}
			esperandoEntrada = false;
		}
	}

	public void cambiarCOM(string valor){
		int valorInt = 0;
		if (int.TryParse (valor, out valorInt)) {
			configuracionControles.COM = valorInt;
		}
	}

	public void guardar(){
		configuracionControles.guardarConfiguracion ();
	}

	public void reset(){
		configuracionControles.cargarPlayerPrefs ();
		setValores ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
