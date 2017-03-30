﻿using UnityEngine;
using System.Collections;

public class TableroControl : MonoBehaviour {
    public GameObject[] indicadoresSuperiores;

	Transform agujaTemperatura;
	Transform agujaRevoluciones;
	Transform agujaPetroleo;
	// Use this for initialization
	void Start () {
		agujaTemperatura = transform.FindChild ("IndicadorTemperatura");
		agujaRevoluciones = transform.FindChild ("IndicadorRevoluciones");
		agujaPetroleo = transform.FindChild ("IndicadorPetroleo");
	}

    public void encenderReversa(bool encender){ indicadoresSuperiores[13].SetActive(!encender); }
    public void encenderNeutro(bool encender) { indicadoresSuperiores[14].SetActive(!encender); }
    public void encenderAdelante(bool encender) { indicadoresSuperiores[15].SetActive(!encender); }
    public void encenderAuto(bool encender) { indicadoresSuperiores[16].SetActive(!encender); }
    public void encenderManual(bool encender) { indicadoresSuperiores[17].SetActive(!encender); }

	public void setPetroleo(float porcentaje){
		agujaPetroleo.rotation = Quaternion.Euler (0f, 0f, -179f * porcentaje / 100f);
	}
	public void setRevoluciones(float porcentaje){
		agujaRevoluciones.rotation = Quaternion.Euler (0f, 0f, 15f -210f * porcentaje / 100f);
	}
	public void setTemperatura(float porcentaje){
		agujaTemperatura.rotation = Quaternion.Euler (0f, 0f, -179f * porcentaje / 100f);
	}
    // Update is called once per frame
    void Update () {
	
	}
}
