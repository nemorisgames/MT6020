using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour {
    public GameObject maquinaAlta;
    public GameObject maquinaBaja;
    public GameObject camaraEntrada;
    public GameObject controlChecklistGameObject;
    public TableroControl tableroControl;

    public enum EstadoSimulacion { PanelInicial, EncendidoExterior, Conduciendo, Finalizando, ApagadoExterior, Resultados };
	public EstadoSimulacion estado = EstadoSimulacion.PanelInicial;

    [HideInInspector]
    public Configuracion configuracion;
    int tiempoFaena = 90;
    [HideInInspector]
    public float tiempoFaenaActual = -1f;

    public Transform posicionFinal;
    public Transform maquina;
    public Transform maquinaFinal;

    //[HideInInspector]
    public int repeticiones = 1;
    public float tiempoUtilizado;
    public UILabel tiempoFaenaLabel;
    ControlCheckpoints controlCheckpoints;

    public CapturaPeso pesoEnFinal;
    public CapturaPeso pesoEnInicio;
    public CapturaPeso[] pesoIntermedio;
    public float pesoEnEscena = 0f;

    public float integridadTunel = 100f;
    public int cantidadChoquesTunel = 0;
    public int cantidadChoquesZipper = 0;
    public int cantidadChoquesBuzon = 0;
    //public int cantidadChoquesCamioneta = 0;
    public float integridadCamion = 100f;
    public float integridadCamioneta = 100f;

    public bool realizarEntregaNombrada = false;
    public GameObject supervisorModelo;
    
    public GameObject preguntasGUI;
    public GameObject diapositivaSalir;
    public GameObject diapositivaFinal;
    public GameObject diapositivaFinalResumen;
    GameObject diapositivaFalla;
    GameObject diapositivaFallaMensajeMaquina;
    GameObject diapositivaFallaMensajeTunel;
    GameObject diapositivaFallaMensajeVolcado;
    //public ControlChecklist controlChecklistInicial;
    //public ControlChecklist controlChecklistFinal;

    int puntajeEvaluacionCorta = 0;
    int puntajeEvaluacionCorta1 = 0;
    int puntajeEvaluacionCorta2 = 0;
    int puntajeEvaluacionCorta3 = 0;
    int puntajeEvaluacionCorta4 = 0;
    
    Rigidbody maquinaRigidbody;

    public bool pausado = false;
    public GameObject panelAyuda;
    //AVProMovieCaptureFromCamera avpro;
    //int secuenciaGrabacion = 0;


    // Use this for initialization
    void Start ()
    {
        GameObject g = GameObject.FindWithTag("Configuracion");
        configuracion = g.GetComponent<Configuracion>();
        GameObject f = GameObject.Find("checkpoints");
        if (f != null)
            controlCheckpoints = f.GetComponent<ControlCheckpoints>();

        if (configuracion != null)
        {
            tiempoFaena = configuracion.TiempoFaena * 60;
            if (controlCheckpoints != null) controlCheckpoints.nVueltasObjetivo = configuracion.CantidadVueltas;
        }
        GameObject maq = GameObject.FindWithTag("Maquina");
        //if (maq != null) maquinaRigidbody = maq.transform.FindChild("Back").gameObject.GetComponent<Rigidbody>();
        if (preguntasGUI != null) preguntasGUI.SetActive(false);
    
        configuracion.ResultadoTerminoFaena = "Si";

        if (diapositivaFinalResumen != null && diapositivaFinalResumen.transform.parent.FindChild("DiapositivaFalla") != null)
        {
            diapositivaFalla = diapositivaFinalResumen.transform.parent.FindChild("DiapositivaFalla").gameObject;
            diapositivaFallaMensajeMaquina = diapositivaFinalResumen.transform.parent.FindChild("DiapositivaFalla/Maquina").gameObject;
            diapositivaFallaMensajeTunel = diapositivaFinalResumen.transform.parent.FindChild("DiapositivaFalla/Tunel").gameObject;
            diapositivaFallaMensajeVolcado = diapositivaFinalResumen.transform.parent.FindChild("DiapositivaFalla/Volcamiento").gameObject;
            diapositivaFalla.SetActive(false);
        }
        //if (controlChecklistFinal != null)
        //	controlChecklistFinal.activar (false);
        StartCoroutine(contarRepeticiones());
        /*print ("Displays: " + Display.displays.Length);
		for (int i = 0; i < 6; i++)
        {
            Display.displays[i].Activate();
        }
        Display.displays[0].SetParams(1366, 768, 0, 0);
        //if (Display.displays.Length > 1) 
			Display.displays[1].SetParams(1920, 1080, 0, 0);
        //if (Display.displays.Length > 2) 
			Display.displays[2].SetParams(1920, 1080, 0, 0);
        //if (Display.displays.Length > 3) 
			Display.displays[3].SetParams(1920, 1080, 0, 0);
        //if (Display.displays.Length > 4) 
			Display.displays[4].SetParams(800, 480, 0, 0);
        //if (Display.displays.Length > 5) 
			Display.displays[5].SetParams(1920, 1080, 0, 0);

        string[] names = Input.GetJoystickNames();
        Debug.Log("Connected Joysticks:");
        for (int i = 0; i < names.Length; i++) {
            Debug.Log("Joystick" + (i + 1) + " = " + names[i]);
        }
        */
        activarMaquinaAlta(true);
		print("pantallas: " + Display.displays.Length);
		for (int i = 0; i < Display.displays.Length; i++)
		{
			Display.displays[i].Activate();
		}
		Display.displays[0].SetParams(1366, 768, 0, 0);
		if (Display.displays.Length > 1) 
		Display.displays[1].SetParams(1920, 1080, 0, 0);
		if (Display.displays.Length > 2) 
		Display.displays[2].SetParams(1920, 1080, 0, 0);
		if (Display.displays.Length > 3) 
		Display.displays[3].SetParams(1920, 1080, 0, 0);
		if (Display.displays.Length > 4) 
		Display.displays[4].SetParams(800, 480, 0, 0);
		if (Display.displays.Length > 5) 
		Display.displays[5].SetParams(1920, 1080, 0, 0);
    }

    void activarMaquinaAlta(bool activar)
    {
        maquinaAlta.SetActive(activar);
        maquinaBaja.SetActive(!activar);
        camaraEntrada.SetActive(false);
        controlChecklistGameObject.SetActive(activar);
    }

    public void ejecutarEntradaMaquina(bool ingresar)
    {
        print("entrando");
        StartCoroutine(ejecutarEntradaMaquinaDelay(ingresar));
    }


    public IEnumerator ejecutarEntradaMaquinaDelay(bool ingresar) { 
        yield return new WaitForSeconds(ingresar?10f:0f);
        print(ingresar?"entrar":"salir");
        activarMaquinaAlta(!ingresar);
    }

    public void cambiarEstado(EstadoSimulacion e)
    {
        estado = e;
    }

    IEnumerator contarRepeticiones()
    {
        WWWForm form = new WWWForm();
        form.AddField("numeroNivel", configuracion.NumeroModulo);
        form.AddField("idAlumno", configuracion.alumno);
        print("numeroNivel " + configuracion.NumeroModulo);
        print("idAlumno " + configuracion.alumno);
        //print (VariablesGlobales.direccion + "SimuladorLHD/login" + (operador ? "Usuario" : "") + ".php");
        WWW download = new WWW(VariablesGlobales.direccion + "SimuladorLHD/repeticionesModulo.php", form);
        yield return download;
        if (download.error != null)
        {
            print("Error downloading: " + download.error);
            //mostrarError("Error de conexion");
        }
        else
        {
            string retorno = download.text;
            print("repeticiones = " + retorno);
            if (retorno != "")
            {
                repeticiones = int.Parse(retorno);
            }

        }

    }


    public void mostrarPanelSalir()
    {
        diapositivaSalir.SetActive(true);
        //maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
        //controlMouseOperador.enabled = true;
    }

    public void salir()
    {
        diapositivaSalir.SetActive(false);
        //maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(true);
        //controlMouseOperador.enabled = false;
    }

    public void salirCancelar()
    {
        diapositivaSalir.SetActive(false);
        //maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(true);
        //controlMouseOperador.enabled = false;
    }

    public void mostrarChecklistFinal()
    {
        //controlChecklistFinal.controlUsuarioChecklist.realizarChecklist = true;
        //controlChecklistFinal.activar(true);
    }

    public void mostrarPanelFinal()
    {
        
        diapositivaFinal.SetActive(true);
        //controlMouseOperador.enabled = true;
		maquina.SendMessage("resetMaquina", SendMessageOptions.DontRequireReceiver);
        //maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
    }

    IEnumerator delayPanelFinal()
    {
        yield return new WaitForSeconds(2f);
        //if (maquina != null) maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
        //controlMouseOperador.enabled = true;
        diapositivaFinalResumen.SetActive(true);
        diapositivaFinalResumen.transform.FindChild("NombreOperario").gameObject.GetComponent<UILabel>().text = configuracion.alumno;
        diapositivaFinalResumen.transform.FindChild("TiempoPractica").gameObject.GetComponent<UILabel>().text = calcularReloj(tiempoUtilizado);
        diapositivaFinalResumen.transform.FindChild("Repeticiones").gameObject.GetComponent<UILabel>().text = "" + repeticiones;

        //SceneManager.LoadScene ("Login");
    }

    public void simulacionFinalizar()
    {
		//caso en el que nunca se encendió la maquina
		if ((estado == EstadoSimulacion.EncendidoExterior && (maquina == null || !maquina.gameObject.active))|| estado == EstadoSimulacion.PanelInicial) {
			print ("nunca se encendio");
			moduloFinalizar ();
			return;
		}

		if (estado == EstadoSimulacion.ApagadoExterior) {
			return;
		}
        //controlMouseOperador.enabled = false;
        print("finalizar");
		estado = EstadoSimulacion.ApagadoExterior;
        //test 
        Time.timeScale = 0f;
		maquina.FindChild("Trasero_B").gameObject.SetActive(maquina.FindChild("Trasero_B").gameObject.activeSelf);
		maquina.FindChild("Delantera_B").gameObject.SetActive(maquina.FindChild("Delantera_B").gameObject.activeSelf);
        //maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(true);
        maquinaFinal.gameObject.SetActive(maquinaFinal.gameObject.activeSelf);
        diapositivaFinal.SetActive(false);
        Time.timeScale = 1f;
    }

    public void moduloFinalizar()
    {
        //if ((controlChecklistFinal == null || controlChecklistFinal.activa) || (!controlChecklistFinal.activa && controlChecklistFinal.controlUsuarioChecklist.realizarChecklist))
        //{
            print("finalizar modulo");
            StartCoroutine(delayPanelFinal());
        /*}
        else
        {
            print("finalizar modulo generar falla");
            controlChecklistFinal.generarFallas();
            controlChecklistFinal.activar(true);
            controlExterior.GetComponent<ControlUsuarioChecklist>().controlChecklist = controlChecklistFinal.GetComponent<ControlChecklist>();
            controlExterior.GetComponent<ControlUsuarioChecklist>().realizarChecklist = true;
            controlChecklistFinal.controlCamaraInterior.controlChecklist = controlChecklistFinal;
        }
        */
    }

    public void pausaToggle()
    {
        pausar(!pausado);
    }

    void pausar(bool p)
    {
        pausado = p;
        Time.timeScale = pausado ? 0f : 1f;
        panelAyuda.SetActive(pausado);
        //controlMouseOperador.enabled = pausado;
        maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(!pausado);
    }

    public void salirSimulacion()
    {
        if (estado != EstadoSimulacion.EncendidoExterior && estado != EstadoSimulacion.PanelInicial)
        {
            ControlCamion controlExcavadora = maquina.gameObject.GetComponent<ControlCamion>();
            //configuracion.ResultadoOrdenEjecucion = (controlExcavadora.ordenEjecucionCorrecta ? "Si" : "No");
            //configuracion.ResultadoOrdenEjecTiempo = Mathf.RoundToInt(controlExcavadora.tiempoOrdenEjecucion);
            //if (avanceEnBalde && controlCheckpoints != null)
            //    configuracion.ResultadoBaldePunta = (controlCheckpoints.indiceActual >= 1 || controlCheckpoints.nVueltas > 0) ? "Si" : "No";
            //if (!avanceEnBalde && controlCheckpoints != null)
            //    configuracion.ResultadoMotorPunta = (controlCheckpoints.indiceActual >= 1 || controlCheckpoints.nVueltas > 0) ? "Si" : "No";
            //se almacena en segundos
            if (tiempoUtilizado <= 0) tiempoUtilizado = Time.time - tiempoFaenaActual;
            configuracion.ResultadoTiempo = Mathf.RoundToInt(tiempoUtilizado);

            if (controlCheckpoints != null)
            {
                configuracion.ResultadoVueltasRealizadas = controlCheckpoints.nVueltas;
                configuracion.vuelta = new ArrayList();
                configuracion.vuelta = controlCheckpoints.tiempos;
            }
            if (controlCheckpoints != null && pesoEnInicio != null && pesoEnFinal != null)
            {
                float pesoCaido = 0;
                foreach (CapturaPeso c in pesoIntermedio)
                    pesoCaido += (c.enCarga);
                configuracion.ResultadoTonelajeTotal = Mathf.RoundToInt(pesoEnFinal.acumulado + pesoCaido);//Mathf.RoundToInt (pesoEnFinal.enCarga + (pesoEnEscena - (pesoEnFinal.enCarga + pesoEnInicio.enCarga)));
				configuracion.ResultadoPorcentajeCaidaMat = Mathf.RoundToInt((100f * pesoCaido) / (1f * configuracion.ResultadoTonelajeTotal)); //%

                //configuracion.ResultadoCantidadCamion = Mathf.RoundToInt(cantidadChoquesCamion / 2);
            }
            if (controlCheckpoints != null && pesoEnInicio != null && pesoEnFinal != null)
                configuracion.ResultadoCorrectoCargio = Mathf.RoundToInt(100f * pesoEnFinal.acumulado / configuracion.ResultadoTonelajeTotal);

            configuracion.ResultadoIntFrontal = Mathf.RoundToInt(controlExcavadora.integridadFrontal);
            configuracion.ResultadoIntMotorDer = Mathf.RoundToInt(controlExcavadora.integridadMotorDer);
            configuracion.ResultadoIntMotorIzq = Mathf.RoundToInt(controlExcavadora.integridadMotorIzq);
            configuracion.ResultadoIntTolvaDer = Mathf.RoundToInt(controlExcavadora.integridadTolvaDer);
            configuracion.ResultadoIntTolvaIzq = Mathf.RoundToInt(controlExcavadora.integridadTolvaIzq);
            //configuracion.ResultadoIntPostIzq = Mathf.RoundToInt(controlExcavadora.integridadPosteriorIzquierdo);
            configuracion.ResultadoIntMaquina = Mathf.RoundToInt((configuracion.ResultadoIntFrontal + configuracion.ResultadoIntMotorDer + configuracion.ResultadoIntMotorIzq + configuracion.ResultadoIntTolvaDer + configuracion.ResultadoIntTolvaIzq) / 5f);
            configuracion.ResultadoIntTunel = Mathf.RoundToInt(integridadTunel);
            //configuracion.ResultadoCantidadTunel = Mathf.RoundToInt(cantidadChoquesTunel / 2);
            //configuracion.ResultadoZipper = Mathf.RoundToInt(cantidadChoquesZipper / 2);
            //print("porcentaje zipper " + Mathf.Clamp(Mathf.RoundToInt((100f * (cantidadChoquesZipper / 200f))), 0, 100));
            //configuracion.ResultadoCantidadZipper = Mathf.Clamp(Mathf.RoundToInt((100f * (cantidadChoquesZipper / 200f))), 0, 100);
            configuracion.CantidadPreguntas = 4;

            GameObject gcap = GameObject.Find("CapturadorCarguio");
            if (gcap != null)
            {
                //configuracion.cicloCarguio = gcap.GetComponent<CapturadorCarguio>().ciclosCarguio;
            }

            /*if (controlChecklistInicial != null)
            {
                configuracion.ResultadoCheck1 = (int)controlChecklistInicial.resultadoTotal;
                configuracion.ResultadoRevFunc1 = (int)controlChecklistInicial.resultadoSeccion1;
                configuracion.ResultadoRevCab1 = (int)controlChecklistInicial.resultadoSeccion2;
                configuracion.ResultadoRevEst1 = (int)controlChecklistInicial.resultadoSeccion3;
                configuracion.ResultadoPrevRies1 = (int)controlChecklistInicial.resultadoSeccion4;

                configuracion.CheckRFNivPet = controlChecklistInicial.partesMaquina[19].danado ? -1 : 1;
                configuracion.ResultadoCheckRFNivPet = ((controlChecklistInicial.partesMaquina[19].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[19].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[19].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[19].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivAceMot = controlChecklistInicial.partesMaquina[26].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivAceMot = ((controlChecklistInicial.partesMaquina[26].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[26].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[26].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[26].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivAceHid = controlChecklistInicial.partesMaquina[27].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivAceHid = ((controlChecklistInicial.partesMaquina[27].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[27].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[27].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[27].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFEstLuc = controlChecklistInicial.partesMaquina[3].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFEstLuc = ((controlChecklistInicial.partesMaquina[3].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[3].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFEstNeu = controlChecklistInicial.partesMaquina[1].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFEstNeu = ((controlChecklistInicial.partesMaquina[1].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[1].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[1].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[1].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivRef = controlChecklistInicial.partesMaquina[20].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivRef = ((controlChecklistInicial.partesMaquina[20].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[20].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[20].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[20].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivAceTra = controlChecklistInicial.partesMaquina[21].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivAceTra = ((controlChecklistInicial.partesMaquina[21].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[21].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[21].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[21].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREBal = controlChecklistInicial.partesMaquina[5].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREBal = ((controlChecklistInicial.partesMaquina[5].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[5].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[5].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[5].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREAnt = controlChecklistInicial.partesMaquina[4].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREAnt = ((controlChecklistInicial.partesMaquina[4].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[4].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[4].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[4].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREArtCen = controlChecklistInicial.partesMaquina[8].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREArtCen = ((controlChecklistInicial.partesMaquina[8].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[8].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[8].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[8].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREPasGen = controlChecklistInicial.partesMaquina[15].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREPasGen = ((controlChecklistInicial.partesMaquina[15].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[15].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[15].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[15].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREFug = controlChecklistInicial.partesMaquina[14].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREFug = ((controlChecklistInicial.partesMaquina[14].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[14].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[14].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[14].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCLimPar = controlChecklistInicial.partesMaquina[10].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCLimPar = ((controlChecklistInicial.partesMaquina[10].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[10].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[10].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[10].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCCab = controlChecklistInicial.partesMaquina[6].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCCab = ((controlChecklistInicial.partesMaquina[6].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[6].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[6].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[6].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCMan = controlChecklistInicial.partesMaquina[13].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCMan = ((controlChecklistInicial.partesMaquina[13].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[13].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[13].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[13].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCLucGen = controlChecklistInicial.partesMaquina[12].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCLucGen = ((controlChecklistInicial.partesMaquina[12].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[12].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[12].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[12].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCMonDis = controlChecklistInicial.partesMaquina[22].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCMonDis = ((controlChecklistInicial.partesMaquina[22].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[22].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[22].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[22].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCAseCab = controlChecklistInicial.partesMaquina[11].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCAseCab = ((controlChecklistInicial.partesMaquina[11].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[11].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[11].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[11].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCBoc = controlChecklistInicial.partesMaquina[16].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCBoc = ((controlChecklistInicial.partesMaquina[16].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[16].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[16].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[16].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPREstExtMan = controlChecklistInicial.partesMaquina[2].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPREstExtMan = ((controlChecklistInicial.partesMaquina[2].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[2].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[2].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[2].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPREstExtInc = controlChecklistInicial.partesMaquina[3].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPREstExtInc = ((controlChecklistInicial.partesMaquina[20].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[3].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPREstEsc = controlChecklistInicial.partesMaquina[9].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPREstEsc = ((controlChecklistInicial.partesMaquina[9].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[9].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[9].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[9].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPRSalEme = controlChecklistInicial.partesMaquina[18].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPRSalEme = ((controlChecklistInicial.partesMaquina[18].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[18].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[18].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[18].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPRSenMov = controlChecklistInicial.partesMaquina[23].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPRSenMov = ((controlChecklistInicial.partesMaquina[23].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[23].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[23].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[23].indicePregunta] == 1)) ? 1 : -1;

                configuracion.CheckRCTemAceMot = controlChecklistInicial.partesMaquina[24].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCTemAceMot = ((controlChecklistInicial.partesMaquina[24].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[24].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[24].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[24].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCTemAceTra = controlChecklistInicial.partesMaquina[25].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCTemAceTra = ((controlChecklistInicial.partesMaquina[25].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[25].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[25].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[25].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCVen = controlChecklistInicial.partesMaquina[0].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCVen = ((controlChecklistInicial.partesMaquina[0].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[0].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[0].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[0].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCJoy = controlChecklistInicial.partesMaquina[28].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCJoy = ((controlChecklistInicial.partesMaquina[28].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[28].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[28].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[28].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCPed = controlChecklistInicial.partesMaquina[29].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCPed = ((controlChecklistInicial.partesMaquina[29].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[29].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[29].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[29].indicePregunta] == 1)) ? 1 : -1;

            }

            if (controlChecklistFinal != null)
            {
                configuracion.ResultadoCheck2 = (int)controlChecklistFinal.resultadoTotal;
                configuracion.ResultadoRevFunc2 = (int)controlChecklistFinal.resultadoSeccion1;
                configuracion.ResultadoRevCab2 = (int)controlChecklistFinal.resultadoSeccion2;
                configuracion.ResultadoRevEst2 = (int)controlChecklistFinal.resultadoSeccion3;
                configuracion.ResultadoPrevRies2 = (int)controlChecklistFinal.resultadoSeccion4;
                print("checklist final almacenado " + (int)controlChecklistFinal.resultadoSeccion1);

                configuracion.CheckRFNivPet2 = controlChecklistFinal.partesMaquina[19].danado ? -1 : 1;
                configuracion.ResultadoCheckRFNivPet2 = ((controlChecklistFinal.partesMaquina[19].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[19].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[19].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[19].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivAceMot2 = controlChecklistFinal.partesMaquina[26].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivAceMot2 = ((controlChecklistFinal.partesMaquina[26].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[26].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[26].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[26].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivAceHid2 = controlChecklistFinal.partesMaquina[27].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivAceHid2 = ((controlChecklistFinal.partesMaquina[27].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[27].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[27].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[27].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFEstLuc2 = controlChecklistFinal.partesMaquina[3].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFEstLuc2 = ((controlChecklistFinal.partesMaquina[3].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[3].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFEstNeu2 = controlChecklistFinal.partesMaquina[1].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFEstNeu2 = ((controlChecklistFinal.partesMaquina[1].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[1].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[1].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[1].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivRef2 = controlChecklistFinal.partesMaquina[20].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivRef2 = ((controlChecklistFinal.partesMaquina[20].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[20].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[20].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[20].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRFNivAceTra2 = controlChecklistFinal.partesMaquina[21].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRFNivAceTra2 = ((controlChecklistFinal.partesMaquina[21].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[21].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[21].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[21].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREBal2 = controlChecklistFinal.partesMaquina[5].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREBal2 = ((controlChecklistFinal.partesMaquina[5].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[5].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[5].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[5].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREAnt2 = controlChecklistFinal.partesMaquina[4].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREAnt2 = ((controlChecklistFinal.partesMaquina[4].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[4].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[4].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[4].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREArtCen2 = controlChecklistFinal.partesMaquina[8].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREArtCen2 = ((controlChecklistFinal.partesMaquina[8].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[8].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[8].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[8].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREPasGen2 = controlChecklistFinal.partesMaquina[15].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREPasGen2 = ((controlChecklistFinal.partesMaquina[15].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[15].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[15].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[15].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckREFug2 = controlChecklistFinal.partesMaquina[14].danado ? -1 : 1; ;
                configuracion.ResultadoCheckREFug2 = ((controlChecklistFinal.partesMaquina[14].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[14].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[14].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[14].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCLimPar2 = controlChecklistFinal.partesMaquina[10].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCLimPar2 = ((controlChecklistFinal.partesMaquina[10].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[10].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[10].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[10].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCCab2 = controlChecklistFinal.partesMaquina[6].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCCab2 = ((controlChecklistFinal.partesMaquina[6].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[6].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[6].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[6].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCMan2 = controlChecklistFinal.partesMaquina[13].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCMan2 = ((controlChecklistFinal.partesMaquina[13].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[13].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[13].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[13].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCLucGen2 = controlChecklistFinal.partesMaquina[12].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCLucGen2 = ((controlChecklistFinal.partesMaquina[12].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[12].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[12].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[12].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCMonDis2 = controlChecklistFinal.partesMaquina[22].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCMonDis2 = ((controlChecklistFinal.partesMaquina[22].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[22].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[22].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[22].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCAseCab2 = controlChecklistFinal.partesMaquina[11].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCAseCab2 = ((controlChecklistFinal.partesMaquina[11].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[11].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[11].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[11].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCBoc2 = controlChecklistFinal.partesMaquina[16].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCBoc2 = ((controlChecklistFinal.partesMaquina[16].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[16].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[16].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[16].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPREstExtMan2 = controlChecklistFinal.partesMaquina[2].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPREstExtMan2 = ((controlChecklistFinal.partesMaquina[2].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[2].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[2].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[2].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPREstExtInc2 = controlChecklistFinal.partesMaquina[3].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPREstExtInc2 = ((controlChecklistFinal.partesMaquina[20].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[3].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPREstEsc2 = controlChecklistFinal.partesMaquina[9].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPREstEsc2 = ((controlChecklistFinal.partesMaquina[9].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[9].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[9].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[9].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPRSalEme2 = controlChecklistFinal.partesMaquina[18].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPRSalEme2 = ((controlChecklistFinal.partesMaquina[18].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[18].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[18].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[18].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckPRSenMov2 = controlChecklistFinal.partesMaquina[23].danado ? -1 : 1; ;
                configuracion.ResultadoCheckPRSenMov2 = ((controlChecklistFinal.partesMaquina[23].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[23].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[23].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[23].indicePregunta] == 1)) ? 1 : -1;

                configuracion.CheckRCTemAceMot2 = controlChecklistFinal.partesMaquina[24].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCTemAceMot2 = ((controlChecklistFinal.partesMaquina[24].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[24].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[24].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[24].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCTemAceTra2 = controlChecklistFinal.partesMaquina[25].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCTemAceTra2 = ((controlChecklistFinal.partesMaquina[25].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[25].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[25].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[25].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCVen2 = controlChecklistFinal.partesMaquina[0].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCVen2 = ((controlChecklistFinal.partesMaquina[0].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[0].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[0].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[0].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCJoy2 = controlChecklistFinal.partesMaquina[28].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCJoy2 = ((controlChecklistFinal.partesMaquina[28].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[28].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[28].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[28].indicePregunta] == 1)) ? 1 : -1;
                configuracion.CheckRCPed2 = controlChecklistFinal.partesMaquina[29].danado ? -1 : 1; ;
                configuracion.ResultadoCheckRCPed2 = ((controlChecklistFinal.partesMaquina[29].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[29].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[29].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[29].indicePregunta] == 1)) ? 1 : -1;

            }
            if (SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18")
            {
                configuracion.ResultadoEntregaNombrada = "Si";
                configuracion.ResultadoEntregaNombradaSup = "Si";

                configuracion.ResultadoPreguntasCorta1 = puntajeEvaluacionCorta1;
                configuracion.ResultadoPreguntasCorta2 = puntajeEvaluacionCorta2;
                configuracion.ResultadoPreguntasCorta3 = puntajeEvaluacionCorta3;
                configuracion.ResultadoPreguntasCorta4 = puntajeEvaluacionCorta4;
                configuracion.ResultadoPreguntas = puntajeEvaluacionCorta;
                //if(SceneManager.GetActiveScene().name == "Modulo16") configuracion.ResultadoIntCamion = Mathf.CeilToInt(camioneta.integridadActual);
                //else 
                configuracion.ResultadoIntCamion = Mathf.Clamp(Mathf.CeilToInt(integridadCamion), 0, 100);
                print("integridad " + integridadCamion + " " + configuracion.ResultadoIntCamion);
            }
            */
            configuracion.guardarHistorial();
        }
		else
			SceneManager.LoadScene("Login");
        //gameObject.SendMessage ("apagarLeds");
    }

    public void iniciarSimulacion()
    {
        //		print ("iniciarSimulacion");
        /*if (controlExterior == null || (((controlChecklistFinal == null || (controlChecklistFinal != null && controlChecklistFinal.activa)) && (estado != EstadoSimulacion.PanelInicial && estado != EstadoSimulacion.Conduciendo && estado != EstadoSimulacion.EncendidoExterior)) || (controlChecklistFinal != null && !controlChecklistFinal.activa && estado == EstadoSimulacion.ApagadoExterior)))
        {
            moduloFinalizar();
        }
        else
        {*/
            //controlExterior.SetActive(true);
            
                estado = EstadoSimulacion.EncendidoExterior;
                iniciarTiempo();
        //}

    }

    public void iniciarTiempo()
    {
        if (tiempoFaenaActual < 0)
            tiempoFaenaActual = Time.time;
    }

    public void conduciendo()
    {
        if (estado != EstadoSimulacion.Finalizando && estado != EstadoSimulacion.ApagadoExterior)
        {
            estado = EstadoSimulacion.Conduciendo;
            //if (camioneta != null) camioneta.activar();
        }
    }

    public void salirCabina()
    {
        if (estado == EstadoSimulacion.Finalizando || estado == EstadoSimulacion.ApagadoExterior)
            estado = EstadoSimulacion.ApagadoExterior;
        else
            estado = EstadoSimulacion.EncendidoExterior;
    }

    public void setCalidadBaja()
    {
        QualitySettings.SetQualityLevel(0, true);
    }

    public void setCalidadMedia()
    {
        QualitySettings.SetQualityLevel(2, true);
    }

    public void setCalidadAlta()
    {
        QualitySettings.SetQualityLevel(5, true);
    }

    public void reset()
    {
        //gameObject.SendMessage ("apagarLeds");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void  Update()
    {
        /*if (avpro != null && avpro.GetCaptureFileSize() > 10000000)
        {
            string nombreVideo = avpro._forceFilename;
            avpro.StopCapture();
            if (secuenciaGrabacion > 0)
            {
                if (secuenciaGrabacion > 9)
                {
                    if (secuenciaGrabacion > 99)
                        avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 8) + "_" + secuenciaGrabacion + ".avi";
                    else
                        avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 7) + "_" + secuenciaGrabacion + ".avi";
                }
                else
                {
                    avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 6) + "_" + secuenciaGrabacion + ".avi";
                }
            }
            else
                avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 4) + "_" + secuenciaGrabacion + ".avi";
            print("grabando " + avpro._forceFilename);
            avpro.StartCapture();
            secuenciaGrabacion++;
        }*/
        if (estado == EstadoSimulacion.Finalizando || estado == EstadoSimulacion.ApagadoExterior)
            return;
        /*if (shake_intensity > 0f)
        {
            camaraInterior.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
            camaraInterior.localRotation = new Quaternion(
                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .1f,
                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .1f,
                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .1f,
                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .1f);
            shake_intensity -= shake_decay;
        }*/
        if (estado == EstadoSimulacion.EncendidoExterior || estado == EstadoSimulacion.Conduciendo || tiempoFaenaActual >= 0f)
        {
            tiempoFaenaLabel.text = "" + calcularReloj(tiempoFaena * 1f - Time.time + tiempoFaenaActual);
            if (tiempoFaena * 1f - Time.time + tiempoFaenaActual <= 0f)
            {
                condicionesTerminoListas();
            }
        }
        

        if (integridadCamion < configuracion.IntBuzonCarga)
        {
            fallaOperacion(ControlCamion.LugarMaquina.Buzon);
        }
    }

    public void condicionesTerminoListas()
    {
        //if (diapositivaFalla == null || diapositivaFalla.activeSelf || estado == EstadoSimulacion.Finalizando || estado == EstadoSimulacion.ApagadoExterior)
        //    return;
        if (estado != EstadoSimulacion.EncendidoExterior && estado != EstadoSimulacion.PanelInicial)
        {
            tiempoUtilizado = Time.time - tiempoFaenaActual;
            estado = EstadoSimulacion.Finalizando;
            mostrarPanelFinal();
			maquina.FindChild("Trasero_B").localPosition = new Vector3(0.0002759445f, 0.7498303f, 0f);
			maquina.FindChild("Delantera_B").localPosition = new Vector3(0.0002759445f, 0.7498303f, 0f);
			maquina.FindChild("Trasero_B").localRotation = Quaternion.identity;
			maquina.FindChild("Delantera_B").localRotation = Quaternion.identity;
            maquina.position = posicionFinal.position;
            maquina.rotation = posicionFinal.rotation;
            maquinaFinal.position = posicionFinal.position;
            maquinaFinal.rotation = posicionFinal.rotation;
        }
        //else
        //    SceneManager.LoadScene("Login");
    }

    string calcularReloj(float tiempo)
    {
        int minutos = (int)tiempo / 60;
        int segundos = (int)tiempo % 60;
        return ((minutos < 10) ? ("0" + minutos) : "" + minutos) + ":" + ((segundos < 10) ? ("0" + segundos) : "" + segundos);
    }

    public void fallaOperacion(ControlCamion.LugarMaquina lugarFalla)
    {
        print("falla " + lugarFalla.ToString());
        maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
        ControlCamion c = maquina.GetComponent<ControlCamion>();
        c.estado = ControlCamion.EstadoMaquina.apagadaTotal;
        Time.timeScale = 0f;
        //controlMouseOperador.enabled = true;
        diapositivaFalla.SetActive(true);
        switch (lugarFalla)
        {
            case ControlCamion.LugarMaquina.Frontal:
            case ControlCamion.LugarMaquina.MotorDer:
            case ControlCamion.LugarMaquina.MotorIzq:
            case ControlCamion.LugarMaquina.TolvaDer:
            case ControlCamion.LugarMaquina.TolvaIzq:
                diapositivaFallaMensajeMaquina.SetActive(true);
                diapositivaFallaMensajeTunel.SetActive(false);
                diapositivaFallaMensajeVolcado.SetActive(false);
                configuracion.fallaOperacion = "1";
                break;
            case ControlCamion.LugarMaquina.Tunel:
                diapositivaFallaMensajeMaquina.SetActive(false);
                diapositivaFallaMensajeTunel.SetActive(true);
                diapositivaFallaMensajeVolcado.SetActive(false);
                configuracion.fallaOperacion = "1";
                break;
            default:
                diapositivaFallaMensajeMaquina.SetActive(false);
                diapositivaFallaMensajeTunel.SetActive(false);
                diapositivaFallaMensajeVolcado.SetActive(true);
                if (lugarFalla == ControlCamion.LugarMaquina.Buzon)
                {
                    configuracion.fallaOperacion = "3";
                }
                break;
        }

        configuracion.ResultadoTerminoFaena = "No";
    }

    public void choqueTunel()
    {
        print("tunel chocado ");
        cantidadChoquesTunel++;
        integridadTunel -= configuracion.DescuentoTunel;
        if (integridadTunel < configuracion.IntTunel)
            fallaOperacion(ControlCamion.LugarMaquina.Tunel);
    }


    /*public Transform camaraInterior;
	Vector3 originPosition;
	Quaternion originRotation;
	float shake_decay;
	float shake_intensity;
	public GameObject controlExterior;
	public GameObject diapositivaFinal;
	public GameObject diapositivaFinalResumen;
	GameObject diapositivaFalla;
	GameObject diapositivaFallaMensajeMaquina;
	GameObject diapositivaFallaMensajeTunel;
	GameObject diapositivaFallaMensajeVolcado;
    [HideInInspector]
	public Configuracion configuracion;
	int tiempoFaena = 90;
	[HideInInspector]
	public float tiempoFaenaActual = -1f;
	//[HideInInspector]
	public int repeticiones = 1;
	public float tiempoUtilizado;
	public UILabel tiempoFaenaLabel;

	public Transform posicionFinal;
	public Transform maquina;
	public Transform maquinaFinal;

	public bool pausado = false;
	public GameObject panelAyuda;
	ControlMouseOperador controlMouseOperador;

	public enum EstadoSimulacion {PanelInicial, EncendidoExterior, Conduciendo, Finalizando, ApagadoExterior, Resultados};
	public EstadoSimulacion estado = EstadoSimulacion.PanelInicial;

	public bool avanceEnBalde = true;
	public bool verificarAvance = false;
	public TweenAlpha mensajeDireccionErroneaBalde;
	public TweenAlpha mensajeDireccionErroneaMotor;
	ControlCheckpoints controlCheckpoints;

	public CapturaPeso pesoEnFinal;
	public CapturaPeso pesoEnInicio;
	public CapturaPeso[] pesoIntermedio;
	public float pesoEnEscena = 0f;

	public float integridadTunel = 100f;
	public int cantidadChoquesTunel = 0;
	public int cantidadChoquesZipper = 0;
	public int cantidadChoquesCamion = 0;
    public int cantidadChoquesCamioneta = 0;
    public float integridadCamion = 100f;
    public float integridadCamioneta = 100f;

    public bool realizarEntregaNombrada = false;
	public EntregaNombrada supervisor;
	public GameObject supervisorModelo;

	public Camioneta camioneta;
	public GameObject preguntasGUI;
	public GameObject diapositivaSalir;

	public ControlChecklist controlChecklistInicial;
	public ControlChecklist controlChecklistFinal;

	int puntajeEvaluacionCorta = 0;
    int puntajeEvaluacionCorta1 = 0;
    int puntajeEvaluacionCorta2 = 0;
    int puntajeEvaluacionCorta3 = 0;
    int puntajeEvaluacionCorta4 = 0;

    public Light luzAmbiente;

	Rigidbody maquinaRigidbody;
	float tiempoComprobacionDireccion = 0f;

	AVProMovieCaptureFromCamera avpro;
    int secuenciaGrabacion = 0;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 30;
		GameObject c = GameObject.Find ("CameraGrabacion");
		if(c != null)
			avpro = c.GetComponent<AVProMovieCaptureFromCamera> ();
		setCalidadAlta ();
		GameObject[] rocas = GameObject.FindGameObjectsWithTag ("Roca");
		foreach (GameObject g1 in rocas) {
			pesoEnEscena += g1.GetComponent<Rigidbody>().mass * 0.001f;
		}
		if (diapositivaFinalResumen != null && diapositivaFinalResumen.transform.parent.FindChild ("DiapositivaFalla") != null) {
			diapositivaFalla = diapositivaFinalResumen.transform.parent.FindChild ("DiapositivaFalla").gameObject;
			diapositivaFallaMensajeMaquina = diapositivaFinalResumen.transform.parent.FindChild ("DiapositivaFalla/Maquina").gameObject;
			diapositivaFallaMensajeTunel = diapositivaFinalResumen.transform.parent.FindChild ("DiapositivaFalla/Tunel").gameObject;
			diapositivaFallaMensajeVolcado = diapositivaFinalResumen.transform.parent.FindChild ("DiapositivaFalla/Volcamiento").gameObject;
			diapositivaFalla.SetActive (false);
		}
		Time.timeScale = 1f;
		//gameObject.SendMessage ("apagarLeds");
		GameObject f = GameObject.Find ("checkpoints");
		if(f != null)
			controlCheckpoints = f.GetComponent<ControlCheckpoints> ();
		controlMouseOperador = gameObject.GetComponent<ControlMouseOperador> ();
		GameObject g = GameObject.FindWithTag ("Configuracion");
		if (g != null)
			configuracion = g.GetComponent<Configuracion> ();
		if (configuracion != null) {
			tiempoFaena = configuracion.TiempoFaena * 60;
			if(controlCheckpoints != null) controlCheckpoints.nVueltasObjetivo = configuracion.CantidadVueltas;
		}
		GameObject maq = GameObject.FindWithTag ("Maquina");
		if(maq != null) maquinaRigidbody = maq.transform.FindChild ("Back").gameObject.GetComponent<Rigidbody> ();

		if (camaraInterior != null) {
			originPosition = camaraInterior.localPosition;
			originRotation = camaraInterior.localRotation;
		}
		//Screen.SetResolution(6400, 720, false);
		Screen.SetResolution(9600, 1080, false);
		if(preguntasGUI != null) preguntasGUI.SetActive (false);

		controlExterior.SetActive (false);

		configuracion.ResultadoTerminoFaena = "Si";
		//if (controlChecklistFinal != null)
		//	controlChecklistFinal.activar (false);
		StartCoroutine (contarRepeticiones ());
	}

	public void grabar(){
        secuenciaGrabacion = 0;
        System.DateTime fecha = System.DateTime.Now;
		avpro._outputFolderPath = "Movies\\"+configuracion.usuario+"\\"+configuracion.alumno+"\\"+"Modulo" + configuracion.NumeroModulo;
		string f = fecha.ToString ().Replace(":", ".").Replace("/", ".");
		avpro._forceFilename =  f + ".avi";
		avpro.StartCapture ();
	}

	public void stop(){
		avpro.StopCapture ();
	}

	public void pause(){
		if(avpro.IsPaused())
			avpro.ResumeCapture ();
		else 
			avpro.PauseCapture ();
	}

	IEnumerator contarRepeticiones(){
		WWWForm form = new WWWForm();
		form.AddField( "numeroNivel", configuracion.NumeroModulo );
		form.AddField( "idAlumno", configuracion.alumno );
		print ("numeroNivel " + configuracion.NumeroModulo);
		print ("idAlumno " + configuracion.alumno);
		//print (VariablesGlobales.direccion + "SimuladorLHD/login" + (operador ? "Usuario" : "") + ".php");
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorLHD/repeticionesModulo.php", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			return false;
		} else {
			string retorno = download.text;
			print ("repeticiones = " + retorno);
			if(retorno != ""){
				repeticiones = int.Parse(retorno);
}
			
		}

	}

	public void mostrarPreguntasGUI()
{
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
    pausar(true);
    controlMouseOperador.enabled = false;
    if (preguntasGUI != null) preguntasGUI.SetActive(true);
}

public void preguntasTerminadas(int puntaje, int pregunta1, int pregunta2, int pregunta3, int pregunta4)
{
    ControlExcavadora controlExcavadora = maquina.gameObject.GetComponent<ControlExcavadora>();
    controlExcavadora.advertenciaAmarillaAdelante(false);
    controlExcavadora.advertenciaAmarillaAtras(false);
    controlExcavadora.advertenciaRojaAdelante(false);
    controlExcavadora.advertenciaRojaAtras(false);
    puntajeEvaluacionCorta1 = pregunta1;
    puntajeEvaluacionCorta2 = pregunta2;
    puntajeEvaluacionCorta3 = pregunta3;
    puntajeEvaluacionCorta4 = pregunta4;
    puntajeEvaluacionCorta = puntaje;
    if (controlChecklistFinal == null || controlChecklistFinal.activa)
    {
        moduloFinalizar();
    }
    else
    {
        maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(true);
        pausar(false);
        controlMouseOperador.enabled = false;
        if (preguntasGUI != null)
            preguntasGUI.SetActive(false);
    }
}

public void mostrarMensajeDireccionErronea()
{
    print("direccion erronea");
    if (avanceEnBalde)
    {
        mensajeDireccionErroneaBalde.ResetToBeginning();
        mensajeDireccionErroneaBalde.PlayForward();
    }
    else
    {
        mensajeDireccionErroneaMotor.ResetToBeginning();
        mensajeDireccionErroneaMotor.PlayForward();
    }
}

public void mostrarPanelSalir()
{
    diapositivaSalir.SetActive(true);
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
    controlMouseOperador.enabled = true;
}

public void salir()
{
    diapositivaSalir.SetActive(false);
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(true);
    controlMouseOperador.enabled = false;
}

public void salirCancelar()
{
    diapositivaSalir.SetActive(false);
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(true);
    controlMouseOperador.enabled = false;
}

public void mostrarChecklistFinal()
{
    controlChecklistFinal.controlUsuarioChecklist.realizarChecklist = true;
    controlChecklistFinal.activar(true);
}

public void mostrarPanelFinal()
{

    //test Time.timeScale = 0f;
    diapositivaFinal.SetActive(true);
    controlMouseOperador.enabled = true;
    maquina.SendMessage("resetMaquina");
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
    //maquinaFinal.gameObject.SetActive (false);
    //maquina.FindChild ("Back").gameObject.SetActive (false);
    //maquina.FindChild ("Front").gameObject.SetActive (false);
}

IEnumerator delayPanelFinal()
{
    yield return new WaitForSeconds(2f);
    if (maquina != null) maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
    controlMouseOperador.enabled = true;
    diapositivaFinalResumen.SetActive(true);
    diapositivaFinalResumen.transform.FindChild("NombreOperario").gameObject.GetComponent<UILabel>().text = configuracion.alumno;
    diapositivaFinalResumen.transform.FindChild("TiempoPractica").gameObject.GetComponent<UILabel>().text = calcularReloj(tiempoUtilizado);
    diapositivaFinalResumen.transform.FindChild("Repeticiones").gameObject.GetComponent<UILabel>().text = "" + repeticiones;

    //SceneManager.LoadScene ("Login");
}

public void simulacionFinalizar()
{
    if (estado == EstadoSimulacion.ApagadoExterior)
        return;
    controlMouseOperador.enabled = false;
    print("finalizar");
    //test 
    Time.timeScale = 0f;
    maquina.FindChild("Back").gameObject.SetActive(maquina.FindChild("Back").gameObject.activeSelf);
    maquina.FindChild("Front").gameObject.SetActive(maquina.FindChild("Front").gameObject.activeSelf);
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(true);
    maquinaFinal.gameObject.SetActive(maquinaFinal.gameObject.activeSelf);
    diapositivaFinal.SetActive(false);
    Time.timeScale = 1f;
    //SceneManager.LoadScene ("Login");
}

public void moduloFinalizar()
{
    if ((controlChecklistFinal == null || controlChecklistFinal.activa) || (!controlChecklistFinal.activa && controlChecklistFinal.controlUsuarioChecklist.realizarChecklist))
    {
        print("finalizar modulo");
        StartCoroutine(delayPanelFinal());
    }
    else
    {
        print("finalizar modulo generar falla");
        controlChecklistFinal.generarFallas();
        controlChecklistFinal.activar(true);
        controlExterior.GetComponent<ControlUsuarioChecklist>().controlChecklist = controlChecklistFinal.GetComponent<ControlChecklist>();
        controlExterior.GetComponent<ControlUsuarioChecklist>().realizarChecklist = true;
        controlChecklistFinal.controlCamaraInterior.controlChecklist = controlChecklistFinal;
    }

}

public void pausaToggle()
{
    pausar(!pausado);
}

void pausar(bool p)
{
    pausado = p;
    Time.timeScale = pausado ? 0f : 1f;
    panelAyuda.SetActive(pausado);
    controlMouseOperador.enabled = pausado;
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(!pausado);
    //maquina.FindChild ("Front").gameObject.SetActive (!pausado);
}

public void salirSimulacion()
{
    if (estado != EstadoSimulacion.EncendidoExterior && estado != EstadoSimulacion.PanelInicial)
    {
        ControlExcavadora controlExcavadora = maquina.gameObject.GetComponent<ControlExcavadora>();
        configuracion.ResultadoOrdenEjecucion = (controlExcavadora.ordenEjecucionCorrecta ? "Si" : "No");
        configuracion.ResultadoOrdenEjecTiempo = Mathf.RoundToInt(controlExcavadora.tiempoOrdenEjecucion);
        if (avanceEnBalde && controlCheckpoints != null)
            configuracion.ResultadoBaldePunta = (controlCheckpoints.indiceActual >= 1 || controlCheckpoints.nVueltas > 0) ? "Si" : "No";
        if (!avanceEnBalde && controlCheckpoints != null)
            configuracion.ResultadoMotorPunta = (controlCheckpoints.indiceActual >= 1 || controlCheckpoints.nVueltas > 0) ? "Si" : "No";
        //se almacena en segundos
        if (tiempoUtilizado <= 0) tiempoUtilizado = Time.time - tiempoFaenaActual;
        configuracion.ResultadoTiempo = Mathf.RoundToInt(tiempoUtilizado);

        if (controlCheckpoints != null)
        {
            configuracion.ResultadoVueltasRealizadas = controlCheckpoints.nVueltas;
            configuracion.vuelta = new ArrayList();
            configuracion.vuelta = controlCheckpoints.tiempos;
        }
        if (controlCheckpoints != null && pesoEnInicio != null && pesoEnFinal != null)
        {
            float pesoCaido = 0;
            foreach (CapturaPeso c in pesoIntermedio)
                pesoCaido += (c.enCarga);
            configuracion.ResultadoTonelajeTotal = Mathf.RoundToInt(pesoEnFinal.acumulado + pesoCaido);//Mathf.RoundToInt (pesoEnFinal.enCarga + (pesoEnEscena - (pesoEnFinal.enCarga + pesoEnInicio.enCarga)));
            configuracion.ResultadoCaidaMat = Mathf.RoundToInt((100f * pesoCaido) / (1f * configuracion.ResultadoTonelajeTotal)); //%

            configuracion.ResultadoCantidadCamion = Mathf.RoundToInt(cantidadChoquesCamion / 2);
        }
        if (controlCheckpoints != null && pesoEnInicio != null && pesoEnFinal != null)
            configuracion.ResultadoCorrectoCargio = Mathf.RoundToInt(100f * pesoEnFinal.acumulado / configuracion.ResultadoTonelajeTotal);

        configuracion.ResultadoIntBrazo = Mathf.RoundToInt(controlExcavadora.integridadBrazo);
        configuracion.ResultadoIntCabina = Mathf.RoundToInt(controlExcavadora.integridadCabina);
        configuracion.ResultadoIntMedioDer = Mathf.RoundToInt(controlExcavadora.integridadMedioDerecho);
        configuracion.ResultadoIntPostDer = Mathf.RoundToInt(controlExcavadora.integridadPosteriorDerecho);
        configuracion.ResultadoIntPosterior = Mathf.RoundToInt(controlExcavadora.integridadPosterior);
        configuracion.ResultadoIntPostIzq = Mathf.RoundToInt(controlExcavadora.integridadPosteriorIzquierdo);
        configuracion.ResultadoIntMaquina = Mathf.RoundToInt((configuracion.ResultadoIntBrazo + configuracion.ResultadoIntCabina + configuracion.ResultadoIntMedioDer + configuracion.ResultadoIntPostDer + configuracion.ResultadoIntPostIzq) / 5f);
        configuracion.ResultadoIntTunel = Mathf.RoundToInt(integridadTunel);
        configuracion.ResultadoCantidadTunel = Mathf.RoundToInt(cantidadChoquesTunel / 2);
        configuracion.ResultadoZipper = Mathf.RoundToInt(cantidadChoquesZipper / 2);
        print("porcentaje zipper " + Mathf.Clamp(Mathf.RoundToInt((100f * (cantidadChoquesZipper / 200f))), 0, 100));
        configuracion.ResultadoCantidadZipper = Mathf.Clamp(Mathf.RoundToInt((100f * (cantidadChoquesZipper / 200f))), 0, 100);
        configuracion.CantidadPreguntas = 4;

        GameObject gcap = GameObject.Find("CapturadorCarguio");
        if (gcap != null)
        {
            configuracion.cicloCarguio = gcap.GetComponent<CapturadorCarguio>().ciclosCarguio;
            //print("n ciclos carguio " + gcap.GetComponent<CapturadorCarguio>().ciclosCarguio.Count);
        }

        if (controlChecklistInicial != null)
        {
            configuracion.ResultadoCheck1 = (int)controlChecklistInicial.resultadoTotal;
            configuracion.ResultadoRevFunc1 = (int)controlChecklistInicial.resultadoSeccion1;
            configuracion.ResultadoRevCab1 = (int)controlChecklistInicial.resultadoSeccion2;
            configuracion.ResultadoRevEst1 = (int)controlChecklistInicial.resultadoSeccion3;
            configuracion.ResultadoPrevRies1 = (int)controlChecklistInicial.resultadoSeccion4;

            configuracion.CheckRFNivPet = controlChecklistInicial.partesMaquina[19].danado ? -1 : 1;
            configuracion.ResultadoCheckRFNivPet = ((controlChecklistInicial.partesMaquina[19].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[19].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[19].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[19].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivAceMot = controlChecklistInicial.partesMaquina[26].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivAceMot = ((controlChecklistInicial.partesMaquina[26].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[26].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[26].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[26].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivAceHid = controlChecklistInicial.partesMaquina[27].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivAceHid = ((controlChecklistInicial.partesMaquina[27].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[27].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[27].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[27].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFEstLuc = controlChecklistInicial.partesMaquina[3].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFEstLuc = ((controlChecklistInicial.partesMaquina[3].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[3].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFEstNeu = controlChecklistInicial.partesMaquina[1].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFEstNeu = ((controlChecklistInicial.partesMaquina[1].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[1].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[1].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[1].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivRef = controlChecklistInicial.partesMaquina[20].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivRef = ((controlChecklistInicial.partesMaquina[20].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[20].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[20].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[20].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivAceTra = controlChecklistInicial.partesMaquina[21].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivAceTra = ((controlChecklistInicial.partesMaquina[21].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[21].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[21].danado && controlChecklistInicial.checklistLista.respuestas1[controlChecklistInicial.partesMaquina[21].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREBal = controlChecklistInicial.partesMaquina[5].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREBal = ((controlChecklistInicial.partesMaquina[5].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[5].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[5].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[5].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREAnt = controlChecklistInicial.partesMaquina[4].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREAnt = ((controlChecklistInicial.partesMaquina[4].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[4].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[4].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[4].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREArtCen = controlChecklistInicial.partesMaquina[8].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREArtCen = ((controlChecklistInicial.partesMaquina[8].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[8].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[8].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[8].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREPasGen = controlChecklistInicial.partesMaquina[15].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREPasGen = ((controlChecklistInicial.partesMaquina[15].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[15].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[15].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[15].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREFug = controlChecklistInicial.partesMaquina[14].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREFug = ((controlChecklistInicial.partesMaquina[14].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[14].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[14].danado && controlChecklistInicial.checklistLista.respuestas2[controlChecklistInicial.partesMaquina[14].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCLimPar = controlChecklistInicial.partesMaquina[10].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCLimPar = ((controlChecklistInicial.partesMaquina[10].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[10].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[10].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[10].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCCab = controlChecklistInicial.partesMaquina[6].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCCab = ((controlChecklistInicial.partesMaquina[6].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[6].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[6].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[6].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCMan = controlChecklistInicial.partesMaquina[13].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCMan = ((controlChecklistInicial.partesMaquina[13].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[13].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[13].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[13].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCLucGen = controlChecklistInicial.partesMaquina[12].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCLucGen = ((controlChecklistInicial.partesMaquina[12].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[12].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[12].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[12].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCMonDis = controlChecklistInicial.partesMaquina[22].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCMonDis = ((controlChecklistInicial.partesMaquina[22].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[22].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[22].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[22].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCAseCab = controlChecklistInicial.partesMaquina[11].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCAseCab = ((controlChecklistInicial.partesMaquina[11].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[11].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[11].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[11].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCBoc = controlChecklistInicial.partesMaquina[16].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCBoc = ((controlChecklistInicial.partesMaquina[16].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[16].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[16].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[16].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPREstExtMan = controlChecklistInicial.partesMaquina[2].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPREstExtMan = ((controlChecklistInicial.partesMaquina[2].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[2].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[2].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[2].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPREstExtInc = controlChecklistInicial.partesMaquina[3].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPREstExtInc = ((controlChecklistInicial.partesMaquina[20].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[3].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPREstEsc = controlChecklistInicial.partesMaquina[9].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPREstEsc = ((controlChecklistInicial.partesMaquina[9].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[9].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[9].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[9].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPRSalEme = controlChecklistInicial.partesMaquina[18].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPRSalEme = ((controlChecklistInicial.partesMaquina[18].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[18].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[18].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[18].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPRSenMov = controlChecklistInicial.partesMaquina[23].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPRSenMov = ((controlChecklistInicial.partesMaquina[23].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[23].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[23].danado && controlChecklistInicial.checklistLista.respuestas4[controlChecklistInicial.partesMaquina[23].indicePregunta] == 1)) ? 1 : -1;

            configuracion.CheckRCTemAceMot = controlChecklistInicial.partesMaquina[24].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCTemAceMot = ((controlChecklistInicial.partesMaquina[24].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[24].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[24].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[24].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCTemAceTra = controlChecklistInicial.partesMaquina[25].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCTemAceTra = ((controlChecklistInicial.partesMaquina[25].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[25].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[25].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[25].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCVen = controlChecklistInicial.partesMaquina[0].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCVen = ((controlChecklistInicial.partesMaquina[0].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[0].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[0].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[0].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCJoy = controlChecklistInicial.partesMaquina[28].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCJoy = ((controlChecklistInicial.partesMaquina[28].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[28].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[28].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[28].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCPed = controlChecklistInicial.partesMaquina[29].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCPed = ((controlChecklistInicial.partesMaquina[29].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[29].indicePregunta] == 2) || (!controlChecklistInicial.partesMaquina[29].danado && controlChecklistInicial.checklistLista.respuestas3[controlChecklistInicial.partesMaquina[29].indicePregunta] == 1)) ? 1 : -1;

        }

        if (controlChecklistFinal != null)
        {
            configuracion.ResultadoCheck2 = (int)controlChecklistFinal.resultadoTotal;
            configuracion.ResultadoRevFunc2 = (int)controlChecklistFinal.resultadoSeccion1;
            configuracion.ResultadoRevCab2 = (int)controlChecklistFinal.resultadoSeccion2;
            configuracion.ResultadoRevEst2 = (int)controlChecklistFinal.resultadoSeccion3;
            configuracion.ResultadoPrevRies2 = (int)controlChecklistFinal.resultadoSeccion4;
            print("checklist final almacenado " + (int)controlChecklistFinal.resultadoSeccion1);

            configuracion.CheckRFNivPet2 = controlChecklistFinal.partesMaquina[19].danado ? -1 : 1;
            configuracion.ResultadoCheckRFNivPet2 = ((controlChecklistFinal.partesMaquina[19].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[19].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[19].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[19].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivAceMot2 = controlChecklistFinal.partesMaquina[26].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivAceMot2 = ((controlChecklistFinal.partesMaquina[26].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[26].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[26].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[26].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivAceHid2 = controlChecklistFinal.partesMaquina[27].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivAceHid2 = ((controlChecklistFinal.partesMaquina[27].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[27].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[27].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[27].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFEstLuc2 = controlChecklistFinal.partesMaquina[3].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFEstLuc2 = ((controlChecklistFinal.partesMaquina[3].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[3].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFEstNeu2 = controlChecklistFinal.partesMaquina[1].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFEstNeu2 = ((controlChecklistFinal.partesMaquina[1].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[1].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[1].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[1].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivRef2 = controlChecklistFinal.partesMaquina[20].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivRef2 = ((controlChecklistFinal.partesMaquina[20].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[20].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[20].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[20].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRFNivAceTra2 = controlChecklistFinal.partesMaquina[21].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRFNivAceTra2 = ((controlChecklistFinal.partesMaquina[21].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[21].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[21].danado && controlChecklistFinal.checklistLista.respuestas1[controlChecklistFinal.partesMaquina[21].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREBal2 = controlChecklistFinal.partesMaquina[5].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREBal2 = ((controlChecklistFinal.partesMaquina[5].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[5].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[5].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[5].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREAnt2 = controlChecklistFinal.partesMaquina[4].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREAnt2 = ((controlChecklistFinal.partesMaquina[4].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[4].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[4].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[4].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREArtCen2 = controlChecklistFinal.partesMaquina[8].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREArtCen2 = ((controlChecklistFinal.partesMaquina[8].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[8].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[8].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[8].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREPasGen2 = controlChecklistFinal.partesMaquina[15].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREPasGen2 = ((controlChecklistFinal.partesMaquina[15].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[15].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[15].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[15].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckREFug2 = controlChecklistFinal.partesMaquina[14].danado ? -1 : 1; ;
            configuracion.ResultadoCheckREFug2 = ((controlChecklistFinal.partesMaquina[14].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[14].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[14].danado && controlChecklistFinal.checklistLista.respuestas2[controlChecklistFinal.partesMaquina[14].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCLimPar2 = controlChecklistFinal.partesMaquina[10].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCLimPar2 = ((controlChecklistFinal.partesMaquina[10].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[10].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[10].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[10].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCCab2 = controlChecklistFinal.partesMaquina[6].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCCab2 = ((controlChecklistFinal.partesMaquina[6].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[6].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[6].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[6].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCMan2 = controlChecklistFinal.partesMaquina[13].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCMan2 = ((controlChecklistFinal.partesMaquina[13].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[13].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[13].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[13].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCLucGen2 = controlChecklistFinal.partesMaquina[12].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCLucGen2 = ((controlChecklistFinal.partesMaquina[12].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[12].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[12].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[12].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCMonDis2 = controlChecklistFinal.partesMaquina[22].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCMonDis2 = ((controlChecklistFinal.partesMaquina[22].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[22].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[22].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[22].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCAseCab2 = controlChecklistFinal.partesMaquina[11].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCAseCab2 = ((controlChecklistFinal.partesMaquina[11].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[11].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[11].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[11].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCBoc2 = controlChecklistFinal.partesMaquina[16].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCBoc2 = ((controlChecklistFinal.partesMaquina[16].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[16].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[16].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[16].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPREstExtMan2 = controlChecklistFinal.partesMaquina[2].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPREstExtMan2 = ((controlChecklistFinal.partesMaquina[2].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[2].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[2].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[2].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPREstExtInc2 = controlChecklistFinal.partesMaquina[3].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPREstExtInc2 = ((controlChecklistFinal.partesMaquina[20].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[3].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[3].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[3].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPREstEsc2 = controlChecklistFinal.partesMaquina[9].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPREstEsc2 = ((controlChecklistFinal.partesMaquina[9].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[9].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[9].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[9].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPRSalEme2 = controlChecklistFinal.partesMaquina[18].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPRSalEme2 = ((controlChecklistFinal.partesMaquina[18].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[18].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[18].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[18].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckPRSenMov2 = controlChecklistFinal.partesMaquina[23].danado ? -1 : 1; ;
            configuracion.ResultadoCheckPRSenMov2 = ((controlChecklistFinal.partesMaquina[23].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[23].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[23].danado && controlChecklistFinal.checklistLista.respuestas4[controlChecklistFinal.partesMaquina[23].indicePregunta] == 1)) ? 1 : -1;

            configuracion.CheckRCTemAceMot2 = controlChecklistFinal.partesMaquina[24].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCTemAceMot2 = ((controlChecklistFinal.partesMaquina[24].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[24].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[24].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[24].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCTemAceTra2 = controlChecklistFinal.partesMaquina[25].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCTemAceTra2 = ((controlChecklistFinal.partesMaquina[25].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[25].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[25].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[25].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCVen2 = controlChecklistFinal.partesMaquina[0].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCVen2 = ((controlChecklistFinal.partesMaquina[0].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[0].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[0].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[0].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCJoy2 = controlChecklistFinal.partesMaquina[28].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCJoy2 = ((controlChecklistFinal.partesMaquina[28].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[28].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[28].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[28].indicePregunta] == 1)) ? 1 : -1;
            configuracion.CheckRCPed2 = controlChecklistFinal.partesMaquina[29].danado ? -1 : 1; ;
            configuracion.ResultadoCheckRCPed2 = ((controlChecklistFinal.partesMaquina[29].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[29].indicePregunta] == 2) || (!controlChecklistFinal.partesMaquina[29].danado && controlChecklistFinal.checklistLista.respuestas3[controlChecklistFinal.partesMaquina[29].indicePregunta] == 1)) ? 1 : -1;

        }

        if (SceneManager.GetActiveScene().name == "Modulo16" || SceneManager.GetActiveScene().name == "Modulo17" || SceneManager.GetActiveScene().name == "Modulo18")
        {
            configuracion.ResultadoEntregaNombrada = "Si";
            configuracion.ResultadoEntregaNombradaSup = "Si";

            configuracion.ResultadoPreguntasCorta1 = puntajeEvaluacionCorta1;
            configuracion.ResultadoPreguntasCorta2 = puntajeEvaluacionCorta2;
            configuracion.ResultadoPreguntasCorta3 = puntajeEvaluacionCorta3;
            configuracion.ResultadoPreguntasCorta4 = puntajeEvaluacionCorta4;
            configuracion.ResultadoPreguntas = puntajeEvaluacionCorta;
            //if(SceneManager.GetActiveScene().name == "Modulo16") configuracion.ResultadoIntCamion = Mathf.CeilToInt(camioneta.integridadActual);
            //else 
            configuracion.ResultadoIntCamion = Mathf.Clamp(Mathf.CeilToInt(integridadCamion), 0, 100);
            print("integridad " + integridadCamion + " " + configuracion.ResultadoIntCamion);
        }
        configuracion.guardarHistorial();
    }
    //gameObject.SendMessage ("apagarLeds");
    SceneManager.LoadScene("Login");
}

public void iniciarSimulacion()
{
    //		print ("iniciarSimulacion");
    if (controlExterior == null || (((controlChecklistFinal == null || (controlChecklistFinal != null && controlChecklistFinal.activa)) && (estado != EstadoSimulacion.PanelInicial && estado != EstadoSimulacion.Conduciendo && estado != EstadoSimulacion.EncendidoExterior)) || (controlChecklistFinal != null && !controlChecklistFinal.activa && estado == EstadoSimulacion.ApagadoExterior)))
    {
        moduloFinalizar();
    }
    else
    {
        //			print ("iniciarSimulacion final null");
        controlExterior.SetActive(true);
        gameObject.GetComponent<ControlMouseOperador>().enabled = false;
        if (realizarEntregaNombrada)
        {
            supervisor.activar(true);
            supervisorModelo.SetActive(true);
            //operador.activar(true);
        }
        else
        {
            estado = EstadoSimulacion.EncendidoExterior;
            iniciarTiempo();
        }
    }

}

public void iniciarTiempo()
{
    if (tiempoFaenaActual < 0)
        tiempoFaenaActual = Time.time;
}

public void conduciendo()
{
    if (estado != EstadoSimulacion.Finalizando && estado != EstadoSimulacion.ApagadoExterior)
    {
        estado = EstadoSimulacion.Conduciendo;
        if (camioneta != null) camioneta.activar();
    }
}

public void salirCabina()
{
    if (estado == EstadoSimulacion.Finalizando || estado == EstadoSimulacion.ApagadoExterior)
        estado = EstadoSimulacion.ApagadoExterior;
    else
        estado = EstadoSimulacion.EncendidoExterior;
}

public void setCalidadBaja()
{
    QualitySettings.SetQualityLevel(0, true);
}

public void setCalidadMedia()
{
    QualitySettings.SetQualityLevel(2, true);
}

public void setCalidadAlta()
{
    QualitySettings.SetQualityLevel(5, true);
}

public void reset()
{
    //gameObject.SendMessage ("apagarLeds");
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

void Update()
{
    if (avpro != null && avpro.GetCaptureFileSize() > 10000000)
    {
        string nombreVideo = avpro._forceFilename;
        avpro.StopCapture();
        if (secuenciaGrabacion > 0)
        {
            if (secuenciaGrabacion > 9)
            {
                if (secuenciaGrabacion > 99)
                    avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 8) + "_" + secuenciaGrabacion + ".avi";
                else
                    avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 7) + "_" + secuenciaGrabacion + ".avi";
            }
            else
            {
                avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 6) + "_" + secuenciaGrabacion + ".avi";
            }
        }
        else
            avpro._forceFilename = nombreVideo.Substring(0, nombreVideo.Length - 4) + "_" + secuenciaGrabacion + ".avi";
        print("grabando " + avpro._forceFilename);
        avpro.StartCapture();
        secuenciaGrabacion++;
    }
    if (estado == EstadoSimulacion.Finalizando || estado == EstadoSimulacion.ApagadoExterior)
        return;
    if (shake_intensity > 0f)
    {
        camaraInterior.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
        camaraInterior.localRotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .1f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .1f,
            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .1f,
            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .1f);
        shake_intensity -= shake_decay;
    }
    if (estado == EstadoSimulacion.EncendidoExterior || estado == EstadoSimulacion.Conduciendo || tiempoFaenaActual >= 0f)
    {
        tiempoFaenaLabel.text = "" + calcularReloj(tiempoFaena * 1f - Time.time + tiempoFaenaActual);
        if (tiempoFaena * 1f - Time.time + tiempoFaenaActual <= 0f)
        {
            condicionesTerminoListas();
        }
    }
    if (verificarAvance && Time.time > tiempoComprobacionDireccion)
    {
        tiempoComprobacionDireccion = Time.time + 3f;
        print(maquinaRigidbody.velocity.magnitude);
        if (maquinaRigidbody.velocity.magnitude > 1f)
        {
            print("vel " + maquinaRigidbody.transform.InverseTransformDirection(maquinaRigidbody.velocity).z);
            if (avanceEnBalde && maquinaRigidbody.transform.InverseTransformDirection(maquinaRigidbody.velocity).z > 0f)
            {
            }
            else
            {
                if (!avanceEnBalde && maquinaRigidbody.transform.InverseTransformDirection(maquinaRigidbody.velocity).z < 0f)
                {
                }
                else
                    mostrarMensajeDireccionErronea();
            }
        }
    }

    if (integridadCamion < configuracion.IntCamion)
    {
        fallaOperacion(ControlExcavadora.LugarMaquina.Camion);
    }
    if (integridadCamioneta < configuracion.IntCamioneta)
    {
        fallaOperacion(ControlExcavadora.LugarMaquina.Camioneta);
    }
}

public void condicionesTerminoListas()
{
    if (diapositivaFalla.activeSelf || estado == EstadoSimulacion.Finalizando || estado == EstadoSimulacion.ApagadoExterior)
        return;
    if (estado != EstadoSimulacion.EncendidoExterior && estado != EstadoSimulacion.PanelInicial)
    {
        tiempoUtilizado = Time.time - tiempoFaenaActual;
        estado = EstadoSimulacion.Finalizando;
        mostrarPanelFinal();
        maquina.FindChild("Back").localPosition = new Vector3(0.0002759445f, 0.7498303f, 0f);
        maquina.FindChild("Front").localPosition = new Vector3(0.0002759445f, 0.7498303f, 0f);
        maquina.FindChild("Back").localRotation = Quaternion.identity;
        maquina.FindChild("Front").localRotation = Quaternion.identity;
        maquina.position = posicionFinal.position;
        maquina.rotation = posicionFinal.rotation;
        maquinaFinal.position = posicionFinal.position;
        maquinaFinal.rotation = posicionFinal.rotation;
    }
    else
        SceneManager.LoadScene("Login");
}

string calcularReloj(float tiempo)
{
    int minutos = (int)tiempo / 60;
    int segundos = (int)tiempo % 60;
    return ((minutos < 10) ? ("0" + minutos) : "" + minutos) + ":" + ((segundos < 10) ? ("0" + segundos) : "" + segundos);
}

public void fallaOperacion(ControlExcavadora.LugarMaquina lugarFalla)
{
    print("falla " + lugarFalla.ToString());
    maquina.FindChild("Back/ST14EstrucBack/Camaras").gameObject.SetActive(false);
    ControlExcavadora c = maquina.GetComponent<ControlExcavadora>();
    c.estado = ControlExcavadora.EstadoMaquina.apagadaTotal;
    Time.timeScale = 0f;
    controlMouseOperador.enabled = true;
    diapositivaFalla.SetActive(true);
    switch (lugarFalla)
    {
        case ControlExcavadora.LugarMaquina.Brazo:
        case ControlExcavadora.LugarMaquina.Cabina:
        case ControlExcavadora.LugarMaquina.MedioDerecho:
        case ControlExcavadora.LugarMaquina.Posterior:
        case ControlExcavadora.LugarMaquina.PosteriorDerecho:
        case ControlExcavadora.LugarMaquina.PosteriorIzquierdo:
            diapositivaFallaMensajeMaquina.SetActive(true);
            diapositivaFallaMensajeTunel.SetActive(false);
            diapositivaFallaMensajeVolcado.SetActive(false);
            configuracion.fallaOperacion = 1;
            break;
        case ControlExcavadora.LugarMaquina.Tunel:
            diapositivaFallaMensajeMaquina.SetActive(false);
            diapositivaFallaMensajeTunel.SetActive(true);
            diapositivaFallaMensajeVolcado.SetActive(false);
            configuracion.fallaOperacion = 1;
            break;
        default:
            diapositivaFallaMensajeMaquina.SetActive(false);
            diapositivaFallaMensajeTunel.SetActive(false);
            diapositivaFallaMensajeVolcado.SetActive(true);
            if (lugarFalla == ControlExcavadora.LugarMaquina.Camion)
            {
                configuracion.fallaOperacion = 3;
            }
            if (lugarFalla == ControlExcavadora.LugarMaquina.Camioneta)
                configuracion.fallaOperacion = 2;
            break;
    }

    configuracion.ResultadoTerminoFaena = "No";
}

public void choqueTunel()
{
    print("tunel chocado ");
    cantidadChoquesTunel++;
    integridadTunel -= configuracion.DescuentoTunel;
    if (integridadTunel < configuracion.IntAreaExtraccion)
        fallaOperacion(ControlExcavadora.LugarMaquina.Tunel);
}

public void Shake(float throttle)
{
    shake_intensity += .00002f * throttle;
    shake_intensity = Mathf.Clamp(shake_intensity, 0.001f, 0.01f);
    //print (shake_intensity);
    shake_decay = 0.0002f;
}
*/
}
