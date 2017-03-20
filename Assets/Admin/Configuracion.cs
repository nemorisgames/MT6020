using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine.SceneManagement;

public class Configuracion : MonoBehaviour {
    public bool testing = true;
    public bool checkMac = false;
	public bool logeado=false;
	public string usuario;
	public string pass;
	public string alumno;
    public string mailInstructor;
    /*configuracion*/
    public string idModulo;
	public string NumeroModulo;
	public int TiempoVuelta;
	public int TiempoFaena;
	public int CantidadVueltas;
	//public int ChoqueZipper;
	public int IntTunel;
	public int IntBuzonCarga;
    //public int IntCamioneta;
    public int IntFrontal;
	public int IntMotorDer;
	public int IntMotorIzq;
	public int IntMedioDer;
	public int IntTolvaDer;
	public int IntTolvaIzq;
	public int ExitoPreguntas;
	public int CantidadPreguntas;
	//public float MinimoCargar;
	//public float MaximoCargar;
	public int TonelajeTotal;
	public int CaidaPermitida;
	public int DescuentoChoque;
	public int DescuentoTunel;
	public int DescuentoBuzonCarga;
    //public int DescuentoCamioneta;
    public int check1;
	public int check2;
	/*resultados*/
	public string Fecha;
    public int ResultadoPreguntasCorta1;
    public int ResultadoRespuestaCorta1;
    public int ResultadoPreguntasCorta2;
    public int ResultadoRespuestaCorta2;
    public int ResultadoPreguntasCorta3;
    public int ResultadoRespuestaCorta3;
    public int ResultadoPreguntasCorta4;
    public int ResultadoRespuestaCorta4;
    public int ResultadoPreguntas;
	public int ResultadoTiempo;
	public int ResultadoCheck1;
	public int ResultadoRevFunc1;
	public int ResultadoRevCab1;
	public int ResultadoRevEst1;
	public int ResultadoPrevRies1;
	public int ResultadoCheck2;
	//public int ResultadoRevFunc2;
	//public int ResultadoRevCab2;
	//public int ResultadoRevEst2;
	//public int ResultadoPrevRies2;
	//public string ResultadoOrdenEjecucion;
	//public string ResultadoMotorPunta;
	//public string ResultadoBaldePunta;
	public int ResultadoVueltasRealizadas;
	public int ResultadoVueltasCorrectas;
	//public string ResultadoEntregaNombrada;
	//public string ResultadoEntregaNombradaSup;
	public int ResultadoTonelajeTotal;
	public int ResultadoPorcentajeCaidaMat;
	public int ResultadoCorrectoCargio;
	//public int ResultadoPatinaje;
	public int ResultadoIntMaquina;
	public int ResultadoIntFrontal;
	public int ResultadoIntMotorDer;
	public int ResultadoIntMotorIzq;
	public int ResultadoIntMedioDer;
	public int ResultadoIntTolvaDer;
	public int ResultadoIntTolvaIzq;
	//public int ResultadoZipper;
	//public int ResultadoCantidadZipper;
	public int ResultadoIntTunel;
    public int ResultadoIntBuzonCarga;
    //public int ResultadoIntCamioneta;
    //public int ResultadoCantidadTunel;
	//public int ResultadoCantidadCamion;
	public string ResultadoTraslado;
    public string ResultadoTerminoFaena;

    public string fallaOperacion;

	//public bool camionConvencionalSeleccionado;
	
	public MatrizDanio[] matrizDanio;
	public string[] macs;

