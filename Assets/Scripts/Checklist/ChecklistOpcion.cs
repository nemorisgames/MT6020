using UnityEngine;
using System.Collections;

public class ChecklistOpcion : MonoBehaviour {
	public int categoria;
	public int indice;
	public UIToggle check1;
	public UIToggle check2;

	public UILabel titulo;

	// Use this for initialization
	void Start () {
	
	}

	public void activarVerde(bool activo){
		if(activo)
			transform.parent.gameObject.SendMessage ("guardarRespuestaVerde", indice);
	}
	public void activarRojo(bool activo){
		if(activo)
			transform.parent.gameObject.SendMessage ("guardarRespuestaRojo", indice);
	}

	public void inicializar(int categoria, int indice, string titulo, int sel){
		this.categoria = categoria;
		if(indice%2 == 0)
			gameObject.GetComponent<UISprite>().spriteName = "velo-popup";
		this.indice = indice;
		this.titulo.text = titulo;
		check1.startsActive = sel == 1;
		check2.startsActive = sel == 2;
		check1.group = indice + 1;
		check2.group = indice + 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
