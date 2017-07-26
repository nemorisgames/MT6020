using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManos : MonoBehaviour {

	// Use this for initialization
	public void Enable(){
		transform.Find ("Render").gameObject.SetActive (true);
	}

	public void Disable(){
		transform.Find ("Render").gameObject.SetActive (false);
	}
}
