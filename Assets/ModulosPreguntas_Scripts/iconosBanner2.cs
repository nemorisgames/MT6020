using UnityEngine;
using System.Collections;

public class iconosBanner2 : MonoBehaviour {
	public GameObject presentacion;
	PresentacionInfo diapos;
	public GameObject banner1;
	public GameObject banner2;
	public GameObject banner3;
	public GameObject banner4;
	public GameObject banner5;

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
		if(diapos.diapoActual>2 && diapos.diapoActual<6){
			banner1.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>6 && diapos.diapoActual<13){
			banner2.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>13 && diapos.diapoActual<18){
			banner3.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>18 && diapos.diapoActual<21){
			banner4.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>21 && diapos.diapoActual<33 ){
			banner5.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}

	}
}
