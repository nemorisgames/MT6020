using UnityEngine;
using System.Collections;

public class BotonesOpciones : MonoBehaviour {
	public GameObject pantalla;
	public GameObject historial;
	public GameObject vincular;
	public GameObject editar;
	public GameObject NumeroNivel;

	public GameObject botonCancelar;
	public GameObject alumnos;
	public GameObject niveles;
	public GameObject niveles2;
	public GameObject historial2;
	public GameObject editarNivel;
	public GameObject cosasHistorial;

	public bool esAdmin = false;
	// Use this for initialization
	void Start () {
	
	}
	public void clickCrear(){
		pantalla.SetActive (true);
		NumeroNivel.SetActive (true);

		botonCancelar.SetActive (true);
	}
	public void clickHistorial(){
		alumnos.SetActive (true);
		alumnos.transform.localPosition  = new Vector3 (-663.9f,195,0);
		if (esAdmin) {
			alumnos.transform.localPosition  = new Vector3 (-663.9f,91.93f,0);
		}
		historial.SetActive (true);
		if (!esAdmin) {
			historial2.transform.localPosition  = new Vector3 (-663.9f, 12f,0);
		}
		botonCancelar.SetActive (true);
		historial2.SetActive (true);
	}
	public void clickVincular(){
		niveles.SetActive (true);
		niveles.transform.localPosition  = new Vector3 (0,102,0);
		/*if (!esAdmin)
			alumnos.transform.localPosition  = new Vector3 (-663.9f,101.8f,0);

		//niveles.GetComponent<editarNivel> ().enabled=false;
		alumnos.SetActive (true);*/
		vincular.SetActive (true);
		botonCancelar.SetActive (true);
	}
	public void clickEditar(){
		if (!esAdmin) {
						alumnos.SetActive (true);
						alumnos.transform.localPosition = new Vector3 (25, 189, 0);
				}
		editar.SetActive (true);
		botonCancelar.SetActive (true);
	}
	public void clickEditarNivel(){
		niveles2.SetActive (true);
		if(!esAdmin){
			niveles2.transform.FindChild ("Mirar Nivel").gameObject.GetComponent<verNiveles> ().verNivel ();
		}
		//niveles.transform.localPosition = new Vector3 (-579,236,0);
		//niveles.GetComponent<editarNivel> ().enabled=true;
		editarNivel.SetActive (true);
		botonCancelar.SetActive (true);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
