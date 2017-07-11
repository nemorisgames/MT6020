using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenalTunel : MonoBehaviour {
	Señaleticas ctrl;
	public enum direccionSeñal {left,right,forward,stop};
	public direccionSeñal direccion;
	[HideInInspector]
	public bool active = true;
	[HideInInspector]
	public int index;

	void Awake(){
		ctrl = GetComponentInParent<Señaleticas> ();
	}

	void OnTriggerEnter(Collider other){
		if (active && other.gameObject.transform.root.CompareTag ("Maquina")) {
			ctrl.ShowSign (direccion, true);
			ctrl.currentIndex = index;
		}
	}

	void OnTriggerExit(Collider other){
		if (active && other.gameObject.transform.root.CompareTag ("Maquina")) {
			ctrl.ShowSign (direccion, false);
			ctrl.currentIndex = -1;
		}
	}

	public void enable(bool b){
		active = b;
	}
}
