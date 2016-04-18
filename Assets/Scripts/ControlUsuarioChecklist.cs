using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlUsuarioChecklist : MonoBehaviour {
    ControlTarjetaControladora controlTarjetaControladora;
    public float velocidadRotacion = 10f;
	public float velocidadZoom = 10f;
	public Vector2 limiteZoom;
	public Vector2 limiteAlturas;
	public float limiteHorizontal;
	public Camera camara;
	//public bool enfocandoPuertaHidraulica = false;
	//public bool enfocandoPuertaHidraulicaActual = false;

	public bool enfocandoCabina = false;
	public bool enfocandoCabinaActual = false;
    //public bool enfocandoBrazo = false;
    //public bool enfocandoBrazoActual = false;

    //public bool enfocandoEncendido = false;
    //public bool enfocandoEncendidoActual = false;

    /*public ControlChecklist controlChecklist;
	public UILabel mensajeInteraccion;
	public bool realizarChecklist = true;
	GameObject maquina;
	ControlExcavadora controlExcavadora;
	Central central;
	int[] valoresPotenciometro = new int[6];
    */

    public LayerMask mascaraLayers;

    public bool movimientoActivado = true;

    public InGame inGame;
    public Animator operarioAnimator;
    public Animator camionAnimator;
    public Animator camaraEntradaAnimator;

	// Use this for initialization
	void Start () {
        inGame = GameObject.FindWithTag("InGame").GetComponent<InGame>();
        controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();
        /*central = GameObject.FindWithTag ("Central").GetComponent<Central>();
		maquina = GameObject.FindWithTag ("Maquina");
		if(maquina != null) controlExcavadora = maquina.GetComponent<ControlExcavadora> ();
		mensajeInteraccion.gameObject.SetActive (false);*/
    }

	public void desactivar(){
        /*
		print("desactiva");
		mensajeInteraccion.gameObject.SetActive(false);
		enfocandoPuertaHidraulicaActual = false;
		enfocandoCabinaActual = false;
		enfocandoBrazoActual = false;
		controlChecklist.extintorFrontActivada = false;
		controlChecklist.extintorBackActivada = false;
		controlChecklist.extintorAutomaticoActivada = false;
		controlChecklist.nivelRefrigeranteActivada = false;
		controlChecklist.nivelPetroleoActivada = false;
		controlChecklist.nivelAceiteActivada = false;
		controlChecklist.nivelHidraulicoActivada = false;*/
	}



	//0: freno
	//1: acelerador
	//2: joy izq X
	//3: joy izq Y
	//4: joy der X
	//5: joy der Y
	void potenciometros(int[] valores){
		//valoresPotenciometro = valores;
	}
	
	// Update is called once per frame
	void Update () {
		float hor = 0f;
		float hor2 = 0f;
		float ver = 0f;
		float ver2 = 0f;
#if UNITY_EDITOR
		hor = Input.GetAxis("ManubrioEditor");
#else
        hor = Input.GetAxis("Manubrio");
#endif
        hor2 = 0f;// Input.GetAxis("Horizontal");
        ver = 0f;
        ver2 = controlTarjetaControladora.Acelerador();
//#else
		/*hor = ((valoresPotenciometro[2] * 1f) - 512f) / 1024f; //Input.GetAxis ("Joy1 Axis 1"); 
		hor2 = ((valoresPotenciometro[4] * 1f) - 512f) / 1024f; //Input.GetAxis("Joy1 Axis 3");//joy der
		ver = ((valoresPotenciometro[3] * 1f) - 512f) / 1024f; //Input.GetAxis("Joy1 Axis 2"); 
		ver2 = ((valoresPotenciometro[5] * 1f) - 512f) / 1024f; //Input.GetAxis("Joy1 Axis 4");//joy der
		
		hor = VariablesGlobales.calcularPresicion(hor);
		hor2 = VariablesGlobales.calcularPresicion(hor2);
		ver = VariablesGlobales.calcularPresicion(ver);
		ver2 = VariablesGlobales.calcularPresicion(ver2);
        */
//#endif

		if(GetComponent<Rigidbody>() != null && movimientoActivado)
			GetComponent<Rigidbody>().AddForce ((hor2 * transform.right + ver2 * new Vector3(transform.forward.x, 0f, transform.forward.z)).normalized * 450f * 1f *  Time.deltaTime);
		transform.Rotate (Vector3.right, ver * velocidadRotacion * 1.5f * Time.deltaTime);
		transform.Rotate (Vector3.up, hor * velocidadRotacion * 1.5f * Time.deltaTime);
		transform.eulerAngles = new Vector3 (Mathf.Clamp((transform.eulerAngles.x>270f?(transform.eulerAngles.x - 360f):transform.eulerAngles.x), -20f, 30f), transform.eulerAngles.y, 0f);


        enfocandoCabina = false;
        /*
        enfocandoPuertaHidraulica = false;
		enfocandoBrazo = false;
		enfocandoEncendido = false;
        if (controlChecklist != null) {
			controlChecklist.nivelAceiteActivada = false;
			controlChecklist.nivelHidraulicoActivada = false;
			controlChecklist.nivelPetroleoActivada = false;
			controlChecklist.nivelRefrigeranteActivada = false;
			controlChecklist.extintorBackActivada = false;
			controlChecklist.extintorFrontActivada = false;
			controlChecklist.extintorAutomaticoActivada = false;
			controlChecklist.pasadorActivada = false;
			controlChecklist.ejeCentralActivada = false;
			controlChecklist.ejeCentralBajoActivada = false;
		}
		if(controlChecklist == null || (controlChecklist != null && (!controlChecklist.activa || !controlChecklist.listaActiva))){
				RaycastHit hit;
				
				Debug.DrawRay(camara.transform.position, camara.transform.forward);
				if (Physics.Raycast (camara.transform.position, camara.transform.forward, out hit, 1.2f, mascaraLayers)) {
					//print (hit.transform.gameObject.name + " " + hit.distance);
					switch(hit.transform.gameObject.name){
						case "nivelExtintorFront": controlChecklist.extintorFrontActivada = true; break;
						case "nivelExtintorBack": controlChecklist.extintorBackActivada = true; break;
						case "nivelExtintorAutomatico": controlChecklist.extintorAutomaticoActivada = true; break;
						case "nivelRefrigerante": controlChecklist.nivelRefrigeranteActivada = true; break;
						case "FiltroAceite": controlChecklist.nivelPetroleoActivada = true; break;
						case "nivelAceite": controlChecklist.nivelAceiteActivada = true; break;
						case "nivelHidraulico": controlChecklist.nivelHidraulicoActivada = true; break;
						case "PuertaHidraulica": enfocandoPuertaHidraulica = true; break;
						case "puertaCabina": enfocandoCabina = true; break;
						case "Brazo": enfocandoBrazo = true; break;
						case "ISO_switch": enfocandoEncendido = true; break;
						case "Pasador": controlChecklist.pasadorActivada = true; break;
						case "EjeCentral": controlChecklist.ejeCentralActivada = true; break;
						case "EjeCentralBajo": controlChecklist.ejeCentralBajoActivada = true; break;
					}
				}

		}
		if (realizarChecklist) {
			if (enfocandoPuertaHidraulicaActual != enfocandoPuertaHidraulica) {
				enfocandoPuertaHidraulicaActual = enfocandoPuertaHidraulica;
				if (enfocandoPuertaHidraulica) {
					controlChecklist.habilitarPuertaHidraulica ();
					if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18"){
						print ("enfocando puerta h " + SceneManager.GetActiveScene().name);
						mensajeInteraccion.text = "Presione el gatillo derecho para abrir/cerrar la puerta hidráulica";
					}
				} else 
					controlChecklist.deshabilitarPuertaHidraulica ();
				if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18") 
					mensajeInteraccion.gameObject.SetActive (enfocandoPuertaHidraulica);

			}
			if (enfocandoCabinaActual != enfocandoCabina) {
				enfocandoCabinaActual = enfocandoCabina;
				if (enfocandoCabina) {
					controlChecklist.habilitarCabina ();
					if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18"){
						print ("enfocando puerta " + SceneManager.GetActiveScene().name);
						mensajeInteraccion.text = "Presione el gatillo derecho para ingresar a la cabina";
					}
				} else 
					controlChecklist.deshabilitarCabina ();
				if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18") 
					mensajeInteraccion.gameObject.SetActive (enfocandoCabina);

			}
			if (enfocandoBrazoActual != enfocandoBrazo) {
					enfocandoBrazoActual = enfocandoBrazo;
					if (enfocandoBrazo) {
						controlChecklist.habilitarBrazo ();
					if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18"){
							print ("enfocando brazo " + SceneManager.GetActiveScene().name);
							mensajeInteraccion.text = "Presione el gatillo derecho para subir/bajar el brazo";
						}
					} else 
							controlChecklist.deshabilitarBrazo ();
				if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18") mensajeInteraccion.gameObject.SetActive (enfocandoBrazo);

			}
			if (enfocandoEncendidoActual != enfocandoEncendido) {
				enfocandoEncendidoActual = enfocandoEncendido;
				if (enfocandoEncendido) {
					if(controlChecklist.estadoExcavadoraChecklist == ControlExcavadora.EstadoMaquina.apagadaTotal)
						mensajeInteraccion.text = "Presione el gatillo derecho para girar la llave de encendido";
					else
						mensajeInteraccion.text = "La acción ya fue realizada. Entre a la cabina";
				}
				mensajeInteraccion.gameObject.SetActive (enfocandoEncendido);
			}
		} else {
			if(central.estado != Central.EstadoSimulacion.ApagadoExterior && central.estado != Central.EstadoSimulacion.Finalizando){
				if (enfocandoCabinaActual != enfocandoCabina) {
					enfocandoCabinaActual = enfocandoCabina;
					if (enfocandoCabina) {
						if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18") 
							mensajeInteraccion.text = "Presione el gatillo derecho para ingresar a la cabina";
					} 
					if(SceneManager.GetActiveScene().name == "Modulo5" || SceneManager.GetActiveScene().name == "Modulo4" || SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18") 
						mensajeInteraccion.gameObject.SetActive (enfocandoCabina);
					
				}
				if (enfocandoEncendidoActual != enfocandoEncendido) {
					enfocandoEncendidoActual = enfocandoEncendido;
					if (enfocandoEncendido) {
						if(controlExcavadora.estado == ControlExcavadora.EstadoMaquina.apagadaTotal)
							if(SceneManager.GetActiveScene().name == "Modulo5") mensajeInteraccion.text = "Presione el gatillo derecho para girar la llave de encendido";
						else
							if(SceneManager.GetActiveScene().name == "Modulo5") mensajeInteraccion.text = "La acción ya fue realizada. Entre a la cabina";
					}
					if(SceneManager.GetActiveScene().name == "Modulo5") mensajeInteraccion.gameObject.SetActive (enfocandoEncendido);
				}
			}
			else{
				if (enfocandoCabinaActual != enfocandoCabina) {
					enfocandoCabinaActual = enfocandoCabina;
					if (enfocandoCabina) {
						if(SceneManager.GetActiveScene().name == "Modulo5") mensajeInteraccion.text = "Vaya al área del Iso Switch";
					} 
					if(SceneManager.GetActiveScene().name == "Modulo5") mensajeInteraccion.gameObject.SetActive (enfocandoCabina);
					
				}
				if (enfocandoEncendidoActual != enfocandoEncendido) {
					enfocandoEncendidoActual = enfocandoEncendido;
					if (enfocandoEncendido) {
						if(controlExcavadora.estado == ControlExcavadora.EstadoMaquina.apagada)
							if(SceneManager.GetActiveScene().name == "Modulo5") mensajeInteraccion.text = "Para finalizar el apagado de equipo, presione el gatillo derecho";
					}
					if(SceneManager.GetActiveScene().name == "Modulo5") mensajeInteraccion.gameObject.SetActive (enfocandoEncendido);
				}
			}
		}*/

        //NUEVO
        if (!enfocandoCabina)
        {
            RaycastHit hit;

            Debug.DrawRay(camara.transform.position, camara.transform.forward);
            if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, 1.2f, mascaraLayers))
            {
                //print (hit.transform.gameObject.name + " " + hit.distance);
                switch (hit.transform.gameObject.name)
                {
                    case "Puerta": enfocandoCabina = true; break;
                }
            }
        }

        if (enfocandoCabina && Input.GetKeyDown(KeyCode.Space))
        {
            operarioAnimator.gameObject.SetActive(true);
            operarioAnimator.SetTrigger("Entrar");
            camionAnimator.SetTrigger("Entrada");
            camaraEntradaAnimator.gameObject.SetActive(true);
            camaraEntradaAnimator.SetTrigger("Entrar");
            print("entrar aqui");
            inGame.ejecutarEntradaMaquina();
            gameObject.SetActive(false);
        }
    }

    void OnDisable(){
		/*if (mensajeInteraccion != null) {
			mensajeInteraccion.text = "";
			mensajeInteraccion.gameObject.SetActive (false);
		}*/
	}

	void entregaTerminada(){
		movimientoActivado = true;
		/*controlChecklist.activar (true);*/
	}

	public void resetValores(){
		//enfocandoEncendidoActual = false;
	}
}
