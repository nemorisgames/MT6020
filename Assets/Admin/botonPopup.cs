using UnityEngine;
using System.Collections;

public class botonPopup : MonoBehaviour {
	public GameObject padre;
	// Use this for initialization
	void Start () {
	
	}
	public void click(){
		padre.SetActive (false);
	}
	public void terminarPreguntas(){
		GameObject.FindWithTag ("Configuracion").SendMessage ("finalizar");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
