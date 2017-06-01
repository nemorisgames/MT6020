using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajarMaquina : MonoBehaviour {

	public bool triggerEnabled = false;

	void OnTriggerExit(Collider c){
		if (c.name == "ControlUsuarioChecklist" && triggerEnabled) {
			Vector3 pos = c.transform.FindChild("Camera").transform.position;
			pos.y += 0.7f;
			c.transform.FindChild ("Camera").transform.position = pos;
		}
	}
}
