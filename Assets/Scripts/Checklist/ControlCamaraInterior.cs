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

    Quaternion localRotation = Quaternion.identity;
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
	float rotY;
	// Use this for initialization

	void Awake(){
		rotacionInicial = gameObject.transform.rotation;
	}

	void Start () {
		GameObject g = GameObject.FindWithTag ("Configuracion");
		if (g != null)
			configuracionControles = g.GetComponent<ConfiguracionControles> ();
		mensajeInteraccion.gameObject.SetActive (false);
		controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();

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
        ver = controlTarjetaControladora.Retardador() + controlTarjetaControladora.Freno();
#else
		//print(Input.GetAxis("Manubrio"));
		hor = Input.GetAxis("Manubrio");
		ver = - controlTarjetaControladora.Retardador() + controlTarjetaControladora.Freno();
#endif
       	//print(hor +" && " + transform.rotation.eulerAngles.y);
        /*if (hor != 0f && (transform.rotation.eulerAngles.y <= 80f || transform.rotation.eulerAngles.y >= 260f))
        {
            if(transform.rotation.eulerAngles.y > 80f)
                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 80f, transform.rotation.eulerAngles.z);
            if (transform.rotation.eulerAngles.y < 260f)
                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 260f, transform.rotation.eulerAngles.z);
            transform.Rotate(Vector3.up, hor * velocidadRotacion * 3f * Time.deltaTime, Space.World);
        }*/
        /*if (hor > 0f)
        {
            if ((transform.rotation.eulerAngles.y <= 80f || transform.rotation.eulerAngles.y >= 260f))
            {
                transform.Rotate(Vector3.up, hor * velocidadRotacion * 3f * Time.deltaTime, Space.World);
            }
        }
        if ((transform.rotation.eulerAngles.y <= 80f || transform.rotation.eulerAngles.y >= 260f))
        { 
            if(transform.rotation.eulerAngles.y >= 260f)
                transform.Rotate(Vector3.up, hor * velocidadRotacion * 3f * Time.deltaTime, Space.World);
        }*/
        transform.Rotate(Vector3.right, ver * velocidadRotacion * 3f * Time.deltaTime);
        transform.Rotate(Vector3.up, hor * velocidadRotacion * 3f * Time.deltaTime, Space.World);
		/*Debug.Log (transform.eulerAngles.y);
		Debug.Log ((transform.eulerAngles.y - 360f) + "," + ((rotacionInicial.eulerAngles.y - 80f)) + "," + ((rotacionInicial.eulerAngles.y + 80f)));
		Debug.Log ((transform.eulerAngles.y) + "," + (rotacionInicial.eulerAngles.y - 80f) + "," + (rotacionInicial.eulerAngles.y + 80f));*/
		float auxY = 0;
		if (transform.eulerAngles.y >= 270 && rotacionInicial.eulerAngles.y < 270)
			auxY = Mathf.Clamp (transform.eulerAngles.y - 360f, rotacionInicial.eulerAngles.y - 80f, rotacionInicial.eulerAngles.y + 80f);
		else
			auxY = Mathf.Clamp (transform.eulerAngles.y, rotacionInicial.eulerAngles.y - 80f, rotacionInicial.eulerAngles.y + 80f);
		//transform.eulerAngles = new Vector3(Mathf.Clamp((transform.eulerAngles.x > 270f ? (transform.eulerAngles.x - 360f) : transform.eulerAngles.x), -20f, 40f), Mathf.Clamp((transform.eulerAngles.y >= 270f ? (transform.eulerAngles.y - 360f) : transform.eulerAngles.y), -80f,  80f), 0f);
		transform.eulerAngles = new Vector3(Mathf.Clamp((transform.eulerAngles.x > 270f ? (transform.eulerAngles.x - 360f) : transform.eulerAngles.x), -20f, 40f), auxY, 0f);

        

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
				mensajeInteraccion.text = "Presione el boton del manubrio para salir de la cabina";
			}
			else 
				controlChecklist.deshabilitarCabina();
			mensajeInteraccion.gameObject.SetActive (enfocandoCabina);
			
		}

		/*if (enfocandoControlesActual != enfocandoControles) {
			enfocandoControlesActual = enfocandoControles;
			if(enfocandoControles){
				controlChecklist.habilitarControles();
				mensajeInteraccion.text = "Presione el boton del manubrio para revisar los Controles";
			}
			else 
				controlChecklist.deshabilitarControles();
			mensajeInteraccion.gameObject.SetActive (enfocandoControles);
			
		}*/

		if (enfocandoPanelActual != enfocandoPanel) {
			enfocandoPanelActual = enfocandoPanel;
			if(enfocandoPanel){
				controlChecklist.habilitarPanel();
				mensajeInteraccion.text = "Presione el boton del manubrio para revisar el panel";
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
