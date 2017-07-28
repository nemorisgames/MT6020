using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTolva : MonoBehaviour {
	UILabel label;

	// Use this for initialization
	void Start () {
		label = GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		label.text = "tolva: " + Input.GetAxis("ControlTolbaEditor");
		#else
		label.text = "tolva: " + Input.GetAxis ("ControlTolba");	
		#endif
	}
}
