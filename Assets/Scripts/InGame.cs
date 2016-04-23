using UnityEngine;
using System.Collections;

public class InGame : MonoBehaviour {
    public GameObject maquinaAlta;
    public GameObject maquinaBaja;
    public GameObject camaraEntrada;
    public GameObject controlChecklistGameObject;
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        string[] names = Input.GetJoystickNames();
        Debug.Log("Connected Joysticks:");
        for (int i = 0; i < names.Length; i++) {
            Debug.Log("Joystick" + (i + 1) + " = " + names[i]);
        }

        activarMaquinaAlta(true);
    }

    void activarMaquinaAlta(bool activar)
    {
        maquinaAlta.SetActive(activar);
        maquinaBaja.SetActive(!activar);
        camaraEntrada.SetActive(false);
        controlChecklistGameObject.SetActive(activar);
    }

    public void ejecutarEntradaMaquina()
    {
        print("entrando");
        StartCoroutine(ejecutarEntradaMaquinaDelay());
    }


    public IEnumerator ejecutarEntradaMaquinaDelay() { 
        yield return new WaitForSeconds(10f);
        print("entrar");
        activarMaquinaAlta(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
