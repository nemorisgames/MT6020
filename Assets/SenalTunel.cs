using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenalTunel : MonoBehaviour {
	Señaleticas ctrl;
	public enum direccionSeñal {left,right,forward,stop};
	public direccionSeñal direccion;
	public bool active = true;
	[HideInInspector]
	public int index;
	bool visible = false;

	void Awake(){
		ctrl = GetComponentInParent<Señaleticas> ();
	}

	void OnTriggerEnter(Collider other){
		if (active && !visible && other.gameObject.transform.root.CompareTag ("Maquina")) {
			ctrl.ShowSign (direccion, true);
			ctrl.currentIndex = index;
			visible = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (active && visible && other.gameObject.transform.root.CompareTag ("Maquina")) {
			ctrl.ShowSign (direccion, false);
			ctrl.currentIndex = -1;
			visible = false;
			if (index == 8)
				ctrl.ToggleMidRestart (true);
			if (index == 11)
				ctrl.ToggleMidRestart (false);
		}
	}

	public void enable(bool b){
		active = b;
	}
}
