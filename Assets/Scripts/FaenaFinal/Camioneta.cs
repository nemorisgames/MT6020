using UnityEngine;
using System.Collections;

public class Camioneta : MonoBehaviour {
	EntregaNombrada entregaNombrada;
	public bool activa = false;
	public Transform maquina;
	public Transform maquinaIA;

	public float integridadActual = 100f;
	// Use this for initialization
	void Start () {
		entregaNombrada = GetComponent<EntregaNombrada> ();
	}

	public void activar(){
		activa = true;
		entregaNombrada.activar (true);
		print ("camioneta activada");
	}

	public void recibirGolpe(float fuerza){
		fuerza *= 10f;
		print (fuerza);
		integridadActual -= fuerza;
		if (integridadActual <= 0) {
			integridadActual = 0f;
			maquina.parent.SendMessage("fallaOperacionalCamioneta");
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*if (!activa) {
			entregaNombrada.navmesh.enabled = false;
			return;
		}*/
		//if (maquina.gameObject.activeSelf) {

		if(maquinaIA != null && Vector3.Distance(maquinaIA.position, transform.position) < 10f){
			print(Vector3.Distance(maquinaIA.position, transform.position));
			entregaNombrada.detenerse();
		}
		else{
			if(Vector3.Distance(maquina.position, transform.position) > 30f){
				entregaNombrada.detenerse();
			}
			else{
				entregaNombrada.continuar();
			}
		}
		//}

	}
}
