using UnityEngine;
using System.Collections;

public class TestPantalla : MonoBehaviour {
    public UILabel output;
    public UIInput[] pw;
    public UIInput[] ph;
    public UIInput[] px;
    public UIInput[] py;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        Display.displays[0].SetParams(1366, 768, 0, 0);
        Display.displays[1].SetParams(1920, 1080, 0, 0);
        Display.displays[2].SetParams(1920, 1080, 0, 0);
        Display.displays[3].SetParams(1920, 1080, 0, 0);
        Display.displays[4].SetParams(800, 480, 0, 0);
        Display.displays[5].SetParams(1920, 1080, 0, 0);
        /*Display.displays[0].Activate(1366, 768, 60);
        Display.displays[1].Activate(1920, 1080, 60);
        Display.displays[2].Activate(1920, 1080, 60);
        Display.displays[3].Activate(1920, 1080, 60);
        Display.displays[4].Activate(800, 480, 60);
        Display.displays[5].Activate(1920, 1080, 60);*/
        output.text = "pantallas: " + Display.displays.Length;
    }

    public void setResolucion()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].SetParams(int.Parse(pw[i].value), int.Parse(ph[i].value), int.Parse(px[i].value), int.Parse(py[i].value));
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
