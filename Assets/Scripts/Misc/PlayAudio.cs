using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void playAudio(){
		GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
