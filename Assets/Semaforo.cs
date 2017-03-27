using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour {

	public Material go;
	public Material stop;
	MeshRenderer semaforo;
	public GameObject dummy;
	bool initialState = false;
	bool active = false;

	// Use this for initialization
	void Start () {
		semaforo = GameObject.Find ("Semaforo").GetComponent<MeshRenderer> ();
		if (initialState) {
			semaforo.material = go;
		} else {
			semaforo.material = stop;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.transform.root.CompareTag("semaforo_dummy") && !active) {
			semaforo.material = stop;
			dummy.SetActive (true);
			StartCoroutine (SemaforoRojo ());
			active = true;
		}
	}

	IEnumerator SemaforoRojo(){
		yield return new WaitForSeconds (40f);
		semaforo.material = go;
		yield return new WaitForSeconds (5f);
		active = false;
	}
}
