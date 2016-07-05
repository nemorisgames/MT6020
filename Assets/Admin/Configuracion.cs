using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine.SceneManagement;

public class Configuracion : MonoBehaviour {
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
	public int ChoqueZipper;
	public int IntAreaExtraccion;
	public int IntCamion;
    public int IntCamioneta;
    public int IntPosterior;
	public int IntPostDer;
	public int IntPostIzq;
	public int IntMedioDer;
	public int IntCabina;
	public int IntBrazo;
	public int ExitoPreguntas;
	public int CantidadPreguntas;
	public float MinimoCargar;
	public float MaximoCargar;
	public int TonelajeTotal;
	public int CaidaPermitida;
	public int DescuentoChoque;
	public int DescuentoTunel;
	public int DescuentoCamion;
    public int DescuentoCamioneta;
    public int check1;
	public int check2;
	/*resultados*/
	public string Fecha;
    public int ResultadoPreguntasCorta1;
    public int ResultadoPreguntasCorta2;
    public int ResultadoPreguntasCorta3;
    public int ResultadoPreguntasCorta4;
    public int ResultadoPreguntas;
	public int ResultadoTiempo;
	public int ResultadoCheck1;
	public int ResultadoRevFunc1;
	public int ResultadoRevCab1;
	public int ResultadoRevEst1;
	public int ResultadoPrevRies1;
	public int ResultadoCheck2;
	public int ResultadoRevFunc2;
	public int ResultadoRevCab2;
	public int ResultadoRevEst2;
	public int ResultadoPrevRies2;
	public string ResultadoOrdenEjecucion;
	public string ResultadoMotorPunta;
	public string ResultadoBaldePunta;
	public int ResultadoVueltasRealizadas;
	public int ResultadoVueltasCorrectas;
	public string ResultadoEntregaNombrada;
	public string ResultadoEntregaNombradaSup;
	public int ResultadoTonelajeTotal;
	public int ResultadoCaidaMat;
	public int ResultadoCorrectoCargio;
	public int ResultadoPatinaje;
	public int ResultadoIntMaquina;
	public int ResultadoIntPosterior;
	public int ResultadoIntPostDer;
	public int ResultadoIntPostIzq;
	public int ResultadoIntMedioDer;
	public int ResultadoIntCabina;
	public int ResultadoIntBrazo;
	public int ResultadoZipper;
	public int ResultadoCantidadZipper;
	public int ResultadoIntTunel;
    public int ResultadoIntCamion;
    public int ResultadoIntCamioneta;
    public int ResultadoCantidadTunel;
	public int ResultadoCantidadCamion;
	public string ResultadoTraslado;
	public string ResultadoTerminoFaena;

    public string fallaOperacion;

	public bool camionConvencionalSeleccionado;
	
	public MatrizDanio[] matrizDanio;
	public string[] macs;

