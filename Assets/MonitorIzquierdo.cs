using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorIzquierdo : MonoBehaviour {
	public Transform [] guias;

	// Use this for initialization
	void Start () {
		if (guias == null) {
			guias = new Transform[3];
			guias [0] = transform.Find ("Cam_Izq_Izquierdo");
			guias [1] = transform.Find ("Cam_Izq_Recto");
			guias [2] = transform.Find ("Cam_Izq_Derecho");
		}
		guias [0].gameObject.SetActive (false);
		guias [2].gameObject.SetActive (false);
	}

	public void MostrarGuia(int index){
		index = Mathf.Clamp (index, 0, 2);
		for (int i = 0; i < 3; i++) {
			guias [i].gameObject.SetActive (false);
		}
		guias [index].gameObject.SetActive (true);
	}
}
