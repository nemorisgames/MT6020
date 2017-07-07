using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlChecklist : MonoBehaviour {
	ControlTarjetaControladora controlTarjetaControladora;
	public AudioClip sonidoBomba;
	Configuracion configuracion;

	public ChecklistLista checklistLista;
	public ControlMouseOperador controlMouseOperador;
	public ControlMouseOperador controlMouseOperadorInterior;
	public ControlUsuarioChecklist controlUsuarioChecklist;
	public ControlCamaraInterior controlCamaraInterior;

	public GameObject diapositivaInicioChecklist;
	public GameObject diapositivaFinalChecklist;

	public GameObject camaraTrasera;
	public GameObject camaraBalde;

	public GameObject nivelPetroleo; //OK
	[HideInInspector]
	public bool nivelPetroleoActivada = false; 
	public GameObject nivelAceite; //OK
	[HideInInspector]
	public bool nivelAceiteActivada = false;
	public GameObject nivelHidraulico; //OK
	[HideInInspector]
	public bool nivelHidraulicoActivada = false;
	public GameObject nivelRefrigerante; //OK
	[HideInInspector]
	public bool nivelRefrigeranteActivada = false;
	public GameObject nivelAceiteTrans;//OK
	[HideInInspector]
	public bool nivelAceiteTransActivada = false;
	public GameObject nivelAceiteCajaTransf; //OK
	[HideInInspector]
	public bool nivelAceiteCajaTransfActivada = false;
	public GameObject filtroAire; //OK
	[HideInInspector]
	public bool filtroAireActivada = false;
	public GameObject filtroCombustible; //OK
	[HideInInspector]
	public bool filtroCombustibleActivada = false;
	public GameObject topeEjeCentral; //OK
	[HideInInspector]
	public bool topeEjeCentralActivada = false;
	public GameObject indicadoresObstruccion;//OK
	[HideInInspector]
	public bool indicadoresObstruccionActivada = false;

	//NUEVOS
	public GameObject articulacionCentral;//OK
	[HideInInspector]
	public bool articulacionCentralActivada = false;
	public GameObject articulacionDireccional;//OK
	[HideInInspector]
	public bool articulacionDireccionalActivada = false;
	public GameObject pasadoresGeneral;//OK
	[HideInInspector]
	public bool pasadoresGeneralActivada = false;
	public GameObject fugasCilindrosMangueras;//OK
	[HideInInspector]
	public bool fugasCilindrosManguerasActivada = false;
	public GameObject sistemaAnsul;//OK
	[HideInInspector]
	public bool sistemaAnsulActivada = false;
	public GameObject extintorManual;//OK
	[HideInInspector]
	public bool extintorManualActivada = false;

	public Transform posicionSobreMaquina;
	public Transform posicionBajoMaquina;
	bool escaleraHabilitada = false;
	bool escaleraActivada = false;
	public TweenRotation[] ansu;
	bool ansuHabilitada = false;
	bool ansuActivada = false;
	public TweenRotation[] motor;
	bool motorHabilitada = false;
	bool motorActivada = false;

	public TweenRotation[] puertaHidraulica;
	bool puertaHidraulicaHabilitada = false;
	bool puertaHidraulicaActivada = false;
	public TweenRotation[] puertaCabina;
	public bool puertaCabinaHabilitada = false;
	public bool puertaCabinaActivada = false;

	public TweenRotation[] puertaAceite;
	public bool puertaAceiteHabilitada = false;
	public bool puertaAceiteActivada = false;

	public TweenRotation[] puertaDropBox;
	public bool puertaDropBoxHabilitada = false;
	public bool puertaDropBoxActivada = false;

	public TweenRotation[] brazo;
	public TweenPosition cilindroBrazo;

	public UILabel mensajeInteraccion;

	bool brazoHabilitado = false;
	bool brazoActivado = false;
	public bool listaActiva = false;

	public bool checkeandoCabina = false;
	bool checkeandoControles = false;
	bool checkeandoPanel = false;

	public enum estadoChequeo {exterior, interior, revisionControles, revisionPanel };
	public estadoChequeo estado = estadoChequeo.exterior;

	public ParteMaquina[] partesMaquina;
	public GameObject varillaPetroleoDefault;

	public GameObject[] GUINormal;
	public GameObject instruccionesControles;
	public GameObject instruccionesPantalla;

	public ControlChecklistControles controlChecklistControles;
	public TableroControl tableroControl;

	GameObject pantallaTactil;

	public float resultadoSeccion1 = 0f;
	public float resultadoSeccion2 = 0f;
	public float resultadoSeccion3 = 0f;
	public float resultadoSeccion4 = 0f;
	public UILabel tiempoFaenaLabel;
	public float tiempo = 0f;
	public float resultadoTotal = 0f;

	public GameObject[] panelResultados;
	public GameObject[] panelAprobado;
	public GameObject[] panelReprobado;
	public UILabel resultadoSeccion1Label;
	public UILabel resultadoSeccion2Label;
	public UILabel resultadoSeccion3Label;
	public UILabel resultadoSeccion4Label;
	public UILabel resultadoTotalLabel;
	public UILabel resultadoTiempoLabel;
	public UILabel resultadoRepeticionesLabel;
	public UILabel resultadoSeccion1LabelUser;
	public UILabel resultadoSeccion2LabelUser;
	public UILabel resultadoSeccion3LabelUser;
	public UILabel resultadoSeccion4LabelUser;
	public UILabel resultadoTotalLabelUser;
	public UILabel resultadoTiempoLabelUser;
	public UILabel resultadoRepeticionesLabelUser;

	ConfiguracionControles configuracionControles;
	public bool generarFallasAutomaticamente = true;

	public bool activa = true;
	GameObject camara;
	
	public bool activarMouseInicio = false;

	public ControlCamion.EstadoMaquina estadoExcavadoraChecklist = ControlCamion.EstadoMaquina.apagadaTotal;
	LectorControles lectorControles;
	public TweenRotation iso_switch;
	public float tiempoEncendido = 2f;
	float tiempoEncendidoB = 0f;
	public AudioClip sonidoMotor;
	public AudioClip sonidoApagadoMotor;
    public AudioSource audioEncendido;

    public Light[] lucesDelanteras;
	public Light[] lucesCarga;
	public Light[] lucesTraseras;

	public Animator animatorTolba;
	public int checklistIndex = 1;
	public bool singleChecklist;
	bool sobreMaquina = false;
	InGame ingame;

	// Use this for initialization
	void Start () {
		controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();
		//Central central = GameObject.FindWithTag ("Central").GetComponent<Central>();
		camara = transform.FindChild ("Camera").gameObject;
		//pantallaTactil = tableroControl.transform.parent.parent.parent.gameObject;
		GameObject g = GameObject.FindGameObjectWithTag ("Configuracion");
		configuracionControles = GameObject.FindWithTag ("Configuracion").GetComponent<ConfiguracionControles> ();
		//lectorControles = central.gameObject.GetComponent<LectorControles> ();
		if (g != null)
			configuracion = g.GetComponent<Configuracion> ();
		activarLista(listaActiva);
		if (activarMouseInicio)
			controlMouseOperador.enabled = true;
		tiempo = Time.timeSinceLevelLoad;
		if (generarFallasAutomaticamente)
			generarFallas ();
		activar (activa);
		mensajeInteraccion.text = "";
		checklistIndex = 1;
		ingame = GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame>();
	}

	/*void BotonDown(int indice){
		//if (estado == EstadoMaquina.apagadaTotal)
		//	return;
		indice = indice + 1;
		if(indice == configuracionControles.idBotonEncendido){
			if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal) {
				tiempoEncendido = Time.time + 2f;

                audioEncendido.clip = Resources.Load("camion") as AudioClip;
				if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.encendida) audioEncendido.Play();
            }
		}
	}*/
	
	public void arranque(bool activar){
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagadaTotal)
			return;
		estadoExcavadoraChecklist = activar?ControlCamion.EstadoMaquina.encendida:ControlCamion.EstadoMaquina.apagada;
		if (activar) {
			audioEncendido.clip = sonidoMotor;
			audioEncendido.loop = true;
			audioEncendido.Play ();
			InGame aux = GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ();
			StartCoroutine(aux.ShakeForSecs(1f));
			if (ingame.monitorDerecho.activadoInicial)
				ingame.monitorDerecho.ToggleEncendido ();
		}
		else { 
			audioEncendido.loop = false;
			audioEncendido.Stop ();
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) 
				audioEncendido.PlayOneShot(sonidoApagadoMotor);
			lucesAltasPala(false);
			lucesAltasMotor(false);
			lucesBajasPala(false);
			//lucesBajasMotor(false);
			/*lectorControles.OutCmd (byte.Parse("" + configuracionControles.idLedLucesAltasDelanteras), false);
			lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesAltasTraseras), false);
			lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasDelanteras), false);
			lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasTraseras), false);*/
		}
		print ("arranque" + activar);
		//enciende leds iniciales
		//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedTransmisionAutomatica), true);
		//tableroControl.motorEncendido (activar);
		//tableroControl.neutro (true);
	}

	public void cambiarCamara()
	{
		print("cambiando camara");
		camaraTrasera.SetActive(!camaraTrasera.activeSelf);
		camaraBalde.SetActive(!camaraBalde.activeSelf);
	}

	public void arreglarMaquina(){
		Debug.Log ("arreglar maquina");
		for (int i = 0; i < partesMaquina.Length; i++) {
				
			ParteMaquina p = partesMaquina[i];
			if(p.parteBuena != null)
				for(int j = 0; j < p.parteBuena.Length; j++)
					p.parteBuena[j].SetActive(true);
			if (p.parteMala != null)
				for(int j = 0; j < p.parteMala.Length; j++)
					p.parteMala[j].SetActive(false);
		}
		/*tableroControl.setPorcentajePetroleo ("90.0");
		
		tableroControl.setOilTemp ("10.0");
		tableroControl.setOp4OperadorConvert ("10.0");
		
		tableroControl.setOp4OperadorConvert ("10.0");

		tableroControl.tiempoMotorFuncionando = 0f;*/

		//iso_switch.PlayReverse ();
		iso_switch.ResetToBeginning();

		nivelPetroleoActivada = false;
		nivelAceiteActivada = false;
		nivelHidraulicoActivada = false;
		nivelRefrigeranteActivada = false;
		nivelAceiteTransActivada = false;
		nivelAceiteCajaTransfActivada = false;
		filtroAireActivada = false;
		filtroCombustibleActivada = false;
		topeEjeCentralActivada = false;
		indicadoresObstruccionActivada = false;
		articulacionCentralActivada = false;
		articulacionDireccionalActivada = false;
		pasadoresGeneralActivada = false;
		fugasCilindrosManguerasActivada = false;
		sistemaAnsulActivada = false;
		extintorManualActivada = false;
		nivelPetroleo.SetActive(nivelPetroleoActivada);
		nivelAceite.SetActive(nivelAceiteActivada);
		nivelHidraulico.SetActive(nivelHidraulicoActivada);
		nivelRefrigerante.SetActive(nivelRefrigeranteActivada);
		nivelAceiteTrans.SetActive(nivelAceiteTransActivada);
		nivelAceiteCajaTransf.SetActive(nivelAceiteCajaTransfActivada);
		filtroAire.SetActive(filtroAireActivada);
		filtroCombustible.SetActive(filtroCombustibleActivada);
		topeEjeCentral.SetActive(topeEjeCentralActivada);
		indicadoresObstruccion.SetActive(indicadoresObstruccionActivada);
		articulacionCentral.SetActive(articulacionCentralActivada);
		articulacionDireccional.SetActive(articulacionDireccionalActivada);
		pasadoresGeneral.SetActive(pasadoresGeneralActivada);
		fugasCilindrosMangueras.SetActive(fugasCilindrosManguerasActivada);
		sistemaAnsul.SetActive(sistemaAnsulActivada);
		extintorManual.SetActive (extintorManualActivada);
	}

	public void ReiniciarMaquina(){
		checklistIndex = 2;
		listaActiva = true;
		activa = true;
		activarLista(listaActiva);
		checklistLista.restartListas ();
		controlMouseOperador.enabled = true;
		tiempo = Time.timeSinceLevelLoad;
		generarFallas ();
		activar (activa);
		estado = estadoChequeo.exterior;
		checkeandoCabina = !checkeandoCabina;
		gameObject.SetActive (false);
		controlUsuarioChecklist.gameObject.SetActive (!checkeandoCabina);
		controlCamaraInterior.gameObject.SetActive (checkeandoCabina);
		controlCamaraInterior.enabled = checkeandoCabina;
		gameObject.SetActive (true);
		deshabilitarCabina();
		mensajeInteraccion.text = "";
		mensajeInteraccion.gameObject.SetActive(false);
		GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ().ejecutarEntradaMaquina (false);
		foreach(GameObject g in GUINormal)
			g.SetActive(true);
		resultadoSeccion1 = 0;
		resultadoSeccion2 = 0;
		resultadoSeccion3 = 0;
		resultadoSeccion4 = 0;
		nivelPetroleoActivada = true;
		nivelAceiteActivada = true;
		nivelHidraulicoActivada = true;
		nivelRefrigeranteActivada = true;
		nivelAceiteTransActivada = true;
		nivelAceiteCajaTransfActivada = true;
		filtroAireActivada = true;
		filtroCombustibleActivada = true;
		topeEjeCentralActivada = true;
		indicadoresObstruccionActivada = true;
		articulacionCentralActivada = true;
		articulacionDireccionalActivada = true;
		pasadoresGeneralActivada = true;
		fugasCilindrosManguerasActivada = true;
		sistemaAnsulActivada = true;
		extintorManualActivada = true;
		nivelPetroleo.SetActive(nivelPetroleoActivada);
		nivelAceite.SetActive(nivelAceiteActivada);
		nivelHidraulico.SetActive(nivelHidraulicoActivada);
		nivelRefrigerante.SetActive(nivelRefrigeranteActivada);
		nivelAceiteTrans.SetActive(nivelAceiteTransActivada);
		nivelAceiteCajaTransf.SetActive(nivelAceiteCajaTransfActivada);
		filtroAire.SetActive(filtroAireActivada);
		filtroCombustible.SetActive(filtroCombustibleActivada);
		topeEjeCentral.SetActive(topeEjeCentralActivada);
		indicadoresObstruccion.SetActive(indicadoresObstruccionActivada);
		articulacionCentral.SetActive(articulacionCentralActivada);
		articulacionDireccional.SetActive(articulacionDireccionalActivada);
		pasadoresGeneral.SetActive(pasadoresGeneralActivada);
		fugasCilindrosMangueras.SetActive(fugasCilindrosManguerasActivada);
		sistemaAnsul.SetActive(sistemaAnsulActivada);
		extintorManual.SetActive (extintorManualActivada);
	}

	public void mostrarDanio(ParteMaquina p, bool danado, bool danioAleatorio){
		if(p.parteBuena != null)
			for(int i = 0; i < p.parteBuena.Length; i++)
				p.parteBuena[i].SetActive(!danado);

		if (danioAleatorio) {
			if (p.parteMala != null && p.parteMala.Length > 0) {
				if (!danado) {
					print ("mostrando todos ok");
					for (int i = 0; i < p.parteMala.Length; i++) {
						//Debug.Log ("1:"+""+p.indiceSeccion +","+p.indicePregunta);
						p.parteMala [i].SetActive (danado);
					}
				} else {
					int indiceDanio = 0;
					if (p.parteMala.Length > 1)
						indiceDanio = Random.Range (0, p.parteMala.Length);
					print ("parte mala seleccionada " + indiceDanio);
					for (int i = 0; i < p.parteMala.Length; i++) {
						if (i == indiceDanio) { 
							//Debug.Log ("2:"+""+p.indiceSeccion +","+p.indicePregunta+";"+i+","+indiceDanio);
							p.parteMala [i].SetActive (danado);
						//	if (p.indicePregunta == 6 && p.indiceSeccion == 2)
						//		print (i + " " + indiceDanio + " " + danado);
						} else {
						//	if (p.indicePregunta == 6 && p.indiceSeccion == 2)
						//		print (i + " " + indiceDanio + " " + (p.parteBuena.Length - 1 >= indiceDanio));
							if (p.parteBuena.Length - 1 >= indiceDanio) {
								p.parteMala [i].SetActive (!danado);
								if(i<= p.parteBuena.Length - 1) p.parteBuena [i].SetActive (danado);
							} else {
								p.parteMala [i].SetActive (danado);
							}
						}
					}
				}
			} else {
				if (danado) {
					//muestra todas las partes buenas menos una
					int indiceDanio = 0;
					if (p.parteBuena.Length > 1)
						indiceDanio = Random.Range (0, p.parteBuena.Length);
					for (int i = 0; i < p.parteBuena.Length; i++)
						if (indiceDanio != i)
							p.parteBuena [i].SetActive (true);
				}
			}
		} else {
			if(p.parteMala != null)
				for(int i = 0; i < p.parteMala.Length; i++)
					p.parteMala[i].SetActive(danado);
		}
	}

	public void generarFallas(){
		for (int i = 0; i < partesMaquina.Length; i++) {
			//if(i != 24 && i != 25 && i != 28 && i != 29){
				partesMaquina[i].danado = Random.Range(0, 100) < 50;
				mostrarDanio (partesMaquina [i], partesMaquina [i].danado, true); //i!=8);
			//}
		}
		//luces general
		partesMaquina [11].danado = partesMaquina [3].danado;
		/*
		if (Random.Range (0, 100) < 50) { 
			partesMaquina[28].danado = true;
			mostrarDanio(partesMaquina[28], partesMaquina[28].danado, true);
			controlChecklistControles.generarFallaJoysticks ();
		}
		if (Random.Range (0, 100) < 50) { 
			partesMaquina [29].danado = true;
			mostrarDanio(partesMaquina[29], partesMaquina[29].danado, true);
			controlChecklistControles.generarFallaPedales ();
		}
*/
		
		/*tableroControl.setPorcentajePetroleo (partesMaquina [19].danado ? "10.0" : "90.0");

		tableroControl.setOilTemp (partesMaquina [24].danado ? "85.0" : "20.0");
		tableroControl.setOp4OperadorConvert (partesMaquina [25].danado ? "20.0" : "35.0");

		tableroControl.setOp4OperadorConvert (partesMaquina [26].danado ? "100.0" : "10.0");*/
	}

	public void comenzarTiempo(){
		tiempo = Time.timeSinceLevelLoad;
	}

	public void desactivarMouse(){
		controlMouseOperador.enabled = false;
	}

	public void habilitarControles(){
		checkeandoControles = true;
	}
	public void deshabilitarControles(){
		checkeandoControles = false;
	}

	public void habilitarPanel(){
		checkeandoPanel = true;
	}
	public void deshabilitarPanel(){
		checkeandoPanel = false;
	}

	public void habilitarPuertaHidraulica(){
		puertaHidraulicaHabilitada = true;
	}
	public void deshabilitarPuertaHidraulica(){
		puertaHidraulicaHabilitada = false;
	}

	public void habilitarEscalera(){
		print ("habilitar escalera");
		escaleraHabilitada = true;
	}
	public void deshabilitarEscalera(){
		print ("deshabilitar escalera");
		escaleraHabilitada = false;
	}
	public void habilitarAnsu(){
		print ("habilitar escalera");
		ansuHabilitada = true;
	}
	public void deshabilitarAnsu(){
		print ("deshabilitar escalera");
		ansuHabilitada = false;
	}
	public void habilitarMotor(){
		print ("habilitar escalera");
		motorHabilitada = true;
	}
	public void deshabilitarMotor(){
		print ("deshabilitar escalera");
		motorHabilitada = false;
	}
	public void habilitarAceite(){
		print ("habilitar aceite");
		puertaAceiteHabilitada = true;
	}
	public void deshabilitarAceite(){
		print ("deshabilitar aceite");
		puertaAceiteHabilitada = false;
	}
	public void habilitarDropBox(){
		puertaDropBoxHabilitada = true;
	}
	public void deshabilitarDropBox(){
		puertaDropBoxHabilitada = false;
	}
	public void habilitarCabina(){
		print ("habilitar cabina");
		puertaCabinaHabilitada = true;
	}
	public void deshabilitarCabina(){
		print ("deshabilitar cabina");
		puertaCabinaHabilitada = false;
	}

	public void habilitarBrazo(){
		brazoHabilitado = true;
	}
	public void deshabilitarBrazo(){
		brazoHabilitado = false;
	}

	public void abrirPuertaHidraulica(){
		puertaHidraulicaActivada = !puertaHidraulicaActivada;
		foreach (TweenRotation t in puertaHidraulica) {
			t.Play(puertaHidraulicaActivada);
		}
	}
	public void abrirPuertaAceite(){
		puertaAceiteActivada = !puertaAceiteActivada;
		foreach (TweenRotation t in puertaAceite) {
			t.Play(puertaAceiteActivada);
		}
	}
	public void abrirAnsu(){
		ansuActivada = !ansuActivada;
		foreach (TweenRotation t in ansu) {
			t.Play(ansuActivada);
		}
	}
	public void abrirMotor(){
		motorActivada = !motorActivada;
		foreach (TweenRotation t in motor) {
			t.Play(motorActivada);
		}
	}

	public void abrirDropBox(){
		puertaDropBoxActivada = !puertaDropBoxActivada;
		foreach (TweenRotation t in puertaDropBox)
			t.Play (puertaDropBoxActivada);
	}


	/*public void abrirCabina(){
		puertaCabinaActivada = !puertaCabinaActivada;
		foreach (TweenRotation t in puertaCabina) {
			t.Play(puertaCabinaActivada);
		}
	}*/

	public void abrirBrazo(){
		brazoActivado = !brazoActivado;
		foreach (TweenRotation t in brazo) {
			t.Play(brazoActivado);
		}
		if(cilindroBrazo != null) cilindroBrazo.Play (brazoActivado);
	}
	
	public void verLista1(){
		listaActiva = true;
		checklistLista.gameObject.SetActive (listaActiva);
		checklistLista.verLista1 ();
	}

	public void verLista2(){
		listaActiva = true;
		checklistLista.gameObject.SetActive (listaActiva);
		checklistLista.verLista2 ();
	}

	public void verLista3(){
		listaActiva = true;
		checklistLista.gameObject.SetActive (listaActiva);
		checklistLista.verLista3 ();
	}

	public void verLista4(){
		listaActiva = true;
		checklistLista.gameObject.SetActive (listaActiva);
		checklistLista.verLista4 ();
	}

	public void ocultarLista(){
		listaActiva = false;
		checklistLista.gameObject.SetActive (listaActiva);
	}

	public void activarLista(bool activar){
		if (activar) {
			if(controlUsuarioChecklist.gameObject.activeSelf) controlUsuarioChecklist.desactivar ();
			else controlCamaraInterior.desactivar();
		}
		listaActiva = activar;

		if(!checkeandoCabina) controlMouseOperador.enabled = listaActiva;
		else controlMouseOperadorInterior.enabled = listaActiva;

		checklistLista.gameObject.SetActive (listaActiva);
		if(controlUsuarioChecklist.gameObject.activeSelf) controlUsuarioChecklist.enabled = !listaActiva;
		else controlCamaraInterior.enabled = !listaActiva;	
	}

	public void lucesAltasPala(bool activar){
		print ("luces altas pala " + activa);
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) {
			foreach (Light l in lucesDelanteras)
				l.gameObject.SetActive (activar);
		}
	}
	public void lucesAltasMotor(bool activar){
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) {
			foreach (Light l in lucesTraseras)
				l.gameObject.SetActive (activar);
		}
	}
	public void lucesBajasPala(bool activar){
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) {
			foreach (Light l in lucesCarga)
				l.gameObject.SetActive (activar);
		}
	}
	/*
	public void lucesBajasMotor(bool activar){
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) 
			foreach (Light l in lucesTraserasBajas)
				l.gameObject.SetActive (activar);
	}*/


	void BotonUp(int indice){
		if (!activa)
			return;
		indice = indice + 1;
		//print ("recibido checklist " + indice);

	}

	string calcularReloj(float tiempo){
		int minutos = (int)tiempo / 60;
		int segundos = (int)tiempo % 60;
		return ((minutos < 10) ? ("0" + minutos) : "" + minutos) + ":" + ((segundos < 10) ? ("0" + segundos) : "" + segundos);
	}

	bool pitidoIgnicion = false;

	public IEnumerator SonidoIgnicion(){
		if (!audioEncendido.isPlaying && !pitidoIgnicion) {
			pitidoIgnicion = true;
			audioEncendido.loop = false;
			audioEncendido.clip = sonidoBomba;
			audioEncendido.Play ();
			yield return new WaitForSeconds (1f);
			pitidoIgnicion = false;
		}
	}

	bool startFromZero = false;
	int lastPosIgnicion = -1;

	// Update is called once per frame
	void Update () {
		//Debug.Log (estado);
		if (!activa)
			return;
		/*if (controlTarjetaControladora.ignicion() == 0)
		{
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)
			{
				tiempoEncendido = 0f;
				arranque(false);
			}
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagada  && checkeandoCabina)
				if(tiempoEncendido - Time.time < 4)
					StartCoroutine(SonidoIgnicion ());
		}
		else
		{
			if (controlTarjetaControladora.ignicion() == 1)
			{
				if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagada && tiempoEncendido <= 0f)
				{
					tiempoEncendido = Time.time + 5f;
					//audioRetroceso.clip = Resources.Load("camion") as AudioClip;
					//if (estado != EstadoMaquina.encendida) audioRetroceso.Play();
				}
				else
				{
					if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)
					{
						tiempoEncendido = 0f;
						arranque(false);
					}
				}
			}
			else
			{
				if (controlTarjetaControladora.ignicion() == 2)
				{
					if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal && (tiempoEncendido < Time.time))
					{
						tiempoEncendido = 0f;
						audioEncendido.clip = Resources.Load("camion") as AudioClip;
						if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.encendida) audioEncendido.Play();
						arranque(true);
					}
				}
			}
		}*/

		if (controlCamaraInterior.enabled) {
			if (controlTarjetaControladora.ignicion () == 2 || Input.GetKey (KeyCode.Keypad1)) {
				if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.encendida) {
					//tiempoEncendido = 0f;
					//arranque(false);
					tiempoEncendido = Time.time + 5f;
					if (lastPosIgnicion != 2)
						tiempoEncendidoB = Time.time + 8f;

					//Debug.Log(tiempoEncendidoB + " | "+(tiempoEncendidoB - Time.time) + " | "+lastPosIgnicion);

					/*if ((tiempoEncendidoB - Time.time) < 7 && lastPosIgnicion == 2) {
					StartCoroutine (SonidoIgnicion ());
				}*/

				}
				if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) {
					arranque (false);
					tiempoEncendidoB = Time.time + 8f;
					tiempoEncendido = 0f;
				}

				lastPosIgnicion = 2;

			} else {
				if ((controlTarjetaControladora.ignicion () == 0 || Input.GetKey (KeyCode.Keypad3)) && estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.encendida && (lastPosIgnicion == 2 || lastPosIgnicion == 0)) {
					if (tiempoEncendido - Time.time < 4 && lastPosIgnicion == 0 && estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal) {
						StartCoroutine (SonidoIgnicion ());
					}
					lastPosIgnicion = 0;
				} else {
					if (controlTarjetaControladora.ignicion () == 1 || Input.GetKey (KeyCode.Keypad2)) {
						if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal && (tiempoEncendido < Time.time) && tiempoEncendido > 0f && lastPosIgnicion == 0) {
							tiempoEncendido = 0f;
							tiempoEncendidoB = 0f;
							arranque (true);
							lastPosIgnicion = 1;
						} else if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal && (tiempoEncendidoB < Time.time) && (tiempoEncendidoB + 2f > Time.time) && tiempoEncendidoB > 0f && lastPosIgnicion == 2) {
							tiempoEncendidoB = 0f;
							tiempoEncendido = 0f;
							arranque (true);
							lastPosIgnicion = 1;
						} else if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal && (tiempoEncendidoB > Time.time) && lastPosIgnicion == 2) {
							//Debug.Log ("skip 1");
							lastPosIgnicion = 2;
						} else
							lastPosIgnicion = -1;
					}
				}
			}
		}

		//Debug.Log (lastPosIgnicion);

		/*
		//if (Input.GetButtonDown("Encendido"))
		if(Input.GetKey(KeyCode.Keypad1))
		{
			if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal)
			{
				tiempoEncendido = Time.time + 5f;
				//audioRetroceso.clip = Resources.Load("camion") as AudioClip;
				//if (estado != EstadoMaquina.encendida) audioRetroceso.Play();
			}
		}
		if(Input.GetKey(KeyCode.Keypad3) && checkeandoCabina && estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal){
		//if (Input.GetButton ("Encendido") && checkeandoCabina && estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal) {
			if(tiempoEncendido - Time.time < 4)
				StartCoroutine (SonidoIgnicion ());
		}
		if(Input.GetKey(KeyCode.Keypad2))
		//if (Input.GetButtonUp("Encendido"))
		{
			if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal && (tiempoEncendido < Time.time))
			{
				tiempoEncendido = 0f;
				arranque(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagada);

				//audioEncendido.clip = Resources.Load("camion") as AudioClip;
				//if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.encendida) audioEncendido.Play();
			}
			//audioRetroceso.Stop();
			//if ((central.estado == Central.EstadoSimulacion.Finalizando || central.estado == Central.EstadoSimulacion.ApagadoExterior) && estado == EstadoMaquina.encendida) mensajeApagar.Toggle();
		}*/

		if (tiempoFaenaLabel != null) {
			tiempoFaenaLabel.text = "" + calcularReloj (configuracion.TiempoFaena * 60f + tiempo * 1f - Time.timeSinceLevelLoad);
			if (configuracion != null && Time.timeSinceLevelLoad - tiempo > configuracion.TiempoFaena * 60f)
				terminarSimulacion (true);
		}
		if(GetComponent<Rigidbody>() != null) Destroy (GetComponent<Rigidbody>());
		if (camara.activeSelf) {
			if (nivelPetroleo != null)
				nivelPetroleo.SetActive (nivelPetroleoActivada);
			if (nivelAceite != null)
				nivelAceite.SetActive (nivelAceiteActivada);
			if (nivelHidraulico != null)
				nivelHidraulico.SetActive (nivelHidraulicoActivada);
			if (nivelRefrigerante != null)
				nivelRefrigerante.SetActive (nivelRefrigeranteActivada);
			if (nivelAceiteTrans != null)
				//Debug.Log (nivelAceiteTransActivada);
				nivelAceiteTrans.SetActive (nivelAceiteTransActivada);
			if (nivelAceiteCajaTransf != null)
				nivelAceiteCajaTransf.SetActive (nivelAceiteCajaTransfActivada);
			if (filtroAire != null)
				filtroAire.SetActive (filtroAireActivada);
			if (filtroCombustible != null)
				filtroCombustible.SetActive (filtroCombustibleActivada);
			if (topeEjeCentral != null)
				topeEjeCentral.SetActive (topeEjeCentralActivada);
			if (indicadoresObstruccion != null)
				indicadoresObstruccion.SetActive (indicadoresObstruccionActivada);
			if (articulacionCentral != null)
				articulacionCentral.SetActive (articulacionCentralActivada);
			if (articulacionDireccional != null)
				articulacionDireccional.SetActive (articulacionDireccionalActivada);
			if (pasadoresGeneral != null)
				pasadoresGeneral.SetActive (pasadoresGeneralActivada);
			if (fugasCilindrosMangueras != null)
				fugasCilindrosMangueras.SetActive (fugasCilindrosManguerasActivada);
			if (sistemaAnsul != null)
				sistemaAnsul.SetActive (sistemaAnsulActivada);
			if (extintorManual != null)
				extintorManual.SetActive (extintorManualActivada);
		}
		/*if (partesMaquina [14].danado) {
			//manometros dañados
		} else {
			if(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida){
				tableroControl.setPetroleo(partesMaquina [0].danado ? 0f : 90f);
				tableroControl.setTemperatura(Random.Range(47f, 50f));
				tableroControl.setRevoluciones(Random.Range(10f, 12f));
			}
			else{
				tableroControl.setTemperatura(0f);
				tableroControl.setPetroleo(0f);
				tableroControl.setRevoluciones(-8f);
			}
		}*/

		//if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.encendida) {
			//nivel aceite transmision
			partesMaquina [6].parteBuena [0].transform.parent.gameObject.SetActive (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida);
			//nivel petroleo
			//partesMaquina [0].parteBuena [0].transform.parent.gameObject.SetActive (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida);
			partesMaquina [0].parteBuena [1].transform.parent.gameObject.SetActive (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida);

		//}
		varillaPetroleoDefault.SetActive(!(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida));

		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) {
			float brazo = 0f;
			#if UNITY_EDITOR
			brazo = Input.GetAxis("ControlTolbaEditor");
			#else
			print("Tolba: " + Input.GetAxis("ControlTolba") + ", Cambio: " + Input.GetAxis("Cambio"));
			brazo = Input.GetAxis("ControlTolba");
			#endif
			if (brazo != 0)
				animatorTolba.SetBool ("TolvaArriba", true);
			else
				animatorTolba.SetBool ("TolvaArriba", false);
			if(animatorTolba != null && animatorTolba.GetBool("TolvaArriba")) animatorTolba.SetFloat ("multiplicadorVelocidadBalde", Mathf.Clamp (brazo, -1f, 1f));
			float animTime = animatorTolba.GetCurrentAnimatorStateInfo (0).normalizedTime;
			animTime = Mathf.Clamp01 (animTime);
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

		if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal) {
			bool frenoParqEncendido = false;
			#if UNITY_EDITOR
			if(Input.GetKeyDown(KeyCode.Keypad5))
				frenoParqEncendido = !frenoParqEncendido;
			#else
			frenoParqEncendido = controlTarjetaControladora.BotonAccion() == 0;
			#endif
			tableroControl.encenderFrenoParq (frenoParqEncendido);
		}


		/*
		if(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida){
			tableroControl.setOp1menuD3 ("" + Random.Range(43, 45));
			tableroControl.setOp2menuD3 ("" + Random.Range(18, 19));
			//tableroControl.setOp3menuD3 ("" + Random.Range(100, 102));

			tableroControl.setOp1OperadorConvert ("799");
			tableroControl.setOp2OperadorConvert ("62");
			tableroControl.setOp3OperadorConvert ("14.0%");
			tableroControl.setOp4OperadorConvert ("54.0");
		}
		else{
			tableroControl.setOp1menuD3 ("0.0");
			tableroControl.setOp2menuD3 ("0.0");
			//tableroControl.setOp3menuD3 ("0.0");
			tableroControl.setOp1OperadorConvert ("0");
			tableroControl.setOp2OperadorConvert ("0");
			tableroControl.setOp3OperadorConvert ("0.0%");
			tableroControl.setOp4OperadorConvert ("0");
		}*/
		//if(controlTarjetaControladora.ControlLucesDelanteras() == 1 ){
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida){
			if (partesMaquina [0].danado)
				tableroControl.setPetroleo (0f);
			
			else
				tableroControl.setPetroleo (90f);
			tableroControl.encenderNeutro (true);
			tableroControl.encenderManual (true);
			tableroControl.setTemperatura (20f);
			if(lucesDelanteras.Length > 0){
				lucesAltasPala(controlTarjetaControladora.ControlLucesDelanteras() == 1);
				//lectorControles.OutCmd(byte.Parse("" + configuracionControles.idLedLucesAltasDelanteras), lucesDelanteras[0].gameObject.activeSelf);
			}
			if (lucesTraseras.Length > 0){
				lucesAltasMotor (controlTarjetaControladora.ControlLucesTraseras() == 1);
				//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesAltasTraseras), lucesTraseras [0].gameObject.activeSelf);
			}
			if(lucesCarga.Length > 0){
				lucesBajasPala(controlTarjetaControladora.ControlLucesCarga() == 1);
				//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasDelanteras), lucesCarga [0].gameObject.activeSelf);
			}
		}
		if (controlTarjetaControladora.ControlLucesTraseras() == 1) {
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)
			if (lucesTraseras.Length > 0){
				lucesAltasMotor (!lucesTraseras [0].gameObject.activeSelf);
				//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesAltasTraseras), lucesTraseras [0].gameObject.activeSelf);
			}
		}
		if (controlTarjetaControladora.ControlLucesCarga () == 1) {
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)
			if (lucesCarga.Length > 0) {
				lucesBajasPala (!lucesCarga [0].gameObject.activeSelf);
				//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasDelanteras), lucesCarga [0].gameObject.activeSelf);
			}
		} else {
			tableroControl.encenderNeutro (false);
			tableroControl.encenderManual (false);
			tableroControl.setPetroleo (0f);
			tableroControl.setTemperatura (0f);
		}

		tableroControl.setRevoluciones (-8f);
		float c = Input.GetAxis("Cambio");
		int cambioActual = 0;
		int  factorRetroceso = 1;
		#if UNITY_EDITOR
		if(Input.GetKey(KeyCode.Alpha1)) cambioActual = 1;
		if(Input.GetKey(KeyCode.Alpha2)) cambioActual = 2;
		if(Input.GetKey(KeyCode.Alpha3)) cambioActual = 3;
		if(Input.GetKey(KeyCode.Alpha4)) cambioActual = 4;

