 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class verAlumnos : MonoBehaviour {
	public Dictionary<string,string> id=new Dictionary<string,string>();
	public UIPopupList list;
	List<string> miLista=new List<string>();
	string idalumno;
	public bool esAdmin=false;
	public GameObject admin;
	public GameObject popup;
	// Use this for initialization
	void Start () {
		miLista = new List<string> ();
		id = new Dictionary<string,string > ();
		if (!esAdmin) {
			StartCoroutine (verAlumnosEjecutar ());
		} else {
			//StartCoroutine (verAlumnosEjecutar2());
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void actualizarOpciones(){
		list.items = miLista;
	}
	public void MirarAlumnos(){
		StartCoroutine (verAlumnosEjecutar ());
	}
	public void MirarAlumnosAdmin(){
		StartCoroutine (verAlumnosEjecutar2());
	}
	public void eliminar(){
		StartCoroutine(eliminarEjecutar());
	}
	public void verTodosAlumnos(){
		StartCoroutine (verTodosAlumnosEjecutar());
	}
	public void vincularAlumno(){
		StartCoroutine (vincularEjecutar());
	}
	public IEnumerator vincularEjecutar(){
		WWWForm form = new WWWForm();
		id.TryGetValue (gameObject.GetComponent<UIPopupList> ().value, out idalumno);
		//print (idalumno);
		form.AddField( "idAlumno", idalumno );
		form.AddField( "idAdmin", admin.GetComponent<verNiveles>().getIDADMIN() );
		//print ("id admin " + PlayerPrefs.GetString ("idAdmin"));		
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Vinculando Alumno";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/vincularAlumno.php", form);
		yield return download;
		//print (download.text);
		gameObject.GetComponent<UIPopupList> ().value = "";
		admin.GetComponent<UIPopupList> ().value = "";
		StartCoroutine (verAlumnosEjecutar ());

		popup.GetComponent<UILabel>().text="Vinculado Correctamente";
		popup.transform.FindChild ("Boton").gameObject.SetActive (true);
	}
	public IEnumerator eliminarEjecutar(){
		WWWForm form = new WWWForm();
		id.TryGetValue (gameObject.GetComponent<UIPopupList> ().value, out idalumno);
		//print (idalumno);
		form.AddField( "id", idalumno );
		popup.SetActive (true);
		popup.GetComponent<UILabel>().text="Desvinculando Alumno";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		//print ("id admin " + PlayerPrefs.GetString ("idAdmin"));		
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/desvincularAlumno.php", form);
		yield return download;
		//print (download.text);
		gameObject.GetComponent<UIPopupList> ().value = "";
		admin.GetComponent<UIPopupList> ().value = "";
		StartCoroutine (verAlumnosEjecutar ());
		popup.GetComponent<UILabel>().text="Desvinculado Correctamente";
		popup.transform.FindChild ("Boton").gameObject.SetActive (true);
	}

	public IEnumerator verTodosAlumnosEjecutar(){
		miLista = new List<string> ();
		id = new Dictionary<string,string > ();
		WWWForm form = new WWWForm();
		form.AddField( "id", "todos" );
		//print ("id admin " + PlayerPrefs.GetString ("idAdmin"));
		
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/verAlumnos.php", form);
		yield return download;
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			
			string retorno = download.text;
			//print (retorno);
			string[] ret = retorno.Split(new char[]{'*'});
			
			for(int i=0;i<ret.Length-1;i++){
				//print (ret[i]);
				string[] ret2=ret[i].Split(new char[]{'|'});
				//print (ret2[0]);
				//print (ret2[1]);
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
	public IEnumerator verAlumnosEjecutar(){
		miLista = new List<string> ();
		id = new Dictionary<string,string > ();
		WWWForm form = new WWWForm();
		form.AddField( "id", PlayerPrefs.GetString("idAdmin") );
		//print ("id admin " + PlayerPrefs.GetString ("idAdmin"));
		
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/verAlumnos.php", form);
		yield return download;
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			yield return false;
		} else {

			string retorno = download.text;
			//print (retorno);
			string[] ret = retorno.Split(new char[]{'*'});

			for(int i=0;i<ret.Length-1;i++){
				print (ret[i]);
				string[] ret2=ret[i].Split(new char[]{'|'});
				//print (ret2[0]);
				//print (ret2[1]);
				id.Add (ret2[1],ret2[0]);//para cada nombre guardo el ID del alumno correspondiente
				miLista.Add(ret2[1]);


			}
			actualizarOpciones();

			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}
		
	}
	public IEnumerator verAlumnosEjecutar2(){
		miLista = new List<string> ();
		id = new Dictionary<string,string > ();
		WWWForm form = new WWWForm();
		print (admin.GetComponent<verNiveles> ().getIDADMIN ());
		form.AddField( "id", admin.GetComponent<verNiveles>().getIDADMIN() );
		//print ("id admin " + PlayerPrefs.GetString ("idAdmin"));
		
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/verAlumnos.php", form);
		yield return download;
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			
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
			
			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}
		
	}
}
