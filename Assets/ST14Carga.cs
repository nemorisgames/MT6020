using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST14Carga : MonoBehaviour {
	Animator anim;
	EntregaNombrada st14;
	public bool cargando = false;

	public ST14CargaTrigger trigger;
	public GameObject tapa;
	public GameObject carga;
	public GameObject carga_base;
	public Semaforo semaforo;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		st14 = GetComponent<EntregaNombrada> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (trigger.activado) {
			if ((st14.puntoActual % 2) != 0 && st14.enEspera && !cargando) {
				EjecutarCarga ();
			}
			if (st14.puntos [st14.puntoActual].punto.name == "p1")
				ReiniciarCarga ();
		}
	}

	public void EncenderST(){
		st14.gameObject.SetActive (true);
	}

	public void EjecutarCarga(){
		if (!cargando) {
			anim.SetTrigger ("Brazo");
			cargando = true;
		}
	}

	public void GenerarCargaExtra(){
		GameObject aux = Instantiate (carga_base, carga_base.transform.position, carga_base.transform.rotation, null);
		aux.SetActive (true);
	}

	public void ToggleTapa(){
		tapa.SetActive (!tapa.activeSelf);
		carga.transform.parent = null;
	}

	public void ReiniciarCarga(){
		cargando = false;
		if (carga.transform.parent != carga_base.transform.parent) {
			carga = Instantiate (carga_base, carga_base.transform.position, carga_base.transform.rotation, carga_base.transform.parent);
			carga.SetActive (true);
		}
	}

	public void entregaTerminada(){
		st14.gameObject.SetActive (false);
		semaforo.gameObject.SetActive (true);
	}
}
