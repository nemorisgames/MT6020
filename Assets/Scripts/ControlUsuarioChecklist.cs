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
	public bool enfocandoPuertaHidraulica = false;
	public bool enfocandoPuertaHidraulicaActual = false;
	public bool enfocandoEscalera = false;
	public bool enfocandoEscaleraActual = false;

	public bool enfocandoAnsu = false;
	public bool enfocandoAnsuActual = false;

	public bool enfocandoMotor = false;
	public bool enfocandoMotorActual = false;

	public bool enfocandoCabina = false;
	public bool enfocandoCabinaActual = false;
    public bool enfocandoBrazo = false;
    public bool enfocandoBrazoActual = false;

    public bool enfocandoEncendido = false;
	public bool enfocandoEncendidoActual = false;

	public bool enfocandoAceite = false;
	public bool enfocandoAceiteActual = false;

    public ControlCamion controlCamion;
    public bool puertaIsoSwitchAbierta = false;
    public TweenRotation puertaIsoSwitch;
    public TweenRotation isoSwitch;
	public TweenRotation tapaRadiador;

    public ControlChecklist controlChecklist;
	public UILabel mensajeInteraccion;
	public bool realizarChecklist = true;
	GameObject maquina;
	ControlCamion controlExcavadora;
	InGame central;
	//int[] valoresPotenciometro = new int[6];
    

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
		central = GameObject.FindWithTag ("InGame").GetComponent<InGame>();
		/*maquina = GameObject.FindWithTag ("Maquina");
		if(maquina != null) controlExcavadora = maquina.GetComponent<ControlExcavadora> ();
		mensajeInteraccion.gameObject.SetActive (false);*/
    }

	public void desactivar(){  
		print("desactiva");
		mensajeInteraccion.gameObject.SetActive(false);
		enfocandoPuertaHidraulicaActual = false;
		enfocandoCabinaActual = false;
		enfocandoBrazoActual = false;
		controlChecklist.nivelPetroleoActivada = false;
		controlChecklist.nivelAceiteActivada = false;
		controlChecklist.nivelHidraulicoActivada = false;
		controlChecklist.nivelRefrigeranteActivada = false;
		controlChecklist.nivelAceiteTransActivada = false;
		controlChecklist.nivelAceiteCajaTransfActivada = false;
		controlChecklist.filtroAireActivada = false;
		controlChecklist.filtroCombustibleActivada = false;
		controlChecklist.topeEjeCentralActivada = false;
		controlChecklist.indicadoresObstruccionActivada = false;
		controlChecklist.articulacionCentralActivada = false;
		controlChecklist.articulacionDireccionalActivada = false;
		controlChecklist.pasadoresGeneralActivada = false;
		controlChecklist.fugasCilindrosManguerasActivada = false;
		controlChecklist.sistemaAnsulActivada = false;
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

		ver = controlTarjetaControladora.Retardador() + controlTarjetaControladora.Freno();
		ver2 = controlTarjetaControladora.Acelerador();
#else
        //print(Input.GetAxis("Manubrio"));
        hor = Input.GetAxis("Manubrio");

		ver = -controlTarjetaControladora.Retardador() + controlTarjetaControladora.Freno();
		ver2 = controlTarjetaControladora.Acelerador();
#endif
        hor2 = 0f;// Input.GetAxis("Horizontal");
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
        //print(ver2);
		if(GetComponent<Rigidbody>() != null && movimientoActivado)
			GetComponent<Rigidbody>().AddForce ((hor2 * transform.right + ver2 * new Vector3(transform.forward.x, 0f, transform.forward.z)).normalized * 450f * 1f *  Time.deltaTime);
		transform.Rotate (Vector3.right, ver * velocidadRotacion * 1.5f * Time.deltaTime);
		transform.Rotate (Vector3.up, hor * velocidadRotacion * 1.5f * Time.deltaTime);
		transform.eulerAngles = new Vector3 (Mathf.Clamp((transform.eulerAngles.x>270f?(transform.eulerAngles.x - 360f):transform.eulerAngles.x), -20f, 40f), transform.eulerAngles.y, 0f);


        enfocandoCabina = false;
		enfocandoEscalera = false;
		enfocandoAceite = false;
		enfocandoAnsu = false;
		enfocandoMotor = false;
        
        enfocandoPuertaHidraulica = false;
		enfocandoBrazo = false;
		enfocandoEncendido = false;
        if (controlChecklist != null) {
			controlChecklist.nivelPetroleoActivada = false;
			controlChecklist.nivelAceiteActivada = false;
			controlChecklist.nivelHidraulicoActivada = false;
			controlChecklist.nivelRefrigeranteActivada = false;
			controlChecklist.nivelAceiteTransActivada = false;
			controlChecklist.nivelAceiteCajaTransfActivada = false;
			controlChecklist.filtroAireActivada = false;
			controlChecklist.filtroCombustibleActivada = false;
			controlChecklist.topeEjeCentralActivada = false;
			controlChecklist.indicadoresObstruccionActivada = false;
			controlChecklist.articulacionCentralActivada = false;
			controlChecklist.articulacionDireccionalActivada = false;
			controlChecklist.pasadoresGeneralActivada = false;
			controlChecklist.fugasCilindrosManguerasActivada = false;
			controlChecklist.sistemaAnsulActivada = false;
		}
		if(controlChecklist == null || (controlChecklist != null && (!controlChecklist.activa || !controlChecklist.listaActiva))){
				RaycastHit hit;
				
				Debug.DrawRay(camara.transform.position, camara.transform.forward);
				if (Physics.Raycast (camara.transform.position, camara.transform.forward, out hit, 2f, mascaraLayers)) {
					print (hit.transform.gameObject.name + " " + hit.distance);
					switch(hit.transform.gameObject.name){
					case "nivelPetroleo": controlChecklist.nivelPetroleoActivada = true; break;
					case "nivelAceite": controlChecklist.nivelAceiteActivada = true; break;
					case "nivelHidraulico": controlChecklist.nivelHidraulicoActivada = true; break;
					case "nivelRefrigerante": controlChecklist.nivelRefrigeranteActivada = true; break;
					case "nivelAceiteTrans": controlChecklist.nivelAceiteTransActivada = true; break;
					case "nivelAceiteCajaTransf": controlChecklist.nivelAceiteCajaTransfActivada = true; break;
					case "filtroAire": controlChecklist.filtroAireActivada = true; break;
					case "filtroCombustible": controlChecklist.filtroCombustibleActivada = true; break;
					case "topeEjeCentral": controlChecklist.topeEjeCentralActivada = true; break;
					case "indicadoresObstruccion": controlChecklist.indicadoresObstruccionActivada = true; break;
					case "articulacionCentral": controlChecklist.articulacionCentralActivada = true; break;
					case "articulacionDireccional": controlChecklist.articulacionDireccionalActivada = true; break;
					case "pasadoresGeneral": controlChecklist.pasadoresGeneralActivada = true; break;
					case "fugasCilindrosMangueras": controlChecklist.fugasCilindrosManguerasActivada = true; break;
					case "sistemaAnsul": controlChecklist.sistemaAnsulActivada = true; break;

					case "TapaRadiador": enfocandoPuertaHidraulica = true; break;
				case "Escalera": enfocandoEscalera = true; break;
				case "TapaAnsu": enfocandoAnsu = true; break;
				case "TapaMotor": enfocandoMotor = true; break;
					case "TapaAceite": enfocandoAceite = true; break;
						case "puertaCabina": enfocandoCabina = true; break;
						case "Brazo": enfocandoBrazo = true; break;
				case "ISO_switch":case "IsoSwitch": enfocandoEncendido = true; break;
					}
				}

		}
		//if (realizarChecklist) {
		if (enfocandoPuertaHidraulicaActual != enfocandoPuertaHidraulica) {
			enfocandoPuertaHidraulicaActual = enfocandoPuertaHidraulica;
			if (enfocandoPuertaHidraulica) {
				controlChecklist.habilitarPuertaHidraulica ();
				if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
					print ("enfocando puerta h " + SceneManager.GetActiveScene ().name);
					mensajeInteraccion.text = "Presione el gatillo derecho para abrir/cerrar la puerta hidráulica";
				}
			} else
				controlChecklist.deshabilitarPuertaHidraulica ();
			if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8")
				mensajeInteraccion.gameObject.SetActive (enfocandoPuertaHidraulica);

		}
		if (enfocandoEscaleraActual != enfocandoEscalera) {
			enfocandoEscaleraActual = enfocandoEscalera;
			if (enfocandoEscalera) {
				controlChecklist.habilitarEscalera ();
				if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
					print ("enfocando escalera " + SceneManager.GetActiveScene ().name);
					mensajeInteraccion.text = "Presione el gatillo derecho para subir";
				}
			} else
				controlChecklist.deshabilitarEscalera ();
			if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8")
				mensajeInteraccion.gameObject.SetActive (enfocandoEscalera);

		}
		if (enfocandoAnsuActual != enfocandoAnsu) {
			enfocandoAnsuActual = enfocandoAnsu;
			if (enfocandoAnsu) {
				controlChecklist.habilitarAnsu ();
				if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
					print ("enfocando escalera " + SceneManager.GetActiveScene ().name);
					mensajeInteraccion.text = "Presione el gatillo derecho para abrir/cerrar";
				}
			} else
				controlChecklist.deshabilitarAnsu ();
			if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8")
				mensajeInteraccion.gameObject.SetActive (enfocandoAnsu);

		}
		if (enfocandoMotorActual != enfocandoMotor) {
			enfocandoMotorActual = enfocandoMotor;
			if (enfocandoMotor) {
				controlChecklist.habilitarMotor ();
				if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
					print ("enfocando escalera " + SceneManager.GetActiveScene ().name);
					mensajeInteraccion.text = "Presione el gatillo derecho para abrir/cerrar";
				}
			} else
				controlChecklist.deshabilitarMotor ();
			if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8")
				mensajeInteraccion.gameObject.SetActive (enfocandoMotor);

		}
		if (enfocandoAceiteActual != enfocandoAceite) {
			enfocandoAceiteActual = enfocandoAceite;
			if (enfocandoAceite) {
				controlChecklist.habilitarAceite ();
				if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
					print ("enfocando escalera " + SceneManager.GetActiveScene ().name);
					mensajeInteraccion.text = "Presione el gatillo derecho para abrir/cerrar";
				}
			} else
				controlChecklist.deshabilitarAceite ();
			if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8")
				mensajeInteraccion.gameObject.SetActive (enfocandoAceite);

		}
			if (enfocandoCabinaActual != enfocandoCabina) {
				enfocandoCabinaActual = enfocandoCabina;
				if (enfocandoCabina) {
					controlChecklist.habilitarCabina ();
					if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
						print ("enfocando puerta " + SceneManager.GetActiveScene ().name);
						mensajeInteraccion.text = "Presione el gatillo derecho para ingresar a la cabina";
					}
				} else
					controlChecklist.deshabilitarCabina ();
				if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8")
					mensajeInteraccion.gameObject.SetActive (enfocandoCabina);

			}
			if (enfocandoBrazoActual != enfocandoBrazo) {
				enfocandoBrazoActual = enfocandoBrazo;
				if (enfocandoBrazo) {
					controlChecklist.habilitarBrazo ();
					if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
						print ("enfocando brazo " + SceneManager.GetActiveScene ().name);
						mensajeInteraccion.text = "Presione el gatillo derecho para subir/bajar el brazo";
					}
				} else
					controlChecklist.deshabilitarBrazo ();
				if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8")
					mensajeInteraccion.gameObject.SetActive (enfocandoBrazo);

			}
			if (enfocandoEncendidoActual != enfocandoEncendido) {
				enfocandoEncendidoActual = enfocandoEncendido;
				if (enfocandoEncendido) {
					print ("enfocando iso " + SceneManager.GetActiveScene ().name);
					if (controlChecklist != null) {
						if (controlChecklist.estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagadaTotal)
							mensajeInteraccion.text = "Presione el gatillo derecho para girar la llave de encendido";
						else
							mensajeInteraccion.text = "La acción ya fue realizada. Entre a la cabina";
					} else {
						
					}
				}
			if(mensajeInteraccion != null) mensajeInteraccion.gameObject.SetActive (enfocandoEncendido);
			}
		//} else {
		if (central.estado != InGame.EstadoSimulacion.ApagadoExterior && central.estado != InGame.EstadoSimulacion.Finalizando) {
			if (enfocandoCabinaActual != enfocandoCabina) {
				enfocandoCabinaActual = enfocandoCabina;
				if (enfocandoCabina) {
					if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
						mensajeInteraccion.text = "Presione el gatillo derecho para ingresar a la cabina";
					} 
					if (SceneManager.GetActiveScene ().name == "Modulo4" || SceneManager.GetActiveScene ().name == "Modulo7" || SceneManager.GetActiveScene ().name == "Modulo8") {
						mensajeInteraccion.gameObject.SetActive (enfocandoCabina);
					
					}
					if (enfocandoEncendidoActual != enfocandoEncendido) {
						enfocandoEncendidoActual = enfocandoEncendido;
						if (enfocandoEncendido) {
							if (controlExcavadora.estado == ControlCamion.EstadoMaquina.apagadaTotal)
							if (SceneManager.GetActiveScene ().name == "Modulo5")
								mensajeInteraccion.text = "Presione el gatillo derecho para girar la llave de encendido";
							else if (SceneManager.GetActiveScene ().name == "Modulo5")
								mensajeInteraccion.text = "La acción ya fue realizada. Entre a la cabina";
						}
						if (SceneManager.GetActiveScene ().name == "Modulo5")
							mensajeInteraccion.gameObject.SetActive (enfocandoEncendido);
					}
				} else {
					if (enfocandoCabinaActual != enfocandoCabina) {
						enfocandoCabinaActual = enfocandoCabina;
						if (enfocandoCabina) {
							if (SceneManager.GetActiveScene ().name == "Modulo5")
								mensajeInteraccion.text = "Vaya al área del Iso Switch";
						} 
						if (SceneManager.GetActiveScene ().name == "Modulo5")
							mensajeInteraccion.gameObject.SetActive (enfocandoCabina);
					
					}
					if (enfocandoEncendidoActual != enfocandoEncendido) {
						enfocandoEncendidoActual = enfocandoEncendido;
						if (enfocandoEncendido) {
							if (controlExcavadora.estado == ControlCamion.EstadoMaquina.apagada)
							if (SceneManager.GetActiveScene ().name == "Modulo5")
							if(mensajeInteraccion!=null)mensajeInteraccion.text = "Para finalizar el apagado de equipo, presione el gatillo derecho";
						}
						if (SceneManager.GetActiveScene ().name == "Modulo5")
							if(mensajeInteraccion!=null)mensajeInteraccion.gameObject.SetActive (enfocandoEncendido);
					}
				}
			}

			//NUEVO
			//if (!enfocandoCabina)
			//{
			RaycastHit hit2;

			Debug.DrawRay (camara.transform.position, camara.transform.forward);
			if (Physics.Raycast (camara.transform.position, camara.transform.forward, out hit2, 4.2f, mascaraLayers)) {
				//print (hit2.transform.gameObject.name + " " + hit2.distance);
				switch (hit2.transform.gameObject.name) {
				case "Puerta":
					enfocandoCabina = true;
					break;
				case "puertaCabina":
					enfocandoCabina = true;
					break;
				case "IsoSwitch":
					enfocandoEncendido = true;
					break;
				case "Brazo":
					enfocandoBrazo = true;
					break;
				case "ISO_switch":
					enfocandoEncendido = true;
					break;
				}
			}
			//}

			if (enfocandoCabina && (Input.GetKeyDown (KeyCode.E) || Input.GetButton ("Fire3"))) {
				ingresarCabina (true);
			}

			if (enfocandoEncendido && (Input.GetKeyDown (KeyCode.E) || Input.GetButton ("Fire3"))) {
				print ("enfocandoEncendido boton press");
				switch (inGame.estado) {
				case InGame.EstadoSimulacion.EncendidoExterior:
				case InGame.EstadoSimulacion.ApagadoExterior:
					if (!puertaIsoSwitchAbierta)
						puertaIsoSwitch.Toggle ();
					else {
						isoSwitch.Toggle ();
					}
					break;
                
				}
				/*camionAnimator.SetTrigger("Entrada");
            camaraEntradaAnimator.gameObject.SetActive(true);
            camaraEntradaAnimator.SetTrigger("Entrar");
            print("entrar aqui");
            inGame.ejecutarEntradaMaquina();
            gameObject.SetActive(false);*/
			}

			if (enfocandoPuertaHidraulica && (Input.GetKeyDown (KeyCode.E) || Input.GetButton ("Fire3"))) {
				tapaRadiador.Toggle ();

				/*camionAnimator.SetTrigger("Entrada");
	            camaraEntradaAnimator.gameObject.SetActive(true);
	            camaraEntradaAnimator.SetTrigger("Entrar");
	            print("entrar aqui");
	            inGame.ejecutarEntradaMaquina();
	            gameObject.SetActive(false);*/
			}
		} else {
			RaycastHit hit2;

			Debug.DrawRay (camara.transform.position, camara.transform.forward);
			if (Physics.Raycast (camara.transform.position, camara.transform.forward, out hit2, 4.2f, mascaraLayers)) {
				//print (hit2.transform.gameObject.name + " " + hit2.distance);
				switch (hit2.transform.gameObject.name) {
				case "Puerta":
					enfocandoCabina = true;
					break;
				case "puertaCabina":
					enfocandoCabina = true;
					break;
				case "IsoSwitch":
					enfocandoEncendido = true;
					break;
				case "Brazo":
					enfocandoBrazo = true;
					break;
				case "ISO_switch":
					enfocandoEncendido = true;
					break;
				}
			}
			//}

			if (enfocandoCabina && (Input.GetKeyDown (KeyCode.E) || Input.GetButton ("Fire3"))) {
				ingresarCabina (true);
			}

			if (enfocandoEncendido && (Input.GetKeyDown (KeyCode.E) || Input.GetButton ("Fire3"))) {
				print ("enfocandoEncendido boton press");
				switch (inGame.estado) {
				case InGame.EstadoSimulacion.EncendidoExterior:
				case InGame.EstadoSimulacion.ApagadoExterior:
					if (!puertaIsoSwitchAbierta)
					if(!puertaIsoSwitch.isActiveAndEnabled)
						puertaIsoSwitch.Toggle ();
					else {
						if(!isoSwitch.isActiveAndEnabled)
						isoSwitch.Toggle ();
					}
					break;

				}
			}
		}
		//}			
    }

    public void ingresarCabina(bool ingresar)
    {
		if(operarioAnimator != null) operarioAnimator.gameObject.SetActive(ingresar);
        camaraEntradaAnimator.gameObject.SetActive(ingresar);
        if (ingresar)
        {
			if(operarioAnimator != null) operarioAnimator.SetTrigger("Entrar");
            camionAnimator.SetTrigger("Entrada");
            StartCoroutine(entradaPausa());
            camaraEntradaAnimator.SetTrigger("Entrar");
            print("entrar aqui");
        }
        inGame.ejecutarEntradaMaquina(ingresar);
        gameObject.SetActive(!ingresar);
    }

    IEnumerator entradaPausa() {
        print("pausando ingreso");
        yield return new WaitForSeconds(1f);
        camionAnimator.StopPlayback();
        yield return new WaitForSeconds(1f);
        camionAnimator.Play("Entrada");
    }
    
    //se ejecuta cuando la animacion de switch termina
    public void isoSwitchEncendidoTotal()
    {
        puertaIsoSwitch.PlayReverse();
		controlCamion.isoSwitchActivado(controlCamion.estado == ControlCamion.EstadoMaquina.apagadaTotal);
		if(controlChecklist != null)
			controlChecklist.estadoExcavadoraChecklist = controlCamion.estado;
        //inGame.cambiarEstado(InGame.EstadoSimulacion.EncendidoExterior);
		puertaIsoSwitchAbierta = false;
    }
    //se ejecuta cuando la animacion de puerta de switch termina
    public void puertaIsoSwitchAbiertaTotal()
    {
        puertaIsoSwitchAbierta = puertaIsoSwitch.direction == AnimationOrTween.Direction.Forward;
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
