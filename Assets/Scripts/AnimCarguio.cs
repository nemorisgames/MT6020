using UnityEngine;
using System.Collections;

public class AnimCarguio : MonoBehaviour {

	public GameObject barrera;
	public GameObject cargaTapa;
	public GameObject cargaRocas;
	public GameObject cargaPosicion;
	public TableroControl tableroControl;
	public Camera [] camarasMaquina;

	GameObject carga2;
	bool cargado = false;
	bool boton = false;

	// Use this for initialization
	void Start () {
		if(barrera != null)
			barrera.SetActive (true);
		cargaTapa.SetActive (true);
		carga2 = (GameObject)Instantiate (cargaRocas, cargaPosicion.transform.position, cargaPosicion.transform.rotation);
		carga2.SetActive (false);
		if (tableroControl == null)
			tableroControl = GameObject.Find ("TableroControl").GetComponent<TableroControl> ();
		if (camarasMaquina == null) {
			camarasMaquina = new Camera[3];
			camarasMaquina [0] = GameObject.Find ("CamaraCabinaAdelante").GetComponent<Camera>();
			camarasMaquina [1] = GameObject.Find ("CamaraCabinaIzquierda").GetComponent<Camera>();
			camarasMaquina [2] = GameObject.Find ("CamaraCabinaDerecha").GetComponent<Camera>();
		}
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
		if(!cargado && carga2 !=null)
			carga2.transform.position = cargaPosicion.transform.position;

		if(Input.GetKeyDown(KeyCode.X)){
			float t = Time.time;
			SacudirCamaras ();
			Debug.Log (t + "->" + (Time.time - t));
		}
	}

	public void ToggleTablero(){
		if (tableroControl != null) {
			tableroControl.encenderCarga (!boton);
			boton = !boton;
		}
	}

	public void SacudirCamaras(){
		StartCoroutine (ShakeMulti (45));
	}

	IEnumerator ShakeSingle(){
		float rnd = Random.Range (-0.05f, 0.05f);
		Vector3[] target = new Vector3[3];
		for (int i = 0; i < 3; i++) {
			target [i] = new Vector3 (camarasMaquina [i].transform.position.x, camarasMaquina [i].transform.position.y + rnd, camarasMaquina [i].transform.position.z);
		}
		while (camarasMaquina [0].transform.position != target [0] && camarasMaquina[1].transform.position != target[1] && camarasMaquina[2].transform.position != target[2]) {
			for(int i=0;i<3;i++)
				camarasMaquina [i].transform.position = Vector3.Lerp (camarasMaquina [i].transform.position, target [i], Time.deltaTime);
		}
		yield return new WaitForSeconds (0.01f);
		for (int i = 0; i < 3; i++) {
			target [i] = new Vector3 (camarasMaquina [i].transform.position.x, camarasMaquina [i].transform.position.y - rnd, camarasMaquina [i].transform.position.z);
		}
		while (camarasMaquina [0].transform.position != target [0] && camarasMaquina[1].transform.position != target[1] && camarasMaquina[2].transform.position != target[2]) {
			for(int i=0;i<3;i++)
				camarasMaquina [i].transform.position = Vector3.Lerp (camarasMaquina [i].transform.position, target [i], Time.deltaTime);		
		}
	}

	IEnumerator ShakeMulti(int n){
		for(int i=0;i<n;i++){
			StartCoroutine(ShakeSingle ());
			yield return new WaitForSeconds (0.07f);
		}
	}
}
