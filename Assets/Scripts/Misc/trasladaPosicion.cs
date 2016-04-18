using UnityEngine;
using System.Collections;

public class trasladaPosicion : MonoBehaviour {
	Vector3 posicionInicial;
	public string tagColision = "CapturaPesoFinal";
	public float tiempo = 10f;
	float tiempoActual = 0f;
	bool detectado = false;
	bool enBalde = false;
    Vector3 escalaOriginal;
	// Use this for initialization
	void Start () {
		posicionInicial = transform.position;
        escalaOriginal = transform.localScale;
    }

	public void respawn()
    {
        transform.parent = null;
        transform.position = posicionInicial;
        transform.localScale = escalaOriginal;
    }

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("PesoBalde"))
			enBalde = true;
		/*if (other.CompareTag (tagColision) || (other.CompareTag ("PesoIntermedio") && !enBalde)) {
			detectado = true;
			tiempoActual = Time.time + tiempo;
		}*/
	}

	void OnTriggerStay(Collider other){
		if (detectado)
			return;
		if (other.CompareTag (tagColision) || (other.CompareTag ("PesoIntermedio") && !enBalde)) {
			detectado = true;
			tiempoActual = Time.time + tiempo;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("PesoBalde"))
			enBalde = false;
	}

	
	// Update is called once per frame
	void Update () {
		if (detectado) {
			if(tiempoActual <= Time.time){
				detectado = false;
				respawn();
			}
		}
	}
}
