using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class Pregunta : MonoBehaviour {
    public int preguntaID;
    public UILabel preguntaLabel;
    public UIToggle[] respuestaToggle;
    public int[] respuestaID;
    public int respuestaCorrectaID;
    public int respuestaUsuarioID = -1;
 


    // Use this for initialization
    void Start ()
    {
	    
	}

    public void inicializar(int preguntaID, string pregunta, int[] respuestaID, string[] respuesta, int respuestaCorrecta)
    {
        this.preguntaID = preguntaID;
        preguntaLabel.text = pregunta;
        this.respuestaID = respuestaID;
        for (int i = 0; i < 4; i++)
        {
            respuestaToggle[i].transform.FindChild("Label").GetComponent<UILabel>().text = respuesta[i];
            respuestaToggle[i].group = preguntaID;
        }
        respuestaCorrectaID = respuestaCorrecta;
        
    }

    void respuestaSeleccionada(int indiceRespuesta)
    {
        respuestaUsuarioID = respuestaID[indiceRespuesta];
    }

    public void respuestaSeleccionada0()
    {
        if(respuestaToggle[0].value)
            respuestaSeleccionada(0);
    }

    public void respuestaSeleccionada1()
    {
        if (respuestaToggle[1].value)
            respuestaSeleccionada(1);
    }

    public void respuestaSeleccionada2()
    {
        if (respuestaToggle[2].value)
            respuestaSeleccionada(2);
    }

    public void respuestaSeleccionada3()
    {
        if (respuestaToggle[3].value)
            respuestaSeleccionada(3);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
