using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class editarNivel : MonoBehaviour {
	public UILabel numniv;
	public GameObject obnumniv;
	List<string> lista;
	public UIPopupList list;
	public GameObject grid;
	public GameObject grid1;
	string ultimoActual;
	string ultimoTiempo4;
	public GameObject popup;
	public UILabel numero;
	public UILabel niv4;
	public UIInput nombre;
	
	public UILabel cantPreguntas;
	public UIInput preguntas;//exito preguntas
	public UIInput repeticiones;
	/*tiempos*/
	public UIInput tiempoVuelta;
	public UIInput tiempoFaena;//tiempo faena = tiempo preguntas = tiempo total
//	public UIInput tiempoextMin;
	//public UIInput tiempoextMax;
	
	
	/*integridad maquina*/
	public UIInput zipper;
	public UIInput postder;
	public UIInput post;
	public UIInput postizq;
	public UIInput balde;
	public UIInput cabina;
	public UIInput medioder;
	public UIInput descuentoChoque;
	
	
	public UIInput intTunel;
	public UIInput descuentoTunel;
	public UIInput intCamion;
	public UIInput descuentoCamion;
	
	public UIInput check1;
	public UIInput check2;
	
	public UIInput TonelajeTotal;
    public GameObject obFallaInducida;
    public UIPopupList FallaInducida;
    //public UIInput TonelajeMax;
    public UIInput caidaPermitida;
	
	
	
	
	public GameObject obtiempoVuelta;
	public GameObject obtiempoFaena;
	//public GameObject obtiempoextMin;
	//public GameObject obtiempoextMax;
	
	public GameObject obrepeticiones;
	
	public GameObject obzipper;
	public GameObject obpostder;
	public GameObject obpost;
	public GameObject obpostizq;
	public GameObject obbalde;
	public GameObject obmedioder;
	public GameObject obCabina;
	public GameObject label;
	public GameObject obdescuentoChoque;
	
	public GameObject obcheck1;
	public GameObject obcheck2;
	
	public GameObject obTonelajeTotal;
	//public GameObject obTonelajeMin;
	//public GameObject obTonelajeMax;
	public GameObject obcaidaPermitida;
	
	public GameObject obintTunel;
	public GameObject obdescuentoTunel;
	public GameObject obintCamion;
	public GameObject obdescuentoCamion;
	
	public GameObject obcantPreguntas;
	public GameObject obpreguntas;
	public GameObject tiempo4;

	string numeroNivel;
	bool preguntar=false;

	void Start(){
		apagarTodo ();
	}
	void Update(){

	}
	public	void apagarTodo(){
			obnumniv.SetActive (false);
		obtiempoVuelta.SetActive (false);
		obtiempoVuelta.transform.localPosition=new Vector3(0,-240,0);
		//obtiempoextMin.SetActive (false);
		//obtiempoextMax.SetActive (false);
		obtiempoFaena.SetActive (false);
		obtiempoFaena.transform.localPosition=new Vector3(0,-40,0);
		tiempo4.SetActive (false);
		
		
		obrepeticiones.SetActive (false);
		obrepeticiones.transform.localPosition=new Vector3(0,-140,0);
		
		obzipper.SetActive (false);
		obzipper.transform.localPosition=new Vector3(0,-340,0);
		obpostder.SetActive (false);
		obpostizq.SetActive (false);
		obmedioder.SetActive (false);
		obpost.SetActive (false);
		obbalde.SetActive (false);
		label.SetActive (false);
		label.transform.localPosition=new Vector3(-196,-600,0);
		obCabina.SetActive (false);
		obdescuentoChoque.SetActive (false);
		
		obintCamion.SetActive (false);
		//obintCamion.transform.FindChild ("Camion").GetComponent<UILabel> ().text = "";
		obintTunel.SetActive (false);
		obdescuentoTunel.SetActive (false);
		obdescuentoCamion.SetActive (false);
		
		obcheck1.SetActive (false);
		obcheck2.SetActive (false);

        obFallaInducida.SetActive (false);
		//obTonelajeMin.SetActive (false);
		obTonelajeTotal.SetActive (false);
		obcaidaPermitida.SetActive (false);
		
		obcantPreguntas.SetActive (false);
		obpreguntas.SetActive (false);
		grid.GetComponent<UIGrid>().Reposition();
		grid1.GetComponent<UIGrid>().Reposition();
		grid.GetComponentInParent<UIScrollView> ().ResetPosition ();
		grid1.GetComponentInParent<UIScrollView> ().ResetPosition ();
			
		}
	public void mirarNumeroNivel(){
		apagarTodo ();
		StartCoroutine (obtenerNivelEjecutar());
	}
	public void guardarCambios(){
		StartCoroutine (ActualizarNivelEjecutar());
	}

	public void escogeNivel(){
		apagarTodo ();
		obnumniv.SetActive(true);
		numero.text = "" + numeroNivel;

			switch (numeroNivel) {
		case "Módulo 1":
			//obtiempoFaena.SetActive(true);
			//obtiempoFaena.transform.localPosition=new Vector3(0,-40,0);
			obpreguntas.SetActive(true);
			obpreguntas.transform.localPosition=new Vector3(0,-240,0);
			obcantPreguntas.SetActive(true);
			obcantPreguntas.transform.localPosition=new Vector3(0,-140,0);
			lista= new List<string>();
			lista.Add("10");
			lista.Add("12");
			lista.Add("15");
			obcantPreguntas.GetComponent<UIPopupList>().items=lista;
			obcantPreguntas.GetComponent<UIPopupList>().value=ultimoActual;
			
			break;
		case "Módulo 2":
			//obtiempoFaena.SetActive(true);			
			obpreguntas.SetActive(true);
			obpreguntas.transform.localPosition=new Vector3(0,-240,0);
			obcantPreguntas.SetActive(true);
			lista= new List<string>();
			lista.Add("10");
			lista.Add("12");
			lista.Add("15");
			obcantPreguntas.GetComponent<UIPopupList>().items=lista;
			obcantPreguntas.GetComponent<UIPopupList>().value=ultimoActual;
			obcantPreguntas.transform.localPosition=new Vector3(0,-140,0);
			break;
		case "Módulo 3":
			//obtiempoFaena.SetActive(true);			
			obpreguntas.SetActive(true);
			obpreguntas.transform.localPosition=new Vector3(0,-240,0);
			lista= new List<string>();
			lista.Add("10");
			lista.Add("12");
			lista.Add("15");
			obcantPreguntas.GetComponent<UIPopupList>().items=lista;
			obcantPreguntas.GetComponent<UIPopupList>().value=ultimoActual;
			obcantPreguntas.SetActive(true);
			obcantPreguntas.transform.localPosition=new Vector3(0,-140,0);
			break;
		case "Módulo 4":
			//corregir
			tiempo4.SetActive(true);
			tiempo4.transform.localPosition=new Vector3(0,-40,0);
			tiempo4.GetComponent<UIPopupList>().value=ultimoTiempo4;
			niv4.text=ultimoTiempo4;
			obcheck1.SetActive(true);
			obcheck1.transform.localPosition=new Vector3(0,-140,0);
			
			break;
		case "Módulo 5":
			obtiempoFaena.SetActive(true);
			break;
		case "Módulo 6":
			obrepeticiones.SetActive (true);
			obtiempoVuelta.SetActive(true);
			obtiempoFaena.SetActive(true);
			
			//obzipper.SetActive (true);
			//obzipper.transform.localPosition=new Vector3(0,-300,0);
			
			obpostder.SetActive (true);
			obpostizq.SetActive (true);
			obmedioder.SetActive (true);
			obpost.SetActive (true);
			obbalde.SetActive (true);
			obCabina.SetActive (true);
			label.SetActive (true);
			label.transform.localPosition=new Vector3(-196,-600,0);
			obdescuentoChoque.SetActive (true);

            obintTunel.SetActive(true);
            obintTunel.transform.localPosition = new Vector3(0, -800, 0);

            obdescuentoTunel.SetActive(true);
            obdescuentoTunel.transform.localPosition = new Vector3(0, -900, 0);

            break;
		case "Módulo 7": case "Módulo 8":
            obFallaInducida.SetActive(true);
            obFallaInducida.transform.localPosition = new Vector3(0, -840, 0);
            obrepeticiones.SetActive (true);
			obtiempoVuelta.SetActive(true);
			obtiempoFaena.SetActive(true);
			
			obzipper.SetActive (true);
			//obzipper.transform.localPosition=new Vector3(0,-300,0);
			obpostder.SetActive (true);
			obpostizq.SetActive (true);
			obmedioder.SetActive (true);
			obpost.SetActive (true);
			obbalde.SetActive (true);
			obCabina.SetActive (true);
			label.SetActive (true);
			label.transform.localPosition=new Vector3(-196,-600,0);
			obdescuentoChoque.SetActive (true);
                
            obTonelajeTotal.SetActive(true);
            obTonelajeTotal.transform.localPosition = new Vector3(0, -440f, 0);
            obcaidaPermitida.SetActive(true);
            obcaidaPermitida.transform.localPosition = new Vector3(0, -540, 0);
            obcheck1.SetActive(true);
            obcheck1.transform.localPosition = new Vector3(0, -640, 0);
            obcheck2.SetActive(true);
            obcheck2.transform.localPosition = new Vector3(0, -740, 0);

            obintTunel.SetActive(true);
            obintTunel.transform.localPosition = new Vector3(0, -800, 0);

            obdescuentoTunel.SetActive(true);
            obdescuentoTunel.transform.localPosition = new Vector3(0, -900, 0);

            obintCamion.SetActive(true);
            obintCamion.transform.localPosition = new Vector3(0, -1000, 0);

            obdescuentoCamion.SetActive(true);
            obdescuentoCamion.transform.localPosition = new Vector3(0, -1100, 0);
            break;
            /*case "Módulo 8":
                obrepeticiones.SetActive (true);
                obtiempoVuelta.SetActive(true);
                obtiempoFaena.SetActive(true);

                //	obzipper.transform.localPosition=new Vector3(0,-300,0);
                obzipper.SetActive (true);

                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                break;
            case "9":
                obrepeticiones.SetActive (true);
                obtiempoVuelta.SetActive(true);
                obtiempoFaena.SetActive(true);

                obzipper.SetActive (true);
                //obzipper.transform.localPosition=new Vector3(0,-300,0);

                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                break;
            case "10":
                obrepeticiones.SetActive (true);
                obtiempoVuelta.SetActive(true);
                obtiempoFaena.SetActive(true);

                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);



                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                break;
            case "11":
                obrepeticiones.SetActive (true);
                obtiempoVuelta.SetActive(true);
                obtiempoFaena.SetActive(true);

                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);


                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                break;
            case "12":
                obtiempoFaena.SetActive(true);

                obpreguntas.SetActive(true);
                obpreguntas.transform.localPosition=new Vector3(0,-240,0);
                obcantPreguntas.SetActive(true);
                lista= new List<string>();
                lista.Add("10");
                lista.Add("15");
                lista.Add("20");
                obcantPreguntas.GetComponent<UIPopupList>().items=lista;
                obcantPreguntas.GetComponent<UIPopupList>().value=ultimoActual;
                obcantPreguntas.transform.localPosition=new Vector3(0,-140,0);
                break;
            case "13":
                //obrepeticiones.SetActive (true);
                obtiempoVuelta.SetActive(true);
                obtiempoVuelta.transform.localPosition=new Vector3(0,-140,0);
                obtiempoFaena.SetActive(true);

                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);


                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                obTonelajeMax.SetActive(true);
                obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obTonelajeMin.SetActive(true);
                obTonelajeMin.transform.localPosition=new Vector3(0,-440,0);
                obTonelajeTotal.SetActive(true);
                obTonelajeTotal.transform.localPosition=new Vector3(0,-240.9f,0);
                obcaidaPermitida.SetActive(true);
                obcaidaPermitida.transform.localPosition=new Vector3(0,-540,0);
                break;
            case "14-a":
                obtiempoVuelta.SetActive(true);
                obtiempoVuelta.transform.localPosition=new Vector3(0,-140,0);
                obtiempoFaena.SetActive(true);

                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);

                obintCamion.SetActive (true);
                obintCamion.transform.localPosition=new Vector3(0,-1000,0);

                obintCamion.transform.FindChild("Camion").GetComponent<UILabel>().text = "Camion Bajo Perfil";

                obdescuentoCamion.SetActive(true);
                obdescuentoCamion.transform.localPosition=new Vector3(0,-1100,0);

                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                obTonelajeMax.SetActive(true);
                obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obTonelajeMin.SetActive(true);
                obTonelajeMin.transform.localPosition=new Vector3(0,-440,0);
                obTonelajeTotal.SetActive(true);
                obTonelajeTotal.transform.localPosition=new Vector3(0,-240.9f,0);
                obcaidaPermitida.SetActive(true);
                obcaidaPermitida.transform.localPosition=new Vector3(0,-540,0);
                break;
            case "14-b":
                obtiempoVuelta.SetActive(true);
                obtiempoVuelta.transform.localPosition=new Vector3(0,-140,0);
                obtiempoFaena.SetActive(true);

                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);

                obintCamion.SetActive (true);
                obintCamion.transform.localPosition=new Vector3(0,-1000,0);
                obintCamion.transform.FindChild ("Camion").GetComponent<UILabel> ().text = "Camion convencional";

                obdescuentoCamion.SetActive(true);
                obdescuentoCamion.transform.localPosition=new Vector3(0,-1100,0);

                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                obTonelajeMax.SetActive(true);
                obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obTonelajeMin.SetActive(true);
                obTonelajeMin.transform.localPosition=new Vector3(0,-440,0);
                obTonelajeTotal.SetActive(true);
                obTonelajeTotal.transform.localPosition=new Vector3(0,-240.9f,0);
                obcaidaPermitida.SetActive(true);
                obcaidaPermitida.transform.localPosition=new Vector3(0,-540,0);
                break;
            case "15":
                obtiempoFaena.SetActive(true);
                obpreguntas.SetActive(true);
                lista= new List<string>();
                lista.Add("10");
                lista.Add("15");
                lista.Add("20");
                obcantPreguntas.GetComponent<UIPopupList>().items=lista;
                obcantPreguntas.GetComponent<UIPopupList>().value=ultimoActual;
                obpreguntas.transform.localPosition=new Vector3(0,-240,0);
                obcantPreguntas.SetActive(true);
                break;
            case "16":

                obtiempoFaena.SetActive(true);
                obpreguntas.SetActive(true);
                obpreguntas.transform.localPosition=new Vector3(0,-140,0);
                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);

                obintCamion.SetActive (true);
                obintCamion.transform.localPosition=new Vector3(0,-1000,0);

                obdescuentoCamion.SetActive(true);
                obdescuentoCamion.transform.localPosition=new Vector3(0,-1100,0);

                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                obTonelajeMax.SetActive(true);
                obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obTonelajeMin.SetActive(true);
                obTonelajeMin.transform.localPosition=new Vector3(0,-440,0);
                obTonelajeTotal.SetActive(true);
                obTonelajeTotal.transform.localPosition=new Vector3(0,-240.9f,0);
                obcaidaPermitida.SetActive(true);
                obcaidaPermitida.transform.localPosition=new Vector3(0,-540,0);
                obcheck1.SetActive(true);
                obcheck1.transform.localPosition=new Vector3(0,-640,0);
                obcheck2.SetActive(true);
                obcheck2.transform.localPosition=new Vector3(0,-740,0);
                break;
            case "17":
                obtiempoFaena.SetActive(true);
                obpreguntas.SetActive(true);
                obpreguntas.transform.localPosition=new Vector3(0,-140,0);

                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);

                obintCamion.SetActive (true);
                obintCamion.transform.localPosition=new Vector3(0,-1000,0);

                    obintCamion.transform.FindChild("Camion").GetComponent<UILabel>().text = "Camion Bajo Perfil";

                    obdescuentoCamion.SetActive(true);
                obdescuentoCamion.transform.localPosition=new Vector3(0,-1100,0);

                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196f,-600,0);
                obdescuentoChoque.SetActive (true);

                obTonelajeMax.SetActive(true);
                obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obTonelajeMin.SetActive(true);
                obTonelajeMin.transform.localPosition=new Vector3(0,-440,0);
                obTonelajeTotal.SetActive(true);
                obTonelajeTotal.transform.localPosition=new Vector3(0,-240.9f,0);
                obcaidaPermitida.SetActive(true);
                obcaidaPermitida.transform.localPosition=new Vector3(0,-540,0);
                obcheck1.SetActive(true);
                obcheck1.transform.localPosition=new Vector3(0,-640,0);
                obcheck2.SetActive(true);
                obcheck2.transform.localPosition=new Vector3(0,-740,0);
                break;
            case "18":
                obtiempoFaena.SetActive(true);
                obpreguntas.SetActive(true);
                obpreguntas.transform.localPosition=new Vector3(0,-140,0);

                obintTunel.SetActive (true);
                obintTunel.transform.localPosition=new Vector3(0,-800,0);

                obdescuentoTunel.SetActive(true);
                obdescuentoTunel.transform.localPosition=new Vector3(0,-900,0);

                obintCamion.SetActive (true);
                obintCamion.transform.FindChild ("Camion").GetComponent<UILabel> ().text = "Camión convencional";
                obintCamion.transform.localPosition=new Vector3(0,-1000,0);

                obdescuentoCamion.SetActive(true);
                obdescuentoCamion.transform.localPosition=new Vector3(0,-1100,0);

                obpostder.SetActive (true);
                obpostizq.SetActive (true);
                obmedioder.SetActive (true);
                obpost.SetActive (true);
                obbalde.SetActive (true);
                obCabina.SetActive (true);
                label.SetActive (true);
                label.transform.localPosition=new Vector3(-196,-600,0);
                obdescuentoChoque.SetActive (true);

                obTonelajeMax.SetActive(true);
                obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obTonelajeMin.SetActive(true);
                obTonelajeMin.transform.localPosition=new Vector3(0,-440,0);
                obTonelajeTotal.SetActive(true);
                obTonelajeTotal.transform.localPosition=new Vector3(0,-240.9f,0);
                obcaidaPermitida.SetActive(true);
                obcaidaPermitida.transform.localPosition=new Vector3(0,-540,0);
                obcheck1.SetActive(true);
                obcheck1.transform.localPosition=new Vector3(0,-640,0);
                obcheck2.SetActive(true);
                obcheck2.transform.localPosition=new Vector3(0,-740,0);
                break;*/
            default:
				
				
			break;
			}

			preguntar = false;
		}

	void reset(){
		numniv.text = "";
		//nombre.value="";
		niv4.text = "";
		
		tiempoVuelta.value= "";
		//tiempoextMin.value= "";
		//tiempoextMax.value= "";
		
		repeticiones.value = "";
		
		zipper.value = "";
		postder.value = "";
		post.value = "";
		postizq.value = "";
		medioder.value = "";
		balde.value = "";
		cabina.value = "";
		descuentoChoque.value = "";

        //TonelajeMax.value = "";
        FallaInducida.value = "Ninguna";
        //TonelajeMax.value = "";
        caidaPermitida.value = "";
		
		intCamion.value = "";
		descuentoCamion.value = "";
		intTunel.value = "";
		descuentoTunel.value = "";
		
		check1.value = "";
		check2.value = "";
		
		preguntas.value = "";
		cantPreguntas.text = "";
	}
	public IEnumerator obtenerNivelEjecutar(){
		WWWForm form = new WWWForm();
		string idniv="";
		gameObject.GetComponent<verNiveles> ().id.TryGetValue (list.GetComponentInChildren<UILabel>().text, out idniv);
        print (list.GetComponentInChildren<UILabel>().text);
        print (idniv);
        //print ("solicitud");
        //print(idniv);
		form.AddField ("id", idniv);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/numeroNivel.php", form);
		yield return download;
		if (download.error != null) {
			print ("Error downloading: " + download.error);
			//mostrarError("Error de conexion");
			return false;
	    } else {
			//print ("hola");
			string retorno = download.text;

			print (retorno);
			string[] ret = retorno.Split (new char[]{'*'});
			numeroNivel = ret [0];
			//print (numeroNivel);
			tiempoVuelta.value=ret[1];
			tiempoFaena.value=ret[2];
			ultimoTiempo4=ret[2];
			niv4.text=ret[2];
			//print(ret[2]);
			//tiempoextMin.value=ret[3];
			//tiempoextMax.value=ret[4];
			repeticiones.value=ret[5];
			zipper.value=ret[6];
			intTunel.value=ret[7];
			intCamion.value=ret[8];
			post.value=ret[9];
			postder.value=ret[10];
			postizq.value=ret[11];
			medioder.value=ret[12];
			cabina.value=ret[13];
			balde.value=ret[14];
			preguntas.value=ret[15];
			cantPreguntas.text=ret[16];
			ultimoActual=ret[16];
			FallaInducida.value=ret[17];
			//TonelajeMax.value=ret[18];
			TonelajeTotal.value=ret[19];
			caidaPermitida.value=ret[20];
			descuentoChoque.value=ret[21];
			check1.value=ret[22];
			check2.value=ret[23];
			descuentoTunel.value=ret[24];
			descuentoCamion.value=ret[25];

						//print(numeroNivel);
					
						preguntar=true;
						escogeNivel ();
						//print ("avisando");
				}

	}
	public IEnumerator ActualizarNivelEjecutar(){
		string idniv="";
		gameObject.GetComponent<verNiveles> ().id.TryGetValue (list.GetComponentInChildren<UILabel>().text, out idniv);
		WWWForm form = new WWWForm();
	
		form.AddField( "tiempoVuelta", tiempoVuelta.value );
		if(preguntas.value != null && preguntas.value != "") preguntas.value = "" + Mathf.Clamp(int.Parse (preguntas.value), 1, 100);
        else preguntas.value = "0";
        zipper.value = "" + Mathf.Clamp(int.Parse (zipper.value), 1, 100);
		postder.value = "" + Mathf.Clamp(int.Parse (postder.value), 1, 100);
		post.value = "" + Mathf.Clamp(int.Parse (post.value), 1, 100);
		postizq.value = "" + Mathf.Clamp(int.Parse (postizq.value), 1, 100);
		balde.value = "" + Mathf.Clamp(int.Parse (balde.value), 1, 100);
		cabina.value = "" + Mathf.Clamp(int.Parse (cabina.value), 1, 100);
		medioder.value = "" + Mathf.Clamp(int.Parse (medioder.value), 1, 100);
		descuentoChoque.value = "" + Mathf.Clamp(int.Parse (descuentoChoque.value), 1, 100);
		intTunel.value = "" + Mathf.Clamp(int.Parse (intTunel.value), 1, 100);
		descuentoTunel.value = "" + Mathf.Clamp(int.Parse (descuentoTunel.value), 1, 100);
		intCamion.value = "" + Mathf.Clamp(int.Parse (intCamion.value), 1, 100);
		descuentoCamion.value = "" + Mathf.Clamp(int.Parse (descuentoCamion.value), 1, 100);
		
		check1.value = "" + Mathf.Clamp(int.Parse (check1.value), 1, 100);
		check2.value = "" + Mathf.Clamp(int.Parse (check2.value), 1, 100);
		caidaPermitida.value = "" + Mathf.Clamp(int.Parse (caidaPermitida.value), 1, 100);

		if (numero.text != "Módulo 4") {
			form.AddField ("tiempoFaena", tiempoFaena.value);
		} else {
			//print (niv4.text);
			form.AddField ("tiempoFaena", niv4.text);
		}
		//form.AddField( "tiempoExtMin", tiempoextMin.value );
		//form.AddField( "tiempoExtMax", tiempoextMax.value );
		
		form.AddField( "tonelaje", TonelajeTotal.value );
		//form.AddField( "cargarMin", TonelajeMin.value );
        form.AddField("fallaInducida", FallaInducida.GetComponentInChildren<UILabel>().text);
        form.AddField ("caidaPer", Mathf.Clamp(int.Parse(caidaPermitida.value), 0, 100) );
		
		form.AddField( "reps", repeticiones.value );
		form.AddField( "zipper", zipper.value );
		form.AddField( "intpd", Mathf.Clamp(int.Parse(postder.value), 0, 100) );
		form.AddField( "intpi", Mathf.Clamp(int.Parse(postizq.value), 0, 100) );
		form.AddField( "intmd", Mathf.Clamp(int.Parse(medioder.value), 0, 100) );
		form.AddField( "intp", Mathf.Clamp(int.Parse(post.value), 0, 100) );
		form.AddField( "intb", Mathf.Clamp(int.Parse(balde.value), 0, 100) );
		form.AddField( "cabina", Mathf.Clamp(int.Parse(cabina.value), 0, 100) );
		
		
		form.AddField( "area", Mathf.Clamp(int.Parse(intTunel.value ), 0, 100) );
		form.AddField( "descArea", Mathf.Clamp(int.Parse(descuentoTunel.value ), 0, 100) );
		form.AddField( "mercedes", Mathf.Clamp(int.Parse(intCamion.value ), 0, 100) );
		form.AddField( "descCamion", Mathf.Clamp(int.Parse(descuentoCamion.value ), 0, 100) );
		
		
		form.AddField( "check1", Mathf.Clamp(int.Parse(check1.value ), 0, 100) );
		form.AddField( "check2", Mathf.Clamp(int.Parse(check2.value ), 0, 100) );
		form.AddField( "descChoque", Mathf.Clamp(int.Parse(descuentoChoque.value ), 0, 100) );		
		form.AddField ("idniv", idniv);
		form.AddField( "preguntas", preguntas.value );
		form.AddField( "cantpreguntas", (cantPreguntas.text == ""? "0": cantPreguntas.text));
		//revisar
		
		reset();
		
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Guardando Módulo ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/editarNivel.php", form);
		yield return download;
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			return false;
		} else {
			string retorno = download.text;
            print(retorno);
			if(retorno!="-3"&&retorno!="-4"&&retorno!="-2"){
				print(retorno);
				popup.GetComponent<UILabel>().text="Módulo Guardado Exitosamente";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
				//gameObject.GetComponent<UIPopupList>().value=" ";
				if(gameObject.transform.name=="Mirar Nivel Admin"){
				//	GameObject.Find("MirarAdminsPopUp").gameObject.GetComponent<UIPopupList>().value=" ";
				}
				apagarTodo ();
			}
			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}
		
	}
}

