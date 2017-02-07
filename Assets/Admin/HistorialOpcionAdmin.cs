using UnityEngine;
using System.Collections;

public class HistorialOpcionAdmin : MonoBehaviour {
	public UILabel numniv;
	public GameObject obnumniv;
	public GameObject historial;	
	public UIPopupList list;	
	public GameObject popup;	
	string numeroNivel;
	bool preguntar=false;
	public bool esAdmin = false;
	public GameObject admins;

	public GameObject scroll;
	public GameObject intMaquina; //530 px
	public GameObject check1; //250
	public GameObject check2;//250
	public GameObject zipper;//135
	public GameObject metaFaena;//250
	public GameObject tunel;//180
	public GameObject camion;//180
	public GameObject preguntas; //145
	public GameObject tiempo; //110
	public GameObject ordenEjecucion;//75
	public GameObject motorPunta;//75
	public GameObject baldePunta; //75
	public GameObject entrega;//75
	public GameObject entregaSup;//75
	public GameObject vueltas; //145
	public GameObject traslado;//75
	public GameObject termino;//75
	public GameObject infoGeneral; //110 px

    public GameObject vueltasDetalles; //110 px
    public GameObject cicloDetalles; //110 px
    public GameObject fallaOperacional; //110 px
    public GameObject puntoPartidaFaena;
    public GameObject porcentajeColision;
    public GameObject integridadCamioneta;
    public GameObject integridadCamion;
    public GameObject preguntasFaena;

    public UILabel alumno;

	//para la exportada de excel
	public string nombreAlumno;
	public string numeroModulo;
	public string fecha;
	public string tiempoE;
	public string tiempoO;
	public string aprobacion;
	public string aprobacionO;
	public string cantpreguntas;
    public string cantpreguntascontestadas;
    public string intexigido;
	public string intO;
	public string postE;
	public string postO;
	public string postIE;
	public string postIO;
	public string postDE;
	public string postDO;
	public string medioDE;
	public string medioDO;
	public string cabE;
	public string cabO;
	public string baldeE;
	public string baldeO;
	public string tunelE;
	public string desctunel;
	public string tunelO;
	public string canttunel;
	public string checkE;
	public string checkO;
	public string revFunc1;
	public string revEst1;
	public string revCab1;
	public string prevRies1;
	public string checkE2;
	public string checkO2;
	public string revFunc2;
	public string revEst2;
	public string revCab2;
	public string prevRies2;
	public string terminoFaena;
	public string ENS;
	public string ENO;
	public string correctoTraslado;
	public string avanceMotor;
	public string avanceBalde;
	public string ordenEj;
	public string vueltasE;
	public string vueltasR;
	public string vueltasC;
	public string tonelajeE;
	public string tonelajeO;
	public string caidaPer;
	public string caidaO;
	public string correctoCarguio;
	public string patinaje;
	public string camionE;
	public string desccamion;
	public string camionO;
	public string cantcamion;
	public string zipperE;
	public string zipperO;
	public string cantzipper;


    public string numeroID;
    public string repeticion;

    public string[] check1lista;
    public string[] check1resp;
    public string[] check2lista;
    public string[] check2resp;
    public string[] check1por;
    public string[] check2por;

    public ArrayList vueltasDatos;
    public ArrayList cicloDatos;

    public string pregunta1;
    public string pregunta2;
    public string pregunta3;
    public string pregunta4;

    //public string descmaq;
    public string fallaOperacionalMaquina;

