using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerST : MonoBehaviour {
	public GameObject dummy;
	bool active = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.transform.root.CompareTag("Maquina") && !active) {
			dummy.SetActive (true);
			active = true;
		}
	}

	public void entregaTerminada(){
		//active = false;
		dummy.SetActive (false);
	}
}
