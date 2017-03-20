using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ejecutarAccion : MonoBehaviour {
	public Camera camara;
	public GameObject[] elementos;
	public LayerMask mascaraLayers;
	public bool enEjecucion = false;
	// Use this for initialization
	void Start () {
		
	}



	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		Debug.DrawRay(camara.transform.position, camara.transform.forward);
		if (Physics.Raycast (camara.transform.position, camara.transform.forward, out hit, 2f, mascaraLayers)) {
			print (hit.transform.gameObject.name + " " + hit.distance);
			if (hit.transform.gameObject.name == gameObject.name && !enEjecucion) {
				enEjecucion = true;
				foreach (GameObject g in elementos) {
					g.SendMessage ("PlayForward", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	public void animacionTerminada(){
		enEjecucion = false;
	}
}
