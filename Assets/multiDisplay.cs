using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);

		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (gameObject);
		} 
		else{
			for (int i = 0; i < Display.displays.Length; i++)
			{
				Display.displays[i].Activate();
			}

			Display.displays[0].SetParams(1920, 1080, 0, 0);
			if (Display.displays.Length > 1) 
				Display.displays[1].SetParams(1920, 1080, 0, 0);
			if (Display.displays.Length > 2) 
				Display.displays[2].SetParams(1920, 1080, 0, 0);
			if (Display.displays.Length > 3) 
				Display.displays[3].SetParams(1920, 1080, 0, 0);
			if (Display.displays.Length > 4) 
				Display.displays[4].SetParams(800, 480, 0, 0);
			if (Display.displays.Length > 5) 
				Display.displays[5].SetParams(1920, 1080, 0, 0);
		}
	}
}
