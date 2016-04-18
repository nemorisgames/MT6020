using UnityEngine;
using System.Collections;

public class DestruccionTiempo : MonoBehaviour {
	public float tiempoVida = 3f;
	// Use this for initialization
	void Start () {
		tiempoVida += Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= tiempoVida)
			Destroy (gameObject);
	}
}
