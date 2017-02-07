using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class vincular : MonoBehaviour {
	public GameObject alumnos;
	public GameObject niveles;
	public GameObject popup;
	public GameObject interfazOperador;
	string idalumno;
	string idnivel;
	Configuracion conf;
	// Use this for initialization
	void Start () {
		GameObject confi=GameObject.FindGameObjectWithTag("Configuracion");
		conf=confi.GetComponent<Configuracion>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public IEnumerator vincularNivelEjecutar(){
		
		WWWForm form = new WWWForm();
		form.AddField( "idNivel", idnivel  );
		form.AddField( "idAlumno", idalumno );
		
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Vinculando Módulo ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/vincularNivel.php", form);
		yield return download;
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			popup.GetComponent<UILabel>().text="Módulo Vinculado Exitosamente";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			print (download.text);
			StartCoroutine(cargarConfiguracion());
		}
		
	}
	IEnumerator cargarConfiguracion(){
		WWWForm form = new WWWForm();


		//print (list.value);
		//print (idniv);
		print ("solicitud");
		form.AddField ("id", idnivel);
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

			//print (int.Parse (ret[0]));
			conf.idModulo=idnivel;
			conf.alumno=idalumno;
			conf.NumeroModulo = ret[0];
			//print (numeroNivel);
			conf.TiempoVuelta=int.Parse (ret[1]);
			conf.TiempoFaena=int.Parse (ret[2]);

			conf.CantidadVueltas=int.Parse (ret[5]);
            /*
			conf.ChoqueZipper=int.Parse (ret[6]);
			conf.IntAreaExtraccion=int.Parse (ret[7]);
			conf.IntCamion=int.Parse (ret[8]);
			conf.IntPosterior=int.Parse (ret[9]);
			conf.IntPostDer=int.Parse (ret[10]);
			conf.IntPostIzq=int.Parse (ret[11]);
			conf.IntMedioDer=int.Parse (ret[12]);
			conf.IntCabina=int.Parse (ret[13]);
			conf.IntBrazo=int.Parse (ret[14]);
            */
			if (ret[15] != "") conf.ExitoPreguntas=int.Parse (ret[15]);
            if (ret[16] != "") conf.CantidadPreguntas=int.Parse (ret[16]);
			conf.fallaOperacion=(ret[17]);
			//conf.MaximoCargar=int.Parse (ret[18]);
			conf.TonelajeTotal=int.Parse (ret[19]);
			conf.CaidaPermitida=int.Parse (ret[20]);
			conf.DescuentoChoque=int.Parse (ret[21]);
			conf.check1=int.Parse (ret[22]);
			conf.check2=int.Parse (ret[23]);
			conf.DescuentoTunel=int.Parse (ret[24]);
            /*
			conf.DescuentoCamion=int.Parse (ret[25]);
            conf.IntCamioneta = int.Parse(ret[26]==""?"0":ret[26]);
            conf.DescuentoCamioneta = int.Parse(ret[27] == "" ? "0" : ret[27]);
            */
            //interfazOperador.SendMessage("simulacionConfigurada");
            print(ret[0]);
			if(ret[0]=="Módulo 1"){
				SceneManager.LoadScene("Modulo1");
			}
			if(ret[0]== "Módulo 2")
            {
				SceneManager.LoadScene("Modulo2");
			}
			if(ret[0]== "Módulo 3")
            {
				SceneManager.LoadScene("Modulo3");
			}
			if(ret[0]== "Módulo 4")
            {
				SceneManager.LoadScene("Modulo4");
			}
			if(ret[0]== "Módulo 5")
            {
				SceneManager.LoadScene("Modulo5");
			}
			if(ret[0]== "Módulo 6")
            {
				SceneManager.LoadScene("Modulo6");
			}
			if(ret[0]== "Módulo 7")
            {
				SceneManager.LoadScene("Modulo7");
			}
			if(ret[0]== "Módulo 8")
            {
				SceneManager.LoadScene("Modulo8");
			}
			/*if(ret[0]=="9"){
				SceneManager.LoadScene("Modulo9");
			}
			if(ret[0]=="10"){
				SceneManager.LoadScene("Modulo10");
			}
			if(ret[0]=="11"){
				SceneManager.LoadScene("Modulo11");
			}
			if(ret[0]=="12"){
				SceneManager.LoadScene("Modulo12");
			}
			if(ret[0]=="13"){
				SceneManager.LoadScene("Modulo13");
			}
			if(ret[0]=="14-a"){
				conf.camionConvencionalSeleccionado = true;
				SceneManager.LoadScene("Modulo14");
			}
			if(ret[0]=="14-b"){
				conf.camionConvencionalSeleccionado = false;
				SceneManager.LoadScene("Modulo14");
			}
			if(ret[0]=="15"){
				SceneManager.LoadScene("Modulo15");
			}
			if(ret[0]=="16"){
				SceneManager.LoadScene("Modulo16");
			}
			if(ret[0]=="17"){
				SceneManager.LoadScene("Modulo17");
			}
			if(ret[0]=="18"){
				SceneManager.LoadScene("Modulo18");
			}*/

			print ("avisando");
		}

	}
	public void vincularNivel(){
		//como ya no se cinvula (por el momento) se salta el insert de BD y procede altiro a cargar la info
		//string aux = alumnos.GetComponent<UIPopupList> ().value;
		//alumnos.GetComponent<verAlumnos> ().id.TryGetValue (aux,out idalumno);
		if (conf.alumno == null || conf.alumno == "")
						return;
		string aux = niveles.GetComponent<UIPopupList> ().GetComponentInChildren<UILabel>().text;
        print(aux);
		idalumno = conf.alumno;
		niveles.GetComponent<verNiveles> ().id.TryGetValue (aux,out idnivel);
		print (idalumno);
		print (idnivel);
		//StartCoroutine (vincularNivelEjecutar ());
		StartCoroutine (cargarConfiguracion ());
	}
}
