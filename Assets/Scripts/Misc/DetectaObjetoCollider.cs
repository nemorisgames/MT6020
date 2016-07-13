using UnityEngine;
using System.Collections;

public class DetectaObjetoCollider : MonoBehaviour {
	public string nombreFuncion;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.name.Contains("detectorSenal") || other.gameObject.name.Contains("cp") || other.gameObject.name.Contains("Peso") || other.gameObject.name.Contains("Rock")) return;
		//print (other.gameObject.name);
		transform.parent.parent.SendMessage (nombreFuncion, true);
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.name.Contains("detectorSenal") || other.gameObject.name.Contains("cp") || other.gameObject.name.Contains("Peso") || other.gameObject.name.Contains("Rock")) return;
		transform.parent.parent.SendMessage (nombreFuncion, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
