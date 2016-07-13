using UnityEngine;
using System.Collections;

public class ControlRutaBloqueos : MonoBehaviour {
	public rutaBloqueo[] rutaBloqueos;
	public ControlCheckpoints controlcheckpoints;
	int checkpointActual = -1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(checkpointActual == controlcheckpoints.indiceActual) return;
		checkpointActual = controlcheckpoints.indiceActual;
		for (int i = 0; i < rutaBloqueos.Length; i++) {
			//if(i == 3)
			//	print ((checkpointActual >= rutaBloqueos[i].checkpointRangoMinimo) + "&&" + (rutaBloqueos[i].permanente || checkpointActual <= rutaBloqueos[i].checkpointRangoMaximo));
			if((checkpointActual >= rutaBloqueos[i].checkpointRangoMinimo && checkpointActual <= rutaBloqueos[i].checkpointRangoMaximo) || (rutaBloqueos[i].checkpointRangoMinimo < 0 && rutaBloqueos[i].checkpointRangoMaximo < 0)){
				rutaBloqueos[i].objeto.SetActive(true);
			}
			else
				if(rutaBloqueos[i].permanente){
					if(controlcheckpoints.nVueltas == 0)
						rutaBloqueos[i].objeto.SetActive(false);
				}
				else rutaBloqueos[i].objeto.SetActive(false);
		}
	}

	public void esconderBloqueos(){
		for (int i = 0; i < rutaBloqueos.Length; i++) {
			rutaBloqueos[i].objeto.SetActive(false);
		}
	}
}

[System.Serializable]
public class rutaBloqueo{
	public int checkpointRangoMinimo = -1;
	public int checkpointRangoMaximo = -1;
	public GameObject objeto;
	public bool permanente = false;
}
