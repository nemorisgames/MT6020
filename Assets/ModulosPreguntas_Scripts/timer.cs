using UnityEngine;
using System.Collections;

public class timer : MonoBehaviour {
	public GameObject test;
	public UILabel tiempo;
	public float tiempototal;
	public float tiempoActual;
	int minutos;
	int segundos;
	int mseg;
	public bool start=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			tiempoActual=tiempototal;
			minutos=(int)tiempoActual/60;
			segundos=(int)(tiempoActual-(minutos*60));

			mseg=(int)(tiempoActual-tiempoActual*100);
			tiempo.text=""+(0+(int)minutos/10)+""+(minutos-((int)minutos/10)*10)+":"+(0+(int)segundos/10)+""+(segundos-((int)segundos/10)*10);
			tiempototal-=1*Time.deltaTime;
			if(tiempototal==0){
				test.GetComponent<cargarPreguntas>().finTiempo();
				start=false;
			}
		}
	}
	public void comenzar(int tiempot){
		start = true;
		tiempototal = tiempot;
	}
}
