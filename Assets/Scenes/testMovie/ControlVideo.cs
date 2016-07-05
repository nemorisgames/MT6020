using UnityEngine;
using System.Collections;

public class ControlVideo : MonoBehaviour {

	public MovieTexture movTexture;

	public void PlayVideo() {
		GetComponent<Renderer>().material.mainTexture = movTexture;
		movTexture.Play();
	}

	public void PauseVideo() {
		GetComponent<Renderer>().material.mainTexture = movTexture;
		movTexture.Pause();
	}

	public void StopVideo() {
		GetComponent<Renderer>().material.mainTexture = movTexture;
		movTexture.Stop();
	}
}
