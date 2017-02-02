using UnityEngine;
using System.Collections;

public class MaquinaIA : MonoBehaviour {
	EntregaNombrada entregaNombrada;
	public bool autorizadoContinuar = false;
	// Use this for initialization
	void Start () {
		entregaNombrada = GetComponent<EntregaNombrada> ();
	}

	void maquinaEnPosicion(){
		autorizadoContinuar = true;
	}

	// Update is called once per frame
	void Update () {

		if(!autorizadoContinuar && entregaNombrada.puntoActual == 2){
			entregaNombrada.detenerse();
		}
		else{
			entregaNombrada.continuar();
		}
	}
}
