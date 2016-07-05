using UnityEngine;
using System.Collections;

public class LocucionManejo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnDisable(){
		transform.FindChild ("playButton").gameObject.SetActive (true);
		transform.FindChild ("playPressButton").gameObject.SetActive (false);
		stopSonido ();
	}

	public void playSonido(){
		GetComponent<AudioSource>().Play ();
	}

	public void stopSonido(){
		GetComponent<AudioSource>().Stop ();
	}

	public void pausaSonido(){
		GetComponent<AudioSource>().Pause ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
