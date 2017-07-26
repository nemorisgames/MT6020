﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorDerecho : MonoBehaviour {
	public bool activadoInicial;
	public UISprite botonEncendido;
	public UISprite boton1;
	public UISprite boton2;
	public GameObject bg;
	public ControlCamion ctrl;
	public ControlChecklist ctrl2;
	bool encendido = false;
	int modo = 1;
	bool visible = false;
	ControlTarjetaControladora tarjeta;
	bool pressed1 = false;
	bool pressed2 = false;
	public bool pressedOn = false;
	int lastEncendido = 0;

	// Use this for initialization
	void Start () {
		tarjeta = GameObject.FindGameObjectWithTag ("TarjetaControladora").GetComponent<ControlTarjetaControladora> ();
		/*if (botonEncendido == null && boton1 == null && boton2 == null && bg == null) {
			botonEncendido = transform.FindChild ("Boton").GetComponentInChildren<UISprite> ();
			boton1 = transform.FindChild ("Boton1").GetComponentInChildren<UISprite> ();
			boton2 = transform.FindChild ("Boton2").GetComponentInChildren<UISprite> ();
			bg = transform.FindChild ("bg_negro").gameObject;
		}
		if(ctrl == null)
			ctrl = GameObject.FindGameObjectWithTag ("Maquina").GetComponent<ControlCamion> ();*/
	}

	void Update(){
		if (tarjeta.monitorDModo1 () == 1 && !pressed1) {
			ToggleModo1 ();
			pressed1 = true;
		} else
			pressed1 = false;
		if (tarjeta.monitorDModo2 () == 1 && !pressed2) {
			ToggleModo2 ();
			pressed2 = true;
		} else
			pressed2 = false;
		if (tarjeta.monitorDEncendido () == 1) {
			pressedOn = true;
			if(tarjeta.monitorDEncendido() != lastEncendido)
				ToggleEncendido ();
		} else
			pressedOn = false;

		lastEncendido = tarjeta.monitorDEncendido ();

		if (ctrl != null && ctrl2 != null && (ctrl.estado == ControlCamion.EstadoMaquina.encendida || ctrl2.estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida))
			bg.GetComponent<UISprite> ().depth = 0;
		else {
			bg.GetComponent<UISprite> ().depth = 5;
			bg.SetActive (true);
			encendido = false;
			botonEncendido.spriteName = botonEncendido.name + "_Off";
			boton1.spriteName = boton1.name + "_off";
			boton2.spriteName = boton2.name + "_off";
		}
	}


	public void ToggleEncendido(){
		//Debug.Log (encendido);
		if (ctrl != null && ctrl2 != null && (ctrl.estado == ControlCamion.EstadoMaquina.encendida || ctrl2.estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)) {
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
		if (ctrl != null && ctrl2 != null && (ctrl.estado == ControlCamion.EstadoMaquina.encendida || ctrl2.estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) && modo == 2) {
			ctrl.cambiarCamara ();
			ctrl2.cambiarCamara ();
			modo = 1;
			boton1.spriteName = boton1.name + "_on";
			boton2.spriteName = boton2.name + "_off";
		}
	}

	public void ToggleModo2(){
		if (ctrl != null && ctrl2 != null && (ctrl.estado == ControlCamion.EstadoMaquina.encendida || ctrl2.estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) && modo == 1) {
			ctrl.cambiarCamara ();
			ctrl2.cambiarCamara ();
			modo = 2;
			boton1.spriteName = boton1.name + "_off";
			boton2.spriteName = boton2.name + "_on";
		}
	}
}