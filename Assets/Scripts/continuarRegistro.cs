using UnityEngine;
using System.Collections;

public class continuarRegistro : MonoBehaviour {
	public GameObject Login;
	public GameObject Registro;
	//public GameObject estado;
	public GameObject sexo;
	public GameObject ConfirmarRegistro;
	public GameObject botonL;
	public GameObject botonR;
	public GameObject botonV;
	// Use this for initialization
	void Start () {
	
	}
	public void click(){
		Login.SetActive(true);
		Registro.SetActive(true);
		ConfirmarRegistro.SetActive(true);
		botonL.SetActive (false);
		botonR.SetActive (false);
		botonV.SetActive (true);
		//estado.SetActive (true);
		sexo.SetActive (true);
	}
	// Update is called once per frame
	void Update () {

	}
}