    public int CheckRFNivPet;
    public int ResultadoCheckRFNivPet;
    public int CheckRFNivAceMot;
    public int ResultadoCheckRFNivAceMot;
    public int CheckRFNivAceHid;
    public int ResultadoCheckRFNivAceHid;
    public int CheckRFEstLuc;
    public int ResultadoCheckRFEstLuc;
    public int CheckRFEstNeu;
    public int ResultadoCheckRFEstNeu;
    public int CheckRFNivRef;
    public int ResultadoCheckRFNivRef;
    public int CheckRFNivAceTra;
    public int ResultadoCheckRFNivAceTra;
    public int CheckREBal;
    public int ResultadoCheckREBal;
    public int CheckREAnt;
    public int ResultadoCheckREAnt;
    public int CheckREArtCen;
    public int ResultadoCheckREArtCen;
    public int CheckREPasGen;
    public int ResultadoCheckREPasGen;
    public int CheckREFug;
    public int ResultadoCheckREFug;
    public int CheckRCLimPar;
    public int ResultadoCheckRCLimPar;
    public int CheckRCMan;
    public int ResultadoCheckRCMan;
    public int CheckRCLucGen;
    public int ResultadoCheckRCLucGen;
    public int CheckRCMonDis;
    public int ResultadoCheckRCMonDis;
    public int CheckRCAseCab;
    public int ResultadoCheckRCAseCab;
    public int CheckRCBoc;
    public int ResultadoCheckRCBoc;
    public int CheckPREstExtMan;
    public int ResultadoCheckPREstExtMan;
    public int CheckPREstExtInc;
    public int ResultadoCheckPREstExtInc;
    public int CheckPREstEsc;
    public int ResultadoCheckPREstEsc;
    public int CheckPRSalEme;
    public int ResultadoCheckPRSalEme;
    public int CheckPRSenMov;
    public int ResultadoCheckPRSenMov;
    public int CheckRCCab;
    public int ResultadoCheckRCCab;
    public int CheckRCTemAceMot;
    public int ResultadoCheckRCTemAceMot;
    public int CheckRCTemAceTra;
    public int ResultadoCheckRCTemAceTra;
    public int CheckRCVen;
    public int ResultadoCheckRCVen;
    public int CheckRCJoy;
    public int ResultadoCheckRCJoy;
    public int CheckRCPed;
    public int ResultadoCheckRCPed;

    public int CheckRFNivPet2;
    public int ResultadoCheckRFNivPet2;
    public int CheckRFNivAceMot2;
    public int ResultadoCheckRFNivAceMot2;
    public int CheckRFNivAceHid2;
    public int ResultadoCheckRFNivAceHid2;
    public int CheckRFEstLuc2;
    public int ResultadoCheckRFEstLuc2;
    public int CheckRFEstNeu2;
    public int ResultadoCheckRFEstNeu2;
    public int CheckRFNivRef2;
    public int ResultadoCheckRFNivRef2;
    public int CheckRFNivAceTra2;
    public int ResultadoCheckRFNivAceTra2;
    public int CheckREBal2;
    public int ResultadoCheckREBal2;
    public int CheckREAnt2;
    public int ResultadoCheckREAnt2;
    public int CheckREArtCen2;
    public int ResultadoCheckREArtCen2;
    public int CheckREPasGen2;
    public int ResultadoCheckREPasGen2;
    public int CheckREFug2;
    public int ResultadoCheckREFug2;
    public int CheckRCLimPar2;
    public int ResultadoCheckRCLimPar2;
    public int CheckRCMan2;
    public int ResultadoCheckRCMan2;
    public int CheckRCLucGen2;
    public int ResultadoCheckRCLucGen2;
    public int CheckRCMonDis2;
    public int ResultadoCheckRCMonDis2;
    public int CheckRCAseCab2;
    public int ResultadoCheckRCAseCab2;
    public int CheckRCBoc2;
    public int ResultadoCheckRCBoc2;
    public int CheckPREstExtMan2;
    public int ResultadoCheckPREstExtMan2;
    public int CheckPREstExtInc2;
    public int ResultadoCheckPREstExtInc2;
    public int CheckPREstEsc2;
    public int ResultadoCheckPREstEsc2;
    public int CheckPRSalEme2;
    public int ResultadoCheckPRSalEme2;
    public int CheckPRSenMov2;
    public int ResultadoCheckPRSenMov2;
    public int CheckRCCab2;
    public int ResultadoCheckRCCab2;
    public int CheckRCTemAceMot2;
    public int ResultadoCheckRCTemAceMot2;
    public int CheckRCTemAceTra2;
    public int ResultadoCheckRCTemAceTra2;
    public int CheckRCVen2;
    public int ResultadoCheckRCVen2;
    public int CheckRCJoy2;
    public int ResultadoCheckRCJoy2;
    public int CheckRCPed2;
    public int ResultadoCheckRCPed2;

