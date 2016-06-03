using UnityEngine;
using System.Collections;

public class TableroControl : MonoBehaviour {
    public GameObject[] indicadoresSuperiores;
	// Use this for initialization
	void Start () {
	
	}

    public void encenderReversa(bool encender){ indicadoresSuperiores[13].SetActive(!encender); }
    public void encenderNeutro(bool encender) { indicadoresSuperiores[14].SetActive(!encender); }
    public void encenderAdelante(bool encender) { indicadoresSuperiores[15].SetActive(!encender); }
    public void encenderAuto(bool encender) { indicadoresSuperiores[16].SetActive(!encender); }
    public void encenderManual(bool encender) { indicadoresSuperiores[17].SetActive(!encender); }

    // Update is called once per frame
    void Update () {
	
	}
}
