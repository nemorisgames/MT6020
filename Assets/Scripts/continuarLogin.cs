using UnityEngine;
using System.Collections;

public class continuarLogin : MonoBehaviour {
	public GameObject Login;
	//public GameObject estado;
	public GameObject sexo;
	public GameObject ConfirmarLogin;
	public GameObject botonL;
	public GameObject botonR;
	public GameObject botonV;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void click(){
		Login.SetActive(true);
		ConfirmarLogin.SetActive(true);
		botonL.SetActive (false);
		botonR.SetActive (false);
		botonV.SetActive (true);

	}
}
