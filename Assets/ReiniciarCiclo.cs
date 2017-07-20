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
	Vector3 camionetaPosIni;
	Vector3 estoqueSt14PosIni;
	Vector3 SemaforoSt14PosIni;
	Vector3 rocasPosIni;
	Quaternion camionetaRotIni;
	Quaternion estoqueSt14RotIni;
	Quaternion semaforoSt14RotIni;
	Quaternion rocasRotIni;
	public bool active = true;

	// Use this for initialization
	void Awake () {
		camionetaPosIni = camioneta.transform.position;
		estoqueSt14PosIni = st14Tunel.transform.position;
		SemaforoSt14PosIni = st14Semaforo.transform.position;
		rocasPosIni = rocas.transform.position;
		camionetaRotIni = camioneta.transform.rotation;
		estoqueSt14RotIni = st14Tunel.transform.rotation;
		semaforoSt14RotIni = st14Semaforo.transform.rotation;
		rocasRotIni = rocas.transform.rotation;
	}
	
	public void Reiniciar(){
		camioneta.puntoActual = 0;
		camioneta.transform.position = camionetaPosIni;
		camioneta.transform.rotation = camionetaRotIni;
		camioneta.gameObject.SetActive (true);

		st14Tunel.transform.position = estoqueSt14PosIni;
		st14Tunel.puntoActual = 0;
		st14Tunel.transform.rotation = estoqueSt14RotIni;

		st14Semaforo.transform.position = SemaforoSt14PosIni;
		st14Semaforo.puntoActual = 0;
		st14Semaforo.transform.rotation = semaforoSt14RotIni;

		triggerTunel.Restart ();
		semaforo.Restart ();
		GameObject aux = Instantiate (rocas, rocas.transform.position, rocas.transform.rotation);
		aux.SetActive (true);
		active = false;
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.transform.root.CompareTag("Maquina") && active) {
			Reiniciar ();
		}
	}

	public void Enable(){
		active = true;
	}
}
