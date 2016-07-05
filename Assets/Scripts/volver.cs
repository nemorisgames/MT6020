using UnityEngine;
using System.Collections;

public class volver : MonoBehaviour {
	public GameObject Login;
	public GameObject estado;
	public GameObject sexo;
	public GameObject Register;
	public GameObject botonL;
	public GameObject botonR;
	public GameObject ConfirmarRegistro;
	public GameObject ConfirmarLogin;


	public GameObject crearNivel;
	public GameObject Vincular;
	public GameObject Historial;
	public GameObject Historial2;
	public GameObject editar;
	public GameObject NumeroNivel;
	public GameObject cantPreguntas;
	public GameObject eliminarNivel;


	public GameObject alumnos;public GameObject alumnos2;
	public GameObject niveles;
	public GameObject niveles2;
	public GameObject CosasDatos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void clickOpciones(){
		crearNivel.SetActive (false);
		Vincular.SetActive (false);
		Historial.SetActive (false);
		editar.SetActive (false);
		NumeroNivel.SetActive (false);
		cantPreguntas.SetActive (false);
		eliminarNivel.SetActive (false);
	
		alumnos.SetActive (false);
		//alumnos.GetComponent<UIPopupList> ().value = "";
		alumnos2.SetActive (false);
		alumnos2.GetComponent<UIPopupList> ().value = "";
		niveles.SetActive (false);
		niveles2.SetActive (false);
		Historial2.SetActive (false);
		gameObject.SetActive (false);
		CosasDatos.SetActive (false);

	}
	public void click(){
		//Login.SetActive(false);
		Register.SetActive(false);
		botonL.SetActive (true);
		botonR.SetActive (true);
		ConfirmarRegistro.SetActive (false);
		ConfirmarLogin.SetActive (true);
		gameObject.SetActive (false);
		estado.SetActive (false);
		sexo.SetActive (false);
	}
}