    public int CheckNivPet;
    public int ResultadoCheckNivPet;
    public int CheckNivAceMot;
    public int ResultadoCheckNivAceMot;
    public int CheckNivAceHid;
    public int ResultadoCheckNivAceHid;
    public int CheckEstLuc;
    public int ResultadoCheckEstLuc;
    public int CheckEstNeu;
    public int ResultadoCheckEstNeu;
    public int CheckNivRef;
    public int ResultadoCheckNivRef;
    public int CheckNivAceTra;
    public int ResultadoCheckNivAceTra;
    public int CheckNivAceTransf;
    public int ResultadoCheckNivAceTransf;
    public int CheckFiltro;
    public int ResultadoCheckFiltro;
    public int CheckIndObs;
    public int ResultadoCheckIndObs;
    public int CheckLucGen;
    public int ResultadoCheckLucGen;
    public int CheckLimPar;
    public int ResultadoCheckLimPar;
    public int CheckAirAco;
    public int ResultadoCheckAirAco;
    public int CheckMan;
    public int ResultadoCheckMan;
    public int CheckMon;
    public int ResultadoCheckMon;
    public int CheckAseCab;
    public int ResultadoCheckAseCab;
    public int CheckBoc;
    public int ResultadoCheckBoc;
    public int CheckTol;
    public int ResultadoCheckTol;
    public int CheckTopEjeCen;
    public int ResultadoCheckTopEjeCen;
    public int CheckArtCen;
    public int ResultadoCheckArtCen;
    public int CheckArtDir;
    public int ResultadoCheckArtDir;
    public int CheckPasGen;
    public int ResultadoCheckPasGen;
    public int CheckFugCil;
    public int ResultadoCheckFugCil;
    public int CheckMotEnf;
    public int ResultadoCheckMotEnf;
    public int CheckEstExtMan;
    public int ResultadoCheckEstExtMan;
    public int CheckEstExtInc;
    public int ResultadoCheckEstExtInc;
    public int CheckEstEsc;
    public int ResultadoCheckEstEsc;
    public int CheckSalEme;
    public int ResultadoCheckSalEme;
    public int CheckCheFirCab;
    public int ResultadoCheckCheFirCab;
    public int CheckCabCheFir;
    public int ResultadoCheckCabCheFir;
	public int CheckSistAnsul;
	public int ResultadoCheckSistAnsul;


    public int CheckNivPet2;
    public int ResultadoCheckNivPet2;
    public int CheckNivAceMot2;
    public int ResultadoCheckNivAceMot2;
    public int CheckNivAceHid2;
    public int ResultadoCheckNivAceHid2;
    public int CheckEstLuc2;
    public int ResultadoCheckEstLuc2;
    public int CheckEstNeu2;
    public int ResultadoCheckEstNeu2;
    public int CheckNivRef2;
    public int ResultadoCheckNivRef2;
    public int CheckNivAceTra2;
    public int ResultadoCheckNivAceTra2;
    public int CheckNivAceTransf2;
    public int ResultadoCheckNivAceTransf2;
    public int CheckFiltro2;
    public int ResultadoCheckFiltro2;
    public int CheckIndObs2;
    public int ResultadoCheckIndObs2;
    public int CheckLucGen2;
    public int ResultadoCheckLucGen2;
   // public int CheckFug2;
    //public int ResultadoCheckFug2;
    public int CheckLimPar2;
    public int ResultadoCheckLimPar2;
    public int CheckAirAco2;
    public int ResultadoCheckAirAco2;
    public int CheckEstEsc2;
    public int ResultadoCheckEstEsc2;
    public int CheckMan2;
    public int ResultadoCheckMan2;
    public int CheckMon2;
    public int ResultadoCheckMon2;
    public int CheckBoc2;
    public int ResultadoCheckBoc2;
    public int CheckAseCab2;
    public int ResultadoCheckAseCab2;
    public int CheckTol2;
    public int ResultadoCheckTol2;
    public int CheckTopEjeCen2;
    public int ResultadoCheckTopEjeCen2;
    public int CheckSalEme2;
    public int ResultadoCheckSalEme2;
    public int CheckArtCen2;
    public int ResultadoCheckArtCen2;
    public int CheckArtDir2;
    public int ResultadoCheckArtDir2;
    public int CheckPasGen2;
    public int ResultadoCheckPasGen2;
    public int CheckFugCil2;
    public int ResultadoCheckFugCil2;
    public int CheckMotEnf2;
    public int ResultadoCheckMotEnf2;
    public int CheckEstExtMan2;
    public int ResultadoCheckEstExtMan2;
    public int CheckEstExtInc2;
    public int ResultadoCheckEstExtInc2;
    public int CheckCheFirCab2;
    public int ResultadoCheckCheFirCab2;
    public int CheckPabCheFir2;
    public int ResultadoCheckPabCheFir2;

    public int ResultadoNumPreguntasContestadas;
    //public int ResultadoOrdenEjecTiempo;
    public int ResultadoPuntoPartidaTiempo;

    public ArrayList vuelta = new ArrayList();
    public ArrayList cicloCarguio = new ArrayList();

	public GameObject resultadosAdminGO;
	public GameObject resultadosUserGO;
	UILabel [] resultadosAdmin;
	UILabel [] resultadosUser;

