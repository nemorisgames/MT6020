using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class cargarPreguntas : MonoBehaviour {

	public int cantidad=15;
	public string modulo="1";
	 int paginaActual;
	 int maxPaginas;
	 int tiempo;
	List<GameObject> preguntas;
	public GameObject atras;
	public GameObject siguiente;
	public GameObject terminar;
	public GameObject popup;
	Configuracion conf;

	public UIToggle[] respuestasCorrectasFijas;

	public bool cargarPreguntasPHP = true;
	//coordenadas preguntas
	//x1=-505 y1=307
	//x2=420 y2=307
	//x3=-505 y3=-63
	//x4= 420 y4=-63

	void Start () {
		GameObject confi=GameObject.FindGameObjectWithTag("Configuracion");
		conf=confi.GetComponent<Configuracion>();
		cantidad = conf.CantidadPreguntas;
		modulo =conf.NumeroModulo;
		tiempo = conf.TiempoFaena *60;
		//tiempo = 121;
		paginaActual = 0;
		maxPaginas=(int)cantidad/4;
		if(cantidad%4==0){ //si el resto es 0,significa que es uno menos 
			maxPaginas=maxPaginas-1; 
		}
		atras.SetActive (false);//atras.GetComponent<UIButton> ().enabled = false;
		if(cargarPreguntasPHP)
			cargarPreguntasClick ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setearPreguntas(){
		//print (preguntas.Count);
		for (int i=0; i<cantidad; i++) {
			preguntas[i].SetActive(false);
		}
		preguntas [(paginaActual *4)].SetActive (true);
		preguntas [(paginaActual *4)].transform.localPosition = new Vector3 (-505, 307, 0);
		preguntas [(paginaActual *4)+1].SetActive (true);
		preguntas [(paginaActual *4)+1].transform.localPosition = new Vector3 (420, 307, 0);
		preguntas [(paginaActual *4)+2].SetActive (true);
		preguntas [(paginaActual *4)+2].transform.localPosition = new Vector3 (-505, -63, 0);
		preguntas [(paginaActual *4)+3].SetActive (true);
		preguntas [(paginaActual *4)+3].transform.localPosition = new Vector3 (420, -63, 0);
	}
	public void avanzarPagina(){//avanza la pagina (muestra los siguientes 4 preguntas)
		//apagar actuales
		if (paginaActual * 4 < preguntas.Count)	preguntas [(paginaActual *4)].SetActive (false);
		if (paginaActual * 4 +1< preguntas.Count)preguntas [(paginaActual *4)+1].SetActive (false);
		if (paginaActual * 4 +2 < preguntas.Count)preguntas [(paginaActual *4)+2].SetActive (false);
		if (paginaActual * 4 +3< preguntas.Count)preguntas [(paginaActual *4)+3].SetActive (false);
		//prender siguientes
		paginaActual++;
		if (paginaActual == maxPaginas)
			siguiente.SetActive (false);
			//siguiente.GetComponent<UIButton> ().enabled = false;

		if (paginaActual * 4 < preguntas.Count) {
			preguntas [(paginaActual * 4)].SetActive (true);
			preguntas [(paginaActual * 4)].transform.localPosition = new Vector3 (-505, 307, 0);
		}
		if (paginaActual * 4 + 1 < preguntas.Count) {
			preguntas [(paginaActual * 4) + 1].SetActive (true);
			preguntas [(paginaActual * 4) + 1].transform.localPosition = new Vector3 (420, 307, 0);
		}
		if(paginaActual * 4 + 2 < preguntas.Count){
			preguntas [(paginaActual *4)+2].SetActive (true);
			preguntas [(paginaActual *4)+2].transform.localPosition = new Vector3 (-505, -63, 0);
		}
		if(paginaActual * 4 + 3 < preguntas.Count ){
			preguntas [(paginaActual *4)+3].SetActive (true);
			preguntas [(paginaActual *4)+3].transform.localPosition = new Vector3 (420, -63, 0);
		}
		preguntar ();
	}
	public void volverPagina(){//muestra las 4 preguntas anteriores
		//apagar actuales
		if (paginaActual * 4 < preguntas.Count)	preguntas [(paginaActual *4)].SetActive (false);
		if (paginaActual * 4 +1< preguntas.Count)preguntas [(paginaActual *4)+1].SetActive (false);
		if (paginaActual * 4 +2 < preguntas.Count)preguntas [(paginaActual *4)+2].SetActive (false);
		if (paginaActual * 4 +3< preguntas.Count)preguntas [(paginaActual *4)+3].SetActive (false);
		//prender siguientes
		paginaActual--;
		if (paginaActual == 0)
			atras.SetActive (false);
			//atras.GetComponent<UIButton> ().enabled = false;
		if (paginaActual == maxPaginas)
			siguiente.SetActive (false);
			//siguiente.GetComponent<UIButton> ().enabled = false;
		
		if (paginaActual * 4 < preguntas.Count) {
			preguntas [(paginaActual * 4)].SetActive (true);
			preguntas [(paginaActual * 4)].transform.localPosition = new Vector3 (-505, 307, 0);
		}
		if (paginaActual * 4 + 1 < preguntas.Count) {
			preguntas [(paginaActual * 4) + 1].SetActive (true);
			preguntas [(paginaActual * 4) + 1].transform.localPosition = new Vector3 (420, 307, 0);
		}
		if(paginaActual * 4 + 2 < preguntas.Count){
			preguntas [(paginaActual *4)+2].SetActive (true);
			preguntas [(paginaActual *4)+2].transform.localPosition = new Vector3 (-505, -63, 0);
		}
		if(paginaActual * 4 + 3 < preguntas.Count ){
			preguntas [(paginaActual *4)+3].SetActive (true);
			preguntas [(paginaActual *4)+3].transform.localPosition = new Vector3 (420, -63, 0);
		}
		preguntar ();
	}
	void preguntar(){
		if (paginaActual > 0)
			atras.SetActive (true);// atras.GetComponent<UIButton> ().enabled = true;
		if (paginaActual < maxPaginas)
			siguiente.SetActive (true);// siguiente.GetComponent<UIButton> ().enabled = true;
	}
	public void cargarPreguntasClick(){
		preguntas = new List<GameObject> ();
		StartCoroutine (preguntasPHP());
	}
	IEnumerator preguntasPHP(){
		WWWForm form = new WWWForm();
		form.AddField ("numeroNivel", modulo);
		form.AddField ("cantidad", cantidad);
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Preparando Evaluación";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		popup.transform.FindChild ("Boton2").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/cargarPreguntas.php", form);
		yield return download;


		string retorno = download.text;
		print (retorno);
		string[] ret = retorno.Split(new char[]{'|'});
		for(int i=0;i<ret.Length-1;i++){
			string[] ret2=ret[i].Split(new char[]{'*'});
			//print (ret2[0]);
			//print (ret2[1]);
			string correcto=ret2[1];//resp correcta
			for(int j=1;j<ret2.Length;j++){
				string temp=ret2[j];
				int r= Random.Range(j,ret2.Length);
				ret2[j]=ret2[r];
				ret2[r]=temp;
			}
			/*for(int j=0;j<ret2.Length;j++){
				print (ret2[j]);
			}*/

			preguntas.Add((GameObject)Instantiate(Resources.Load("Pregunta", typeof(GameObject))));
			preguntas[i].transform.parent=gameObject.transform;
			preguntas[i].transform.localScale=new Vector3(1,1,1);
			preguntas[i].gameObject.transform.FindChild("Banner").transform.FindChild("NumeroPregunta").GetComponent<UILabel>().text="Pregunta "+(i+1).ToString();
			preguntas[i].gameObject.transform.FindChild("Banner").transform.FindChild("NumeroPregunta").gameObject.AddComponent<UILocalize> ();
			preguntas[i].gameObject.transform.FindChild("Banner").transform.FindChild("NumeroPregunta").GetComponent<UILocalize>().key="Pregunta "+(i+1).ToString();
			preguntas[i].GetComponent<Preguntas>().setGrupo(i+1);
			print (ret2[0]+","+ret2[1]+","+ret2[2]+","+ret2[3]+","+ret2[4]);
			print (preguntas.Count + " " + (preguntas[i] == null) + " " + (preguntas[i].GetComponent<Preguntas>() == null));
			preguntas[i].GetComponent<Preguntas>().setPreguntas(ret2[0],ret2[1],ret2[2],ret2[3],ret2[4],correcto);

		}
		setearPreguntas ();

		popup.GetComponent<UILabel>().text="Pulse botón para comenzar la evaluación";
		popup.transform.FindChild ("Boton").gameObject.SetActive (true);

	}
	public void comenzarPrueba(){
		terminar.GetComponent<timer> ().comenzar (tiempo);
	}
	public void finTiempo(){
		//terminar.GetComponent<timer> ().start = false;
		float promedio;
		int buenos = 0;
		for (int i=0; i<cantidad; i++) {
			if(preguntas[i].GetComponent<Preguntas>().Correcto) buenos++;
		}
		promedio = buenos * 100 / cantidad;
		conf.guardarHistorial ();
		conf.ResultadoPreguntas = (int)promedio;
		conf.ResultadoTiempo = tiempo  - (int)(terminar.GetComponent<timer> ().tiempototal)/60; //tiempo en minutos, tiempoActual esta en segundos
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Usted obtuvo un porcentaje de aprobación de: "+promedio.ToString()+"%";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		popup.transform.FindChild ("Boton2").gameObject.SetActive (true);

	}
	public void entregarResultados(){
		if (cargarPreguntasPHP) {
			terminar.GetComponent<timer> ().start = false;
			float promedio;
			int buenos = 0;
            int nulas = 0;
			for (int i=0; i<cantidad; i++) {
				if (preguntas [i].GetComponent<Preguntas> ().Correcto)
					buenos++;
                if (preguntas[i].GetComponent<Preguntas>().esNula())
                    nulas++;
			}
			promedio = buenos * 100 / cantidad;
            print(promedio);
			conf.ResultadoPreguntas = (int)promedio;
			float tiempoaux = terminar.GetComponent<timer> ().tiempototal;
			print (tiempoaux);
			tiempoaux = tiempo - tiempoaux;
			print (tiempo);
			print (tiempoaux);
            conf.ResultadoNumPreguntasContestadas = cantidad - nulas;
            conf.ResultadoTiempo = (int)tiempoaux; //tiempo en segundos, tiempoActual esta en segundos
			conf.guardarHistorial ();
			popup.SetActive (true);
			popup.GetComponent<UILabel> ().text = "Usted obtuvo un porcentaje de aprobación de: " + promedio.ToString () + "%";
			popup.transform.FindChild ("Boton").gameObject.SetActive (false);
			popup.transform.FindChild ("Boton2").gameObject.SetActive (true);
		} else {
			if(respuestasCorrectasFijas != null){
				int correctas = 0;
				for(int i = 0; i < respuestasCorrectasFijas.Length; i++){
					if(respuestasCorrectasFijas[i].value)
						correctas++;
				}
				int puntaje = (correctas * 100) / respuestasCorrectasFijas.Length;
                //To Do: enviar respuestas
                //GameObject.Find("Central").GetComponent<Central>().preguntasTerminadas(puntaje, respuestasCorrectasFijas[0].value?1:0, respuestasCorrectasFijas[1].value ? 1 : 0, respuestasCorrectasFijas[2].value ? 1 : 0, respuestasCorrectasFijas[3].value ? 1 : 0);
			}
		}
	}
}
