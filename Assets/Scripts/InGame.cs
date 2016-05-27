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
        Display.displays[0].SetParams(1366, 768, 0, 0);
        if (Display.displays.Length > 1) Display.displays[1].SetParams(1920, 1080, 0, 0);
        if (Display.displays.Length > 2) Display.displays[2].SetParams(1920, 1080, 0, 0);
        if (Display.displays.Length > 3) Display.displays[3].SetParams(1920, 1080, 0, 0);
        if (Display.displays.Length > 4) Display.displays[4].SetParams(800, 480, 0, 0);
        if (Display.displays.Length > 5) Display.displays[5].SetParams(1920, 1080, 0, 0);

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
