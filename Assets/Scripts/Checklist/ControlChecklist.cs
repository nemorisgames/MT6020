using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlChecklist : MonoBehaviour {
	ControlTarjetaControladora controlTarjetaControladora;
	Configuracion configuracion;

	public ChecklistLista checklistLista;
	public ControlMouseOperador controlMouseOperador;
	public ControlMouseOperador controlMouseOperadorInterior;
	public ControlUsuarioChecklist controlUsuarioChecklist;
	public ControlCamaraInterior controlCamaraInterior;

	public GameObject diapositivaInicioChecklist;
	public GameObject diapositivaFinalChecklist;


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

	public Transform posicionSobreMaquina;
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

	public GameObject[] GUINormal;
	public GameObject instruccionesControles;
	public GameObject instruccionesPantalla;

	public ControlChecklistControles controlChecklistControles;
	public ControlPantallaTactil controlPantallaTactil;

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
	public AudioSource sonidoMotor;
	public AudioClip sonidoApagadoMotor;
    public AudioSource audioEncendido;

    public Light[] lucesDelanteras;
	public Light[] lucesCarga;
	public Light[] lucesTraseras;
	public Light[] lucesTraserasBajas;

	// Use this for initialization
	void Start () {
		controlTarjetaControladora = GameObject.FindWithTag("TarjetaControladora").GetComponent<ControlTarjetaControladora>();
		//Central central = GameObject.FindWithTag ("Central").GetComponent<Central>();
		camara = transform.FindChild ("Camera").gameObject;
		//pantallaTactil = controlPantallaTactil.transform.parent.parent.parent.gameObject;
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
	}

	void BotonDown(int indice){
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
	}
	
	public void arranque(bool activar){
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.apagadaTotal)
			return;
		if (activar)
			sonidoMotor.Play ();
		else { 
			sonidoMotor.Stop ();
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) 
				sonidoMotor.PlayOneShot(sonidoApagadoMotor);
			lucesAltasPala(false);
			lucesAltasMotor(false);
			lucesBajasPala(false);
			lucesBajasMotor(false);
			lectorControles.OutCmd(byte.Parse("" + configuracionControles.idLedLucesAltasDelanteras), false);
			lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesAltasTraseras), false);
			lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasDelanteras), false);
			lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasTraseras), false);
		}
		print ("arranque" + activar);
		estadoExcavadoraChecklist = activar?ControlCamion.EstadoMaquina.encendida:ControlCamion.EstadoMaquina.apagada;
		//enciende leds iniciales
		//lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedTransmisionAutomatica), true);
		//controlPantallaTactil.motorEncendido (activar);
		//controlPantallaTactil.neutro (true);
	}

	public void arreglarMaquina(){
		for (int i = 0; i < partesMaquina.Length; i++) {
				
			ParteMaquina p = partesMaquina[i];
			if(p.parteBuena != null)
				for(int j = 0; j < p.parteBuena.Length; j++)
					p.parteBuena[j].SetActive(true);
			if (p.parteMala != null)
				for(int j = 0; j < p.parteMala.Length; j++)
					p.parteMala[j].SetActive(false);
		}
		/*controlPantallaTactil.setPorcentajePetroleo ("90.0");
		
		controlPantallaTactil.setOilTemp ("10.0");
		controlPantallaTactil.setOp4OperadorConvert ("10.0");
		
		controlPantallaTactil.setOp4OperadorConvert ("10.0");

		controlPantallaTactil.tiempoMotorFuncionando = 0f;*/

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

	}

	public void mostrarDanio(ParteMaquina p, bool danado, bool danioAleatorio){
		if(p.parteBuena != null)
			for(int i = 0; i < p.parteBuena.Length; i++)
				p.parteBuena[i].SetActive(!danado);

		if (danioAleatorio) {
			if (p.parteMala != null && p.parteMala.Length > 0) {
				if (!danado) {
					print ("mostrando todos ok");
					for (int i = 0; i < p.parteMala.Length; i++)
						p.parteMala [i].SetActive (danado);
				} else {
					int indiceDanio = 0;
					if (p.parteMala.Length > 1)
						indiceDanio = Random.Range (0, p.parteMala.Length);
					print ("parte mala seleccionada " + indiceDanio);
					for (int i = 0; i < p.parteMala.Length; i++) {
						if (i == indiceDanio) { 
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
		
		/*controlPantallaTactil.setPorcentajePetroleo (partesMaquina [19].danado ? "10.0" : "90.0");

		controlPantallaTactil.setOilTemp (partesMaquina [24].danado ? "85.0" : "20.0");
		controlPantallaTactil.setOp4OperadorConvert (partesMaquina [25].danado ? "20.0" : "35.0");

		controlPantallaTactil.setOp4OperadorConvert (partesMaquina [26].danado ? "100.0" : "10.0");*/
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


	public void abrirCabina(){
		puertaCabinaActivada = !puertaCabinaActivada;
		foreach (TweenRotation t in puertaCabina) {
			t.Play(puertaCabinaActivada);
		}
	}

	public void abrirBrazo(){
		brazoActivado = !brazoActivado;
		foreach (TweenRotation t in brazo) {
			t.Play(brazoActivado);
		}
		cilindroBrazo.Play (brazoActivado);
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
	public void lucesBajasMotor(bool activar){
		if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida) 
			foreach (Light l in lucesTraserasBajas)
				l.gameObject.SetActive (activar);
	}


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

	// Update is called once per frame
	void Update () {
		if (!activa)
			return;
		if (Input.GetKey(KeyCode.Q)) {
			print ("press lista 4");
			if (estado != estadoChequeo.revisionControles && estado != estadoChequeo.revisionPanel) {
				
				if(listaActiva && checklistLista.listaSelec == 3) activarLista(false);
				else{
					activarLista(true);
					verLista4();
				}
			}
		}
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
		}
		/*if (partesMaquina [13].danado) {
			//manometros dañados
		} else {
			if(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida){
				controlPantallaTactil.setOp1menuD2("" + Random.Range(790, 810));
				controlPantallaTactil.setOp2menuD2("" + Random.Range(60, 64));
				controlPantallaTactil.setOp3menuD2(partesMaquina [20].danado ? "0.0" : "2.5");
			}
			else{
				controlPantallaTactil.setOp1menuD2("0.0");
				controlPantallaTactil.setOp2menuD2("0.0");
				controlPantallaTactil.setOp3menuD2("0.0");
			}
		}
		if(estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida){
			controlPantallaTactil.setOp1menuD3 ("" + Random.Range(43, 45));
			controlPantallaTactil.setOp2menuD3 ("" + Random.Range(18, 19));
			//controlPantallaTactil.setOp3menuD3 ("" + Random.Range(100, 102));

			controlPantallaTactil.setOp1OperadorConvert ("799");
			controlPantallaTactil.setOp2OperadorConvert ("62");
			controlPantallaTactil.setOp3OperadorConvert ("14.0%");
			controlPantallaTactil.setOp4OperadorConvert ("54.0");
		}
		else{
			controlPantallaTactil.setOp1menuD3 ("0.0");
			controlPantallaTactil.setOp2menuD3 ("0.0");
			//controlPantallaTactil.setOp3menuD3 ("0.0");
			controlPantallaTactil.setOp1OperadorConvert ("0");
			controlPantallaTactil.setOp2OperadorConvert ("0");
			controlPantallaTactil.setOp3OperadorConvert ("0.0%");
			controlPantallaTactil.setOp4OperadorConvert ("0");
		}*/

		if(controlTarjetaControladora.ControlLucesDelanteras() == 1 ){
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)
			if(lucesDelanteras.Length > 0){
				lucesAltasPala(!lucesDelanteras[0].gameObject.activeSelf);
				lectorControles.OutCmd(byte.Parse("" + configuracionControles.idLedLucesAltasDelanteras), lucesDelanteras[0].gameObject.activeSelf);
			}

		}
		if (controlTarjetaControladora.ControlLucesTraseras() == 1) {
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)
			if (lucesTraseras.Length > 0){
				lucesAltasMotor (!lucesTraseras [0].gameObject.activeSelf);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesAltasTraseras), lucesTraseras [0].gameObject.activeSelf);
			}
		}
		if(controlTarjetaControladora.ControlLucesCarga() == 1){
			if (estadoExcavadoraChecklist == ControlCamion.EstadoMaquina.encendida)
			if(lucesCarga.Length > 0){
				lucesBajasPala(!lucesCarga[0].gameObject.activeSelf);
				lectorControles.OutCmd (byte.Parse ("" + configuracionControles.idLedLucesBajasDelanteras), lucesCarga [0].gameObject.activeSelf);
			}
		}
		float c = Input.GetAxis("Cambio");
		int cambioActual = 0;
		if (c > 0.9f)
		{
			cambioActual = 1;
		}
		else
		{
			if(c > 0.6f)
			{
				cambioActual = 2;
			}
			else
			{
				if (c > 0.1f)
				{
					cambioActual = 3;
				}
				else
				{
					if (c > -0.3f)
					{
						cambioActual = 4;
					}
					else
					{
						if (c > -0.7f)
						{
							cambioActual = 0;
						}
					}
				}
			}
		}
		print("cambio " + cambioActual);

		if(cambioActual == 1){
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
			if (escaleraHabilitada) {
				controlUsuarioChecklist.transform.position = posicionSobreMaquina.position;
				controlUsuarioChecklist.transform.rotation = posicionSobreMaquina.rotation;
				mensajeInteraccion.text = "";
				mensajeInteraccion.gameObject.SetActive(false);
				controlUsuarioChecklist.enfocandoEscalera = false;
				controlUsuarioChecklist.enfocandoEscaleraActual = false;
				escaleraHabilitada = false;
			}
			if (puertaCabinaHabilitada && (estado == estadoChequeo.exterior || estado == estadoChequeo.interior) && !checkeandoControles && !checkeandoPanel) {
				if (estado == estadoChequeo.exterior)
					estado = estadoChequeo.interior;
				else
					estado = estadoChequeo.exterior;
				checkeandoCabina = !checkeandoCabina;
				gameObject.SetActive (false);
				controlUsuarioChecklist.gameObject.SetActive (!checkeandoCabina);
				controlCamaraInterior.gameObject.SetActive (checkeandoCabina);
				controlCamaraInterior.enabled = checkeandoCabina;
				gameObject.SetActive (true);
				deshabilitarCabina();
				print ("ingresando a cabina checklist");
				mensajeInteraccion.text = "";
				mensajeInteraccion.gameObject.SetActive(false);
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
	}

	public void terminarSimulacionFinal(){
		terminarSimulacion (true);
	}

	public void terminarSimulacionParcial(){
		terminarSimulacion (false);
		arreglarMaquina ();
	}

	public void terminarSimulacion(bool mostrarPanelFinal){
		activarLista (false);
		arranque (false);
		puertaHidraulicaActivada = true;
		abrirPuertaHidraulica();
		puertaCabinaActivada = true;
		abrirCabina ();
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

		resultadoSeccion1Label.text = resultadoSeccion1 + "%";
		resultadoSeccion2Label.text = resultadoSeccion2 + "%";
		resultadoSeccion3Label.text = resultadoSeccion3 + "%";
		resultadoSeccion4Label.text = resultadoSeccion4 + "%";
		
		resultadoTotalLabel.text = resultadoTotal + "%";
		tiempo = (int)(Time.timeSinceLevelLoad - tiempo);
		resultadoTiempoLabel.text = Configuracion.calcularReloj(tiempo); 
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
			controlMouseOperador.enabled = false;
			if(diapositivaFinalChecklist != null){
				if(estado == estadoChequeo.exterior) controlMouseOperador.enabled = true;
				else controlMouseOperadorInterior.enabled = true;
				camara.SetActive(false);
				diapositivaFinalChecklist.SetActive(true);
			}
			else{
				terminarSimulacion(true);
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
	}

	public void finalizarModuloChecklist (){
		activar (false);
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
			pantallaTactil.SetActive(false);
		}
	}

	public void activarCamara(){
		camara.SetActive(true);
	}

	public void enviarDatos(){
		configuracion.ResultadoTiempo = Mathf.RoundToInt(tiempo);
		configuracion.ResultadoCheck1 = (int)resultadoTotal;
		/*configuracion.ResultadoRevFunc1 = (int)resultadoSeccion1;
		configuracion.ResultadoRevCab1 = (int)resultadoSeccion2;
		configuracion.ResultadoRevEst1 = (int)resultadoSeccion3;
		configuracion.ResultadoPrevRies1 = (int)resultadoSeccion4;
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
        SceneManager.LoadScene("Login");
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
}


[System.Serializable]
public class ParteMaquina{
	public int indiceSeccion = 0;
	public int indicePregunta = 0;
	public GameObject[] parteBuena;
	public GameObject[] parteMala;
	public bool danado = false;
}