    // Use this for initialization
    void Start ()
    {
        if (GameObject.FindGameObjectsWithTag("Configuracion").Length > 1)
            Destroy(gameObject);
        matrizDanio = new MatrizDanio[2];
        matrizDanio[0] = new MatrizDanio("pared", 0.75f);
        matrizDanio[1] = new MatrizDanio("Obstaculo", 0.1f);
        if (!testing)
        {
            bool encontrado = false;
            foreach (string s in macs)
            {
                if (s == FetchMacId()) encontrado = true;
            }
            if (!encontrado && checkMac) {
                SceneManager.LoadScene ("Pirata");
                return;
            }
            //logeado = true;
            //Screen.SetResolution(6400, 720, false);
            //Screen.SetResolution(9600, 1080, false);
            DontDestroyOnLoad (gameObject);

            PlayerPrefs.SetString("idAdmin", "");

            SceneManager.LoadScene("Login");
        }

		resultadosAdmin = resultadosAdminGO.GetComponentsInChildren<UILabel>();
		resultadosUser = resultadosUserGO.GetComponentsInChildren<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string FetchMacId()
	{
		string macAddresses = "";
		
		foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
		{
			//if (nic.OperationalStatus == OperationalStatus.Up)
			//{
				if(macAddresses == "" && nic.GetPhysicalAddress().ToString() != "")
                    macAddresses = nic.GetPhysicalAddress().ToString();
			//	break;
			//}
		}
        print(macAddresses);
		return macAddresses;
	}

	public void guardarHistorial(){
		StartCoroutine (guardarHistorialEjecutar());
	}

    IEnumerator guardarHistorialEjecutar() {

        /*vuelta.Add(Random.Range(1, 100));
        vuelta.Add(Random.Range(1, 100));
        vuelta.Add(Random.Range(1, 100));
        vuelta.Add(Random.Range(1, 100));
        vuelta.Add(Random.Range(1, 100));

        for (int i = 0; i < 7; i++)
        {
            CicloCarguio c = new CicloCarguio();
            c.numero = i + 1;
            c.carguio = Random.Range(100, 1000);
            c.patinaje = Random.Range(0, 2)==1;
            c.levante = Random.Range(0, 2)==1;
            c.caida = Random.Range(100, 1000);
            c.vaciado = Random.Range(100, 1000);
            c.tiempo = Random.Range(10, 600);
            cicloCarguio.Add(c);
        }*/

		int supervisor = int.Parse(usuario.Substring(usuario.Length-1,1));
        WWWForm form = new WWWForm();
        System.DateTime fecha = System.DateTime.Now;
		string fechaStr = fecha.ToString ().Substring (0, fecha.ToString ().Length - 3);
        //string f = fecha.ToString();
        //f = "Modulo " + NumeroModulo + " " + f;
        print("tiempo empleado " + ResultadoTiempo);
        ///////////////////////
        //public string usuario;
    /*
		//public string pass;
    //public string alumno;
    //public string mailInstructor;
    /*configuracion
    //public string idModulo;
    //public string NumeroModulo;
    //public int TiempoVuelta;
    //public int TiempoFaena;
    //public int CantidadVueltas;
    //public int ChoqueZipper;
    public int IntTunel;
    public int IntBuzonCarga;
    //public int IntCamioneta;
    public int IntFrontal;
    public int IntMotorDer;
    public int IntMotorIzq;
    public int IntMedioDer;
    public int IntTolvaDer;
    public int IntTolvaIzq;
    public int ExitoPreguntas;
    public int CantidadPreguntas;
    //public float MinimoCargar;
    //public float MaximoCargar;
    public int TonelajeTotal;
    public int CaidaPermitida;
    public int DescuentoChoque;
    public int DescuentoTunel;
    public int DescuentoBuzonCarga;
    //public int DescuentoCamioneta;
    public int check1;
    public int check2;
    resultados
    public string Fecha;
    public int ResultadoPreguntasCorta1;
    public int ResultadoRespuestaCorta1;
    public int ResultadoPreguntasCorta2;
    public int ResultadoRespuestaCorta2;
    public int ResultadoPreguntasCorta3;
    public int ResultadoRespuestaCorta3;
    public int ResultadoPreguntasCorta4;
    public int ResultadoRespuestaCorta4;
    public int ResultadoPreguntas;
    public int ResultadoTiempo;
    public int ResultadoCheck1;
    public int ResultadoRevFunc1;
    public int ResultadoRevCab1;
    public int ResultadoRevEst1;
    public int ResultadoPrevRies1;
    public int ResultadoCheck2;
    //public int ResultadoRevFunc2;
    //public int ResultadoRevCab2;
    //public int ResultadoRevEst2;
    //public int ResultadoPrevRies2;
    //public string ResultadoOrdenEjecucion;
    //public string ResultadoMotorPunta;
    //public string ResultadoBaldePunta;
    public int ResultadoVueltasRealizadas;
    public int ResultadoVueltasCorrectas;
    //public string ResultadoEntregaNombrada;
    //public string ResultadoEntregaNombradaSup;
    public int ResultadoTonelajeTotal;
    public int ResultadoPorcentajeCaidaMat;
    public int ResultadoCorrectoCargio;
    //public int ResultadoPatinaje;
    public int ResultadoIntMaquina;
    public int ResultadoIntFrontal;
    public int ResultadoIntMotorDer;
    public int ResultadoIntMotorIzq;
    public int ResultadoIntMedioDer;
    public int ResultadoIntTolvaDer;
    public int ResultadoIntTolvaIzq;
    //public int ResultadoZipper;
    //public int ResultadoCantidadZipper;
    public int ResultadoIntTunel;
    public int ResultadoIntBuzonCarga;
    //public int ResultadoIntCamioneta;
    //public int ResultadoCantidadTunel;
    //public int ResultadoCantidadCamion;
    public string ResultadoTraslado;
    public string ResultadoTerminoFaena;

    public string fallaOperacion;*/
    /// <summary>
    /// ////////////
    /// 
    /// </summary>
		form.AddField("idInstanceModule", idModulo);
		form.AddField("alumno", alumno);
		//form.AddField("supervisor", usuario);
		form.AddField("supervisor",PlayerPrefs.GetString("idAdmin", "1"));
		form.AddField("Fecha", fechaStr); //almacenado
		form.AddField("ResultadoPreguntasCorta1", ResultadoPreguntasCorta1);
		form.AddField("ResultadoRespuestaCorta1", ResultadoRespuestaCorta1);
		form.AddField("ResultadoPreguntasCorta2", ResultadoPreguntasCorta2);
		form.AddField("ResultadoRespuestaCorta2", ResultadoRespuestaCorta2);
		form.AddField("ResultadoPreguntasCorta3", ResultadoPreguntasCorta3);
		form.AddField("ResultadoRespuestaCorta3", ResultadoRespuestaCorta3);
		form.AddField("ResultadoPreguntasCorta4", ResultadoPreguntasCorta4);
		form.AddField("ResultadoRespuestaCorta4", ResultadoRespuestaCorta4);
		form.AddField("ResultadoPreguntas", ResultadoPreguntas); //almacenado
		form.AddField("ResultadoTiempo", ResultadoTiempo); //almacenado
		form.AddField("ResultadoCheck1", ResultadoCheck1); //almacenado
		form.AddField("ResultadoCheck2", ResultadoCheck2); //almacenado
		form.AddField("ResultadoVueltasRealizadas", ResultadoVueltasRealizadas); //almacenado
		form.AddField("ResultadoVueltasCorrectas", ResultadoVueltasCorrectas);
		form.AddField("ResultadoTonelajeTotal", ResultadoTonelajeTotal); //almacenado
		form.AddField("ResultadoPorcentajeCaidaMat", ResultadoPorcentajeCaidaMat); //almacenado
		form.AddField("ResultadoCorrectoCargio", ResultadoCorrectoCargio); //no es necesario(?)

		form.AddField("ResultadoIntMaquina", ResultadoIntMaquina);//almacenado
		form.AddField("ResultadoIntFrontal", ResultadoIntFrontal);//almacenado
		form.AddField("ResultadoIntMotorDer", ResultadoIntMotorDer);//almacenado
		form.AddField("ResultadoIntMotorIzq", ResultadoIntMotorIzq);//almacenado
		form.AddField("ResultadoIntMedioDer", ResultadoIntMedioDer);//almacenado
		form.AddField("ResultadoIntTolvaDer", ResultadoIntTolvaDer);//almacenado
		form.AddField("ResultadoIntTolvaIzq", ResultadoIntTolvaIzq);//almacenado

		form.AddField("ResultadoIntTunel", ResultadoIntTunel);
		form.AddField("ResultadoIntBuzonCarga", ResultadoIntBuzonCarga);
		form.AddField("ResultadoTraslado", ResultadoTraslado);
		form.AddField("ResultadoTerminoFaena", ResultadoTerminoFaena);
    /*form.AddField("Mail", mailInstructor);

    form.AddField("ResultadoFallaOperacion", fallaOperacion);
    form.AddField("Fecha", f); 
    form.AddField("ResultadoPreguntasCorta1", ResultadoPreguntasCorta1);
    form.AddField("ResultadoPreguntasCorta2", ResultadoPreguntasCorta2);
    form.AddField("ResultadoPreguntasCorta3", ResultadoPreguntasCorta3);
    form.AddField("ResultadoPreguntasCorta4", ResultadoPreguntasCorta4);
    form.AddField("IdNivel", idModulo);
    form.AddField("PorPreguntas", ResultadoPreguntas);
    form.AddField("TiempoEmpleado", ResultadoTiempo);
    form.AddField("Check1", ResultadoCheck1);
    form.AddField("revFunc1", ResultadoRevFunc1);
    form.AddField("revEst1", ResultadoRevEst1);
    form.AddField("revCab1", ResultadoRevCab1);
    form.AddField("prevRies1", ResultadoPrevRies1);
    form.AddField("Check2", ResultadoCheck2);
    form.AddField("revFunc2", ResultadoRevFunc2);
    form.AddField("revEst2", ResultadoRevEst2);
    form.AddField("revCab2", ResultadoRevCab2);
    form.AddField("prevRies2", ResultadoPrevRies2);
    form.AddField("OrdenEj", ResultadoOrdenEjecucion);
    form.AddField("MotorPunta", ResultadoMotorPunta);
    form.AddField("BaldePunta", ResultadoBaldePunta);
    form.AddField("VueltasCorrectas", ResultadoVueltasCorrectas); 
    form.AddField("EntregaNombrada", ResultadoEntregaNombrada); 
    form.AddField("EntregaNombradaSup", ResultadoEntregaNombradaSup);

    form.AddField("TonelajeTotal", ResultadoTonelajeTotal); form.AddField("CaidaMat", ResultadoPorcentajeCaidaMat); form.AddField("CorrectoCarguio", ResultadoCorrectoCargio); form.AddField("Patinaje", ResultadoPatinaje);
    form.AddField("IntMaquina", ResultadoIntMaquina); form.AddField("IntBalde", ResultadoIntTolvaIzq); form.AddField("IntTolvaDer", ResultadoIntTolvaDer); form.AddField("IntMedioDer", ResultadoIntMedioDer);
    form.AddField("IntPost", ResultadoIntFrontal); form.AddField("IntMotorIzq", ResultadoIntMotorIzq); form.AddField("IntMotorDer", ResultadoIntMotorDer); form.AddField("Zipper", ResultadoZipper);
    form.AddField("CantZipper", ResultadoCantidadZipper); form.AddField("Tunel", ResultadoIntTunel); form.AddField("CantTunel", ResultadoCantidadTunel);

    form.AddField("CamionMin", IntBuzonCarga); form.AddField("CamionDes", DescuentoBuzonCarga); form.AddField("Camion", ResultadoIntBuzonCarga);
    form.AddField("CamionetaMin", IntCamioneta); form.AddField("CamionetaDes", DescuentoCamioneta); form.AddField("Camioneta", ResultadoIntCamioneta);

    form.AddField("CantCamion", ResultadoCantidadCamion); form.AddField("Traslado", ResultadoTraslado); form.AddField("CantVueltas", ResultadoVueltasRealizadas); form.AddField("TerminoFaena", ResultadoTerminoFaena);
    form.AddField("idAlumno", alumno);*/

    //if (NumeroModulo == "4" || NumeroModulo == "16" || NumeroModulo == "17" || NumeroModulo == "18")
	if (NumeroModulo == "Módulo 4" || NumeroModulo == "Módulo 16" || NumeroModulo == "Módulo 17" || NumeroModulo == "Módulo 18")
    {
        form.AddField("ResultadoCheckNivPet", ResultadoCheckNivPet);
        form.AddField("ResultadoCheckNivAceMot", ResultadoCheckNivAceMot);
        form.AddField("ResultadoCheckNivAceHid", ResultadoCheckNivAceHid);
        form.AddField("ResultadoCheckEstLuc", ResultadoCheckEstLuc);
        form.AddField("ResultadoCheckEstNeu", ResultadoCheckEstNeu);
        //form.AddField("ResultadoCheckEstNeu", ResultadoCheckEstNeu);
        form.AddField("ResultadoCheckNivRef", ResultadoCheckNivRef);
        form.AddField("ResultadoCheckNivAceTra", ResultadoCheckNivAceTra);
        form.AddField("ResultadoCheckNivAceTransf", ResultadoCheckNivAceTransf);
        form.AddField("ResultadoCheckFiltro", ResultadoCheckFiltro);
        form.AddField("ResultadoCheckIndObs", ResultadoCheckIndObs);
        form.AddField("ResultadoCheckLucGen", ResultadoCheckLucGen);
        //form.AddField("ResultadoCheckFug", ResultadoCheckFug);
        form.AddField("ResultadoCheckLimPar", ResultadoCheckLimPar);
        form.AddField("ResultadoCheckAirAco", ResultadoCheckAirAco);
        form.AddField("ResultadoCheckEstEsc", ResultadoCheckEstEsc);
        form.AddField("ResultadoCheckMan", ResultadoCheckMan);
        form.AddField("ResultadoCheckMon", ResultadoCheckMon);
        form.AddField("ResultadoCheckBoc", ResultadoCheckBoc);
        form.AddField("ResultadoCheckAseCab", ResultadoCheckAseCab);
        form.AddField("ResultadoCheckTol", ResultadoCheckTol);
        form.AddField("ResultadoNumPreguntasContestadas", ResultadoNumPreguntasContestadas);
        //form.AddField("ResultadoOrdenEjecTiempo", ResultadoOrdenEjecTiempo);
        form.AddField("ResultadoPuntoPartidaTiempo", ResultadoPuntoPartidaTiempo);
    form.AddField("ResultadoCheckArtDir", ResultadoCheckArtDir);
    form.AddField("ResultadoCheckPasGen", ResultadoCheckPasGen);
    form.AddField("ResultadoCheckFugCil", ResultadoCheckFugCil);
    form.AddField("ResultadoCheckMotEnf", ResultadoCheckMotEnf);
    form.AddField("ResultadoCheckEstExtMan", ResultadoCheckEstExtMan);
    form.AddField("ResultadoCheckEstExtInc", ResultadoCheckEstExtInc);
		form.AddField ("ResultadoCheckCheFirCab", ResultadoCheckCheFirCab);
		form.AddField ("ResultadoCheckCabCheFir", ResultadoCheckCabCheFir);
		form.AddField ("ResultadoCheckSistAnsul", ResultadoCheckSistAnsul);

    form.AddField("CheckNivPet", CheckNivPet);
        form.AddField("CheckNivAceMot", CheckNivAceMot);
        form.AddField("CheckNivAceHid", CheckNivAceHid);
        form.AddField("CheckEstLuc", CheckEstLuc);
        form.AddField("CheckEstNeu", CheckEstNeu);
        //form.AddField("CheckEstNeu", CheckEstNeu);
        form.AddField("CheckNivRef", CheckNivRef);
        form.AddField("CheckNivAceTra", CheckNivAceTra);
        form.AddField("CheckNivAceTransf", CheckNivAceTransf);
        form.AddField("CheckFiltro", CheckFiltro);
        form.AddField("CheckIndObs", CheckIndObs);
        form.AddField("CheckLucGen", CheckLucGen);
        //form.AddField("CheckFug", CheckFug);//
        form.AddField("CheckLimPar", CheckLimPar);
        form.AddField("CheckAirAco", CheckAirAco);
        form.AddField("CheckEstEsc", CheckEstEsc);//
        form.AddField("CheckMan", CheckMan);
        form.AddField("CheckMon", CheckMon);
        form.AddField("CheckBoc", CheckBoc);
        form.AddField("CheckAseCab", CheckAseCab);
        form.AddField("CheckTol", CheckTol);
    form.AddField("CheckArtDir", CheckArtDir);
    form.AddField("CheckPasGen", CheckPasGen);
    form.AddField("CheckFugCil", CheckFugCil);
    form.AddField("CheckMotEnf", CheckMotEnf);
    form.AddField("CheckEstExtMan", CheckEstExtMan);
    form.AddField("CheckEstExtInc", CheckEstExtInc);
			form.AddField ("CheckCheFirCab", CheckCheFirCab);
			form.AddField ("CheckCabCheFir", CheckCabCheFir);
			form.AddField ("CheckSistAnsul", CheckSistAnsul);


    form.AddField("CheckTopEjeCen", CheckTopEjeCen);
    form.AddField("ResultadoCheckTopEjeCen", ResultadoCheckTopEjeCen);
    form.AddField("CheckSalEme", CheckSalEme);
    form.AddField("ResultadoCheckSalEme", ResultadoCheckSalEme);
    form.AddField("CheckArtCen", CheckArtCen);
    form.AddField("ResultadoCheckArtCen", ResultadoCheckArtCen);
			/*
    // if (NumeroModulo != "4")
    //{
    form.AddField("ResultadoCheckNivPet2", ResultadoCheckNivPet2);
            form.AddField("ResultadoCheckNivAceMot2", ResultadoCheckNivAceMot2);
            form.AddField("ResultadoCheckNivAceHid2", ResultadoCheckNivAceHid2);
            form.AddField("ResultadoCheckEstLuc2", ResultadoCheckEstLuc2);
            form.AddField("ResultadoCheckEstNeu2", ResultadoCheckEstNeu2);
            form.AddField("ResultadoCheckEstNeu2", ResultadoCheckEstNeu2);
            form.AddField("ResultadoCheckNivRef2", ResultadoCheckNivRef2);
            form.AddField("ResultadoCheckNivAceTra2", ResultadoCheckNivAceTra2);
            form.AddField("ResultadoCheckNivAceTransf2", ResultadoCheckNivAceTransf2);
            form.AddField("ResultadoCheckFiltro2", ResultadoCheckFiltro2);
            form.AddField("ResultadoCheckIndObs2", ResultadoCheckIndObs2);
            form.AddField("ResultadoCheckLucGen2", ResultadoCheckLucGen2);
            form.AddField("ResultadoCheckFug2", ResultadoCheckFug2);
            form.AddField("ResultadoCheckLimPar2", ResultadoCheckLimPar2);
            form.AddField("ResultadoCheckAirAco2", ResultadoCheckAirAco2);
            form.AddField("ResultadoCheckEstEsc2", ResultadoCheckEstEsc2);
            form.AddField("ResultadoCheckMan2", ResultadoCheckMan2);
            form.AddField("ResultadoCheckMon2", ResultadoCheckMon2);
            form.AddField("ResultadoCheckBoc2", ResultadoCheckBoc2);
            form.AddField("ResultadoCheckAseCab2", ResultadoCheckAseCab2);
            form.AddField("ResultadoCheckTol2", ResultadoCheckTol2);
    form.AddField("ResultadoCheckArtDir2", ResultadoCheckArtDir2);
    form.AddField("ResultadoCheckPasGen2", ResultadoCheckPasGen2);
    form.AddField("ResultadoCheckFugCil2", ResultadoCheckFugCil2);
    form.AddField("ResultadoCheckMotEnf2", ResultadoCheckMotEnf2);
    form.AddField("ResultadoCheckEstExtMan2", ResultadoCheckEstExtMan2);
    form.AddField("ResultadoCheckEstExtInc2", ResultadoCheckEstExtInc2);
			form.AddField ("ResultadoCheckCheFirCab2", ResultadoCheckCheFirCab2);
			form.AddField ("ResultadoCheckCabCheFir2", ResultadoCheckCabCheFir2);
			form.AddField ("ResultadoCheckSistAnsul2", ResultadoCheckSistAnsul2);

    form.AddField("CheckNivPet2", CheckNivPet2);
            form.AddField("CheckNivAceMot2", CheckNivAceMot2);
            form.AddField("CheckNivAceHid2", CheckNivAceHid2);
            form.AddField("CheckEstLuc2", CheckEstLuc2);
            form.AddField("CheckEstNeu2", CheckEstNeu2);
            form.AddField("CheckEstNeu2", CheckEstNeu2);
            form.AddField("CheckNivRef2", CheckNivRef2);
            form.AddField("CheckNivAceTra2", CheckNivAceTra2);
            form.AddField("CheckNivAceTransf2", CheckNivAceTransf2);
            form.AddField("CheckFiltro2", CheckFiltro2);
            form.AddField("CheckIndObs2", CheckIndObs2);
            form.AddField("CheckLucGen2", CheckLucGen2);
            //form.AddField("CheckFug2", CheckFug2);
            form.AddField("CheckLimPar2", CheckLimPar2);
            form.AddField("CheckAirAco2", CheckAirAco2);
            form.AddField("CheckEstEsc2", CheckEstEsc2);
            form.AddField("CheckMan2", CheckMan2);
            form.AddField("CheckMon2", CheckMon2);
            form.AddField("CheckBoc2", CheckBoc2);
            form.AddField("CheckAseCab2", CheckAseCab2);
            form.AddField("CheckTol2", CheckTol2);
    form.AddField("CheckArtDir2", CheckArtDir2);
    form.AddField("CheckPasGen2", CheckPasGen2);
    form.AddField("CheckFugCil2", CheckFugCil2);
    form.AddField("CheckMotEnf2", CheckMotEnf2);
    form.AddField("CheckEstExtMan2", CheckEstExtMan2);
    form.AddField("CheckEstExtInc2", CheckEstExtInc2);
			form.AddField ("CheckCheFirCab2", CheckCheFirCab2);
			form.AddField ("CheckCabCheFir2", CheckCabCheFir2);
			form.AddField ("CheckSistAnsul2", CheckSistAnsul2);

    form.AddField("CheckTopEjeCen2", CheckTopEjeCen2);
    form.AddField("ResultadoCheckTopEjeCen2", ResultadoCheckTopEjeCen2);
    form.AddField("CheckSalEme2", CheckSalEme2);
    form.AddField("ResultadoCheckSalEme2", ResultadoCheckSalEme2);
    form.AddField("CheckArtCen2", CheckArtCen2);
    form.AddField("ResultadoCheckArtCen2", ResultadoCheckArtCen2);
    }*/
	}
    if(vuelta != null)
        for (int i = 0; i < vuelta.Count; i++){
            form.AddField("ResultadoVuelta" + (i + 1), vuelta[i].ToString());
        }
    for (int i = 0; i < cicloCarguio.Count; i++)
    {
        form.AddField("ResultadoCarguioNumero" + (i + 1), "" + ((CicloCarguio)(cicloCarguio[i])).numero);
        form.AddField("ResultadoCarguioCarguio" + (i + 1), "" + ((CicloCarguio)(cicloCarguio[i])).carguio * 1000);
        form.AddField("ResultadoCarguioPatinaje" + (i + 1), "" + (((CicloCarguio)(cicloCarguio[i])).patinaje?1:0));
        form.AddField("ResultadoCarguioLevante" + (i + 1), "" + (((CicloCarguio)(cicloCarguio[i])).levante?1:0));
        form.AddField("ResultadoCarguioCaida" + (i + 1), "" + ((CicloCarguio)(cicloCarguio[i])).caida * 1000);
        form.AddField("ResultadoCarguioVaciado" + (i + 1), "" + ((CicloCarguio)(cicloCarguio[i])).vaciado * 1000);
        form.AddField("ResultadoCarguioTiempo" + (i + 1), "" + ((CicloCarguio)(cicloCarguio[i])).tiempo);


        print("ciclo " + "" + ((CicloCarguio)(cicloCarguio[i])).numero);
        print("cargio " + "" + ((CicloCarguio)(cicloCarguio[i])).carguio * 1000);
        print("caida " + "" + ((CicloCarguio)(cicloCarguio[i])).caida * 1000);
        print("vaciado " + "" + ((CicloCarguio)(cicloCarguio[i])).vaciado * 1000);
        print("tiempo " + ((CicloCarguio)(cicloCarguio[i])).tiempo);
    }

		string tipoModulo = "informacion";
		if (NumeroModulo == "Módulo 5" || NumeroModulo == "Módulo 6" || NumeroModulo == "Módulo 7" || NumeroModulo == "Módulo 8" )
			tipoModulo = "operacional";
		else if(NumeroModulo == "Módulo 4")
				tipoModulo = "checklist";
		form.AddField ("tipoModulo", tipoModulo);

		print (tipoModulo);
		print (VariablesGlobales.direccion + "SimuladorMT6020/crearHistorial.php");
		//return null;
		WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/crearHistorial.php", form);
		yield return download;
		if (download.error != null) {
			print (download.error);
		}
		else{
            //string retorno = download.text;
            //print(download.text);

		}


        vuelta.RemoveRange(0, vuelta.Count);
        cicloCarguio.RemoveRange(0, cicloCarguio.Count);
	}


	public static string calcularReloj(float tiempo){
		int minutos = (int)tiempo / 60;
		int segundos = (int)tiempo % 60;
		return ((minutos < 10) ? ("0" + minutos) : "" + minutos) + ":" + ((segundos < 10) ? ("0" + segundos) : "" + segundos);
	}

	public static float aproximar(float numero, int decimales){
		return (1f * ((Mathf.RoundToInt (numero * Mathf.Pow(10, decimales))) / Mathf.Pow(10f, decimales)));
	}

	public void finalizar(){
		/*Finalizar la etapa volver al login*/
		SceneManager.LoadScene ("Login");
	}

	public float getMultiplicadorDanio(string tag){
		foreach (MatrizDanio m in matrizDanio) {
			if(m.tag == tag) return m.multiplicador;
		}
		return 0f;
	}
	
	public void loadLabels(){
		resultadosAdmin [0].text = ResultadoTiempo.ToString();
		resultadosUser [0].text = ResultadoTiempo.ToString();
		resultadosAdmin [2].text = ResultadoRevFunc1.ToString();
		resultadosUser [2].text = ResultadoRevFunc1.ToString();
		resultadosAdmin [3].text = ResultadoRevEst1.ToString();
		resultadosUser [3].text = ResultadoRevEst1.ToString();
		resultadosAdmin [4].text = ResultadoRevCab1.ToString();
		resultadosUser [4].text = ResultadoRevCab1.ToString();
		resultadosAdmin [5].text = ResultadoPrevRies1.ToString();
		resultadosUser [5].text = ResultadoPrevRies1.ToString();
	}	
	
}



[System.Serializable]
public class MatrizDanio{
	public string tag;
	public float multiplicador = 1f;
	public MatrizDanio(string tag, float multiplicador){
		this.tag = tag;
		this.multiplicador = multiplicador;
	}
}

[System.Serializable]
public class CicloCarguio
{
    public int numero;
    public float carguio;
    public bool patinaje;
    public bool levante;
    public float caida;
    public float vaciado;
    public int tiempo;
}
