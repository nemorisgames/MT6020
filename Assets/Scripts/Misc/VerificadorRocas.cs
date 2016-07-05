using UnityEngine;
using System.Collections;

public class VerificadorRocas : MonoBehaviour {
    public ArrayList rocasVerificadas = new ArrayList();
	// Use this for initialization
	void Start () {
        rocasVerificadas = new ArrayList();
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Roca"))
        {
            if (!rocasVerificadas.Contains(other.GetComponent<Rigidbody>()))
            {
                rocasVerificadas.Add(other.GetComponent<Rigidbody>());
                print("nrocas " + rocasVerificadas.Count);
            }
        }
    }

    public void reset()
    {
        rocasVerificadas.Clear();
        rocasVerificadas = new ArrayList();
    }

    public float peso()
    {
        float p = 0f;
        foreach(Rigidbody r in rocasVerificadas)
        {
            p += r.mass * 0.001f * 3.5f;
        }
        return p;
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
