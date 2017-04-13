using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreguntasTrigger : MonoBehaviour {

	InGame inGame;

	// Use this for initialization
	void Awake () {
		inGame = GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			inGame.mostrarPregunta (true);
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.transform.root.CompareTag("Maquina")) {
			inGame.mostrarPregunta (true);
		}
		
	}

	public void DesactivarTrigger(){
		gameObject.SetActive (false);
	}
}
