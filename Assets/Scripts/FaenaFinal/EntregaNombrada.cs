using UnityEngine;
using System.Collections;

public class EntregaNombrada : MonoBehaviour {
	[HideInInspector]
	public UnityEngine.AI.NavMeshAgent navmesh;
	public bool activo = false;
	public puntosEntrega[] puntos;
	public int puntoActual = 0;
	public bool enEspera = false;
	public GameObject callbackFinal;
	public bool detenido = false;
	public bool autodestruir = true;
	public bool carga = true;
	float velocidad;
	public float velocidadActual = 0f;
	float velocidadObjetivo;
	public TweenRotation[] ruedas;
	// Use this for initialization
	void Start () {
		navmesh = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		navmesh.SetDestination (puntos [puntoActual].punto.position);
		velocidad = navmesh.speed;
		velocidadObjetivo = velocidad;
		navmesh.speed = velocidadActual;
		if (activo)
			activar (true);
	}

	public void detenerse(){
		if (!detenido) {
//			print ("detenerse");
			detenido = true;
			//navmesh.enabled = false; 
			velocidadObjetivo = 0f;
			navmesh.angularSpeed = 0f;
			//navmesh.Stop();
		}
	}

	public void continuar(){
		if (detenido && !enEspera) {
//			print ("continuar");
			detenido = false;
			navmesh.angularSpeed = 60f;
			navmesh.enabled = true; 
			velocidadObjetivo = velocidad;
			navmesh.SetDestination (puntos [puntoActual].punto.position);
			//navmesh.Resume();
		}
	}

	public void activar(bool activo){
		this.activo = activo;
		if (activo) {
			navmesh.enabled = true;
			velocidadObjetivo = velocidad;
			navmesh.angularSpeed = 60f;
			//navmesh.Resume();
		}
	}
	
	// Update is called once per frame
	void Update () {
		velocidadActual = Mathf.Lerp (velocidadActual, velocidadObjetivo, 0.6f * Time.deltaTime);
		navmesh.speed = velocidadActual;
		if (!activo) {
			velocidadObjetivo = 0f;
			navmesh.angularSpeed = 0f;
			//navmesh.Stop();
			return;
		}
		if (Vector3.Distance (puntos [puntoActual].punto.position, transform.position) < 1.5f && !enEspera) {
			StartCoroutine(cambiarPunto());
		}

		if (ruedas != null) {
			foreach(TweenRotation t in ruedas){
				if(t != null)
					if(navmesh.speed > 1f) t.PlayForward();
					else t.enabled = false;
				//t.Rotate(navmesh.speed * 2f, 0f, 0f);
			}
		}
	}

	IEnumerator cambiarPunto(){
		enEspera = true;
//		print ("stop");
		if (puntos [puntoActual].tiempoEspera > 0f) { 
			detenerse ();
		}
		yield return new WaitForSeconds (puntos [puntoActual].tiempoEspera);
		puntoActual++;
		if (puntoActual >= puntos.Length) {
			if (callbackFinal != null) 
				callbackFinal.SendMessage ("entregaTerminada");
			if (carga) {
				AnimacionCarga ();
			}
			if(autodestruir) 
				destruir();
			activar(false);
		} else {
//			print ("mover");
			enEspera = false;
			continuar();
			navmesh.SetDestination (puntos [puntoActual].punto.position);
		}
	}

	public void destruir(){
		Destroy (gameObject);
	}

	public void AnimacionCarga(){
		GetComponent<Animator> ().SetTrigger ("Brazo");
	}

}
[System.Serializable]
public class puntosEntrega{
	public Transform punto; 
	public float tiempoEspera;
}