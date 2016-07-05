using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlOperadorRegistro : MonoBehaviour {

	public GameObject botonListo;
	public UILabel mensaje;
	Configuracion configuracion;
	// Use this for initialization
	void Start () {
		GameObject c=GameObject.FindGameObjectWithTag("Configuracion");
		configuracion=c.GetComponent<Configuracion>();
		botonListo.SetActive (false);
	}

	public void simulacionConfigurada(){
		mensaje.text = "Simulación configurada";
		botonListo.SetActive (true);
	}

	public void lanzarSimulacion(){
		print (configuracion.NumeroModulo);
		SceneManager.LoadScene ("Modulo" + configuracion.NumeroModulo);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
