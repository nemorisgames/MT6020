using UnityEngine;
using System.Collections;

public class DetectorSenal : MonoBehaviour {
	public ControlCamion.senaletica senal;
	public ControlCheckpoints controlCheckpoints;
	public int checkpointMaximoActivo = -1;
	public int checkpointMinimoActivo = -1;
	public int contadorColliders = 0;

	public bool avancePuntaBalde = true;
	ControlCamion controlExcavadora;
	// Use this for initialization
	void Start () {
		controlExcavadora = GameObject.FindWithTag ("Maquina").GetComponent<ControlCamion> ();
	}

	void OnTriggerEnter(Collider other){
		if (!other.gameObject.transform.root.gameObject.CompareTag ("Maquina"))
			return;
		if ((checkpointMaximoActivo >= controlCheckpoints.indiceActual && checkpointMinimoActivo <= controlCheckpoints.indiceActual) || checkpointMaximoActivo == -1) {
			controlExcavadora.cambiarEstado(senal, avancePuntaBalde);
			contadorColliders++;
		}
	}

	void OnTriggerExit(Collider other){
		if (!other.gameObject.transform.root.gameObject.CompareTag ("Maquina"))
			return;
		if ((checkpointMaximoActivo >= controlCheckpoints.indiceActual && checkpointMinimoActivo <= controlCheckpoints.indiceActual) || checkpointMaximoActivo == -1) {
			contadorColliders--;
			if(contadorColliders <= 0)
				controlExcavadora.cambiarEstado(ControlCamion.senaletica.ninguno, avancePuntaBalde);

		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
