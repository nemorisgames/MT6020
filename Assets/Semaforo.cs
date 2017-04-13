using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour {

	public Material go;
	public Material stop;
	public Material off;
	public MeshRenderer semaforoGo;
	public MeshRenderer semaforoStop;
	public GameObject dummy;
	bool initialState = false;
	bool active = false;

	// Use this for initialization
	void Start () {
		if (initialState) {
			ToggleGoStop (true);
		} else {
			ToggleGoStop (false);
		}
	}

	void OnTriggerEnter(Collider other){
		if ((other.gameObject.transform.root.CompareTag("semaforo_dummy")||other.gameObject.transform.root.CompareTag("Maquina")) && !active) {
			ToggleGoStop (false);
			dummy.SetActive (true);
			StartCoroutine (SemaforoRojo ());
			active = true;
		}
	}

	IEnumerator SemaforoRojo(){
		yield return new WaitForSeconds (40f);
		ToggleGoStop (true);
		yield return new WaitForSeconds (5f);
		active = false;
	}

	void ToggleGoStop(bool b){
		if (b) {
			//Go
			semaforoGo.material = go;
			semaforoStop.material = off;
		} else {
			semaforoGo.material = off;
			semaforoStop.material = stop;
		}
	}
}
