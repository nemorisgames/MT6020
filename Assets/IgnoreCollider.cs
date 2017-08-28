using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour {
	public string layer = "IgnoreCollision";
	Collider c;

	// Use this for initialization
	void Start () {
		c = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.layer == LayerMask.NameToLayer (layer)) {
			Physics.IgnoreCollision (collision.collider, c);
		} else
			Debug.Log (collision.gameObject.name);
	}
}
