using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour {
	public GameObject rojo_on;
	public GameObject rojo_off;
	public GameObject verde_on;
	public GameObject verde_off;

	public GameObject dummy;
	Vector3 posInicial;
	bool initialState = true;
	bool active = false;
	bool state = false;

	// Use this for initialization
	void Start () {
		ToggleGoStop (initialState);
		posInicial = dummy.transform.position;
	}

	void Update(){
		if (Input.GetKey (KeyCode.Keypad7))
			ToggleGoStop (true);
		if (Input.GetKey (KeyCode.Keypad9))
			ToggleGoStop (false);
	}


	void OnTriggerEnter(Collider other){
		if ((other.gameObject.transform.root.CompareTag("semaforo_dummy")||other.gameObject.transform.root.CompareTag("Maquina")) && !active) {
			ToggleGoStop (false);
			dummy.SetActive (true);
			active = true;
		}
	}

	void ToggleGoStop(bool b){
		rojo_on.SetActive(!b);
		rojo_off.SetActive(b);
		verde_on.SetActive(b);
		verde_off.SetActive(!b);
		state = b;
	}

	public void entregaTerminada(){
		ToggleGoStop (true);
		dummy.SetActive (false);
	}

	IEnumerator Reset(){
		yield return new WaitForSeconds (60f);
		active = true;
		dummy.transform.position = posInicial;
	}
}
