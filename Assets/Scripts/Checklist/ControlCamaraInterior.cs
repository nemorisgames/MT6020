using UnityEngine;
using System.Collections;

public class ControlCamaraInterior : MonoBehaviour {
	
	public float velocidadRotacion = 10f;	
	public UILabel mensajeInteraccion;
	public bool enfocandoCabina = false;
	bool enfocandoCabinaActual = false;
	bool enfocandoPanel = false;
	bool enfocandoPanelActual = false;
	bool enfocandoControles = false;
	bool enfocandoControlesActual = false;
	public ControlChecklist controlChecklist;
	ControlTarjetaControladora controlTarjetaControladora;
	int[] valoresPotenciometro = new int[6];

	//0: freno
	//1: acelerador
	//2: joy izq X
	//3: joy izq Y
	//4: joy der X
	//5: joy der Y
	void potenciometros(int[] valores){
		valoresPotenciometro = valores;
	}
	public ConfiguracionControles configuracionControles;
	public Quaternion rotacionInicial;
	// Use this for initialization
	void Start () {
		GameObject g = GameObject.FindWithTag ("Configuracion");
		if (g != null)
			configuracionControles = g.GetComponent<ConfiguracionControles> ();
		mensajeInteraccion.gameObject.SetActive (false);
		controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();
		rotacionInicial = gameObject.transform.rotation;
	}

	public void desactivar(){
		print("desactiva");
		mensajeInteraccion.gameObject.SetActive(false);
		enfocandoCabinaActual = false;
	}
	
	// Update is called once per frame
	void Update () {
		float hor = 0f;
		float ver = 0f;
		#if UNITY_EDITOR
		hor = Input.GetAxis("ManubrioEditor");
		#else
		//print(Input.GetAxis("Manubrio"));
		hor = Input.GetAxis("Manubrio");
		#endif
		ver = controlTarjetaControladora.Retardador() + controlTarjetaControladora.Freno();
		transform.Rotate (Vector3.up, hor * velocidadRotacion * 3f * Time.deltaTime, Space.World);
		
		transform.Rotate (Vector3.right, - ver * velocidadRotacion * 3f * Time.deltaTime);
		//Vector3 correccion = new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
		//transform. = correccion;
		enfocandoCabina = false;
		enfocandoControles = false;
		enfocandoPanel = false;
		//if (camera.fieldOfView < 35f) {
			RaycastHit hit;
			
			Debug.DrawRay(transform.position, transform.forward);
			if (Physics.Raycast (transform.position, transform.forward, out hit, 6f)) {
				//print (hit.transform.gameObject.name + " " + hit.distance);
				switch(hit.transform.gameObject.name){
				case "puertaCabina": enfocandoCabina = true; break;
				case "Puerta": enfocandoCabina = true; break;
				case "CabinaControles": enfocandoControles = true; break;
				case "CabinaPantalla": enfocandoPanel = true; break;
				}
			}
		//}
		if (enfocandoCabinaActual != enfocandoCabina) {
			enfocandoCabinaActual = enfocandoCabina;
			if(enfocandoCabina){
				controlChecklist.habilitarCabina();
				mensajeInteraccion.text = "Presione el gatillo derecho para salir de la cabina";
			}
			else 
				controlChecklist.deshabilitarCabina();
			mensajeInteraccion.gameObject.SetActive (enfocandoCabina);
			
		}

		/*if (enfocandoControlesActual != enfocandoControles) {
			enfocandoControlesActual = enfocandoControles;
			if(enfocandoControles){
				controlChecklist.habilitarControles();
				mensajeInteraccion.text = "Presione el gatillo derecho para revisar los Controles";
			}
			else 
				controlChecklist.deshabilitarControles();
			mensajeInteraccion.gameObject.SetActive (enfocandoControles);
			
		}*/

		if (enfocandoPanelActual != enfocandoPanel) {
			enfocandoPanelActual = enfocandoPanel;
			if(enfocandoPanel){
				controlChecklist.habilitarPanel();
				mensajeInteraccion.text = "Presione el gatillo derecho para revisar el panel";
			}
			else 
				controlChecklist.deshabilitarPanel();
			mensajeInteraccion.gameObject.SetActive (enfocandoPanel);
			
		}
	}

	void OnDisable(){
		mensajeInteraccion.text = "";
		mensajeInteraccion.gameObject.SetActive(false);
	}
}
