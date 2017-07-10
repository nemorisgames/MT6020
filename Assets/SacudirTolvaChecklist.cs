using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacudirTolvaChecklist : MonoBehaviour {

	InGame ingame;

	void Start(){
		ingame = GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ();
	}

	public void SacudirTolvaStartEnd(){
		float brazo = 0;
		#if UNITY_EDITOR
		brazo = Input.GetAxis ("ControlTolbaEditor");
		#else
		brazo = Input.GetAxis("ControlTolba");
		#endif
		if(brazo != 0)
			ingame.EnableShaking (true, 8f);
	}

	public void SacudirTolvaMid(){
		float brazo = 0;
		#if UNITY_EDITOR
		brazo = Input.GetAxis ("ControlTolbaEditor");
		#else
		brazo = Input.GetAxis("ControlTolba");
		#endif
		if (brazo > 0) {
			ingame.EnableShaking (true, 8f);
		}
	}
}
