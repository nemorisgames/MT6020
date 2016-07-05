using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class verNiveles : MonoBehaviour {
	public Dictionary<string,string> id=new Dictionary<string,string>();
	public UIPopupList list;
	public GameObject popup;
	public GameObject niveles;
	public string IDADMIN;
	List<string> miLista=new List<string>();
	
	// Use this for initialization
	void Start() {
		miLista = new List<string> ();
		id = new Dictionary<string,string > ();
		if (gameObject.name == "MirarAdminsPopUp") {
			print("entre");
			StartCoroutine (verAdminEjecutar());
		}
		else if (gameObject.name == "Mirar Nivel Admin") {

		}
		else {

			StartCoroutine (verNivelesEjecutar ());
		}
		
	}

	
	// Update is called once per frame
	void Update () {
		
	}
	public void verNivel(){
		StartCoroutine (verNivelesEjecutar ());
	}
	void actualizarOpciones(){
		list.items = miLista;
	}
	public void verNivelAdmin(){
		StartCoroutine (verNivelesEjecutar2 ());
	}
	public void escogeAdmin(){
		string aux="";
		id.TryGetValue (gameObject.GetComponent<UIPopupList> ().value, out aux);
		niveles.GetComponent<verNiveles> ().IDADMIN = aux;
		niveles.GetComponent<verNiveles> ().verNivelAdmin ();
	}
	public string getIDADMIN(){
		string aux="";
		if (gameObject.GetComponent<UIPopupList>().value!="") {
			id.TryGetValue (gameObject.GetComponent<UIPopupList> ().value, out aux);
		}
		return aux;
	}
	public void eliminarNivel(){
		StartCoroutine (eliminarNivelEjecutar ());
	}
	IEnumerator verAdminEjecutar(){
		WWWForm form = new WWWForm();
		form.AddField ("id", "Administrador");
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Buscando Administradores ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/verAdmins.php", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			return false;
		} 
		else {
			//print ("hola");
			string retorno = download.text;
			//print (retorno);
			string[] ret = retorno.Split(new char[]{'*'});
			
			for(int i=0;i<ret.Length-1;i++){
				//print (ret[i]);
				string[] ret2=ret[i].Split(new char[]{'|'});
				//print (ret2[0]);
				//print (ret2[1]);
				popup.GetComponent<UILabel>().text="Cargados Exitosamente ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
				int aux=2;
				string original=ret2[1];
				while(id.ContainsKey(ret2[1])){
					ret2[1]=original+" "+aux.ToString();
					aux++;
				}
				id.Add (ret2[1],ret2[0]);//para cada nombre guardo el ID del alumno correspondiente
				miLista.Add(ret2[1]);
			}
			actualizarOpciones();
			
			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}
	}
	IEnumerator eliminarNivelEjecutar(){
		WWWForm form = new WWWForm();
		string idnivel;
        print(list.GetComponentInChildren<UILabel>().text);
		id.TryGetValue (list.GetComponentInChildren<UILabel>().text, out idnivel);
		print (idnivel);
		form.AddField( "id", idnivel);
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Eliminando Módulo ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/eliminarNivel.php", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			return false;
		} 
		else {
			//print ("hola");
			string retorno = download.text;
			print (retorno);	
			StartCoroutine(verNivelesEjecutar());
			popup.GetComponent<UILabel>().text="Módulo Eliminado Exitosamente";
			gameObject.GetComponent<editarNivel>().apagarTodo();
			//gameObject.GetComponent<UIPopupList>().value=" ";
			if(gameObject.transform.name=="Mirar Nivel Admin"){
			//	GameObject.Find("MirarAdminsPopUp").gameObject.GetComponent<UIPopupList>().value=" ";
			}
			//popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}
	}
	IEnumerator verNivelesEjecutar(){

		WWWForm form = new WWWForm();
		form.AddField( "id", PlayerPrefs.GetString("idAdmin") );
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Cargando Módulos ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/verNiveles.php", form);
		yield return download;
		miLista = new List<string> ();
		id = new Dictionary<string,string> ();
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			return false;
		} else {
			//print ("hola");
			string retorno = download.text;
			print (retorno);
			string[] ret = retorno.Split(new char[]{'*'});
			
			for(int i=0;i<ret.Length-1;i++){
				//print (ret[i]);
				string[] ret2=ret[i].Split(new char[]{'|'});
				//print (ret2[0]);
				//print (ret2[1]);

				id.Add (ret2[1],ret2[0]);//para cada nombre guardo el ID del alumno correspondiente
				miLista.Add(ret2[1]);

				
			}
			actualizarOpciones();
			popup.GetComponent<UILabel>().text="Cargados Exitosamente ";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			
			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}

		
	}
	IEnumerator verNivelesEjecutar2(){
		
				WWWForm form = new WWWForm ();
				form.AddField ("id", IDADMIN);
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Cargando Módulos ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (false);
				WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/verNiveles.php", form);
				yield return download;
				miLista = new List<string> ();
				id = new Dictionary<string,string > ();
				//print(download.text);
				if (download.error != null) {
						print ("Error downloading: " + download.error);
						//mostrarError("Error de conexion");
						return false;
				} else {
						//print ("hola");
						string retorno = download.text;
						//print (retorno);
						string[] ret = retorno.Split (new char[]{'*'});
			
						for (int i=0; i<ret.Length-1; i++) {
								//print (ret[i]);
								string[] ret2 = ret [i].Split (new char[]{'|'});
								//print (ret2[0]);
								//print (ret2[1]);
				
								id.Add (ret2 [1], ret2 [0]);//para cada nombre guardo el ID del alumno correspondiente
								miLista.Add (ret2 [1]);
				
				
						}
						actualizarOpciones ();
						popup.GetComponent<UILabel> ().text = "Cargados Exitosamente ";
						popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			
						//comprueba si lo que devuelve es informacion de alguien que existe
			
				}
		}
}