#else
            if (c > 0.9f)
            {
                cambioActual = 1;
                factorRetroceso = 1;
            }
            else
            {
                if(c > 0.6f)
                {
                    cambioActual = 2;
                    factorRetroceso = 1;
                }
                else
                {
                    if (c > 0.1f)
                    {
                        cambioActual = 3;
                        factorRetroceso = 1;
                    }
                    else
                    {
                        if (c > -0.3f)
                        {
                            cambioActual = 4;
                            factorRetroceso = 1;
                        }
                        else
                        {
                            if (c > -0.7f)
                            {
                                cambioActual = 0;
                                factorRetroceso = 1;
                            }
                        }
                    }
                }
				print("cambio " + cambioActual);
            }
            
        
#endif
        //print("cambio " + cambioActual);

        if (cambioActual == 1){
			//if (estado != estadoChequeo.revisionControles && estado != estadoChequeo.revisionPanel) {
				//if(listaActiva && checklistLista.listaSelec == 0) activarLista(false);
				//else{
					activarLista(true);
					verLista1();
				//}
				//}
		}
		if (cambioActual == 2) {
			//if (estado != estadoChequeo.revisionControles && estado != estadoChequeo.revisionPanel) {
				//if(listaActiva && checklistLista.listaSelec == 1) activarLista(false);
				//else{
					activarLista(true);
					verLista2();
				//}
				//}
		}
		if (cambioActual == 3) {
			//if (estado != estadoChequeo.revisionControles && estado != estadoChequeo.revisionPanel) {
				//if(listaActiva && checklistLista.listaSelec == 2) activarLista(false);
				//else{
					activarLista(true);
					verLista3();
				//}
			//}
		}
		if (cambioActual == 4) {
			//if (estado != estadoChequeo.revisionControles && estado != estadoChequeo.revisionPanel) {

			//	if(listaActiva && checklistLista.listaSelec == 3) activarLista(false);
			//	else{
					activarLista(true);
					verLista4();
			//	}
			//}
		}

		if (cambioActual == 0) {
			//if (estado != estadoChequeo.revisionControles && estado != estadoChequeo.revisionPanel) {

			//	if(listaActiva && checklistLista.listaSelec == 3) activarLista(false);
			//	else{
			activarLista(false);
			//	}
			//}
		}



		if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("Fire3")) {
			if (listaActiva) 
				return;
			if (puertaHidraulicaHabilitada)
				abrirPuertaHidraulica ();
			if (puertaAceiteHabilitada)
				abrirPuertaAceite ();
			if (ansuHabilitada)
				abrirAnsu ();
			if (motorHabilitada)
				abrirMotor ();
			if (puertaDropBoxHabilitada)
				abrirDropBox ();
			if (escaleraHabilitada) {
				Vector3 pos = controlUsuarioChecklist.transform.FindChild ("Camera").transform.localPosition;
				if (!sobreMaquina) {
					sobreMaquina = true;
					pos.y = 0.2f;
					controlUsuarioChecklist.transform.FindChild ("Camera").transform.localPosition = pos;
					controlUsuarioChecklist.transform.position = posicionSobreMaquina.position;
					controlUsuarioChecklist.transform.rotation = posicionSobreMaquina.rotation;
					Debug.Log ("subiendo");
				} else {
					sobreMaquina = false;
					pos.y = 1.1f;
					controlUsuarioChecklist.transform.FindChild ("Camera").transform.localPosition = pos;
					controlUsuarioChecklist.transform.position = posicionBajoMaquina.position;
					controlUsuarioChecklist.transform.rotation = posicionBajoMaquina.rotation;
					Debug.Log ("bajando");
				}
					
				/*if (maquinaTrigger != null)
					maquinaTrigger.triggerEnabled = true;*/
				
				mensajeInteraccion.text = "";
				mensajeInteraccion.gameObject.SetActive(false);
				controlUsuarioChecklist.enfocandoEscalera = false;
				controlUsuarioChecklist.enfocandoEscaleraActual = false;
				escaleraHabilitada = false;
			}
			if (puertaCabinaHabilitada && (estado == estadoChequeo.exterior || estado == estadoChequeo.interior) && !checkeandoControles && !checkeandoPanel) {
				bool aux;
				if (estado == estadoChequeo.exterior) {
					estado = estadoChequeo.interior;
					aux = true;
				} else {
					estado = estadoChequeo.exterior;
					aux = false;
				}
				checkeandoCabina = !checkeandoCabina;
				gameObject.SetActive (false);
				controlUsuarioChecklist.gameObject.SetActive (!checkeandoCabina);
				controlCamaraInterior.gameObject.SetActive (checkeandoCabina);
				controlCamaraInterior.enabled = checkeandoCabina;
				if (controlCamaraInterior.enabled)
					controlCamaraInterior.transform.rotation = controlCamaraInterior.rotacionInicial;
				gameObject.SetActive (true);
				deshabilitarCabina();
				print ("ingresando a cabina checklist");
				mensajeInteraccion.text = "";
				mensajeInteraccion.gameObject.SetActive(false);
				ingame.tableroControl.GetComponentInParent<Camera> ().enabled = aux;
				controlUsuarioChecklist.ingresarCabinaCheck (aux);
				//abrirCabina();
			} else {
				if (brazoHabilitado)
					abrirBrazo ();
				if (estado == estadoChequeo.interior) {
					if (instruccionesControles.activeSelf) {

					} else {
						/*	estado = estadoChequeo.revisionControles;
						mensajeInteraccion.text = "";
						foreach (GameObject g in GUINormal)
							g.SetActive (false);
						controlUsuarioChecklist.enabled = false;
						controlCamaraInterior.enabled = false;
						instruccionesControles.SetActive (true);
						ControlMouseOperador c = controlCamaraInterior.GetComponent<ControlMouseOperador> ();
						c.enabled = true;
						controlMouseOperador.enabled = true;*/

					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("Fire3")) {
			if(!controlUsuarioChecklist.enfocandoEncendido) return;
			print ("abrir tapa");
			/*if(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagadaTotal){
				estadoExcavadoraChecklist = ControlCamion.EstadoMaquina.apagada;	
				iso_switch.PlayForward();
			}
			else{
				estadoExcavadoraChecklist = ControlCamion.EstadoMaquina.apagadaTotal;	
				iso_switch.PlayReverse();
				if (sonidoMotor.isPlaying){	
					sonidoMotor.Stop ();
					sonidoMotor.PlayOneShot(sonidoApagadoMotor);
				}
				pantallaTactil.SetActive (false);
				if(instruccionesPantalla != null) instruccionesPantalla.SetActive(false);
			}*/
		}
		/*
		if(indice == configuracionControles.idBotonEncendido){
			if (estadoExcavadoraChecklist != ControlCamion.EstadoMaquina.apagadaTotal && tiempoEncendido < Time.time){
				tiempoEncendido = 0f;
				arranque(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagada);

				audioEncendido.Stop();
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedEncendido), estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida);
			}
		}
		if (indice == configuracionControles.idBotonClaxon){ 
			if(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagada || estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) {
				if(!partesMaquina[16].danado){
					Transform b = transform.FindChild("Bocina");
					b.GetComponent<AudioSource>().Play();
					print ("bocina" + (!partesMaquina[16].danado) + b.name);
				}
			}
		}
		if (indice == configuracionControles.idBotonDisplayON) {
			if(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagadaTotal) return;
			pantallaTactil.SetActive (!pantallaTactil.activeSelf);

			//lectorControles.encenderLedDigital (byte.Parse ("" + configuracionControles.idLedDisplayON), pantallaTactil.activeSelf);
			if (!pantallaTactil.activeSelf) {
				//	lectorControles.apagarLeds ();
			} else {
				//	lectorControles.encenderLeds ();
			}
		}
		if (indice == configuracionControles.idBotonDisplayOFF) {
		}*/

		if (Input.GetKeyDown (KeyCode.M)) {
			botonTerminarChecklist ();
			//finalizarModuloChecklist ();
			//terminarSimulacionFinal();
		}


	}

	public void botonTerminarChecklist(){
		if (!singleChecklist && checklistIndex == 1) {
			terminarSimulacionParcial ();
			Debug.Log ("simulacion parcial");
		} else {
			terminarSimulacionFinal ();
			Debug.Log ("simulacion final");
		}
	}


	public void terminarSimulacionFinal(){
		terminarSimulacion (true);
		if (checklistIndex == 1) {
			enviarDatos ();
		} else if (checklistIndex == 2) {
			enviarDatos2 ();
		}
	}

	public void terminarSimulacionParcial(){
		terminarSimulacion (false);
		arreglarMaquina ();
		finalizarModuloChecklist ();
		enviarDatos1 ();
		//GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ().modoChecklist = false;
		InGame aux = GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ();
		aux.modoChecklist = false;
		aux.maquina.GetComponent<ControlCamion> ().estado = ControlCamion.EstadoMaquina.apagadaTotal;
	}

	public void terminarSimulacion(bool mostrarPanelFinal){
		GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ().estado = InGame.EstadoSimulacion.Finalizando;
		activarLista (false);
		arranque (false);
		puertaHidraulicaActivada = true;
		abrirPuertaHidraulica();
		puertaCabinaActivada = true;
		//abrirCabina ();
		brazoActivado = true;
		abrirBrazo ();

		for (int i = 0; i < partesMaquina.Length; i++) {
			switch(partesMaquina[i].indiceSeccion){
			case 0:
				if(partesMaquina[i].danado && checklistLista.respuestas1[partesMaquina[i].indicePregunta] == 2){
					resultadoSeccion1++;
				}
				if(!partesMaquina[i].danado && checklistLista.respuestas1[partesMaquina[i].indicePregunta] == 1){
					resultadoSeccion1++;
				}
				print (i + " " + partesMaquina[i].danado + " " + checklistLista.respuestas1[partesMaquina[i].indicePregunta]);
				break;
			case 1:
				if(partesMaquina[i].danado && checklistLista.respuestas2[partesMaquina[i].indicePregunta] == 2){
					resultadoSeccion2++;
				}
				if(!partesMaquina[i].danado && checklistLista.respuestas2[partesMaquina[i].indicePregunta] == 1){
					resultadoSeccion2++;
				}
				break;
			case 2:
				if(partesMaquina[i].danado && checklistLista.respuestas3[partesMaquina[i].indicePregunta] == 2){
					resultadoSeccion3++;
				}
				if(!partesMaquina[i].danado && checklistLista.respuestas3[partesMaquina[i].indicePregunta] == 1){
					resultadoSeccion3++;
				}
				break;
			case 3:
				if(partesMaquina[i].danado && checklistLista.respuestas4[partesMaquina[i].indicePregunta] == 2){
					resultadoSeccion4++;
				}
				if(!partesMaquina[i].danado && checklistLista.respuestas4[partesMaquina[i].indicePregunta] == 1){
					resultadoSeccion4++;
				}
				break;
			}
		}
		//casos especiales
		//luces
		if(partesMaquina[19].danado && checklistLista.respuestas1[3] == 2){
			resultadoSeccion1++;
		}
		if(!partesMaquina[19].danado && checklistLista.respuestas1[3] == 1){
			resultadoSeccion1++;
		}

		resultadoSeccion1 = (int)((resultadoSeccion1 / (1f * checklistLista.respuestas1.Length)) * 100);
		resultadoSeccion2 = (int)((resultadoSeccion2 / (1f * checklistLista.respuestas2.Length)) * 100);
		resultadoSeccion3 = (int)((resultadoSeccion3 / (1f * checklistLista.respuestas3.Length)) * 100);
		resultadoSeccion4 = (int)((resultadoSeccion4 / (1f * checklistLista.respuestas4.Length)) * 100);
		resultadoTotal = (int)((resultadoSeccion1 + resultadoSeccion2 + resultadoSeccion3 + resultadoSeccion4 ) / 4);

		/*resultadoSeccion1Label.text = resultadoSeccion1 + "%";
		resultadoSeccion2Label.text = resultadoSeccion2 + "%";
		resultadoSeccion3Label.text = resultadoSeccion3 + "%";
		resultadoSeccion4Label.text = resultadoSeccion4 + "%";
		
		resultadoTotalLabel.text = resultadoTotal + "%";*/

		tiempo = (int)(Time.timeSinceLevelLoad - tiempo);

		//configuracion.ResultadoRevFunc1 = (int)resultadoSeccion1;
		//configuracion.ResultadoRevCab1 = (int)resultadoSeccion2;
		//configuracion.ResultadoRevEst1 = (int)resultadoSeccion3;
		//configuracion.ResultadoPrevRies1 = (int)resultadoSeccion4;
		//configuracion.ResultadoTiempo = (int)tiempo;
		configuracion.loadLabels ();

		//resultadoTiempoLabel.text = Configuracion.calcularReloj(tiempo); 
		InGame central = GameObject.FindWithTag ("InGame").GetComponent<InGame> ();
		if(resultadoRepeticionesLabel != null)resultadoRepeticionesLabel.text = "" + central.repeticiones;

		if(resultadoSeccion1LabelUser != null) resultadoSeccion1LabelUser.text = resultadoSeccion1 + "%";
		if(resultadoSeccion2LabelUser != null) resultadoSeccion2LabelUser.text = resultadoSeccion2 + "%";
		if(resultadoSeccion3LabelUser != null) resultadoSeccion3LabelUser.text = resultadoSeccion3 + "%";
		if(resultadoSeccion4LabelUser != null) resultadoSeccion4LabelUser.text = resultadoSeccion4 + "%";
		
		if(resultadoTotalLabelUser != null) resultadoTotalLabelUser.text = resultadoTotal + "%";
		if(resultadoTiempoLabelUser != null) resultadoTiempoLabelUser.text = Configuracion.calcularReloj(tiempo); 
		if(resultadoRepeticionesLabelUser != null)resultadoRepeticionesLabelUser.text = "" + central.repeticiones;

		if (mostrarPanelFinal) {
			activa = false;
			controlMouseOperador.enabled = true;
			foreach(GameObject g in GUINormal)
				g.SetActive(false);
			controlUsuarioChecklist.desactivar ();
			controlCamaraInterior.desactivar();
			controlMouseOperador.enabled = false;
			controlMouseOperadorInterior.enabled = false;
			
			checklistLista.gameObject.SetActive (false);
			controlUsuarioChecklist.enabled = false;
			controlCamaraInterior.enabled = false;
			controlCamaraInterior.gameObject.SetActive(false);
			if (singleChecklist) {
				if (configuracion != null && resultadoTotal >= configuracion.check1) {
					panelAprobado [0].SetActive (true);
					panelAprobado [1].SetActive (true);
					panelReprobado [0].SetActive (false);
					panelReprobado [1].SetActive (false);
				} else {
					panelAprobado [0].SetActive (false);
					panelAprobado [1].SetActive (false);
					panelReprobado [0].SetActive (true);
					panelReprobado [1].SetActive (true);
				}
				panelResultados [0].SetActive (true);
				panelResultados [1].SetActive (true);
			} else {
				panelAprobado [0].SetActive (false);
				panelAprobado [1].SetActive (false);
				panelReprobado [0].SetActive (false);
				panelReprobado [1].SetActive (false);
				panelResultados [0].SetActive (false);
				panelResultados [1].SetActive (false);
			}
		} else {
			GameObject.FindGameObjectWithTag ("InGame").GetComponent<InGame> ().modoChecklist = true;
			controlMouseOperador.enabled = false;
			if(diapositivaFinalChecklist != null){
				if(estado == estadoChequeo.exterior) controlMouseOperador.enabled = true;
				else controlMouseOperadorInterior.enabled = true;
				camara.SetActive(false);
				if(singleChecklist)
					diapositivaFinalChecklist.SetActive(true);
			}
			else{
				//terminarSimulacion(true);
				return;
			}
		}
		
		nivelPetroleoActivada = false;
		nivelAceiteActivada = false;
		nivelHidraulicoActivada = false;
		nivelRefrigeranteActivada = false;
		nivelAceiteTransActivada = false;
		nivelAceiteCajaTransfActivada = false;
		filtroAireActivada = false;
		filtroCombustibleActivada = false;
		topeEjeCentralActivada = false;
		indicadoresObstruccionActivada = false;
		articulacionCentralActivada = false;
		articulacionDireccionalActivada = false;
		pasadoresGeneralActivada = false;
		fugasCilindrosManguerasActivada = false;
		sistemaAnsulActivada = false;
		extintorManualActivada = false;
		nivelPetroleo.SetActive(nivelPetroleoActivada);
		nivelAceite.SetActive(nivelAceiteActivada);
		nivelHidraulico.SetActive(nivelHidraulicoActivada);
		nivelRefrigerante.SetActive(nivelRefrigeranteActivada);
		nivelAceiteTrans.SetActive(nivelAceiteTransActivada);
		nivelAceiteCajaTransf.SetActive(nivelAceiteCajaTransfActivada);
		filtroAire.SetActive(filtroAireActivada);
		filtroCombustible.SetActive(filtroCombustibleActivada);
		topeEjeCentral.SetActive(topeEjeCentralActivada);
		indicadoresObstruccion.SetActive(indicadoresObstruccionActivada);
		articulacionCentral.SetActive(articulacionCentralActivada);
		articulacionDireccional.SetActive(articulacionDireccionalActivada);
		pasadoresGeneral.SetActive(pasadoresGeneralActivada);
		fugasCilindrosMangueras.SetActive(fugasCilindrosManguerasActivada);
		sistemaAnsul.SetActive(sistemaAnsulActivada);
		extintorManual.SetActive (extintorManualActivada);
	}

	public void finalizarModuloChecklist (){
		activar (false);
		arranque (false);
		foreach(GameObject g in GUINormal)
			g.SetActive(false);
		//controlUsuarioChecklist.desactivar ();
		controlCamaraInterior.desactivar();
		controlMouseOperador.enabled = false;
		controlMouseOperadorInterior.enabled = false;
		
		checklistLista.gameObject.SetActive (false);
		//controlUsuarioChecklist.enabled = false;
		controlCamaraInterior.enabled = false;
		controlCamaraInterior.gameObject.SetActive(false);

		InGame c = GameObject.Find("InGame").GetComponent<InGame>();
		c.realizarEntregaNombrada = false;
		print (controlUsuarioChecklist.realizarChecklist);
		c.iniciarSimulacion();
		controlUsuarioChecklist.realizarChecklist = false;
	}

	public void activar(bool activa){
//		print ("ACTIVANDO CHECKLIST " + activa);
		this.activa = activa;
		//if(configuracion.NumeroModulo == "4") 
		if(camara == null)
			camara = transform.FindChild ("Camera").gameObject;
		camara.SetActive (activa); 
		if (activa) {
			controlCamaraInterior.enabled = true;
			iso_switch.ResetToBeginning();
			if (diapositivaInicioChecklist != null){
				camara.SetActive(false);
				diapositivaInicioChecklist.SetActive (true);
				controlMouseOperador.enabled = true;
			}
			//controlMouseOperadorInterior.enabled = true;
			//controlCamaraInterior.desactivar();
			//controlUsuarioChecklist.enabled = false;
		}
		else{
			if(pantallaTactil != null)
				pantallaTactil.SetActive(false);
		}
	}

	public void activarCamara(){
		camara.SetActive(true);
	}

	public void enviarDatos(){
		enviarDatos1 ();
		/*
        configuracion.CheckRFNivPet = partesMaquina[19].danado ? -1 : 1;
        configuracion.ResultadoCheckRFNivPet = ((partesMaquina[19].danado && checklistLista.respuestas1[partesMaquina[19].indicePregunta] == 2) || (!partesMaquina[19].danado && checklistLista.respuestas1[partesMaquina[19].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRFNivAceMot = partesMaquina[26].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRFNivAceMot = ((partesMaquina[26].danado && checklistLista.respuestas1[partesMaquina[26].indicePregunta] == 2) || (!partesMaquina[26].danado && checklistLista.respuestas1[partesMaquina[26].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRFNivAceHid = partesMaquina[27].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRFNivAceHid = ((partesMaquina[27].danado && checklistLista.respuestas1[partesMaquina[27].indicePregunta] == 2) || (!partesMaquina[27].danado && checklistLista.respuestas1[partesMaquina[27].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRFEstLuc = partesMaquina[3].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRFEstLuc = ((partesMaquina[3].danado && checklistLista.respuestas1[partesMaquina[3].indicePregunta] == 2) || (!partesMaquina[3].danado && checklistLista.respuestas1[partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRFEstNeu = partesMaquina[1].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRFEstNeu = ((partesMaquina[1].danado && checklistLista.respuestas1[partesMaquina[1].indicePregunta] == 2) || (!partesMaquina[1].danado && checklistLista.respuestas1[partesMaquina[1].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRFNivRef = partesMaquina[20].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRFNivRef = ((partesMaquina[20].danado && checklistLista.respuestas1[partesMaquina[20].indicePregunta] == 2) || (!partesMaquina[20].danado && checklistLista.respuestas1[partesMaquina[20].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRFNivAceTra = partesMaquina[21].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRFNivAceTra = ((partesMaquina[21].danado && checklistLista.respuestas1[partesMaquina[21].indicePregunta] == 2) || (!partesMaquina[21].danado && checklistLista.respuestas1[partesMaquina[21].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckREBal = partesMaquina[5].danado ? -1 : 1; ;
        configuracion.ResultadoCheckREBal = ((partesMaquina[5].danado && checklistLista.respuestas2[partesMaquina[5].indicePregunta] == 2) || (!partesMaquina[5].danado && checklistLista.respuestas2[partesMaquina[5].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckREAnt = partesMaquina[4].danado ? -1 : 1; ;
        configuracion.ResultadoCheckREAnt = ((partesMaquina[4].danado && checklistLista.respuestas2[partesMaquina[4].indicePregunta] == 2) || (!partesMaquina[4].danado && checklistLista.respuestas2[partesMaquina[4].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckREArtCen = partesMaquina[8].danado ? -1 : 1; ;
        configuracion.ResultadoCheckREArtCen = ((partesMaquina[8].danado && checklistLista.respuestas2[partesMaquina[8].indicePregunta] == 2) || (!partesMaquina[8].danado && checklistLista.respuestas2[partesMaquina[8].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckREPasGen = partesMaquina[15].danado ? -1 : 1; ;
        configuracion.ResultadoCheckREPasGen = ((partesMaquina[15].danado && checklistLista.respuestas2[partesMaquina[15].indicePregunta] == 2) || (!partesMaquina[15].danado && checklistLista.respuestas2[partesMaquina[15].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckREFug = partesMaquina[14].danado ? -1 : 1; ;
        configuracion.ResultadoCheckREFug = ((partesMaquina[14].danado && checklistLista.respuestas2[partesMaquina[14].indicePregunta] == 2) || (!partesMaquina[14].danado && checklistLista.respuestas2[partesMaquina[14].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCLimPar = partesMaquina[10].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCLimPar = ((partesMaquina[10].danado && checklistLista.respuestas3[partesMaquina[10].indicePregunta] == 2) || (!partesMaquina[10].danado && checklistLista.respuestas3[partesMaquina[10].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCCab = partesMaquina[6].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCCab = ((partesMaquina[6].danado && checklistLista.respuestas3[partesMaquina[6].indicePregunta] == 2) || (!partesMaquina[6].danado && checklistLista.respuestas3[partesMaquina[6].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCMan = partesMaquina[13].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCMan = ((partesMaquina[13].danado && checklistLista.respuestas3[partesMaquina[13].indicePregunta] == 2) || (!partesMaquina[13].danado && checklistLista.respuestas3[partesMaquina[13].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCLucGen = partesMaquina[12].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCLucGen = ((partesMaquina[12].danado && checklistLista.respuestas3[partesMaquina[12].indicePregunta] == 2) || (!partesMaquina[12].danado && checklistLista.respuestas3[partesMaquina[12].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCMonDis = partesMaquina[22].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCMonDis = ((partesMaquina[22].danado && checklistLista.respuestas3[partesMaquina[22].indicePregunta] == 2) || (!partesMaquina[22].danado && checklistLista.respuestas3[partesMaquina[22].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCAseCab = partesMaquina[11].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCAseCab = ((partesMaquina[11].danado && checklistLista.respuestas3[partesMaquina[11].indicePregunta] == 2) || (!partesMaquina[11].danado && checklistLista.respuestas3[partesMaquina[11].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCBoc = partesMaquina[16].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCBoc = ((partesMaquina[16].danado && checklistLista.respuestas3[partesMaquina[16].indicePregunta] == 2) || (!partesMaquina[16].danado && checklistLista.respuestas3[partesMaquina[16].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckPREstExtMan = partesMaquina[2].danado ? -1 : 1; ;
        configuracion.ResultadoCheckPREstExtMan = ((partesMaquina[2].danado && checklistLista.respuestas4[partesMaquina[2].indicePregunta] == 2) || (!partesMaquina[2].danado && checklistLista.respuestas4[partesMaquina[2].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckPREstExtInc = partesMaquina[3].danado ? -1 : 1; ;
        configuracion.ResultadoCheckPREstExtInc = ((partesMaquina[20].danado && checklistLista.respuestas4[partesMaquina[3].indicePregunta] == 2) || (!partesMaquina[3].danado && checklistLista.respuestas4[partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckPREstEsc = partesMaquina[9].danado ? -1 : 1; ;
        configuracion.ResultadoCheckPREstEsc = ((partesMaquina[9].danado && checklistLista.respuestas4[partesMaquina[9].indicePregunta] == 2) || (!partesMaquina[9].danado && checklistLista.respuestas4[partesMaquina[9].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckPRSalEme = partesMaquina[18].danado ? -1 : 1; ;
        configuracion.ResultadoCheckPRSalEme = ((partesMaquina[18].danado && checklistLista.respuestas4[partesMaquina[18].indicePregunta] == 2) || (!partesMaquina[18].danado && checklistLista.respuestas4[partesMaquina[18].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckPRSenMov = partesMaquina[23].danado ? -1 : 1; ;
        configuracion.ResultadoCheckPRSenMov = ((partesMaquina[23].danado && checklistLista.respuestas4[partesMaquina[23].indicePregunta] == 2) || (!partesMaquina[23].danado && checklistLista.respuestas4[partesMaquina[23].indicePregunta] == 1)) ? 1 : -1;

        configuracion.CheckRCTemAceMot = partesMaquina[24].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCTemAceMot = ((partesMaquina[24].danado && checklistLista.respuestas3[partesMaquina[24].indicePregunta] == 2) || (!partesMaquina[24].danado && checklistLista.respuestas3[partesMaquina[24].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCTemAceTra = partesMaquina[25].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCTemAceTra = ((partesMaquina[25].danado && checklistLista.respuestas3[partesMaquina[25].indicePregunta] == 2) || (!partesMaquina[25].danado && checklistLista.respuestas3[partesMaquina[25].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCVen = partesMaquina[0].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCVen = ((partesMaquina[0].danado && checklistLista.respuestas3[partesMaquina[0].indicePregunta] == 2) || (!partesMaquina[0].danado && checklistLista.respuestas3[partesMaquina[0].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCJoy = partesMaquina[28].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCJoy = ((partesMaquina[28].danado && checklistLista.respuestas3[partesMaquina[28].indicePregunta] == 2) || (!partesMaquina[28].danado && checklistLista.respuestas3[partesMaquina[28].indicePregunta] == 1)) ? 1 : -1;
        configuracion.CheckRCPed = partesMaquina[29].danado ? -1 : 1; ;
        configuracion.ResultadoCheckRCPed = ((partesMaquina[29].danado && checklistLista.respuestas3[partesMaquina[29].indicePregunta] == 2) || (!partesMaquina[29].danado && checklistLista.respuestas3[partesMaquina[29].indicePregunta] == 1)) ? 1 : -1;
*/

        configuracion.guardarHistorial ();
		StartCoroutine (pausaFinal ());
	}

	public void enviarDatos1(){
		//datos 1
		configuracion.ResultadoTiempo = Mathf.RoundToInt(tiempo);
		configuracion.ResultadoCheck1 = (int)resultadoTotal;
		configuracion.ResultadoRevFunc1 = (int)resultadoSeccion1;
		configuracion.ResultadoRevCab1 = (int)resultadoSeccion2;
		configuracion.ResultadoRevEst1 = (int)resultadoSeccion3;
		configuracion.ResultadoPrevRies1 = (int)resultadoSeccion4;
		Debug.Log ("datos1");
	}

	public void enviarDatos2(){
		Debug.Log ("datos2");
		configuracion.ResultadoTiempo = Mathf.RoundToInt(tiempo);
		configuracion.ResultadoCheck2 = (int)resultadoTotal;
		configuracion.ResultadoRevFunc2 = (int)resultadoSeccion1;
		configuracion.ResultadoRevCab2 = (int)resultadoSeccion2;
		configuracion.ResultadoRevEst2 = (int)resultadoSeccion3;
		configuracion.ResultadoPrevRies2 = (int)resultadoSeccion4;
		configuracion.guardarHistorial ();
		StartCoroutine (pausaFinal ());
	}
	
	public void cerrarPanelControles(){
		print ("cerrar");
		StartCoroutine (cerrarPanelControlesDelay ());
	}
	IEnumerator cerrarPanelControlesDelay(){
		yield return new WaitForSeconds (0.1f);
		estado = estadoChequeo.interior;
		controlUsuarioChecklist.enabled = true;
		controlCamaraInterior.enabled = true;
		instruccionesControles.SetActive(false);
		controlMouseOperador.enabled = false;
		ControlMouseOperador c = controlCamaraInterior.GetComponent<ControlMouseOperador>();
		c.enabled = false;
		foreach(GameObject g in GUINormal)
			g.SetActive(true);
	}

	IEnumerator pausaFinal(){
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene("Login");
	}

	public void toggleCabina(){
		if (estado == estadoChequeo.exterior)
			estado = estadoChequeo.interior;
		else
			estado = estadoChequeo.exterior;
		checkeandoCabina = !checkeandoCabina;
		gameObject.SetActive (false);
		controlUsuarioChecklist.gameObject.SetActive (!checkeandoCabina);
		controlCamaraInterior.gameObject.SetActive (checkeandoCabina);
		controlCamaraInterior.enabled = checkeandoCabina;
		if (controlCamaraInterior.enabled)
			controlCamaraInterior.transform.rotation = controlCamaraInterior.rotacionInicial;
		gameObject.SetActive (true);
		deshabilitarCabina();
		print ("ingresando a cabina checklist");
		mensajeInteraccion.text = "";
		mensajeInteraccion.gameObject.SetActive(false);
	}
}


[System.Serializable]
public class ParteMaquina{
	public int indiceSeccion = 0;
	public int indicePregunta = 0;
	public GameObject[] parteBuena;
	public GameObject[] parteMala;
	public bool danado = false;
}