    public int ResultadoNumPreguntasContestadas;
    public int ResultadoOrdenEjecTiempo;
    public int ResultadoPuntoPartidaTiempo;

    public ArrayList vuelta = new ArrayList();
    public ArrayList cicloCarguio = new ArrayList();

    // Use this for initialization
    void Start () {
		bool encontrado = false;
		foreach(string s in macs){
			if(s == FetchMacId ()) encontrado = true;
		}
		/*if (!encontrado) {
			SceneManager.LoadScene ("Pirata");
			return;
		}*/
		matrizDanio = new MatrizDanio[2];
		matrizDanio [0] = new MatrizDanio ("pared", 0.75f);
		matrizDanio [1] = new MatrizDanio ("Obstaculo", 0.1f);
		//logeado = true;
		//Screen.SetResolution(6400, 720, false);
		//Screen.SetResolution(9600, 1080, false);
		//DontDestroyOnLoad (gameObject);

		PlayerPrefs.SetString ("idAdmin", "");

		SceneManager.LoadScene ("Login");

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

        WWWForm form = new WWWForm();
        System.DateTime fecha = System.DateTime.Now;
        string f = fecha.ToString();
        f = "Modulo " + NumeroModulo + " " + f;
        print("tiempo empleado " + ResultadoTiempo);

        form.AddField("Mail", mailInstructor);

        form.AddField("ResultadoFallaOperacion", fallaOperacion);
        form.AddField("Fecha", f); 
        form.AddField("ResultadoPreguntasCorta1", ResultadoPreguntasCorta1);
        form.AddField("ResultadoPreguntasCorta2", ResultadoPreguntasCorta2);
        form.AddField("ResultadoPreguntasCorta3", ResultadoPreguntasCorta3);
        form.AddField("ResultadoPreguntasCorta4", ResultadoPreguntasCorta4);
        form.AddField("IdNivel", idModulo); form.AddField("PorPreguntas", ResultadoPreguntas); form.AddField("TiempoEmpleado", ResultadoTiempo);
        form.AddField("Check1", ResultadoCheck1); form.AddField("revFunc1", ResultadoRevFunc1); form.AddField("revEst1", ResultadoRevEst1); form.AddField("revCab1", ResultadoRevCab1);
        form.AddField("prevRies1", ResultadoPrevRies1); form.AddField("Check2", ResultadoCheck2); form.AddField("revFunc2", ResultadoRevFunc2); form.AddField("revEst2", ResultadoRevEst2);
        form.AddField("revCab2", ResultadoRevCab2); form.AddField("prevRies2", ResultadoPrevRies2); form.AddField("OrdenEj", ResultadoOrdenEjecucion); form.AddField("MotorPunta", ResultadoMotorPunta);
        form.AddField("BaldePunta", ResultadoBaldePunta); form.AddField("VueltasCorrectas", ResultadoVueltasCorrectas); form.AddField("EntregaNombrada", ResultadoEntregaNombrada); form.AddField("EntregaNombradaSup", ResultadoEntregaNombradaSup);

        form.AddField("TonelajeTotal", ResultadoTonelajeTotal); form.AddField("CaidaMat", ResultadoCaidaMat); form.AddField("CorrectoCarguio", ResultadoCorrectoCargio); form.AddField("Patinaje", ResultadoPatinaje);
        form.AddField("IntMaquina", ResultadoIntMaquina); form.AddField("IntBalde", ResultadoIntBrazo); form.AddField("IntCabina", ResultadoIntCabina); form.AddField("IntMedioDer", ResultadoIntMedioDer);
        form.AddField("IntPost", ResultadoIntPosterior); form.AddField("IntPostIzq", ResultadoIntPostIzq); form.AddField("IntPostDer", ResultadoIntPostDer); form.AddField("Zipper", ResultadoZipper);
        form.AddField("CantZipper", ResultadoCantidadZipper); form.AddField("Tunel", ResultadoIntTunel); form.AddField("CantTunel", ResultadoCantidadTunel);

        form.AddField("CamionMin", IntCamion); form.AddField("CamionDes", DescuentoCamion); form.AddField("Camion", ResultadoIntCamion);
        form.AddField("CamionetaMin", IntCamioneta); form.AddField("CamionetaDes", DescuentoCamioneta); form.AddField("Camioneta", ResultadoIntCamioneta);

        form.AddField("CantCamion", ResultadoCantidadCamion); form.AddField("Traslado", ResultadoTraslado); form.AddField("CantVueltas", ResultadoVueltasRealizadas); form.AddField("TerminoFaena", ResultadoTerminoFaena);
        form.AddField("idAlumno", alumno);

        //if (NumeroModulo == "4" || NumeroModulo == "16" || NumeroModulo == "17" || NumeroModulo == "18")
        //{
            form.AddField("ResultadoCheckRFNivPet", ResultadoCheckRFNivPet);
            form.AddField("ResultadoCheckRFNivAceMot", ResultadoCheckRFNivAceMot);
            form.AddField("ResultadoCheckRFNivAceHid", ResultadoCheckRFNivAceHid);
            form.AddField("ResultadoCheckRFEstLuc", ResultadoCheckRFEstLuc);
            form.AddField("ResultadoCheckRFEstNeu", ResultadoCheckRFEstNeu);
            form.AddField("ResultadoCheckRFEstNeu", ResultadoCheckRFEstNeu);
            form.AddField("ResultadoCheckRFNivRef", ResultadoCheckRFNivRef);
            form.AddField("ResultadoCheckRFNivAceTra", ResultadoCheckRFNivAceTra);
            form.AddField("ResultadoCheckREBal", ResultadoCheckREBal);
            form.AddField("ResultadoCheckREAnt", ResultadoCheckREAnt);
            form.AddField("ResultadoCheckREArtCen", ResultadoCheckREArtCen);
            form.AddField("ResultadoCheckREPasGen", ResultadoCheckREPasGen);
            form.AddField("ResultadoCheckREFug", ResultadoCheckREFug);
            form.AddField("ResultadoCheckRCLimPar", ResultadoCheckRCLimPar);
            form.AddField("ResultadoCheckRCMan", ResultadoCheckRCMan);
            form.AddField("ResultadoCheckRCLucGen", ResultadoCheckRCLucGen);
            form.AddField("ResultadoCheckRCMonDis", ResultadoCheckRCMonDis);
            form.AddField("ResultadoCheckRCAseCab", ResultadoCheckRCAseCab);
            form.AddField("ResultadoCheckRCBoc", ResultadoCheckRCBoc);
            form.AddField("ResultadoCheckPREstExtMan", ResultadoCheckPREstExtMan);
            form.AddField("ResultadoCheckPREstExtInc", ResultadoCheckPREstExtInc);
            form.AddField("ResultadoNumPreguntasContestadas", ResultadoNumPreguntasContestadas);
            form.AddField("ResultadoOrdenEjecTiempo", ResultadoOrdenEjecTiempo);
            form.AddField("ResultadoPuntoPartidaTiempo", ResultadoPuntoPartidaTiempo);
        form.AddField("ResultadoCheckRCCab", ResultadoCheckRCCab);
        form.AddField("ResultadoCheckRCTemAceMot", ResultadoCheckRCTemAceMot);
        form.AddField("ResultadoCheckRCTemAceTra", ResultadoCheckRCTemAceTra);
        form.AddField("ResultadoCheckRCVen", ResultadoCheckRCVen);
        form.AddField("ResultadoCheckRCJoy", ResultadoCheckRCJoy);
        form.AddField("ResultadoCheckRCPed", ResultadoCheckRCPed);

        form.AddField("CheckRFNivPet", CheckRFNivPet);
            form.AddField("CheckRFNivAceMot", CheckRFNivAceMot);
            form.AddField("CheckRFNivAceHid", CheckRFNivAceHid);
            form.AddField("CheckRFEstLuc", CheckRFEstLuc);
            form.AddField("CheckRFEstNeu", CheckRFEstNeu);
            form.AddField("CheckRFEstNeu", CheckRFEstNeu);
            form.AddField("CheckRFNivRef", CheckRFNivRef);
            form.AddField("CheckRFNivAceTra", CheckRFNivAceTra);
            form.AddField("CheckREBal", CheckREBal);
            form.AddField("CheckREAnt", CheckREAnt);
            form.AddField("CheckREArtCen", CheckREArtCen);
            form.AddField("CheckREPasGen", CheckREPasGen);
            form.AddField("CheckREFug", CheckREFug);
            form.AddField("CheckRCLimPar", CheckRCLimPar);
            form.AddField("CheckRCMan", CheckRCMan);
            form.AddField("CheckRCLucGen", CheckRCLucGen);
            form.AddField("CheckRCMonDis", CheckRCMonDis);
            form.AddField("CheckRCAseCab", CheckRCAseCab);
            form.AddField("CheckRCBoc", CheckRCBoc);
            form.AddField("CheckPREstExtMan", CheckPREstExtMan);
            form.AddField("CheckPREstExtInc", CheckPREstExtInc);
        form.AddField("CheckRCCab", CheckRCCab);
        form.AddField("CheckRCTemAceMot", CheckRCTemAceMot);
        form.AddField("CheckRCTemAceTra", CheckRCTemAceTra);
        form.AddField("CheckRCVen", CheckRCVen);
        form.AddField("CheckRCJoy", CheckRCJoy);
        form.AddField("CheckRCPed", CheckRCPed);


        form.AddField("CheckPREstEsc", CheckPREstEsc);
        form.AddField("ResultadoCheckPREstEsc", ResultadoCheckPREstEsc);
        form.AddField("CheckPRSalEme", CheckPRSalEme);
        form.AddField("ResultadoCheckPRSalEme", ResultadoCheckPRSalEme);
        form.AddField("CheckPRSenMov", CheckPRSenMov);
        form.AddField("ResultadoCheckPRSenMov", ResultadoCheckPRSenMov);
        
        // if (NumeroModulo != "4")
        //{
        form.AddField("ResultadoCheckRFNivPet2", ResultadoCheckRFNivPet2);
                form.AddField("ResultadoCheckRFNivAceMot2", ResultadoCheckRFNivAceMot2);
                form.AddField("ResultadoCheckRFNivAceHid2", ResultadoCheckRFNivAceHid2);
                form.AddField("ResultadoCheckRFEstLuc2", ResultadoCheckRFEstLuc2);
                form.AddField("ResultadoCheckRFEstNeu2", ResultadoCheckRFEstNeu2);
                form.AddField("ResultadoCheckRFEstNeu2", ResultadoCheckRFEstNeu2);
                form.AddField("ResultadoCheckRFNivRef2", ResultadoCheckRFNivRef2);
                form.AddField("ResultadoCheckRFNivAceTra2", ResultadoCheckRFNivAceTra2);
                form.AddField("ResultadoCheckREBal2", ResultadoCheckREBal2);
                form.AddField("ResultadoCheckREAnt2", ResultadoCheckREAnt2);
                form.AddField("ResultadoCheckREArtCen2", ResultadoCheckREArtCen2);
                form.AddField("ResultadoCheckREPasGen2", ResultadoCheckREPasGen2);
                form.AddField("ResultadoCheckREFug2", ResultadoCheckREFug2);
                form.AddField("ResultadoCheckRCLimPar2", ResultadoCheckRCLimPar2);
                form.AddField("ResultadoCheckRCMan2", ResultadoCheckRCMan2);
                form.AddField("ResultadoCheckRCLucGen2", ResultadoCheckRCLucGen2);
                form.AddField("ResultadoCheckRCMonDis2", ResultadoCheckRCMonDis2);
                form.AddField("ResultadoCheckRCAseCab2", ResultadoCheckRCAseCab2);
                form.AddField("ResultadoCheckRCBoc2", ResultadoCheckRCBoc2);
                form.AddField("ResultadoCheckPREstExtMan2", ResultadoCheckPREstExtMan2);
                form.AddField("ResultadoCheckPREstExtInc2", ResultadoCheckPREstExtInc2);
        form.AddField("ResultadoCheckRCCab2", ResultadoCheckRCCab2);
        form.AddField("ResultadoCheckRCTemAceMot2", ResultadoCheckRCTemAceMot2);
        form.AddField("ResultadoCheckRCTemAceTra2", ResultadoCheckRCTemAceTra2);
        form.AddField("ResultadoCheckRCVen2", ResultadoCheckRCVen2);
        form.AddField("ResultadoCheckRCJoy2", ResultadoCheckRCJoy2);
        form.AddField("ResultadoCheckRCPed2", ResultadoCheckRCPed2);

        form.AddField("CheckRFNivPet2", CheckRFNivPet2);
                form.AddField("CheckRFNivAceMot2", CheckRFNivAceMot2);
                form.AddField("CheckRFNivAceHid2", CheckRFNivAceHid2);
                form.AddField("CheckRFEstLuc2", CheckRFEstLuc2);
                form.AddField("CheckRFEstNeu2", CheckRFEstNeu2);
                form.AddField("CheckRFEstNeu2", CheckRFEstNeu2);
                form.AddField("CheckRFNivRef2", CheckRFNivRef2);
                form.AddField("CheckRFNivAceTra2", CheckRFNivAceTra2);
                form.AddField("CheckREBal2", CheckREBal2);
                form.AddField("CheckREAnt2", CheckREAnt2);
                form.AddField("CheckREArtCen2", CheckREArtCen2);
                form.AddField("CheckREPasGen2", CheckREPasGen2);
                form.AddField("CheckREFug2", CheckREFug2);
                form.AddField("CheckRCLimPar2", CheckRCLimPar2);
                form.AddField("CheckRCMan2", CheckRCMan2);
                form.AddField("CheckRCLucGen2", CheckRCLucGen2);
                form.AddField("CheckRCMonDis2", CheckRCMonDis2);
                form.AddField("CheckRCAseCab2", CheckRCAseCab2);
                form.AddField("CheckRCBoc2", CheckRCBoc2);
                form.AddField("CheckPREstExtMan2", CheckPREstExtMan2);
                form.AddField("CheckPREstExtInc2", CheckPREstExtInc2);
        form.AddField("CheckRCCab2", CheckRCCab2);
        form.AddField("CheckRCTemAceMot2", CheckRCTemAceMot2);
        form.AddField("CheckRCTemAceTra2", CheckRCTemAceTra2);
        form.AddField("CheckRCVen2", CheckRCVen2);
        form.AddField("CheckRCJoy2", CheckRCJoy2);
        form.AddField("CheckRCPed2", CheckRCPed2);

        form.AddField("CheckPREstEsc2", CheckPREstEsc2);
        form.AddField("ResultadoCheckPREstEsc2", ResultadoCheckPREstEsc2);
        form.AddField("CheckPRSalEme2", CheckPRSalEme2);
        form.AddField("ResultadoCheckPRSalEme2", ResultadoCheckPRSalEme2);
        form.AddField("CheckPRSenMov2", CheckPRSenMov2);
        form.AddField("ResultadoCheckPRSenMov2", ResultadoCheckPRSenMov2);
        // }
        //}
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


        WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/crearHistorial.php", form);
		yield return download;
		if (download.error != null) {
		}
		else{
            //string retorno = download.text;
            print(download.text);

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
