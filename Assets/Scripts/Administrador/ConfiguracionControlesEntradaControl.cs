using UnityEngine;
using System.Collections;

public class ConfiguracionControlesEntradaControl : MonoBehaviour {
	public int id;
	public int idLed;
	public int indice;
	string textoBoton;
	public UILabel botonLabel;
	public UILabel idLabel;
	public UIInput idLedLabel;
	ConfiguracionControlesControl configuracionControlesControl;
	
	GameObject botonTest;
	GameObject botonEnTest;

	LectorControles lectorControles;

	// Use this for initialization
	void Start () {
		configuracionControlesControl = transform.parent.gameObject.GetComponent<ConfiguracionControlesControl> ();
		textoBoton = botonLabel.text;
		botonTest = gameObject.transform.FindChild ("BotonTest").gameObject;
		botonEnTest = gameObject.transform.FindChild ("BotonTesteando").gameObject;
		botonEnTest.SetActive (false);
		lectorControles = Camera.main.gameObject.GetComponent<LectorControles> ();
	}

	
	public void testLed(){
		botonTest.SetActive (false);
		botonEnTest.SetActive (true);
		StartCoroutine (testLedRutina ());
	}
	
	IEnumerator testLedRutina(){
		int idLedAux = idLed;
		lectorControles.OutCmd(byte.Parse("" + idLedAux), true);
		yield return new WaitForSeconds (2f);
		lectorControles.OutCmd(byte.Parse("" + idLedAux), false);
		botonTest.SetActive (true);
		botonEnTest.SetActive (false);
	}

	public void cambiarLED(string valor){
		if(int.TryParse(valor, out idLed)){
			configuracionControlesControl.cambiarLED (indice, idLed);
		}
	}

	public void configurar(){
		if (configuracionControlesControl.esperandoEntrada)
			return;
		botonLabel.text = "Presione el botón correspondiente";
		transform.parent.gameObject.SendMessage ("configurarBoton", this);
	}

	public void setID(int id){
		this.id = id;
		idLabel.text = "ID: " + id;
		botonLabel.text = textoBoton;
	}

	public void setIDLed(int id){
		this.idLed = id;
		idLedLabel.value = "" + id;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
