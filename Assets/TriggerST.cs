using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerST : MonoBehaviour {
	public EntregaNombrada camioneta;
	public EntregaNombrada st14;
	bool active = false;
	bool pasoCamioneta = false;
	bool entrarEstoque = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (camioneta.puntoActual == 5 && !entrarEstoque) {
			entrarEstoque = true;
			camioneta.detenerse ();
			camioneta.enEspera = true;
		}

		if (st14.puntoActual == 2 && !pasoCamioneta) {
			pasoCamioneta = true;
			camioneta.enEspera = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.transform.root.CompareTag ("Maquina") && !active) {
			st14.gameObject.SetActive (true);
			active = true;
		}
	}

	public void entregaTerminada(){
		//active = false;
		st14.gameObject.SetActive (false);
	}

	public void Restart(){
		active = false;
	}
}
