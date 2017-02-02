using UnityEngine;
using System.Collections;

public class AnimCarguio : MonoBehaviour {

	public GameObject barrera;
	public GameObject cargaTapa;
	public GameObject cargaRocas;
	public GameObject cargaPosicion;

	GameObject carga2;

	// Use this for initialization
	void Start () {
		barrera.SetActive (true);
		cargaTapa.SetActive (true);
		carga2 = (GameObject)Instantiate (cargaRocas, cargaPosicion.transform.position, cargaPosicion.transform.rotation);
		carga2.SetActive (false);
	}

	public void QuitarBarrera(){
		barrera.SetActive (false);
	}

	public void ToggleTapa(){
		cargaTapa.SetActive (!cargaTapa.activeSelf);
	}

	public void GenerarCarga(){
		carga2.SetActive (true);
	}

	public void Update(){
		carga2.transform.position = cargaPosicion.transform.position;		
	}
}
