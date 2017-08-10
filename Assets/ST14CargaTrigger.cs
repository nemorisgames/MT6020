using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST14CargaTrigger : MonoBehaviour {
	public bool activado;
	public ST14Carga st14;

	void OnTriggerEnter(Collider c){
		if (c.tag == "PesoBalde") {
			activado = true;
			st14.EncenderST ();
		}
	}

	void OnTriggerExit(Collider c){
		if (c.tag == "PesoBalde")
			activado = false;
	}
}
