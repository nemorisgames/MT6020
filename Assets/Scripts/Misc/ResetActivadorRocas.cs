using UnityEngine;
using System.Collections;

public class ResetActivadorRocas : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.transform.root.CompareTag("Maquina"))
        {
            transform.parent.SendMessage("reset");
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