    void Start(){
        check1lista = new string[29];
        check1resp = new string[29];
        check2lista = new string[29];
        check2resp = new string[29];
        check1por = new string[5];
        check2por = new string[5];
        apagarTodo ();
	}
	void Update(){
		
	}
	void apagarTodo(){
		infoGeneral.SetActive (false);
		infoGeneral.transform.localPosition=new Vector3(0,0,0);

		intMaquina.SetActive (false);
		intMaquina.transform.localPosition=new Vector3(0,-230,0);

		check1.SetActive (false);

		check2.SetActive (false);

		zipper.SetActive (false);

		metaFaena.SetActive (false);
		metaFaena.transform.localPosition=new Vector3(600,0,0);

		tunel.SetActive (false);
		tunel.transform.localPosition=new Vector3(600,-380,0);

		camion.SetActive (false);
		camion.transform.FindChild("LabelTitulo").gameObject.GetComponent<UILabel>().text="INTEGRIDAD CAMIÓN BAJO PERFIL";
		camion.transform.localPosition=new Vector3(600,-570,0);

		tiempo.SetActive (false);
		tiempo.transform.localPosition=new Vector3(600,-260,0);

		preguntas.SetActive (false);
		preguntas.transform.localPosition=new Vector3(0,-1000,0);

		ordenEjecucion.SetActive (false);
		ordenEjecucion.transform.localPosition=new Vector3(600,-320,0);

		motorPunta.SetActive (false);
		motorPunta.transform.localPosition=new Vector3(0,-745,0);

		baldePunta.SetActive (false);
		baldePunta.transform.localPosition=new Vector3(0,-745,0);

		entrega.SetActive (false);
		entrega.transform.localPosition=new Vector3(0,-830,0);

		entregaSup.SetActive (false);
		entregaSup.transform.localPosition=new Vector3(0,-915,0);

		vueltas.SetActive (false);

		traslado.SetActive (false);
		traslado.transform.localPosition=new Vector3(0,-1000,0);

		termino.SetActive (false);

        vueltasDetalles.SetActive(false);
        cicloDetalles.SetActive(false);
        fallaOperacional.SetActive(false);
        puntoPartidaFaena.SetActive(false);
        porcentajeColision.SetActive(false);
        integridadCamioneta.SetActive(false);
        integridadCamion.SetActive(false);
        preguntasFaena.SetActive(false);

    }
	public void mirarNumeroNivel(){
		apagarTodo ();
		StartCoroutine (obtenerNivelEjecutar());
	}

