﻿using UnityEngine;
using System.Collections;

public class TestTarjetaControladora : MonoBehaviour {

    ControlTarjetaControladora tarjeta;
    string velocimetro = "00,0";
    string indiceLuz = "0";
    bool luzEstado = true;
    bool luzEstado1 = true;
    bool luzEstado2 = true;
    bool luzEstado3 = true;
    bool luzEstado4 = true;
    bool luzEstado5 = true;
    bool luzEstado6 = true;
    bool luzEstado7 = true;
    bool luzEstado8 = true;
    bool luzEstado9 = true;
    bool luzEstado10 = true;
    bool luzEstado11 = true;
    // Use this for initialization
    void Start () {
        tarjeta = GetComponent<ControlTarjetaControladora>();
    }
	
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 200, 10, 200, 2000), "Estado: " + ((tarjeta.mensaje.Length == 0)?"No iniciada":tarjeta.mensaje));
        /*if(GUI.Button(new Rect(220, 10, 200, 20), "Iniciar tarjeta"))
        {
            tarjeta.MainWindow();
        }*/

        velocimetro = GUI.TextField(new Rect(10, 40, 200, 20), velocimetro);
        if (GUI.Button(new Rect(220, 40, 200, 20), "Escribir en velocímetro"))
        {
            tarjeta.velocimetro(velocimetro);
        }

        indiceLuz = GUI.TextField(new Rect(10, 70, 100, 20), indiceLuz);
        luzEstado = GUI.Toggle(new Rect(120, 70, 80, 20), luzEstado, "on (1-10)");
        if (GUI.Button(new Rect(220, 70, 200, 20), "Encender luz"))
        {
            tarjeta.LuzCircuito(int.Parse(indiceLuz), luzEstado);
        }
        
        luzEstado1 = GUI.Toggle(new Rect(120, 100, 80, 20), luzEstado1, "on");
        if (GUI.Button(new Rect(220, 100, 200, 20), "IntermitenteIzquierda"))
        {
            tarjeta.SendMessage("IntermitenteIzquierda", luzEstado1);
        }

        luzEstado2 = GUI.Toggle(new Rect(120, 130, 80, 20), luzEstado2, "on");
        if (GUI.Button(new Rect(220, 130, 200, 20), "IntermitenteDerecha"))
        {
            tarjeta.SendMessage("IntermitenteDerecha", luzEstado2);
        }

        luzEstado3 = GUI.Toggle(new Rect(120, 160, 80, 20), luzEstado3, "on");
        if (GUI.Button(new Rect(220, 160, 200, 20), "luzPresionDropbox"))
        {
            tarjeta.SendMessage("luzPresionDropbox", luzEstado3);
        }

        luzEstado4 = GUI.Toggle(new Rect(120, 190, 80, 20), luzEstado4, "on");
        if (GUI.Button(new Rect(220, 190, 200, 20), "LuzFiltroTransmision"))
        {
            tarjeta.SendMessage("LuzFiltroTransmision", luzEstado4);
        }

        luzEstado5 = GUI.Toggle(new Rect(120, 220, 80, 20), luzEstado5, "on");
        if (GUI.Button(new Rect(220, 220, 200, 20), "luzPresionUpbox"))
        {
            tarjeta.SendMessage("luzPresionUpbox", luzEstado5);
        }

        luzEstado6 = GUI.Toggle(new Rect(120, 250, 80, 20), luzEstado6, "on");
        if (GUI.Button(new Rect(220, 250, 200, 20), "luzDetenerMotor"))
        {
            tarjeta.SendMessage("luzDetenerMotor", luzEstado6);
        }

        luzEstado7 = GUI.Toggle(new Rect(120, 280, 80, 20), luzEstado7, "on");
        if (GUI.Button(new Rect(220, 280, 200, 20), "luzTemperaturaUpboxDropbox"))
        {
            tarjeta.SendMessage("luzTemperaturaUpboxDropbox", luzEstado7);
        }

        luzEstado8 = GUI.Toggle(new Rect(120, 310, 80, 20), luzEstado8, "on");
        if (GUI.Button(new Rect(220, 310, 200, 20), "luzMantenimientoMotor"))
        {
            tarjeta.SendMessage("luzMantenimientoMotor", luzEstado8);
        }

        luzEstado9 = GUI.Toggle(new Rect(120, 340, 80, 20), luzEstado9, "on");
        if (GUI.Button(new Rect(220, 340, 200, 20), "LuzNivelBajoBombaLubricacion"))
        {
            tarjeta.SendMessage("LuzNivelBajoBombaLubricacion", luzEstado9);
        }

        luzEstado10 = GUI.Toggle(new Rect(120, 370, 80, 20), luzEstado10, "on");
        if (GUI.Button(new Rect(220, 370, 200, 20), "LuzEmergenciaBajaPresion"))
        {
            tarjeta.SendMessage("LuzEmergenciaBajaPresion", luzEstado10);
        }

        luzEstado11 = GUI.Toggle(new Rect(120, 400, 80, 20), luzEstado11, "on");
        if (GUI.Button(new Rect(220, 400, 200, 20), "LuzEmergenciaAltaPresion"))
        {
            tarjeta.SendMessage("LuzEmergenciaAltaPresion", luzEstado11);
        }
        
           
       
        string estadoLuces = "";
        estadoLuces += "ControlLucesDelanteras aux " + tarjeta.ControlLucesDelanteras1() + " " + tarjeta.ControlLucesDelanteras2() + "\n";
        estadoLuces += "ControlLucesDelanteras " + tarjeta.ControlLucesDelanteras() + "\n";
         
        estadoLuces += "ControlLucesCarga " + tarjeta.ControlLucesCarga() + "\n";
        estadoLuces += "ControlLucesTraseras " + tarjeta.ControlLucesTraseras() + "\n";
        estadoLuces += "ControlManualMotor " + tarjeta.controlManualMotor() + "\n";
        //estadoLuces += "PruebaDeFrenos aux " + tarjeta.PruebaDeFrenos1() + " " + tarjeta.PruebaDeFrenos2() + "\n";
        estadoLuces += "PruebaDeFrenos " + tarjeta.PruebaDeFrenos() + "\n";
        estadoLuces += "ignicion aux " + tarjeta.ignicion1() +" "+ tarjeta.ignicion2() + "\n";
        estadoLuces += "ignicion " + tarjeta.ignicion() + "\n";
        estadoLuces += "acelerador " + tarjeta.Acelerador() + "\n";
        estadoLuces += "desacelerador " + tarjeta.Retardador() + "\n";
        estadoLuces += "freno " + tarjeta.Freno() + "\n";
        estadoLuces += "BotonAccion " + tarjeta.BotonAccion() + "\n";
        //estadoLuces += "Horizontal " + Input.GetAxis("Horizontal") + "\n";
        //estadoLuces += "Vertical " + Input.GetAxis("Acelerador") + "\n";
        //estadoLuces += "Horizontal2 " + Input.GetAxis("Horizontal2") + "\n";
        //estadoLuces += "Vertical2 " + Input.GetAxis("Vertical2") + "\n";
        GUI.Label(new Rect(450, 10, 400, Screen.height - 20), estadoLuces);
        

    }

    // Update is called once per frame
    void Update () {
	
	}
}
