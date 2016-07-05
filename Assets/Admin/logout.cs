using UnityEngine;
using System.Collections;

public class logout : MonoBehaviour {
	public GameObject obLogin;
	public GameObject opciones;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	public void logoutEjecutar(){
		GameObject confi=GameObject.FindGameObjectWithTag("Configuracion");
		Configuracion conf=confi.GetComponent<Configuracion>();
		conf.logeado=false;
		conf.pass="";
		conf.usuario="";
		opciones.SetActive (false);
		obLogin.SetActive (true);
	}
}
