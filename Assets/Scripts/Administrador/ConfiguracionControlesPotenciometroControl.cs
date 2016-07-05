using UnityEngine;
using System.Collections;

public class ConfiguracionControlesPotenciometroControl : MonoBehaviour {
	/*public int id;
	public int indice;
	string textoBoton;
	public UILabel botonLabel;
	public UILabel idLabel;*/
	public enum PotenciometroTipo
	{
		PotenciometroFreno,
		PotenciometroAcelerador, 
		PotenciometroJoyIzqX,
		PotenciometroJoyIzqY,
		PotenciometroJoyDerX,
		PotenciometroJoyDerY
	};
	public PotenciometroTipo tipo;
	public UIPopupList listaAnalogos;
	public UISlider lecturaSlider;
	public UILabel lecturaLabel;
	public UIInput minimoInput;
	public UIInput maximoInput;
	ConfiguracionControles configuracionControles;
	ConfiguracionControlesControl configuracionControlesControl;
	
	int[] valoresPotenciometro = new int[6];

	int indiceInternoPotenciometro = -1;

	//0: freno
	//1: acelerador
	//2: joy izq X
	//3: joy izq Y
	//4: joy der X
	//5: joy der Y
	void potenciometros(int[] valores){
		valoresPotenciometro[0] = valores[configuracionControles.idFreno];
		valoresPotenciometro[1] = valores[configuracionControles.idAcelerador];
		valoresPotenciometro[2] = valores[configuracionControles.idJoystickIzquierdoX];
		valoresPotenciometro[3] = valores[configuracionControles.idJoystickIzquierdoY];
		valoresPotenciometro[4] = valores[configuracionControles.idJoystickDerechoX];
		valoresPotenciometro[5] = valores[configuracionControles.idJoystickDerechoY];
	}

	// Use this for initialization
	void Start () {
		configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles> ();
		configuracionControlesControl = transform.parent.gameObject.GetComponent<ConfiguracionControlesControl> ();
		cargarValores ();
	}

	public void cargarValores(){
		switch (tipo) {
		case PotenciometroTipo.PotenciometroFreno: 
			indiceInternoPotenciometro = 0; 
			listaAnalogos.value = "Analog" + ( configuracionControles.idFreno + 1);
			minimoInput.value = "" + configuracionControles.frenoRangoMinimo;
			maximoInput.value = "" + configuracionControles.frenoRangoMaximo;
			break;
		case PotenciometroTipo.PotenciometroAcelerador: 
			indiceInternoPotenciometro = 1;
			listaAnalogos.value = "Analog" + ( configuracionControles.idAcelerador + 1);
			minimoInput.value = "" + configuracionControles.aceleradorRangoMinimo;
			maximoInput.value = "" + configuracionControles.aceleradorRangoMaximo;
			break;
		case PotenciometroTipo.PotenciometroJoyIzqX: 
			indiceInternoPotenciometro = 2;
			listaAnalogos.value = "Analog" + ( configuracionControles.idJoystickIzquierdoX + 1);
			minimoInput.value = "" + configuracionControles.joystickIzquierdoXRangoMinimo;
			maximoInput.value = "" + configuracionControles.joystickIzquierdoXRangoMaximo;
			break;
		case PotenciometroTipo.PotenciometroJoyIzqY: 
			indiceInternoPotenciometro = 3; 
			listaAnalogos.value = "Analog" + ( configuracionControles.idJoystickIzquierdoY + 1);
			minimoInput.value = "" + configuracionControles.joystickIzquierdoYRangoMinimo;
			maximoInput.value = "" + configuracionControles.joystickIzquierdoYRangoMaximo;
			break;
		case PotenciometroTipo.PotenciometroJoyDerX: 
			indiceInternoPotenciometro = 4; 
			listaAnalogos.value = "Analog" + ( configuracionControles.idJoystickDerechoX + 1);
			minimoInput.value = "" + configuracionControles.joystickDerechoXRangoMinimo;
			maximoInput.value = "" + configuracionControles.joystickDerechoXRangoMaximo;
			break;
		case PotenciometroTipo.PotenciometroJoyDerY: 
			indiceInternoPotenciometro = 5; 
			listaAnalogos.value = "Analog" + ( configuracionControles.idJoystickDerechoY + 1);
			minimoInput.value = "" + configuracionControles.joystickDerechoYRangoMinimo;
			maximoInput.value = "" + configuracionControles.joystickDerechoYRangoMaximo;
			break;
		}


		//textoBoton = botonLabel.text;
	}

	public void configurar(string valor){
		valor = valor.Remove (0, 6);
		print (valor);
		switch (indiceInternoPotenciometro) {
		case 0:
			configuracionControles.idFreno = int.Parse(valor) - 1;
			break;
		case 1:
			configuracionControles.idAcelerador = int.Parse(valor) - 1;
			break;
		case 2:
			configuracionControles.idJoystickIzquierdoX = int.Parse(valor) - 1;
			break;
		case 3:
			configuracionControles.idJoystickIzquierdoY = int.Parse(valor) - 1;
			break;
		case 4:
			configuracionControles.idJoystickDerechoX = int.Parse(valor) - 1;
			break;
		case 5:
			configuracionControles.idJoystickDerechoY = int.Parse(valor) - 1;
			break;
		}
	}

	public void cambiarMinimo(string valor){
		int valorInt = 0;
		if (int.TryParse (valor, out valorInt)) {
			switch (indiceInternoPotenciometro) {
			case 0:
				configuracionControles.frenoRangoMinimo = valorInt;
				break;
			case 1:
				configuracionControles.aceleradorRangoMinimo = valorInt;
				break;
			case 2:
				configuracionControles.joystickIzquierdoXRangoMinimo = valorInt;
				break;
			case 3:
				configuracionControles.joystickIzquierdoYRangoMinimo = valorInt;
				break;
			case 4:
				configuracionControles.joystickDerechoXRangoMinimo = valorInt;
				break;
			case 5:
				configuracionControles.joystickDerechoYRangoMinimo = valorInt;
				break;
			}
		}
	}

	public void cambiarMaximo(string valor){
		int valorInt = 0;
		if (int.TryParse (valor, out valorInt)) {
			switch (indiceInternoPotenciometro) {
			case 0:
				configuracionControles.frenoRangoMaximo = valorInt;
				break;
			case 1:
				configuracionControles.aceleradorRangoMaximo = valorInt;
				break;
			case 2:
				configuracionControles.joystickIzquierdoXRangoMaximo = valorInt;
				break;
			case 3:
				configuracionControles.joystickIzquierdoYRangoMaximo = valorInt;
				break;
			case 4:
				configuracionControles.joystickDerechoXRangoMaximo = valorInt;
				break;
			case 5:
				configuracionControles.joystickDerechoYRangoMaximo = valorInt;
				break;
			}
		}
	}


	// Update is called once per frame
	void Update () {
		//switch (tipo) {
		//case PotenciometroTipo.PotenciometroFreno:
			lecturaSlider.value = Mathf.Round((valoresPotenciometro[indiceInternoPotenciometro] + 1024f)/2.048f)/1000f;
			lecturaLabel.text = "" + valoresPotenciometro[indiceInternoPotenciometro];
		//	break;
		//}
	}
}
