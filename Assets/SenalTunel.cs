using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenalTunel : MonoBehaviour {
	public Animator señaletica;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.transform.root.CompareTag ("Maquina"))
			señaletica.SetBool ("Mostrar", true);
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.transform.root.CompareTag("Maquina"))
			señaletica.SetBool ("Mostrar", false);
	}
}
