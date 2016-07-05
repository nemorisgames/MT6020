using UnityEngine;
using System.Collections;

public class iconosBanner3 : MonoBehaviour {
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
		if(diapos.diapoActual>2 && diapos.diapoActual<15){
			banner1.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>15 && diapos.diapoActual<23){
			banner2.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}
		if(diapos.diapoActual>23 && diapos.diapoActual<25){
			banner3.GetComponent<UISprite>().spriteName="fondo-boton-menu-activo";
		}

	}
}
