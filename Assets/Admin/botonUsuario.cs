using UnityEngine;
using System.Collections;

public class botonUsuario : MonoBehaviour {
	public GameObject CosasDatosUsuario;
	public GameObject volver;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void click(){
		CosasDatosUsuario.SetActive (true);
		volver.SetActive (true);
	}
}
