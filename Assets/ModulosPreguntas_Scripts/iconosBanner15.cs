using UnityEngine;
using System.Collections;

public class iconosBanner15 : MonoBehaviour {
	public GameObject presentacion;
	PresentacionInfo diapos;
	public GameObject banner1;
	public GameObject banner2;
	public GameObject banner3;
	public GameObject banner4;
	public GameObject banner5;
	public GameObject banner6;

	// Use this for initialization
	void Start () {
		diapos=presentacion.GetComponent<PresentacionInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		banner1.GetComponent<UISprite>().spriteName="fondo-boton-menu-inactivo";
		banner2.GetComponent<UISprite>().spriteName="fondo-boton-menu-inactivo";
		banner3.GetComponent<UISprite>().spriteName="fondo-boton-menu-inactivo";
		banner4.GetComponent<UISprite>().spriteName="fondo-boton-menu-inactivo";
		banner5.GetComponent<UISprite>().spriteName="fondo-boton-menu-inactivo";
		banner6.GetComponent<UISprite>().spriteName="fondo-boton-menu-inactivo";
		if(diapos.diapoActual>2 && diapos.diapoActual<4){
			banner1.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>4 && diapos.diapoActual<6){
			banner2.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>6 && diapos.diapoActual<8){
			banner3.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>8 && diapos.diapoActual<10){
			banner4.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>10 && diapos.diapoActual<12){
			banner5.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>12 && diapos.diapoActual<15){
			banner6.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}

	}
}
