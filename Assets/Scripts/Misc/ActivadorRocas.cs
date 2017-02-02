using UnityEngine;
using System.Collections;

public class ActivadorRocas : MonoBehaviour {
    bool enProceso = false;
    public Animator dispensadorRocas;

	// Use this for initialization
	void Start () {

	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.P)) {
			dispensadorRocas.SetTrigger("Activar");
		}
	}
	
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.transform.root.CompareTag("Maquina") && !enProceso)
        {
            enProceso = true;
            dispensadorRocas.SetTrigger("Activar");
        }
    }

    public void reset()
    {
        enProceso = false;
    }
}
