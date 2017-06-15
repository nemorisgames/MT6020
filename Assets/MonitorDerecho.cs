using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorDerecho : MonoBehaviour {
	public UISprite botonEncendido;
	public UISprite boton1;
	public UISprite boton2;
	public GameObject bg;
	public ControlCamion ctrl;
	bool encendido = false;
	int modo = 1;
	bool visible = false;

	// Use this for initialization
	void Start () {
		if (botonEncendido == null && boton1 == null && boton2 == null && bg == null) {
			botonEncendido = transform.FindChild ("Boton").GetComponentInChildren<UISprite> ();
			boton1 = transform.FindChild ("Boton1").GetComponentInChildren<UISprite> ();
			boton2 = transform.FindChild ("Boton2").GetComponentInChildren<UISprite> ();
			bg = transform.FindChild ("bg_negro").gameObject;
		}
		if(ctrl == null)
			ctrl = GameObject.FindGameObjectWithTag ("Maquina").GetComponent<ControlCamion> ();
	}

	void Update(){
		if (ctrl.estado == ControlCamion.EstadoMaquina.encendida)
			bg.GetComponent<UISprite> ().depth = 0;
		else {
			bg.GetComponent<UISprite> ().depth = 5;
			bg.SetActive (true);
			botonEncendido.spriteName = botonEncendido.name + "_Off";
			boton1.spriteName = boton1.name + "_off";
			boton2.spriteName = boton2.name + "_off";
		}
	}


	public void ToggleEncendido(){
		if (ctrl.estado == ControlCamion.EstadoMaquina.encendida) {
			bg.SetActive (encendido);
			encendido = !encendido;
			if (encendido) {
				botonEncendido.spriteName = botonEncendido.name + "_On";
				if(modo == 1)
					boton1.spriteName = boton1.name + "_on";
				else if(modo == 2)
					boton2.spriteName = boton2.name + "_on";
			} else {
				botonEncendido.spriteName = botonEncendido.name + "_Off";
				boton1.spriteName = boton1.name + "_off";
				boton2.spriteName = boton2.name + "_off";
			}
		}
	}

	public void ToggleModo1(){
		if (ctrl.estado == ControlCamion.EstadoMaquina.encendida && modo == 2) {
			ctrl.cambiarCamara ();
			modo = 1;
			boton1.spriteName = boton1.name + "_on";
			boton2.spriteName = boton2.name + "_off";
		}
	}

	public void ToggleModo2(){
		if (ctrl.estado == ControlCamion.EstadoMaquina.encendida && modo == 1) {
			ctrl.cambiarCamara ();
			modo = 2;
			boton1.spriteName = boton1.name + "_off";
			boton2.spriteName = boton2.name + "_on";
		}
	}
}