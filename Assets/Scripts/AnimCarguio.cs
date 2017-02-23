using UnityEngine;
using System.Collections;

public class AnimCarguio : MonoBehaviour {

	public GameObject barrera;
	public GameObject cargaTapa;
	public GameObject cargaRocas;
	public GameObject cargaPosicion;

	GameObject carga2;
	bool cargado = false;

	// Use this for initialization
	void Start () {
		if(barrera != null)
			barrera.SetActive (true);
		cargaTapa.SetActive (true);
		carga2 = (GameObject)Instantiate (cargaRocas, cargaPosicion.transform.position, cargaPosicion.transform.rotation);
		carga2.SetActive (false);
	}

	public void QuitarBarrera(){
		if(barrera != null)
			barrera.SetActive (false);
	}

	public void ToggleTapa(){
		cargaTapa.SetActive (!cargaTapa.activeSelf);
	}

	public void GenerarCarga(){
		if (!cargado) {
			carga2.SetActive (true);
			cargado = true;
		} else {
			Instantiate (cargaRocas, cargaPosicion.transform.position, cargaPosicion.transform.rotation);
		}
	}

	public void Update(){
		if(!cargado)
			carga2.transform.position = cargaPosicion.transform.position;	
	}
}
