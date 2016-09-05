using UnityEngine;
using System.Collections;

public class CapturaPeso : MonoBehaviour {
	public float enCarga = 0f;
	public float acumulado = 0f;
	public bool convertirHijos = false;

    // Use this for initialization
    void Start () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Roca")) {
			enCarga += other.gameObject.GetComponent<Rigidbody>().mass * 0.001f * 3.5f;
			acumulado += other.gameObject.GetComponent<Rigidbody>().mass * 0.001f * 3.5f;
			if(convertirHijos) other.transform.parent = transform;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("Roca")) {
			enCarga -= other.gameObject.GetComponent<Rigidbody>().mass * 0.001f * 3.5f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	   
	}
}
