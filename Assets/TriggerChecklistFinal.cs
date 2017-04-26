using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecklistFinal : MonoBehaviour {

	public ControlChecklist checklist;
	public InGame ingame;

	void OnTriggerEnter(Collider c){
		if (c.gameObject.transform.root.CompareTag("Maquina")) {
			checklist.ReiniciarMaquina ();
			ingame.salirCabina ();
			gameObject.SetActive (false);
		}
	}
}
