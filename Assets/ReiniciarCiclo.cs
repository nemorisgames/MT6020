using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarCiclo : MonoBehaviour {
	public EntregaNombrada camioneta;
	public EntregaNombrada st14Tunel;
	public EntregaNombrada st14Semaforo;
	public TriggerST triggerTunel;
	public Semaforo semaforo;
	public GameObject rocas;
	Vector3 camionetaIni;
	Vector3 st14TunelIni;
	Vector3 st14SemaforoIni;
	Vector3 rocasIni;
	public bool active = true;

	// Use this for initialization
	void Awake () {
		camionetaIni = camioneta.transform.position;
		st14TunelIni = st14Tunel.transform.position;
		st14SemaforoIni = st14Semaforo.transform.position;
		rocasIni = rocas.transform.position;
	}
	
	public void Reiniciar(){
		camioneta.puntoActual = 0;
		camioneta.transform.position = camionetaIni;
		st14Tunel.transform.position = st14TunelIni;
		st14Tunel.puntoActual = 0;
		st14Semaforo.transform.position = st14SemaforoIni;
		st14Semaforo.puntoActual = 0;
		triggerTunel.Restart ();
		semaforo.Restart ();
		GameObject aux = Instantiate (rocas, rocas.transform.position, rocas.transform.rotation);
		aux.SetActive (true);
		active = false;
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "Maquina" && active) {
			Reiniciar ();
		}
	}

	public void Enable(){
		active = true;
	}
}
