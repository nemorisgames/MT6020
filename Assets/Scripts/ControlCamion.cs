using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlCamion : MonoBehaviour {
    ControlTarjetaControladora controlTarjetaControladora;
    public enum EstadoMaquina{apagadaTotal, apagada, frenoMotorDesactivado, encendida};
	public EstadoMaquina estado = EstadoMaquina.apagadaTotal;
	public ControlCamionMotor controlCamionMotor;
    public HingeJoint jointCentro;
    Animator animator;
    bool animacionEnInicio = true;
    bool animacionEnFinal = false;
    public GameObject camaraTrasera;
    public GameObject camaraBalde;
	public GameObject monitor;

    public InGame ingame;

    /*public HingeJoint jointBrazo;
	Transform cilindroEmpuje;
	Vector2 limiteBrazo = new Vector2 (30f, 100f);
	bool brazoAutomatico = false;
	float fuerzaAplicadaBrazoActual = 0;
	float anguloBrazoFijo = 0f;
	bool nivelandoBrazo = false;
	public HingeJoint jointPala;
	Vector2 limitePala = new Vector2 (-30f, 50f);
	
	public TweenRotation manillaEncendidoExterior;
	public bool vistaExterior = true;
	public GameObject[] PartesMaquina;
	public GameObject[] PartesMaquinaChecklist;
    */
    public ControlUsuarioChecklist controlUsuarioChecklist;
    public Light[] lucesDelanteras;
	public Light[] lucesCarga;
	public Light[] lucesTraseras;
    
    Vector2 rangoLimitEje = new Vector2 (-45f, 45f);
    Vector2 rangoLimitTolba = new Vector2(-1f, 1f);
    /*
	public Vector2 rangoLimitBrazo; 
	//public float limitMaxActual = 0f;
	Vector3 connectedAnchorBrazo;
	Vector3 connectedAnchorPala;

	public Vector2 rangoLimitPala; 
	public Transform[] partesPala;
	Central central;

	public AudioSource fuenteAudio;
	public AudioClip choqueSonido;

	public GameObject monitorDisplay;
	public ControlPantallaTactil controlPantallaTactil;

	public TweenAlpha mensajeApagar;
    */
	public GameObject sonidoGolpePrefab;
	/*
    public bool enTopePala = false;
	public bool enTopeBrazo = false;
	public bool enTopeEje = false;
    */
    public float tiempoEncendido = 2f;
	float tiempoEncendidoB = 0f;
	int lastPosIgnicion = -1;
    /*
	int[] valoresPotenciometro = new int[6];
	
	public int ordenEjecucion = 0;
	public bool ordenEjecucionCorrecta = false;
	ConfiguracionControles configuracionControles;
    */
	Configuracion configuracion;
	//LectorControles lectorControles;
    
	public enum senaletica {ninguno, recta, flechaIzq, flechaDer, giroT, giroTReves, giroU, giroUReves, giroX, giroXReves};
	public senaletica senalActual = senaletica.ninguno;
	public GameObject[] senales;
	public GameObject[] senalesMotor;
    /*
	public CapturaPeso capturaPeso;

	public ControlCamion controlCamion;
	float tiempoSenal = 0f;
	float tiempoSenalSalida = 0f;
    */
	public enum LugarMaquina {Frontal, MotorDer, MotorIzq, TolvaDer, TolvaIzq, Tunel, Buzon};
	public float integridadFrontal = 100f;
	public float integridadMotorDer = 100f;
	public float integridadMotorIzq = 100f;
	public float integridadTolvaDer = 100f;
	public float integridadTolvaIzq = 100f;

    
	//float tiempoDobleClickConteo = -1f;

	//public bool test = false;
    float cooldownGolpe = 0f;

    /*
    bool clickHecho = false;
	bool dobleClickHecho = false;

	int ciclo = 0;

	public Light luzAdvertenciaAdelante;
	public Light luzAdvertenciaAtras;
	public int tipoAdvertenciaAtras = 0;
	public int tipoAdvertenciaAdelante = 0;
	public AudioSource audioRetroceso;
    */
	float tiempoGolpeSonido = 0f;

	float lastDireccion = 0;
    /*
    [HideInInspector]
    public float tiempoOrdenEjecucion = 0f;
    */

	public MonitorIzquierdo monitorTrasero;
	public MonitorIzquierdo monitorDelantero;

    // Use this for initialization
    void Start () {
        ingame = GameObject.FindWithTag("InGame").GetComponent<InGame>();
        controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();
        animator = GetComponent<Animator>();
		monitor = transform.Find ("Delantera_B/Varios/Monitor").gameObject;
		ActivarMonitores (false);
		GameObject.FindGameObjectWithTag ("MonitorDerecho").GetComponent<MonitorDerecho> ().ctrl = this.GetComponent<ControlCamion> ();
		configuracion = GameObject.FindWithTag ("Configuracion").GetComponent<Configuracion>();
        /*
        configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles>();
		connectedAnchorBrazo = jointBrazo.connectedAnchor;
		connectedAnchorPala = jointBrazo.connectedAnchor;
		central = GameObject.FindWithTag ("Central").GetComponent<Central>();
		lectorControles = central.gameObject.GetComponent<LectorControles> ();

		controlPantallaTactil = monitorDisplay.transform.FindChild ("Camera/AnchorBottom/PantallaTactil").gameObject.GetComponent<ControlPantallaTactil> ();
        
        tiempoOrdenEjecucion = Time.time;
        configuracion.cicloCarguio = new ArrayList();
        */
        //quitar esto. se encuentra en ingresar maquina
        resetMaquina();
    }

	void ActivarMonitores(bool b){
		monitor.SetActive (b);
		//camaraBalde.SetActive (b);
	}
    /*
	//0: freno
	//1: acelerador
	//2: joy izq X
	//3: joy izq Y
	//4: joy der X
	//5: joy der Y
	void potenciometros(int[] valores){
		valoresPotenciometro = valores;
	}
    */
	public void cambiarEstado(senaletica senal, bool avancePuntaBalde){
		if (senalActual == senal)
			return;
		senalActual = senal;
		for (int i = 0; i < senales.Length; i++) {
			senales[i].SetActive(false);
			//senalesMotor[i].SetActive(false);
		}
		if(avancePuntaBalde)
			switch(senalActual){
			    case senaletica.ninguno: break;
			    case senaletica.recta: senales[0].SetActive(true); break;
			    case senaletica.giroU: senales[1].SetActive(true); break;
			    case senaletica.giroUReves: senales[2].SetActive(true); break;
			    case senaletica.giroT: senales[3].SetActive(true); break;
			    case senaletica.giroTReves: senales[4].SetActive(true); break;
			    case senaletica.giroX: senales[5].SetActive(true); break;
			    case senaletica.giroXReves: senales[6].SetActive(true); break;
			    case senaletica.flechaIzq: senales[7].SetActive(true); break;
			    case senaletica.flechaDer: senales[8].SetActive(true); break;
			}
		else
			switch(senalActual){
			    case senaletica.ninguno: break;
			    /*case senaletica.recta: senalesMotor[0].SetActive(true); break;
			    case senaletica.giroU: senalesMotor[1].SetActive(true); break;
			    case senaletica.giroUReves: senalesMotor[2].SetActive(true); break;
			    case senaletica.giroT: senalesMotor[3].SetActive(true); break;
			    case senaletica.giroTReves: senalesMotor[4].SetActive(true); break;
			    case senaletica.giroX: senalesMotor[5].SetActive(true); break;
			    case senaletica.giroXReves: senalesMotor[6].SetActive(true); break;
			    case senaletica.flechaIzq: senalesMotor[7].SetActive(true); break;
			    case senaletica.flechaDer: senalesMotor[8].SetActive(true); break;*/
		    }
	}
	/*
	void comprobarOrdenEjecucion(){
		switch (ordenEjecucion) {
		case 0: 
			if(estado == EstadoMaquina.apagada) ordenEjecucion = 1;
			break;
		case 1: 
			if(estado == EstadoMaquina.encendida) ordenEjecucion = 2;
			break;
		case 2:
            if (!controlExcavadoraMotor.frenoParqueoActivado)
            {
                tiempoOrdenEjecucion = Time.time - tiempoOrdenEjecucion;
                ordenEjecucion = 3;
            }
			break;
		case 3: 
			if(controlExcavadoraMotor.frenoParqueoActivado && central.estado == Central.EstadoSimulacion.Finalizando) ordenEjecucion = 4;
			break;
		case 4: 
			if(estado == EstadoMaquina.apagada){ 
				ordenEjecucion = 5;
			}
			break;
		case 5: 
			if(estado == EstadoMaquina.apagadaTotal){ 
				ordenEjecucion = 6;
				ordenEjecucionCorrecta = true;
			}
			break;
		}
	}

	public void advertenciaAmarillaAtras(bool activa){
		if (estado != EstadoMaquina.encendida)
			return;
		if (activa) {
			if (tipoAdvertenciaAtras == 0)
				tipoAdvertenciaAtras = 1;
		} else {
			if(tipoAdvertenciaAtras == 1)
				tipoAdvertenciaAtras = 0;
		}

		if (tipoAdvertenciaAtras == 1 && monitorDisplay.activeSelf) {
			luzAdvertenciaAtras.intensity = .5f;
			luzAdvertenciaAtras.color = Color.yellow;
			if (!audioRetroceso.isPlaying)
				audioRetroceso.Play ();
		} else
		if (tipoAdvertenciaAtras == 0) {
			luzAdvertenciaAtras.intensity = .25f;
			luzAdvertenciaAtras.color = Color.green;
				audioRetroceso.Stop();
			}
	}

	public void advertenciaRojaAtras(bool activa){
		if (estado != EstadoMaquina.encendida)
			return;
		if (activa) {
			tipoAdvertenciaAtras = 2;
		} else {
			tipoAdvertenciaAtras = 0;
		}
		
		if (tipoAdvertenciaAtras == 2 && monitorDisplay.activeSelf) {
			luzAdvertenciaAtras.intensity = .5f;
			luzAdvertenciaAtras.color = Color.red;
			if (!audioRetroceso.isPlaying)
				audioRetroceso.Play ();
		} else
		if (tipoAdvertenciaAtras == 0) {
			luzAdvertenciaAtras.intensity = .25f;
			luzAdvertenciaAtras.color = Color.green;
			audioRetroceso.Stop();
		}
	}

	public void advertenciaAmarillaAdelante(bool activa){
		if (estado != EstadoMaquina.encendida)
			return;
		if (activa) {
			if (tipoAdvertenciaAdelante == 0)
				tipoAdvertenciaAdelante = 1;
		} else {
			if(tipoAdvertenciaAdelante == 1)
				tipoAdvertenciaAdelante = 0;
		}
		
		if (tipoAdvertenciaAdelante == 1 && monitorDisplay.activeSelf) {
			luzAdvertenciaAdelante.intensity = .5f;
			luzAdvertenciaAdelante.color = Color.yellow;
			if (!audioRetroceso.isPlaying)
				audioRetroceso.Play ();
		} else
		if (tipoAdvertenciaAdelante == 0) {
			luzAdvertenciaAdelante.intensity = .25f;
			luzAdvertenciaAdelante.color = Color.green;
			audioRetroceso.Stop();
		}
	}
	
	public void advertenciaRojaAdelante(bool activa){
		if (estado != EstadoMaquina.encendida)
			return;
		if (activa) {
			tipoAdvertenciaAdelante = 2;
		} else {
			tipoAdvertenciaAdelante = 0;
		}
		
		if (tipoAdvertenciaAdelante == 2 && monitorDisplay.activeSelf) {
			luzAdvertenciaAdelante.intensity = .5f;
			luzAdvertenciaAdelante.color = Color.red;
			if (!audioRetroceso.isPlaying)
				audioRetroceso.Play ();
		} else
		if (tipoAdvertenciaAdelante == 0) {
			luzAdvertenciaAdelante.intensity = .25f;
			luzAdvertenciaAdelante.color = Color.green;
			audioRetroceso.Stop();
		}
	}

	//el numero que llega es uno menos que el numero anotado en las notas, por eso se le suma 1
	void BotonDown(int indice){
		//if (estado == EstadoMaquina.apagadaTotal)
		//	return;
		indice = indice + 1;
		//print ("down " + indice);
		
		if(indice == configuracionControles.idBotonEncendido){
			if (estado != EstadoMaquina.apagadaTotal) {
				tiempoEncendido = Time.time + 2f;
                audioRetroceso.clip = Resources.Load("camion") as AudioClip;
                if (estado != EstadoMaquina.encendida)  audioRetroceso.Play();
            }
		}

		if(indice == configuracionControles.idBotonFrenoParqueo){
			if (estado == EstadoMaquina.encendida){
				controlExcavadoraMotor.frenoParqueoActivado = true;
				//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedFrenoParqueo), true);
			}
		}
		if(indice == configuracionControles.idBotonLucesAltasDelanteras){
			if (estado == EstadoMaquina.encendida)
			if(lucesDelanterasAltas.Length > 0){
				lucesAltasPala(!lucesDelanterasAltas[0].gameObject.activeSelf);
				lectorControles.OutCmd(byte.Parse("" + configuracionControles.idLedLucesAltasDelanteras), lucesDelanterasAltas[0].gameObject.activeSelf);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida){ 
				mensajeApagar.Toggle();
			}
			
		}
		if (indice == configuracionControles.idBotonLucesAltasTraseras) {
			if (estado == EstadoMaquina.encendida)
			if (lucesTraserasAltas.Length > 0){
				lucesAltasMotor (!lucesTraserasAltas [0].gameObject.activeSelf);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesAltasTraseras), lucesTraserasAltas [0].gameObject.activeSelf);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonLucesBajasDelanteras){
			if (estado == EstadoMaquina.encendida)
			if(lucesDelanterasBajas.Length > 0){
				lucesBajasPala(!lucesDelanterasBajas[0].gameObject.activeSelf);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasDelanteras), lucesDelanterasBajas [0].gameObject.activeSelf);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
			
		}
		if(indice == configuracionControles.idBotonLucesBajasTraseras){
			if (estado == EstadoMaquina.encendida)
			if(lucesTraserasBajas.Length > 0){
				lucesBajasMotor(!lucesTraserasBajas[0].gameObject.activeSelf);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasTraseras), lucesTraserasBajas [0].gameObject.activeSelf);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonClaxon){
			if (estado == EstadoMaquina.encendida){
				claxon(true);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedClaxon), true);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) 
				mensajeApagar.Toggle();
		}
	}
	void BotonUp(int indice){
		//if (estado == EstadoMaquina.apagadaTotal)
		//	return;
		indice = indice + 1;
		//print ("up " + indice);
		//aux = indice;

		if(indice == configuracionControles.idBotonEncendido){
			if (estado != EstadoMaquina.apagadaTotal && (tiempoEncendido < Time.time || test)){
				tiempoEncendido = 0f;
				arranque(estado == EstadoMaquina.apagada);
                audioRetroceso.clip = Resources.Load("Retroceso") as AudioClip;
                lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedEncendido), estado == EstadoMaquina.encendida);
                //central.SendMessage("encenderLeds", estado == EstadoMaquina.encendida, SendMessageOptions.DontRequireReceiver); 
            }
            audioRetroceso.Stop();
            if ((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonOverride){
			if (estado == EstadoMaquina.encendida){
				overrideMotor(true);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedOverride), true);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonPruebaFrenos){

		}
		if(indice == configuracionControles.idBotonRRC){
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && (estado == EstadoMaquina.encendida || !controlExcavadoraMotor.frenoParqueoActivado)) 
				mensajeApagar.Toggle();
			else{
				RRC(true);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedRRC), true);
			}
		}
		if(indice == configuracionControles.idBotonTransmisionAutomatica){
			if (estado == EstadoMaquina.encendida){
				transmisionAutomatico(controlExcavadoraMotor.tipoCambio == ControlExcavadoraMotor.TipoCambio.manual);
				controlExcavadoraMotor.cambioVelocidad(0);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedTransmisionAutomatica), controlExcavadoraMotor.tipoCambio != ControlExcavadoraMotor.TipoCambio.manual);
				
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), controlExcavadoraMotor.cambioActual == 0);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), controlExcavadoraMotor.cambioActual == 1);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), controlExcavadoraMotor.cambioActual == 2);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), controlExcavadoraMotor.cambioActual == 3);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonDesembragar){
			if (estado == EstadoMaquina.encendida){
				desembragar(true);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedDesembragar), true);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonClaxon){
			if (estado == EstadoMaquina.encendida){
				claxon(false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedClaxon), false);
			}
		}
		if(indice == configuracionControles.idBotonRideControl){
			if (estado == EstadoMaquina.encendida){
				rideControl(!brazoAutomatico);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedRideControl), brazoAutomatico);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonCambio1){
			if (estado == EstadoMaquina.encendida && controlExcavadoraMotor.tipoCambio == ControlExcavadoraMotor.TipoCambio.manual){
				controlExcavadoraMotor.cambioVelocidad(1);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), true);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), false);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonCambio2){
			if (estado == EstadoMaquina.encendida && controlExcavadoraMotor.tipoCambio == ControlExcavadoraMotor.TipoCambio.manual){
				controlExcavadoraMotor.cambioVelocidad(2);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), true);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), false);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonCambio3){
			if (estado == EstadoMaquina.encendida && controlExcavadoraMotor.tipoCambio == ControlExcavadoraMotor.TipoCambio.manual){
				controlExcavadoraMotor.cambioVelocidad(3);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), true);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), false);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonCambio4){
			if (estado == EstadoMaquina.encendida && controlExcavadoraMotor.tipoCambio == ControlExcavadoraMotor.TipoCambio.manual){
				controlExcavadoraMotor.cambioVelocidad(4);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), false);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), true);
			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonFrenoParqueo){
			if (estado == EstadoMaquina.encendida){
				controlExcavadoraMotor.frenoParqueoActivado = false;
				//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedFrenoParqueo), false);
			}
		}
		if(indice == configuracionControles.idBotonDisplayON){
			if (estado != EstadoMaquina.apagadaTotal){
				encenderDisplay(!monitorDisplay.activeSelf);

			}
			if((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}
		if(indice == configuracionControles.idBotonDisplayOFF){
			central.pausaToggle ();
			
		}
		if(indice == configuracionControles.idJDerechoGatillo){
			if (vistaExterior && central.estado != Central.EstadoSimulacion.PanelInicial && (central.controlChecklistFinal == null || (central.controlChecklistFinal != null && !central.controlChecklistFinal.activa))){//estado != EstadoMaquina.encendida){
				//Encendido exterior
					//print ("boton");
					//if (vistaExterior){
					if(controlUsuarioChecklist.enfocandoCabina){
						ingresarCabina(true);
						if(controlExcavadoraMotor.estado == EstadoMaquina.apagada || controlExcavadoraMotor.estado == EstadoMaquina.encendida) 
							central.conduciendo();
					}
					if(controlUsuarioChecklist.enfocandoEncendido){
						controlUsuarioChecklist.resetValores();
						print ("encendido exterior");
						if(controlExcavadoraMotor.estado == EstadoMaquina.apagadaTotal){
							estado = EstadoMaquina.apagada;
							controlExcavadoraMotor.estado = EstadoMaquina.apagada;
							manillaEncendidoExterior.PlayForward();
						}
						else{
							estado = EstadoMaquina.apagadaTotal;
							encenderDisplay (false);
							controlExcavadoraMotor.estado = EstadoMaquina.apagadaTotal;
							manillaEncendidoExterior.PlayReverse();
							if(central.estado == Central.EstadoSimulacion.ApagadoExterior){
								if(central.controlChecklistFinal != null){
									if(!central.controlChecklistFinal.activa)
										central.moduloFinalizar();
								}
								else
									central.moduloFinalizar();
							}
						}
					}
					//}
			}
			else{
				if((central != null && central.estado == Central.EstadoSimulacion.Finalizando && estado == EstadoMaquina.apagada && controlExcavadoraMotor.frenoParqueoActivado) || estado == EstadoMaquina.apagadaTotal || central.estado == Central.EstadoSimulacion.ApagadoExterior){
					if(central.controlChecklistFinal != null){
						if(!central.controlChecklistFinal.activa)
							salirCabinaFinal();
					}
					else
						salirCabinaFinal();
				}
			}
            
		}
		if(indice == configuracionControles.idJDerechoBotonSupIzq){
			if (estado == EstadoMaquina.encendida){
				controlExcavadoraMotor.retrocesoActivado(false);
			}

		}
		if(indice == configuracionControles.idJDerechoBotonSupDer){
			if (estado == EstadoMaquina.encendida){
				controlExcavadoraMotor.retrocesoActivado(true);
			}
		}
		
		if(indice == configuracionControles.idJIzquierdoGatillo){
			if (estado == EstadoMaquina.encendida){
				if(clickHecho) dobleClickHecho = true;
				else StartCoroutine(rutinaDobleClick());
			}
		}
	}

	IEnumerator rutinaDobleClick(){
		clickHecho = true;
		yield return new WaitForSeconds (1f);
		if(!dobleClickHecho){
			controlPantallaTactil.setNumPalas("" + (int.Parse(controlPantallaTactil.numPalas.text) + 1));
			controlPantallaTactil.setPesoPalaAcumulado ("" + (Mathf.Round(capturaPeso.enCarga * 100f) /100f));
			//tiempoDobleClickConteo = -1f;
		}
		else{
			controlPantallaTactil.setNumPalas("" + (int.Parse(controlPantallaTactil.numPalas.text) - 1));
			controlPantallaTactil.setPesoPalaAcumulado ("-" + (Mathf.Round(capturaPeso.enCarga * 100f) / 100f));
			//tiempoDobleClickConteo = -1f;
		}
		dobleClickHecho = false;
		clickHecho = false;
	}

    public void terminaCicloCarguio(int numCiclo, float carga, float tiempo)
    {
        CicloCarguio c = new CicloCarguio();
        c.numero = numCiclo;
        c.carguio = carga;
        c.tiempo = Mathf.RoundToInt(tiempo);
        configuracion.cicloCarguio.Add(c);
    }

    public void encenderDisplay(bool e){
		if (e) {
			advertenciaRojaAtras (false);
			advertenciaRojaAdelante (false);
			advertenciaAmarillaAtras (false);
			advertenciaAmarillaAdelante (false);
			
		} else { 
			lucesAltasPala(false);
			lucesAltasMotor(false);
			lucesBajasMotor(false);
			lucesBajasPala(false);
		}
		luzAdvertenciaAtras.enabled = e;
		luzAdvertenciaAdelante.enabled = e;
		audioRetroceso.Stop ();
		monitorDisplay.SetActive (e);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedDisplayON), monitorDisplay.activeSelf);
	}
	*/

    public void isoSwitchActivado(bool activado)
    {
        if (activado)
        {
            estado = EstadoMaquina.apagada;
            controlCamionMotor.estado = EstadoMaquina.apagada;
            //print("iso activado");
        }
        else
        {
            estado = EstadoMaquina.apagadaTotal;
            controlCamionMotor.estado = EstadoMaquina.apagadaTotal;
            //print("iso desactivado");

			//print ("encendido exterior");

			estado = EstadoMaquina.apagadaTotal;
			if(ingame.estado == InGame.EstadoSimulacion.ApagadoExterior){
				//if(ingame.controlChecklistFinal != null){
				//	if(!central.controlChecklistFinal.activa)
				//		central.moduloFinalizar();
				//}
				//else
				ingame.moduloFinalizar();
			}
        }
    }

    public void resetMaquina(){
        /*
		JointLimits b = jointBrazo.limits;
		b.min = -60f;
		b.max = 0f;
		jointBrazo.transform.localRotation = Quaternion.identity;
		jointBrazo.limits = b;
		jointBrazo.connectedAnchor = connectedAnchorBrazo;

		JointLimits b2 = jointPala.limits;
		b2.min = 0f;
		b2.max = 1f;
		jointPala.transform.localRotation = Quaternion.identity;
		jointPala.limits = b2;
		jointPala.connectedAnchor = connectedAnchorPala;
        */
		JointLimits b3 = jointCentro.limits;
		b3.min = -3.65f;
		b3.max = -3.65f;
		jointCentro.transform.localRotation = Quaternion.identity;
		jointCentro.limits = b3;
	}

    public void animacionInicio()
    {
        animacionEnInicio = true;
        animacionEnFinal = false;
    }

    public void animacionFin()
    {
        animacionEnInicio = false;
        animacionEnFinal = true;
    }

	bool usingArm = false;
	
    // Update is called once per frame
    void Update()
    {
        /*
		ciclo++;
		if (ciclo % 10 == 0) {
			if (estado == ControlExcavadora.EstadoMaquina.encendida) {
				controlPantallaTactil.setOp1menuD2 ("" + Mathf.CeilToInt (Random.Range (2000, 2003) + 1000f * controlExcavadoraMotor.velocidadActual / controlExcavadoraMotor.velocidades [Mathf.Clamp (controlExcavadoraMotor.cambioActual, 1, 4)]));
				controlPantallaTactil.setOp2menuD2 ("" + (Mathf.RoundToInt (Random.Range (24, 26) * 100) / 100f));
				controlPantallaTactil.setOp3menuD2 ("7.5");
				controlPantallaTactil.setBatteryVoltage ("25.0");
				controlPantallaTactil.setAccumPressure ("128.8");
				controlPantallaTactil.setHidraulicTemp ("45.2");
				
				controlPantallaTactil.setOp1menuD3 ("" + (Mathf.RoundToInt (Random.Range (87, 89) * 100) / 100f));
				controlPantallaTactil.setOp2menuD3 ("" + (Mathf.RoundToInt (Random.Range (15, 17) * 100) / 100f));
//				print ((((configuracion.TiempoFaena * 60f - Time.time + central.tiempoFaenaActual) / (configuracion.TiempoFaena * 60f)) * 100f));
				controlPantallaTactil.setPorcentajePetroleo ("" + Mathf.RoundToInt ((((configuracion.TiempoFaena * 60f - Time.time + central.tiempoFaenaActual) / (configuracion.TiempoFaena * 60f)) * 100f)));
			} else {
				controlPantallaTactil.setOp1menuD2 ("0.0");
				controlPantallaTactil.setOp2menuD2 ("0.0");
				controlPantallaTactil.setOp3menuD2 ("0.0");
				
				controlPantallaTactil.setOp1menuD3 ("0.0");
				controlPantallaTactil.setOp2menuD3 ("0.0");
				controlPantallaTactil.setBatteryVoltage ("0.0");
				controlPantallaTactil.setAccumPressure ("0.0");
				controlPantallaTactil.setHidraulicTemp ("0.0");
			}
		}
		comprobarOrdenEjecucion ();

		if (vistaExterior){
			return;
		}
		if (estado != EstadoMaquina.encendida || central.estado == Central.EstadoSimulacion.Finalizando) {
			JointMotor mAux = new JointMotor ();
			mAux.force = 0f;
			mAux.targetVelocity = 0f;
			jointCentro.motor = mAux; 
			return;
		}

		float pala = 0f;*/
        float brazo = 0f;
		float direccion = 0f;
	
		/*
#if !UNITY_EDITOR
		pala = Input.GetAxis ("CucharaEditor");*/
#if UNITY_EDITOR
        brazo = Input.GetAxis("ControlTolbaEditor");
#else
        print("Tolba: " + Input.GetAxis("ControlTolba") + ", Cambio: " + Input.GetAxis("Cambio"));
        brazo = Input.GetAxis("ControlTolba");
#endif
#if UNITY_EDITOR
        direccion = Input.GetAxis("ManubrioEditor");
#else
        direccion = Input.GetAxis("Manubrio");
#endif
        /*
#else
		pala = ((valoresPotenciometro[5] * 1f) - 512f) / 1024f;
		brazo = ((valoresPotenciometro[4] * 1f) - 512f) / 1024f;
		direccion = ((valoresPotenciometro[3] * 1f) - 512f) / 1024f;

		pala = VariablesGlobales.calcularPresicion(pala);
		brazo = VariablesGlobales.calcularPresicion(brazo);
		direccion = VariablesGlobales.calcularPresicion(direccion);
#endif
		if (brazoAutomatico)
			brazo = 1f;
		*/

		float animTime = animator.GetCurrentAnimatorStateInfo (0).normalizedTime;
		animTime = Mathf.Clamp01 (animTime);
		if (estado == EstadoMaquina.encendida)
		{
			//manejarEjeLimites (-(Mathf.Sign(direccion) * direccion * direccion));
            manejarEjeLimites(-direccion);
			if (direccion > 0.25f) {
				monitorDelantero.MostrarGuia (0);
				monitorTrasero.MostrarGuia (0);
			} else if (direccion < -0.25f) {
				monitorDelantero.MostrarGuia (2);
				monitorTrasero.MostrarGuia (2);
			} else {
				monitorDelantero.MostrarGuia (1);
				monitorTrasero.MostrarGuia (1);
			}
            manejarBrazoLimites(brazo);
			if (brazo != 0 && Mathf.Abs (brazo) > 0.5f && !ingame.usingArm) {
				if (animTime != 0 && animTime != 1) {
					ingame.EnableShaking (true);
					ingame.usingArm = true;
				}
			}
			else{
				StartCoroutine (ingame.stopArm ());
			}
		}
		// * (test?100f:1f));
        /*manejarPalaLimites (pala * (test?100f:1f));
		controlPantallaTactil.setPesoPala ("" + (Mathf.Round(capturaPeso.enCarga * 100f) / 100f));
		controlPantallaTactil.setVehicleSpeed ("" + Mathf.RoundToInt(controlExcavadoraMotor.GetComponent<Rigidbody>().velocity.magnitude * 3600f / 1000f));
        */
        
        //print(controlTarjetaControladora.ignicion());
        /*if (controlTarjetaControladora.ignicion() == 2)
        {
            if (estado == EstadoMaquina.encendida)
            {
                tiempoEncendido = 0f;
                arranque(false);
            }
        }
        else
        {
            if (controlTarjetaControladora.ignicion() == 0)
            {
                if (estado == EstadoMaquina.apagada && tiempoEncendido <= 0f)
                {
                    tiempoEncendido = Time.time + 5f;
                    controlCamionMotor.audioSource.loop = true;
                    controlCamionMotor.audioSource.clip = controlCamionMotor.sonidoBomba;
                    controlCamionMotor.audioSource.Play();
                    //audioRetroceso.clip = Resources.Load("camion") as AudioClip;
                    //if (estado != EstadoMaquina.encendida) audioRetroceso.Play();
                }
                else
                {
                    if (estado == EstadoMaquina.encendida)
                    {
                        //tiempoEncendido = 0f;
                        //arranque(false);
                    }
                }
            }
            else
            {
                if (controlTarjetaControladora.ignicion() == 1)
                {
                    if (estado != EstadoMaquina.apagadaTotal && (tiempoEncendido < Time.time))
                    {
                        tiempoEncendido = 0f;
                        arranque(true);
                    }
                }
            }
        }*/

		if(controlTarjetaControladora.ignicion() == 2 || Input.GetKey(KeyCode.Keypad1)) {
			if (estado != EstadoMaquina.encendida)
			{
				tiempoEncendido = Time.time + 5f;
				if(lastPosIgnicion != 2)
					tiempoEncendidoB = Time.time + 8f;

				/*if ((tiempoEncendidoB - Time.time) < 7 && (tiempoEncendidoB - Time.time) > -3f && lastPosIgnicion == 2) {
					StartCoroutine (controlCamionMotor.SonidoIgnicion ());
				}*/
			}
			if (estado == EstadoMaquina.encendida) {
				arranque(false);
				tiempoEncendidoB = Time.time + 8f;
				tiempoEncendido = 0f;
			}

			lastPosIgnicion = 2;
		}
		else{
			if ((controlTarjetaControladora.ignicion() == 0 || Input.GetKey(KeyCode.Keypad3)) && estado != EstadoMaquina.encendida && (lastPosIgnicion == 2 || lastPosIgnicion == 0))
			{
				if(tiempoEncendido - Time.time < 4 && lastPosIgnicion == 0 && estado != EstadoMaquina.apagadaTotal)
					StartCoroutine(controlCamionMotor.SonidoIgnicion ());
				lastPosIgnicion = 0;
			}
			else{
				if (controlTarjetaControladora.ignicion() == 1 || Input.GetKey(KeyCode.Keypad2))
				{
					if (estado != EstadoMaquina.apagadaTotal && (tiempoEncendido < Time.time) && tiempoEncendido > 0f && lastPosIgnicion == 0)
					{
						tiempoEncendido = 0f;
						tiempoEncendidoB = 0f;
						arranque (true);
						lastPosIgnicion = 1;
					}
					else if(estado != EstadoMaquina.apagadaTotal && (tiempoEncendidoB < Time.time) && (tiempoEncendidoB + 2f > Time.time) && tiempoEncendidoB > 0f && lastPosIgnicion == 2){
						tiempoEncendidoB = 0f;
						tiempoEncendido = 0f;
						arranque (true);
						lastPosIgnicion = 1;
					}
					else if(estado != EstadoMaquina.apagadaTotal && (tiempoEncendidoB > Time.time) && lastPosIgnicion == 2){
						//Debug.Log ("skip 1");
						lastPosIgnicion = 2;
					}
					else
						lastPosIgnicion = -1;
				}
			}
		}

        
		#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.Keypad5))
			controlCamionMotor.frenoParqueoActivado = !controlCamionMotor.frenoParqueoActivado;
		#else
		controlCamionMotor.frenoParqueoActivado = controlTarjetaControladora.BotonAccion() == 0;
		#endif

        /*if (Input.GetButtonDown("Encendido"))
        {
            if (estado != EstadoMaquina.apagadaTotal)
            {
                tiempoEncendido = Time.time + 5f;
				//StartCoroutine(controlCamionMotor.SonidoIgnicion ());
                //audioRetroceso.clip = Resources.Load("camion") as AudioClip;
                //if (estado != EstadoMaquina.encendida) audioRetroceso.Play();
            }
			if (estado == EstadoMaquina.encendida) {
				arranque(false);
				tiempoEncendido = 0f;
			}
        }
		if (Input.GetButton ("Encendido")) {
			if(tiempoEncendido - Time.time < 4)
				StartCoroutine (controlCamionMotor.SonidoIgnicion ());
		}
        if (Input.GetButtonUp("Encendido"))
        {
			if (estado != EstadoMaquina.apagadaTotal && (tiempoEncendido < Time.time) && tiempoEncendido > 0f)
            {
                tiempoEncendido = 0f;
                arranque(true);
                //audioRetroceso.clip = Resources.Load("Retroceso") as AudioClip;
                //lectorControles.OutCmd(byte.Parse("" + configuracionControles.idLedEncendido), estado == EstadoMaquina.encendida);
                //central.SendMessage("encenderLeds", estado == EstadoMaquina.encendida, SendMessageOptions.DontRequireReceiver); 
            }
            //audioRetroceso.Stop();
            //if ((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
        }*/

        //if (estado == EstadoMaquina.encendida)
        //{
        //    if (lucesDelanteras.Length > 0)
        //    {
        encenderLucesDelanteras(controlTarjetaControladora.ControlLucesDelanteras() == 1 && estado == EstadoMaquina.encendida);
        encenderLucesTraseras(controlTarjetaControladora.ControlLucesTraseras() == 1 && estado == EstadoMaquina.encendida);
        encenderLucesCarga(controlTarjetaControladora.ControlLucesCarga() == 1 && estado == EstadoMaquina.encendida);

        if ((estado == EstadoMaquina.apagadaTotal || estado == EstadoMaquina.apagada) && (Input.GetKeyDown(KeyCode.E) || Input.GetButton("Fire3")))
        {
            controlUsuarioChecklist.ingresarCabina(false);
        }
        //    }
        //}
        /*if ((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida)
        {
            mensajeApagar.Toggle();
        }*/
    }
    
	public void salirCabina(){
		//print ("salir cabina");
		//ingresarCabina (false);
		//ingame.salirCabina ();
	}
    /*
	public void ingresarCabina(bool ingresar){
		resetMaquina ();
		vistaExterior = !ingresar;
		foreach (GameObject g in PartesMaquina)
			g.SetActive (ingresar);
		foreach (GameObject g2 in PartesMaquinaChecklist)
			g2.SetActive (!ingresar);
	}
    */
    void manejarBrazoLimites(float accionControl){
        //print(animacionEnInicio + " " + animacionEnFinal + " " + accionControl);
        /*if (animacionEnInicio && accionControl > 0f)
        {
            animator.SetFloat("offsetBalde", 0f);
            print("reset1");
            animacionEnInicio = false;
        }
        if (animacionEnFinal && accionControl < 0f)
        {
            animator.SetFloat("offsetBalde", 0f);
            print("reset2");
            animacionEnFinal = false;
        }
        if (!animacionEnInicio && !animacionEnFinal)
        {*/
        //print(Mathf.Clamp(accionControl, rangoLimitTolba.x, rangoLimitTolba.y));
        animator.SetFloat("multiplicadorVelocidadBalde", Mathf.Clamp(accionControl, rangoLimitTolba.x, rangoLimitTolba.y));
        //}
        /*
		JointMotor m = jointBrazo.motor;
		JointLimits b = jointBrazo.limits;
//		print(accionControl);
		//b.min = jointBrazo.angle;
		if (accionControl > 0f) {
            b.max = Mathf.Clamp(b.max - Time.deltaTime * 25f * Mathf.Abs(accionControl), rangoLimitBrazo.x + 1f, rangoLimitBrazo.y);
        }
		else {
            if (accionControl < 0f)
            {
                b.max = Mathf.Clamp(b.max + Time.deltaTime * 25f * Mathf.Abs(accionControl), rangoLimitBrazo.x + 1f, rangoLimitBrazo.y);
            }

        }
        m.force = 300000f;

        if (b.max >= rangoLimitBrazo.y || b.max <= rangoLimitBrazo.x + 1) {
			if (!enTopeBrazo) {
				fuenteAudio.PlayOneShot (choqueSonido);
				enTopeBrazo = true;
			}
		} else
			enTopeBrazo = false;
		cilindroEmpuje.localPosition = new Vector3 (cilindroEmpuje.localPosition.x, cilindroEmpuje.localPosition.y, - 1.5464f + 0.4f * ((b.max - rangoLimitBrazo.x)/(rangoLimitBrazo.y - rangoLimitBrazo.x)));

		//print (m.force + " " + accionControl);
		jointBrazo.motor = m;
		jointBrazo.limits = b;*/
    }
    /*

	void manejarPalaLimites(float accionControl){
		
		JointMotor m = jointPala.motor;
		JointLimits b = jointPala.limits;
		//print(jointPala.angle);
		//b.min = jointBrazo.angle;
		if (accionControl > 0f) {
            b.max = Mathf.Clamp(b.max + 50f * Time.deltaTime * Mathf.Abs(accionControl), rangoLimitPala.x + 1f, rangoLimitPala.y);
        }
		else {
            if (accionControl < 0f) {
				b.max = Mathf.Clamp (b.max - Time.deltaTime * 50f * Mathf.Abs(accionControl), rangoLimitPala.x + 1f, rangoLimitPala.y);
                
            }
        }
        m.force = 100000f;
        if (b.max >= rangoLimitPala.y || b.max <= rangoLimitPala.x + 1) {
			if (!enTopePala) {
				fuenteAudio.PlayOneShot (choqueSonido);
				enTopePala = true;
			}
		} else
			enTopePala = false;

		jointPala.motor = m;
		jointPala.limits = b;

		//print (accionControl + " " + b.max);
		partesPala [1].localEulerAngles = new Vector3 (Mathf.Clamp(-b.max * 6f / 13f, -60f, 0f), partesPala [1].localEulerAngles.y, partesPala [1].localEulerAngles.z);
		partesPala [0].localEulerAngles = new Vector3 (Mathf.Clamp(b.max * 9f / 13f, 0f, 90f), partesPala [0].localEulerAngles.y, partesPala [0].localEulerAngles.z);
		partesPala [3].localEulerAngles = new Vector3 (Mathf.Clamp(b.max * 6f / 13f, 0f, 60f), partesPala [3].localEulerAngles.y, partesPala [3].localEulerAngles.z);
		partesPala [4].localEulerAngles = new Vector3 (Mathf.Clamp(b.max * 1f / 13f, 0f, 10f), partesPala [4].localEulerAngles.y, partesPala [4].localEulerAngles.z);
	}
    */
    void manejarEjeLimites(float accionControl){
		JointLimits b = jointCentro.limits;
		if (Mathf.Abs (accionControl) > 0.05f) {
			if (accionControl > 0) {
				b.min = Mathf.Lerp (b.min, accionControl*rangoLimitEje.y, Time.deltaTime*0.5f);
				//Debug.Log (accionControl + " " + rangoLimitEje.y);
			} else if(accionControl < 0){
				b.min = Mathf.Lerp (b.min, -accionControl*rangoLimitEje.x, Time.deltaTime*0.5f);
				//Debug.Log (accionControl + " " + rangoLimitEje.x);
			}
			b.min = Mathf.Clamp (b.min, rangoLimitEje.x, rangoLimitEje.y);
			//b.min = Mathf.Clamp (b.min + accionControl * accionControl * Mathf.Sign (accionControl) * 15f * Time.deltaTime, rangoLimitEje.x, rangoLimitEje.y);
			//Debug.Log (accionControl + ", "+(b.min + accionControl * accionControl * Mathf.Sign (accionControl) * 15f * Time.deltaTime));

			//print (accionControl + " " + b.max);
		}
		b.max = b.min + 1f;
		b.min = Mathf.Round (b.min*10f)/10f;
		b.max = Mathf.Round (b.max*10f)/10f;
		//b.max = limitMaxActual;
		jointCentro.limits = b;

		lastDireccion = accionControl;
	}
    /*
	void pruebaFrenos(bool activar){
	}


        */
	public void encenderLucesDelanteras(bool activar){
		//if (monitorDisplay.activeSelf) {
			//if(controlCamion != null && Vector3.Distance(controlExcavadoraMotor.transform.position, controlCamion.camiones.transform.position) < 30f) controlCamion.lucesEncendidas(activar);
			foreach (Light l in lucesDelanteras)
				l.gameObject.SetActive (activar);
		//}
	}
	public void encenderLucesTraseras(bool activar){
		//if (monitorDisplay.activeSelf) {
		//	if(controlCamion != null && Vector3.Distance(controlExcavadoraMotor.transform.position, controlCamion.camiones.transform.position) < 30f) controlCamion.lucesEncendidas(activar);
			foreach (Light l in lucesTraseras)
				l.gameObject.SetActive (activar);
		//}
	}
	public void encenderLucesCarga(bool activar){
		//if (monitorDisplay.activeSelf) {
		//	if(controlCamion != null && Vector3.Distance(controlExcavadoraMotor.transform.position, controlCamion.camiones.transform.position) < 30f) controlCamion.lucesEncendidas(activar);
			foreach (Light l in lucesCarga)
				l.gameObject.SetActive (activar);
		//}
	}
    public void arranque(bool activar){
		if (estado == EstadoMaquina.apagadaTotal)
			return;
		//print ("arranque" + activar);
		estado = activar?EstadoMaquina.encendida:EstadoMaquina.apagada;
		if(ingame.estado != InGame.EstadoSimulacion.ApagadoExterior && ingame.estado != InGame.EstadoSimulacion.Finalizando)
			ingame.estado = activar?InGame.EstadoSimulacion.Conduciendo:ingame.estado;
        controlCamionMotor.encender(activar);
		ActivarMonitores (activar);
		ingame.tableroControl.setPetroleo (0f);
		ingame.tableroControl.setTemperatura (0f);
        //enciende leds iniciales
        /*controlPantallaTactil.motorEncendido (activar);
		controlPantallaTactil.neutro (true);
		controlExcavadoraMotor.encender (activar);
		cambio0 (true);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedTransmisionAutomatica), controlExcavadoraMotor.tipoCambio != ControlExcavadoraMotor.TipoCambio.manual);
        */
    }

    public void inicio(float valor)
    {
        rangoLimitTolba.x = valor;
    }
    public void fin(float valor)
    {
        rangoLimitTolba.y = valor;
    }
    public void cambiarCamara()
    {
        print("cambiando camara");
        camaraTrasera.SetActive(!camaraTrasera.activeSelf);
        camaraBalde.SetActive(!camaraBalde.activeSelf);
    }
    
	void salirCabinaFinal(){
		if (ingame.estado == InGame.EstadoSimulacion.Finalizando || ingame.estado == InGame.EstadoSimulacion.ApagadoExterior || ingame.estado == InGame.EstadoSimulacion.EncendidoExterior) {
				//print ("salir cabina");
			ActivarMonitores(false);
				salirCabina ();
				//controlUsuarioChecklist.mensajeInteraccion.gameObject.SetActive (false);
				//controlUsuarioChecklist.enfocandoCabina = false;
		}
	}
    /*
	void claxon(bool activar){
		if (!activar && controlCamion != null && controlCamion.estado == ControlCamion.estadoCamion.enPosicion && Vector3.Distance(controlExcavadoraMotor.transform.position, controlCamion.camiones.transform.position) < 30f) {
			if (tiempoSenalSalida > 0f && tiempoSenalSalida > Time.time)
				controlCamion.avanzar ();
			else
				tiempoSenalSalida = Time.time + 2f;
		}
		controlExcavadoraMotor.sonarClaxon (activar);
	}
	void rideControl(bool activar){
		if(estado == EstadoMaquina.encendida)
			brazoAutomatico = activar;
	}
	void transmisionAutomatico(bool activar){
		controlExcavadoraMotor.tipoCambio = activar ? ControlExcavadoraMotor.TipoCambio.automatico : ControlExcavadoraMotor.TipoCambio.manual;
	}
	void desembragar(bool activar){
	}
	void cambio0(bool activar){
		controlPantallaTactil.neutro (true);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), false);
	}
	void cambio1(bool activar){
		controlPantallaTactil.neutro (false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), true);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), false);
	}
	void cambio2(bool activar){
		controlPantallaTactil.neutro (false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), true);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), false);
	}
	void cambio3(bool activar){
		controlPantallaTactil.neutro (false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), true);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), false);
	}
	void cambio4(bool activar){
		controlPantallaTactil.neutro (false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio1), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio2), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio3), false);
		lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedCambio4), true);
	}

	public void fallaOperacionalCamioneta(){
		central.fallaOperacion (LugarMaquina.Camioneta);
	}
    */
    public void golpe(float intensidad, Vector3 position, string tag, LugarMaquina lugarGolpe){
        if (cooldownGolpe > Time.time) return;
        cooldownGolpe = Time.time + 1f;
		//print ("golpe " + intensidad + " * " + configuracion.getMultiplicadorDanio(tag));
		float mult = configuracion.getMultiplicadorDanio (tag);
		switch (lugarGolpe) {
		case LugarMaquina.Frontal:
			if(intensidad * mult > 1f)
				integridadFrontal -= configuracion.DescuentoChoque;
			if(integridadFrontal < configuracion.IntFrontal) ingame.fallaOperacion(LugarMaquina.Frontal);
			break;
		case LugarMaquina.MotorDer:
			if(intensidad * mult > 1f)
				integridadMotorDer -= configuracion.DescuentoChoque;
			if(integridadMotorDer < configuracion.IntMotorDer) ingame.fallaOperacion(LugarMaquina.MotorDer);
			break;
		case LugarMaquina.MotorIzq:
			if(intensidad * mult > 1f)
				integridadMotorIzq -= configuracion.DescuentoChoque;
			if(integridadMotorIzq < configuracion.IntMedioDer) ingame.fallaOperacion(LugarMaquina.MotorIzq);
			break;
		case LugarMaquina.TolvaDer:
			if(intensidad * mult > 1f){
				integridadTolvaDer -= configuracion.DescuentoChoque;
				//integridadPosterior -= configuracion.DescuentoChoque;
			}
			if(integridadTolvaDer < configuracion.IntTolvaDer) ingame.fallaOperacion(LugarMaquina.TolvaDer);
			//if(integridadPosterior < configuracion.IntPosterior) central.fallaOperacion(LugarMaquina.Posterior);
			break;
		case LugarMaquina.TolvaIzq:
			if(intensidad * mult > 1f){
				integridadTolvaIzq -= configuracion.DescuentoChoque;
			//	integridadPosterior -= configuracion.DescuentoChoque;
			}
			if(integridadTolvaIzq < configuracion.IntTolvaIzq) ingame.fallaOperacion(LugarMaquina.TolvaIzq);
			//if(integridadPosterior < configuracion.IntPosterior) central.fallaOperacion(LugarMaquina.Posterior);
			break;
		}
		if (tiempoGolpeSonido < Time.time) {
			GameObject g = (GameObject)Instantiate (sonidoGolpePrefab, position, Quaternion.identity);
			g.GetComponent<AudioSource> ().volume = Mathf.Clamp (intensidad, 0f, 1f);
			//g.GetComponent<AudioSource> ().Play ();
			tiempoGolpeSonido = Time.time + 0.1f;
		}
		if(position.y >= controlCamionMotor.transform.position.y + 2f && tag == "pared")
			ingame.choqueTunel ();
	}



	public void reset(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	void OnGUI(){
		GUI.Label (new Rect (10, 150, 500, 40), "Ignicion " + controlTarjetaControladora.ignicion ());
		//GUI.Box (new Rect (3f * Screen.width / 5f, 0f, 400f, 60f), "orden ejecucion: " + ordenEjecucion + "\nFreno parqueo: " + controlExcavadoraMotor.frenoParqueoActivado);
	}

	public void SacudirTolvaStartEnd(){
		float brazo = 0;
		#if UNITY_EDITOR
		brazo = Input.GetAxis ("ControlTolbaEditor");
		#else
		brazo = Input.GetAxis("ControlTolba");
		#endif
		if(brazo != 0)
			ingame.EnableShaking (true, 8f);
	}

	public void SacudirTolvaMid(){
		float brazo = 0;
		#if UNITY_EDITOR
		brazo = Input.GetAxis ("ControlTolbaEditor");
		#else
		brazo = Input.GetAxis("ControlTolba");
		#endif
		if (brazo > 0) {
			ingame.EnableShaking (true, 8f);
		}
	}
}
