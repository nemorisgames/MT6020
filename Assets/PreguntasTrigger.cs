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
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.transform.root.CompareTag("Maquina")) {
			StartCoroutine(inGame.mostrarPregunta (true));
		}
		
	}

	public void DesactivarTrigger(){
		gameObject.SetActive (false);
	}
}
