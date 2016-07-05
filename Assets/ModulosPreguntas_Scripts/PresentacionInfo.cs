using UnityEngine;
using System.Collections;

public class PresentacionInfo : MonoBehaviour {
	int cantidadDiapos;
	public int diapoActual;
	public bool esMod3 = false;
	public UILabel titulo;
	public GameObject prueba;

	GameObject botonAnterior;
	GameObject botonSiguiente;

	// Use this for initialization
	void Start () {
		botonAnterior = transform.parent.FindChild ("Botones/Cancelar").gameObject;
		botonSiguiente = transform.parent.FindChild ("Botones/Siguiente").gameObject;
		botonAnterior.SetActive(false);
		cantidadDiapos = gameObject.transform.childCount;
		diapoActual = 1;
		apagarTodo ();
		gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void apagarTodo(){
		for (int i =1; i<=cantidadDiapos; i++) {
			gameObject.transform.FindChild ("Diapo " + i.ToString ()).gameObject.SetActive (false);
		}
	}
	public void siguiente(){
		if (diapoActual != cantidadDiapos) {
			botonAnterior.SetActive(true);
			botonSiguiente.SetActive(true);
			gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (false);
			diapoActual++;		
			gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (true);
			if(esMod3&&diapoActual>=21)
				titulo.text="CHECK LIST, CABINA ENSEÑA";
			if(esMod3&&diapoActual<21)
				titulo.text="CHECK LIST, ENSEÑA";
		} else {
			
			botonSiguiente.SetActive(false);
		
			prueba.SetActive(true);
			GameObject.Find("Informacion").SetActive(false);
		}
	}
	public void revFunc(){
		apagarTodo ();
		diapoActual = 18;
		gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (true);

	}
	public void revCab(){
		apagarTodo ();
		diapoActual = 22;
		gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (true);
	}
	public void revEst(){
		apagarTodo ();
		diapoActual = 20;
		gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (true);
	}
	public void prevRiesg(){
		apagarTodo ();
		diapoActual = 25;
		gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (true);
	}
	public void anterior(){
		if (diapoActual != 1) {
			gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (false);
			diapoActual--;
			gameObject.transform.FindChild ("Diapo " + diapoActual.ToString ()).gameObject.SetActive (true);
			if(esMod3&&diapoActual>=21)
				titulo.text="CHECK LIST, CABINA ENSEÑA";
			if(esMod3&&diapoActual<21)
				titulo.text="CHECK LIST, ENSEÑA";
			if(diapoActual <= 1) 
				botonAnterior.SetActive(false);
		}
	}

}
