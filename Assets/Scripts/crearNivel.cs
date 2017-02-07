using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class crearNivel : MonoBehaviour {
	public GameObject popup;
	public GameObject admin;
	public bool esAdmin=false;
	public GameObject scroll;
	List<string> lista;
	public UILabel numero;
	public UILabel niv4;
	public UIInput nombre;

	public UILabel cantPreguntas;
	public UIInput preguntas;//exito preguntas
	public UIInput repeticiones;
	/*tiempos*/
	public UIInput tiempoVuelta;
	public UIInput tiempoFaena;//tiempo faena = tiempo preguntas = tiempo total
	//public UIInput tiempoextMin;
	//public UIInput tiempoextMax;


	/*integridad maquina*/
	//public UIInput zipper;
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
    public UIPopupList FallaInducida;
    //public UIInput TonelajeMin;
    //public UIInput TonelajeMax;
    public UIInput caidaPermitida;




	public GameObject obtiempoVuelta;
	public GameObject obtiempoFaena;
	//public GameObject obtiempoextMin;
	//public GameObject obtiempoextMax;

	public GameObject obrepeticiones;

	//public GameObject obzipper;
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
	public GameObject obFallaInducida;
	//public GameObject obTonelajeMax;
	public GameObject obcaidaPermitida;

	public GameObject obintTunel;
	public GameObject obdescuentoTunel;
	public GameObject obintCamion;
	public GameObject obdescuentoCamion;

	public GameObject obcantPreguntas;
	public GameObject obpreguntas;
	public GameObject tiempo4;



	void Start () {
		apagarTodo ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}
	void apagarTodo(){
		obtiempoVuelta.SetActive (false);
		obtiempoVuelta.transform.localPosition=new Vector3(0,-240,0);
		//obtiempoextMin.SetActive (false);
		//obtiempoextMax.SetActive (false);
		obtiempoFaena.SetActive (false);
		obtiempoFaena.transform.localPosition=new Vector3(0,-40,0);
		tiempo4.SetActive (false);


		obrepeticiones.SetActive (false);
		obrepeticiones.transform.localPosition=new Vector3(0,-140,0);

		//obzipper.SetActive (false);
		//obzipper.transform.localPosition=new Vector3(0,-340,0);
		obpostder.SetActive (false);
		obpostizq.SetActive (false);
		obmedioder.SetActive (false);
		obpost.SetActive (false);
		obbalde.SetActive (false);
		label.SetActive (false);
		obCabina.SetActive (false);
		obdescuentoChoque.SetActive (false);

		obintCamion.SetActive (false);
		obintCamion.transform.FindChild ("Camion").GetComponent<UILabel> ().text = "Camion Bajo Perfil";
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

	}
	public void escogeNivel(){
		apagarTodo ();
		//print (numero.text);
		switch (numero.text) {
		case "1":
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
			obcantPreguntas.GetComponent<UIPopupList>().value="10";

			break;
		case "2":
			//obtiempoFaena.SetActive(true);			
			obpreguntas.SetActive(true);
			obpreguntas.transform.localPosition=new Vector3(0,-240,0);
			obcantPreguntas.SetActive(true);
			lista= new List<string>();
			lista.Add("10");
			lista.Add("12");
			lista.Add("15");
			obcantPreguntas.GetComponent<UIPopupList>().items=lista;
			obcantPreguntas.GetComponent<UIPopupList>().value="10";
			obcantPreguntas.transform.localPosition=new Vector3(0,-140,0);
			break;
		case "3":
			//obtiempoFaena.SetActive(true);			
			obpreguntas.SetActive(true);
			obpreguntas.transform.localPosition=new Vector3(0,-240,0);
			lista= new List<string>();
			lista.Add("10");
			lista.Add("12");
			lista.Add("15");
			obcantPreguntas.GetComponent<UIPopupList>().items=lista;
			obcantPreguntas.GetComponent<UIPopupList>().value="10";
			obcantPreguntas.SetActive(true);
			obcantPreguntas.transform.localPosition=new Vector3(0,-140,0);
			break;
		case "4":
			//corregir
			tiempo4.SetActive(true);
			tiempo4.transform.localPosition=new Vector3(0,-40,0);
			obcheck1.SetActive(true);
			obcheck1.transform.localPosition=new Vector3(0,-140,0);

			break;
		case "5":
			obtiempoFaena.SetActive(true);
			break;
		case "6":
			obrepeticiones.SetActive (true);
			obtiempoVuelta.SetActive(true);
			obtiempoFaena.SetActive(true);

            //obzipper.SetActive (true);
            //obzipper.transform.localPosition=new Vector3(0,-300,0);
            obintTunel.SetActive(true);
            obintTunel.transform.localPosition = new Vector3(0, -800, 0);

            obdescuentoTunel.SetActive(true);
            obdescuentoTunel.transform.localPosition = new Vector3(0, -900, 0);

            obpostder.SetActive (true);
			obpostizq.SetActive (true);
			obmedioder.SetActive (true);
			obpost.SetActive (true);
			obbalde.SetActive (true);
			obCabina.SetActive (true);
			label.SetActive (true);
			label.transform.localPosition=new Vector3(-196,-700,0);
			obdescuentoChoque.SetActive (true);

			break;
		case "7": case "8":
                obrepeticiones.SetActive (true);
			obtiempoVuelta.SetActive(true);
			obtiempoFaena.SetActive(true);

            obFallaInducida.SetActive(true);
            obFallaInducida.transform.localPosition = new Vector3(0, -440, 0);
            obTonelajeTotal.SetActive(true);
            obTonelajeTotal.transform.localPosition = new Vector3(0, -340f, 0);
            obcaidaPermitida.SetActive(true);
            obcaidaPermitida.transform.localPosition = new Vector3(0, -540, 0);
            obcheck1.SetActive(true);
            obcheck1.transform.localPosition = new Vector3(0, -640, 0);
            obcheck2.SetActive(true);
            obcheck2.transform.localPosition = new Vector3(0, -740, 0);
            //obzipper.SetActive (true);
            //obzipper.transform.localPosition=new Vector3(0,-300,0);
            obpostder.SetActive (true);
			obpostizq.SetActive (true);
			obmedioder.SetActive (true);
			obpost.SetActive (true);
			obbalde.SetActive (true);
			obCabina.SetActive (true);
			label.SetActive (true);
			label.transform.localPosition=new Vector3(-196,-700,0);
			obdescuentoChoque.SetActive (true);

            obintTunel.SetActive(true);
            obintTunel.transform.localPosition = new Vector3(0, -800, 0);

            obdescuentoTunel.SetActive(true);
            obdescuentoTunel.transform.localPosition = new Vector3(0, -900, 0);

            obintCamion.SetActive(true);
            obintCamion.transform.localPosition = new Vector3(0, -1000, 0);

            obdescuentoCamion.SetActive(true);
            obdescuentoCamion.transform.localPosition = new Vector3(0, -1100, 0);
            break;
		/*case "8":
			obrepeticiones.SetActive (true);
			obtiempoVuelta.SetActive(true);
			obtiempoFaena.SetActive(true);
			
		//	obzipper.transform.localPosition=new Vector3(0,-300,0);
			//obzipper.SetActive (true);
			
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
			obcantPreguntas.GetComponent<UIPopupList>().value="10";
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

			//obTonelajeMax.SetActive(true);
			//obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
            obFallaInducida.SetActive(true);
            obFallaInducida.transform.localPosition=new Vector3(0,-440,0);
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

                //obTonelajeMax.SetActive(true);
                //obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obFallaInducida.SetActive(true);
                obFallaInducida.transform.localPosition = new Vector3(0, -440, 0);
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
			obintCamion.transform.FindChild ("Camion").GetComponent<UILabel> ().text = "Camion";
			
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

                //obTonelajeMax.SetActive(true);
                //obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obFallaInducida.SetActive(true);
                obFallaInducida.transform.localPosition = new Vector3(0, -440, 0);
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
			obcantPreguntas.GetComponent<UIPopupList>().value="10";
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

                //obTonelajeMax.SetActive(true);
                //obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obFallaInducida.SetActive(true);
                obFallaInducida.transform.localPosition = new Vector3(0, -440, 0);
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

                //obTonelajeMax.SetActive(true);
                //obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obFallaInducida.SetActive(true);
                obFallaInducida.transform.localPosition = new Vector3(0, -440, 0);
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
			obintCamion.transform.FindChild ("Camion").GetComponent<UILabel> ().text = "Camión";
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
			
			obdescuentoChoque.SetActive (true);

                //obTonelajeMax.SetActive(true);
                //obTonelajeMax.transform.localPosition=new Vector3(0,-340,0);
                obFallaInducida.SetActive(true);
                obFallaInducida.transform.localPosition = new Vector3(0, -440, 0);
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

			apagarTodo();
		break;
			scroll.transform.FindChild("Grid1").gameObject.GetComponent<UIGrid>().Reposition ();
			scroll.transform.FindChild("Grid2").gameObject.GetComponent<UIGrid>().Reposition ();
			scroll.GetComponent<UIScrollView> ().ResetPosition ();
		}
	}
	public void crear(){
		esAdmin = false;
		StartCoroutine (crearNivelEjecutar ());
	}
	public void Admincrear(){
		esAdmin = true;
		StartCoroutine (crearNivelEjecutar ());
	}
	void reset(){
		numero.text = "";
		nombre.value="";
		niv4.text = "";

		tiempoVuelta.value= "";
		//tiempoextMin.value= "";
		//tiempoextMax.value= "";

		repeticiones.value = "";

		//zipper.value = "";
		postder.value = "";
		post.value = "";
		postizq.value = "";
		medioder.value = "";
		balde.value = "";
		cabina.value = "";
		descuentoChoque.value = "";

        //TonelajeMax.value = "";
        FallaInducida.value = "Ninguna";
		TonelajeTotal.value = "";
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
	public IEnumerator crearNivelEjecutar(){

		WWWForm form = new WWWForm();
		if (!esAdmin) {
			form.AddField ("admin", PlayerPrefs.GetString ("idAdmin"));
		} else {
			//print (admin.GetComponent<verNiveles>().getIDADMIN());
			if(admin.GetComponent<UIPopupList>().value!=""&&admin.GetComponent<verNiveles>().getIDADMIN()!=""){
				form.AddField ("admin", admin.GetComponent<verNiveles>().getIDADMIN());
			}
			else{
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Debe Seleccionar un Instructor";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
				yield return false;
			}
		}
		if (nombre.value != "") {

			form.AddField ("numero", "Módulo " + numero.text);
			form.AddField ("nombre", nombre.value);

            //form.AddField("tiempoEmin", "0");
            //form.AddField("tiempoEmax", "0"); 
            form.AddField("orden", "0");


            form.AddField ("tiempoVuelta", tiempoVuelta.value == "" ? "0" : tiempoVuelta.value);
						if (numero.text != "4") {
								form.AddField ("tiempoFaena", tiempoFaena.value);
						} else {
								form.AddField ("tiempoFaena", niv4.text);
						}
						//form.AddField( "tiempoExtMin", tiempoextMin.value );
						//form.AddField( "tiempoExtMax", tiempoextMax.value );

						form.AddField ("tonelaje", TonelajeTotal.value == "" ? "0" : TonelajeTotal.value);
            //form.AddField ("cargarMin", TonelajeMin.value == "" ? "0" : TonelajeMin.value);
            print(FallaInducida.value);
						form.AddField ("fallaInducida", FallaInducida.value);
						form.AddField ("caidaPer", Mathf.Clamp(int.Parse(caidaPermitida.value == "" ? "0" : caidaPermitida.value), 0, 100) );

						form.AddField ("reps", repeticiones.value == "" ? "0" : repeticiones.value);
						//form.AddField ("zipper", zipper.value == "" ? "0" : zipper.value);
						form.AddField ("intpd",Mathf.Clamp(int.Parse( postder.value == ""?"0":postder.value), 0, 100) );
						form.AddField ("intpi", Mathf.Clamp(int.Parse(postizq.value == "" ? "0" : postizq.value), 0, 100) );
						form.AddField ("intmd", Mathf.Clamp(int.Parse(medioder.value == "" ? "0" : medioder.value), 0, 100) );
						form.AddField ("intp", Mathf.Clamp(int.Parse(post.value == "" ? "0" : post.value), 0, 100) );
						form.AddField ("intb", Mathf.Clamp(int.Parse(balde.value == "" ? "0" : balde.value), 0, 100) );
						form.AddField ("cabina", Mathf.Clamp(int.Parse(cabina.value == "" ? "0" : cabina.value), 0, 100) );


						form.AddField ("area", Mathf.Clamp(int.Parse(intTunel.value == "" ? "0" : intTunel.value), 0, 100) );
						form.AddField ("descArea", Mathf.Clamp(int.Parse(descuentoTunel.value == "" ? "0" : descuentoTunel.value), 0, 100) );
						form.AddField ("mercedes", Mathf.Clamp(int.Parse(intCamion.value == "" ? "0" : intCamion.value), 0, 100) );
						form.AddField ("descCamion", Mathf.Clamp(int.Parse(descuentoCamion.value == "" ? "0" : descuentoCamion.value), 0, 100));


						form.AddField ("check1", Mathf.Clamp(int.Parse(check1.value == "" ? "0" : check1.value), 0, 100) );
						form.AddField ("check2", Mathf.Clamp(int.Parse(check2.value == "" ? "0" : check2.value), 0, 100) );
						form.AddField ("descChoque", Mathf.Clamp(int.Parse(balde.value == "" ? "0" : balde.value), 0, 100) );





						form.AddField ("preguntas", preguntas.value == "" ? "0" : preguntas.value);
						if(numero.text == "16" || numero.text == "17" || numero.text == "18") cantPreguntas.text = "4";
						form.AddField ("cantpreguntas", cantPreguntas.text == "" ? "0" : cantPreguntas.text);
						//revisar


						popup.SetActive (true);
						popup.GetComponent<UILabel> ().text = "Guardando Módulo ";
						popup.transform.FindChild ("Boton").gameObject.SetActive (false);
			
						WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/crearNivel.php", form);
						yield return download;
						//print(download.text);
						if (download.error != null) {
								print ("Error downloading: " + download.error);
								//mostrarError("Error de conexion");
				yield return false;
						} else {
								string retorno = download.text;
								print (retorno);
								if (retorno != "-3" && retorno != "-4" && retorno != "-2" && retorno != "ya creado") {
										print (retorno);
										popup.GetComponent<UILabel> ().text = "Módulo Guardado Exitosamente";
										popup.transform.FindChild ("Boton").gameObject.SetActive (true);
										apagarTodo ();
										reset ();
								}
                                else
                                {
                                    popup.GetComponent<UILabel>().text = "Error, revise los campos ingresados";
                                    popup.transform.FindChild("Boton").gameObject.SetActive(true);
                                }
                if (retorno == "ya creado") {
										popup.GetComponent<UILabel> ().text = "No se ha podido guardar el módulo debido a que ya existe uno con el mismo nombre";
										popup.transform.FindChild ("Boton").gameObject.SetActive (true);
								}
								//comprueba si lo que devuelve es informacion de alguien que existe
				
						}
				
			} else {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Debe Ingresar un Nombre para el Módulo";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			}
		
	}
}