	public void escogeNivel(){
		apagarTodo ();
		//obnumniv.SetActive(true);
		//numniv.text = "NUMERO MODULO: " + numeroNivel;

		infoGeneral.SetActive (true);
		switch (numeroNivel) {
		case "Módulo 1":
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, -190, 0);
                preguntas.SetActive(true);
                preguntas.transform.localPosition = new Vector3(600, 0, 0);
                break;
		case "Módulo 2":
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, -190, 0);
                preguntas.SetActive(true);
                preguntas.transform.localPosition = new Vector3(600, 0, 0);
                break;
		case "Módulo 3":
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, -190, 0);
                preguntas.SetActive(true);
                preguntas.transform.localPosition = new Vector3(600, 0, 0);
                break;
		case "Módulo 4":
			tiempo.SetActive(true);
			tiempo.transform.localPosition=new Vector3(0,-230,0);
			check1.SetActive(true);	
			check1.transform.localPosition=new Vector3(600,0,0);
			break;
		case "Módulo 5":
			tiempo.SetActive(true);
			tiempo.transform.localPosition=new Vector3(0,-230,0);
			break;
		case "Módulo 6":
			ordenEjecucion.SetActive(true);
			baldePunta.SetActive(true);
			tiempo.SetActive(true);
			tiempo.transform.localPosition=new Vector3(600,0,0);
			vueltas.SetActive(true);
			vueltas.transform.localPosition=new Vector3(600,-120,0);
			zipper.SetActive(true);
			zipper.transform.localPosition=new Vector3(600,-240,0);
			intMaquina.SetActive(true);

            vueltasDetalles.SetActive(true);
            vueltasDetalles.transform.localPosition = new Vector3(600, -440, 0);
            fallaOperacional.SetActive(true);
            fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
            break;
		case "Módulo 7":
                ordenEjecucion.SetActive(true);
                baldePunta.SetActive(true);
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                vueltas.SetActive(true);
                vueltas.transform.localPosition = new Vector3(600, -120, 0);
                zipper.SetActive(true);
                zipper.transform.localPosition = new Vector3(600, -240, 0);
                intMaquina.SetActive(true);

                vueltasDetalles.SetActive(true);
                vueltasDetalles.transform.localPosition = new Vector3(600, -440, 0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);

                break;
		case "Módulo 8":
                ordenEjecucion.SetActive(true);
                baldePunta.SetActive(true);
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                vueltas.SetActive(true);
                vueltas.transform.localPosition = new Vector3(600, -120, 0);
                zipper.SetActive(true);
                zipper.transform.localPosition = new Vector3(600, -240, 0);
                intMaquina.SetActive(true);

                vueltasDetalles.SetActive(true);
                vueltasDetalles.transform.localPosition = new Vector3(600, -440, 0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);

                break;
		/*case "9":
			    ordenEjecucion.SetActive(true);
			    baldePunta.SetActive(true);
			    tiempo.SetActive(true);
			    tiempo.transform.localPosition=new Vector3(600,0,0);
			    vueltas.SetActive(true);
			    vueltas.transform.localPosition=new Vector3(600,-120,0);
			    zipper.SetActive(true);
			    zipper.transform.localPosition=new Vector3(600,-240,0);
			    intMaquina.SetActive(true);

                vueltasDetalles.SetActive(true);
                vueltasDetalles.transform.localPosition = new Vector3(600, -440, 0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);

                break;
		case "10":
			    ordenEjecucion.SetActive(true);
			    baldePunta.SetActive(true);
			    tiempo.SetActive(true);
			    tiempo.transform.localPosition=new Vector3(600,0,0);
			    vueltas.SetActive(true);
			    vueltas.transform.localPosition=new Vector3(600,-120,0);
			    tunel.SetActive(true);
			    tunel.transform.localPosition=new Vector3(600,-400,0);
                //zipper.SetActive(true);
                //zipper.transform.localPosition = new Vector3(600, -240, 0);
                intMaquina.SetActive(true);

                vueltasDetalles.SetActive(true);
                vueltasDetalles.transform.localPosition = new Vector3(600, -590, 0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
                
                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);
                break;
		case "11":
                ordenEjecucion.SetActive(true);
                baldePunta.SetActive(true);
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                vueltas.SetActive(true);
                vueltas.transform.localPosition = new Vector3(600, -120, 0);
                tunel.SetActive(true);
                tunel.transform.localPosition = new Vector3(600, -400, 0);
                //zipper.SetActive(true);
                //zipper.transform.localPosition = new Vector3(600, -240, 0);
                intMaquina.SetActive(true);

                vueltasDetalles.SetActive(true);
                vueltasDetalles.transform.localPosition = new Vector3(600, -590, 0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);

                
                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);

                break;
		case "12":
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, -190, 0);
                preguntas.SetActive(true);
                preguntas.transform.localPosition = new Vector3(600, 0, 0);
                break;
		case "13":
			    ordenEjecucion.SetActive(true);
			    tiempo.SetActive(true);
                //tiempo.transform.localPosition=new Vector3(600,0,0);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                tunel.SetActive(true);
                tunel.transform.localPosition = new Vector3(600, -400, 0);
                intMaquina.SetActive(true);
			//intMaquina.transform.localPosition=new Vector3(600,-275,0);
			    metaFaena.SetActive(true);
                metaFaena.transform.localPosition = new Vector3(600, -580, 0);

                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
                //puntoPartidaFaena.SetActive(true);
                // puntoPartidaFaena.transform.localPosition = new Vector3(600, -120, 0);

                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);

                cicloDetalles.SetActive(true);
                cicloDetalles.transform.localPosition = new Vector3(300, -800, 0);
                break;
		case "14-a":
                ordenEjecucion.SetActive(true);
                tiempo.SetActive(true);
                //tiempo.transform.localPosition=new Vector3(600,0,0);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                tunel.SetActive(true);
                tunel.transform.localPosition = new Vector3(600, -400, 0);
                intMaquina.SetActive(true);
                //intMaquina.transform.localPosition=new Vector3(600,-275,0);
                metaFaena.SetActive(true);
                metaFaena.transform.localPosition = new Vector3(600, -580, 0);

                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
                // puntoPartidaFaena.SetActive(true);
                //puntoPartidaFaena.transform.localPosition = new Vector3(600, -120, 0);

                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);

                cicloDetalles.SetActive(true);
                cicloDetalles.transform.localPosition = new Vector3(300, -800, 0);
                break;
		case "14-b":
                ordenEjecucion.SetActive(true);
                tiempo.SetActive(true);
                //tiempo.transform.localPosition=new Vector3(600,0,0);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                tunel.SetActive(true);
                tunel.transform.localPosition = new Vector3(600, -400, 0);
                intMaquina.SetActive(true);
                //intMaquina.transform.localPosition=new Vector3(600,-275,0);
                metaFaena.SetActive(true);
                metaFaena.transform.localPosition = new Vector3(600, -580, 0);

                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
                //puntoPartidaFaena.SetActive(true);
                //puntoPartidaFaena.transform.localPosition = new Vector3(600, -120, 0);

                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);

                cicloDetalles.SetActive(true);
                cicloDetalles.transform.localPosition = new Vector3(300, -800, 0);
                camion.transform.FindChild("LabelTitulo").gameObject.GetComponent<UILabel>().text="INTEGRIDAD CAMIÓN";

                break;
		case "15":
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, -190, 0);
                preguntas.SetActive(true);
                preguntas.transform.localPosition = new Vector3(600, 0, 0);
                break;
		case "16":
			ordenEjecucion.SetActive(true);
			tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                tunel.SetActive(true);
                tunel.transform.localPosition = new Vector3(600, -400, 0);
                intMaquina.SetActive(true);
			metaFaena.SetActive(true);
                metaFaena.transform.localPosition = new Vector3(600, -580, 0);
                check1.SetActive(true);
			check1.transform.localPosition=new Vector3(0,-1010,0);
			check2.SetActive(true);
			check2.transform.localPosition=new Vector3(600,-1010,0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);
                //puntoPartidaFaena.SetActive(true);
                //puntoPartidaFaena.transform.localPosition = new Vector3(600, -120, 0);
                cicloDetalles.SetActive(true);
                cicloDetalles.transform.localPosition = new Vector3(300, -2460, 0);

                //integridadCamioneta.SetActive(true);
                //integridadCamioneta.transform.localPosition = new Vector3(0, -620, 0);
                preguntasFaena.SetActive(true);
                preguntasFaena.transform.localPosition = new Vector3(0, -820, 0);
                break;
		case "17":
                ordenEjecucion.SetActive(true);
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                tunel.SetActive(true);
                tunel.transform.localPosition = new Vector3(600, -400, 0);
                intMaquina.SetActive(true);
                metaFaena.SetActive(true);
                metaFaena.transform.localPosition = new Vector3(600, -580, 0);
                check1.SetActive(true);
                check1.transform.localPosition = new Vector3(0, -1010, 0);
                check2.SetActive(true);
                check2.transform.localPosition = new Vector3(600, -1010, 0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);
                //puntoPartidaFaena.SetActive(true);
                //puntoPartidaFaena.transform.localPosition = new Vector3(600, -120, 0);
                cicloDetalles.SetActive(true);
                cicloDetalles.transform.localPosition = new Vector3(300, -2460, 0);

                //integridadCamioneta.SetActive(true);
                //integridadCamioneta.transform.localPosition = new Vector3(0, -620, 0);
                //integridadCamion.SetActive(true);
                //integridadCamion.transform.localPosition = new Vector3(600, -820, 0);
                integridadCamion.SetActive(true);
                integridadCamion.transform.localPosition = new Vector3(0, -620, 0);
                preguntasFaena.SetActive(true);
                preguntasFaena.transform.localPosition = new Vector3(0, -820, 0);
                break;
		case "18":
                ordenEjecucion.SetActive(true);
                tiempo.SetActive(true);
                tiempo.transform.localPosition = new Vector3(600, 0, 0);
                tunel.SetActive(true);
                tunel.transform.localPosition = new Vector3(600, -400, 0);
                intMaquina.SetActive(true);
                metaFaena.SetActive(true);
                metaFaena.transform.localPosition = new Vector3(600, -580, 0);
                check1.SetActive(true);
                check1.transform.localPosition = new Vector3(0, -1010, 0);
                check2.SetActive(true);
                check2.transform.localPosition = new Vector3(600, -1010, 0);
                fallaOperacional.SetActive(true);
                fallaOperacional.transform.localPosition = new Vector3(0, -440, 0);
                porcentajeColision.SetActive(true);
                porcentajeColision.transform.localPosition = new Vector3(0, -520, 0);
                //puntoPartidaFaena.SetActive(true);
                //puntoPartidaFaena.transform.localPosition = new Vector3(600, -120, 0);
                cicloDetalles.SetActive(true);
                cicloDetalles.transform.localPosition = new Vector3(300, -2460, 0);

                //integridadCamioneta.SetActive(true);
                //integridadCamioneta.transform.localPosition = new Vector3(0, -620, 0);
                //integridadCamion.SetActive(true);
                //integridadCamion.transform.localPosition = new Vector3(600, -820, 0);
                integridadCamion.SetActive(true);
                integridadCamion.transform.localPosition = new Vector3(0, -620, 0);
                preguntasFaena.SetActive(true);
                preguntasFaena.transform.localPosition = new Vector3(0, -820, 0);
                break;*/
		default:
			
			
			break;
		}
		//gameObject.GetComponent<UIGrid>().Reposition();
		scroll.GetComponent<UIScrollView> ().ResetPosition ();
		preguntar = false;
	}
	
	void reset(){
		//numniv.text = "";
		//nombre.text="";

	}
	public void clickExportar(){
		StartCoroutine (exportarHistorial ());
	}
	IEnumerator exportarHistorial(){

		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Preparando datos para ser exportados";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWWForm form = new WWWForm();

        form.AddField("nombreAlumno",nombreAlumno);
		form.AddField("numeroModulo",numeroNivel);
        form.AddField("numeroID", numeroID);
        form.AddField("repeticion", repeticion);
        form.AddField("fecha",fecha);
		form.AddField("tiempoO",tiempoO);
		form.AddField("tiempoE","" + (int.Parse(tiempoE) * 60));
		if (esAdmin) {
			form.AddField ("id",admins.GetComponent<verNiveles>().getIDADMIN());
			//print (admins.GetComponent<verNiveles>().getIDADMIN());
		} else {
			GameObject confi=GameObject.FindGameObjectWithTag("Configuracion");
			Configuracion conf=confi.GetComponent<Configuracion>();
			form.AddField ("id",conf.usuario );
            form.AddField("Mail", conf.mailInstructor);
            //print (conf.usuario);
        }

		form.AddField("aprobacion",aprobacion);
		form.AddField("aprobacionO",aprobacionO);
		form.AddField("cantpreguntas",cantpreguntas);
        form.AddField("cantpreguntascontestadas", cantpreguntascontestadas);
        
        form.AddField("intexigido",intexigido);
		form.AddField("intO",intO);
		form.AddField ("postE", postE);
		form.AddField("postO",postO);
		form.AddField("postIE",postIE);
		form.AddField("postIO",postIO);
		form.AddField("postDE",postDE);
		form.AddField("postDO",postDO);
		form.AddField ("medioDE",medioDE );
		form.AddField("medioDO",medioDO);
		form.AddField("cabE",cabE);
		form.AddField("cabO",cabO);
		form.AddField("baldeE",baldeE);
		form.AddField("baldeO",baldeO);
		form.AddField ("tunelE", tunelE);
		form.AddField("desctunel",desctunel);
		form.AddField("tunelO",tunelO);
		form.AddField("canttunel",canttunel);
		form.AddField("checkE",checkE);
		form.AddField("checkO",checkO);
		form.AddField ("revFunc1",revFunc1);
		form.AddField("revEst1",revEst1);
		form.AddField("revCab1",revCab1);
		form.AddField("prevRies1",prevRies1);
		form.AddField("terminoFaena",terminoFaena);
		form.AddField("ENS",ENS);
		form.AddField ("ENO", ENO);
		form.AddField("correctoTraslado",correctoTraslado);
		form.AddField("avanceMotor",avanceMotor);
		form.AddField("avanceBalde",avanceBalde);
		form.AddField("ordenEj",ordenEj);
		form.AddField("vueltasE",vueltasE);
		form.AddField ("vueltasR", vueltasR);
		form.AddField("vueltasC",vueltasC);
		form.AddField("tonelajeE",tonelajeE);
		form.AddField("tonelajeO",tonelajeO);
		form.AddField("caidaO",caidaO);
		form.AddField("correctoCarguio",correctoCarguio);
		form.AddField ("patinaje", patinaje);
		form.AddField("camionE",camionE);
		form.AddField("desccamion",desccamion);
		form.AddField("camionO",camionO);
		form.AddField("cantcamion",cantcamion);
		form.AddField("zipperE",zipperE);
		form.AddField ("zipperO", zipperO);
		form.AddField("cantzipper",cantzipper);
		form.AddField("checkE2",checkE2);
		form.AddField("checkO2",checkO2);
		form.AddField ("revFunc2",revFunc2);
		form.AddField("revEst2",revEst2);
		form.AddField("revCab2",revCab2);
		form.AddField("prevRies2",prevRies2);
		form.AddField("nombreArchivo", nombreAlumno+" - Modulo"+fecha.Split(new char[1]{' '})[1]+" - "+fecha.Split(new char[1]{' '})[2].Replace('/', '.'));
        
        //form.AddField("descuentoColisionMaquina", descmaq);
        form.AddField("FallaOperacionalMaquina", fallaOperacionalMaquina=="1"?"Falla":"Ninguna");

        for (int i = 0; i < check1lista.Length; i++)
        {
            form.AddField("check1_" + i, "" + check1lista[i]);
            form.AddField("check1r_" + i, "" + check1resp[i]);
            print(check1lista[i] + " " + check1resp[i]);
        }
        for (int i = 0; i < check2lista.Length; i++)
        {
            form.AddField("check2_" + i, "" + check2lista[i]);
            form.AddField("check2r_" + i, "" + check2resp[i]);
        }
        for (int i = 0; i < check1por.Length; i++)
        {
            form.AddField("check1por_" + i, "" + check1por[i]);
            print("prom" + i + " " + "" + check1por[i]); 
        }
        for (int i = 0; i < check2por.Length; i++)
        {
            form.AddField("check2por_" + i, "" + check2por[i]);
        }

        for (int i = 0; i < vueltasDatos.Count; i++)
        {
            form.AddField("vuelta" + (i + 1), "" + (string)vueltasDatos[i]);
        }
        form.AddField("nVueltas", "" + vueltasDatos.Count);

        for (int i = 0; i < cicloDatos.Count; i++)
        {
            string[] dat = (string[])cicloDatos[i];
            form.AddField("cicloCarguio" + (i + 1), dat[0]);
            form.AddField("cicloCaida" + (i + 1), dat[3]);
            form.AddField("cicloVaciado" + (i + 1), dat[4]);
            form.AddField("cicloTiempo" + (i + 1), dat[5]);
        }
        form.AddField("nCiclos", "" + cicloDatos.Count);

        form.AddField("pregunta1", "" + pregunta1);
        form.AddField("pregunta2", "" + pregunta2);
        form.AddField("pregunta3", "" + pregunta3);
        form.AddField("pregunta4", "" + pregunta4);

        print (nombreAlumno+"-Modulo"+fecha.Split(new char[1]{' '})[1]+"-"+fecha.Split(new char[1]{' '})[2].Replace('/', '-'));
		WWW download = new WWW( "http://www.nemorisgames.com/juegos/SimuladorMT6020/crearExcel.php", form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			popup.GetComponent<UILabel>().text="No se puede realizar lo solicitado en este momento.";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			yield return false;
		} else {
			string retorno= download.text;
			print (retorno);
			popup.GetComponent<UILabel>().text="Datos enviados correctamente a su correo.";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
		}

	}
	public IEnumerator obtenerNivelEjecutar(){
		WWWForm form = new WWWForm();
		string idniv=historial.GetComponent<obtenerHistorial>().idniv;

		//print (list.value);
		print (idniv);
		//print ("solicitud");
		form.AddField ("id", idniv);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/numeroNivel.php", form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			//print ("hola");
			string retorno = download.text;
			
			print (retorno);
			string[] ret = retorno.Split (new char[]{'*'});

            numeroModulo = ret[0];
            numeroNivel = ret[0];
            nombreAlumno = alumno.text;
            infoGeneral.transform.FindChild("Nombre Alumno").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text = "Nombre Alumno: " + alumno.text;
            infoGeneral.transform.FindChild("Numero Modulo").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Número Módulo: "+ret[0];
            preguntas.transform.FindChild("Conf Preguntas").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Porcentaje Aprobación Exigido: "+ret[15]+" %";
			aprobacion=ret[15];
			preguntas.transform.FindChild("Conf CantPreguntas").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Cantidad de Preguntas: "+ret[16];
			cantpreguntas=ret[16];
			tiempo.transform.FindChild("Conf Tiempo").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Tiempo Máximo Otorgado: "+ret[2]+" min";
			tiempoE=ret[2];
			check1.transform.FindChild("Conf Check1").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Porcentaje Aprobación Exigido: "+ret[22]+" %";
			checkE=ret[22];
			check2.transform.FindChild("Conf Check2").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Porcentaje Aprobación Exigido: "+ret[23]+" %";
			checkE2=ret[23];
			vueltas.transform.FindChild("Conf Vueltas").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Cantidad de Vueltas Exigidas: "+ret[5];
			vueltasE=ret[5];
			metaFaena.transform.FindChild("Conf TonelajeTotal").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Objetivo de Faena: "+ret[19]+ " tn";
			tonelajeE=ret[19];
            //metaFaena.transform.FindChild("Conf CaidaMaterial").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Porcentaje Caida Material Permitido: "+ret[20]+" %";
            caidaPer = "-";// ret[20];
			intMaquina.transform.FindChild("Conf Post").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text=""+ret[9]+" %";
			postE=ret[9];
			intMaquina.transform.FindChild("Conf PostIzq").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text=""+ret[10]+" %";
			postIE=ret[10];
			intMaquina.transform.FindChild("Conf PostDer").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text=""+ret[11]+" %";
			postDE=ret[12];
			intMaquina.transform.FindChild("Conf MedioDer").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text=""+ret[12]+" %";
			medioDE=ret[12];
			intMaquina.transform.FindChild("Conf Cabina").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text=""+ret[13]+" %";
			cabE=ret[13];
			intMaquina.transform.FindChild("Conf Brazo").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text=""+ret[14]+" %";
			baldeE=ret[14];

         
            float porcentaje=0.0f;
			if(postE!=""){
				porcentaje=(float.Parse(postE)+float.Parse(postIE)+float.Parse(postDE)+float.Parse(medioDE)+float.Parse(cabE)+float.Parse(baldeE))*1f/6f;
            }
            porcentajeColision.transform.FindChild("PorcentajeIntegridad/Exigido").gameObject.GetComponent<UILabel>().text = Mathf.RoundToInt(porcentaje) + "%";
            intMaquina.transform.FindChild("Conf Maquina").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Integridad Total Exigida: "+Mathf.RoundToInt(porcentaje)+"%";
			intexigido="" + Mathf.RoundToInt(porcentaje);

 
            porcentajeColision.transform.FindChild("PorcentajeColision").transform.FindChild("Resultado").gameObject.GetComponent<UILabel>().text = ret[21] + "%";

            zipper.transform.FindChild("Conf Zipper").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Máximo Porcentaje de Choque Permitido: "+ret[6]+" %";
			zipperE=ret[6];
			tunel.transform.FindChild("Conf Tunel").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Integridad Exigido Túnel: "+ret[7]+" %";
			tunelE=ret[7];
			tunel.transform.FindChild("Conf DescTunel").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Descuento Por Choque: "+ret[24]+" %";
			desctunel=ret[24];
			//camion.transform.FindChild("Conf DescCamion").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Descuento Por Choque: "+ret[25]+" %";
			//desccamion=ret[25];
			//camion.transform.FindChild("Conf Camion").transform.FindChild("Label").gameObject.GetComponent<UILabel>().text="Integridad Exigido Camión: "+ret[8]+" %";
			//camionE=ret[8];

			//float porcentaje = (float.Parse (post.text) + float.Parse(postder.text) + float.Parse(postizq.text)
			                  //  + float.Parse(balde.text) + float.Parse(cabina.text) + float.Parse(medioder.text)) * 1f/6f;

			
			preguntar=true;
			escogeNivel ();

			popup.GetComponent<UILabel>().text="Datos Cargados";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			//print ("avisando");
		}
		
	}

}
