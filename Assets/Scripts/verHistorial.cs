using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class verHistorial : MonoBehaviour {
	public GameObject alumnos;
	string idalumno;
	public UIPopupList list;
	public UIPopupList modulos;
	public Dictionary<string,string> miLista=new Dictionary<string,string>(); //fecha con id;
	List<string>lista=new List<string>();
	// Use this for initialization
	void Start () {
		//Dictionary<string,string> miLista=new Dictionary<string,string>();
		//List<string>lista=new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public string sacaID(string clave){
		string respuesta = "";
		print (clave);
		miLista.TryGetValue (clave, out respuesta);
		print (respuesta);
		return respuesta;
		
	}
	public void verHistorialAlumno(){

		string aux = alumnos.GetComponent<UIPopupList> ().GetComponentInChildren<UILabel>().text;
		alumnos.GetComponent<verAlumnos> ().id.TryGetValue (aux,out idalumno);
		print (aux);
		if(idalumno != null)
			StartCoroutine (verHistorialEjecutar ());
	}
	void actualizarOpciones(){

	//	print ("actualizando");
		//list.items = lista;
	}
	public IEnumerator verHistorialEjecutar(){
		//print ("entre");
		miLista=new Dictionary<string,string>();
		List<string>lista=new List<string>();
		WWWForm form = new WWWForm();
		list.Clear ();
		form.AddField( "id", idalumno );

		
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/verHistorial.php", form);
		yield return download;
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			return false;
		} else {
			string retorno = download.text;
			print ("historial: " + retorno);

			string[] ret = retorno.Split(new char[]{'*'});

			for(int i=0;i<ret.Length-1;i++){

				string[] ret2=ret[i].Split(new char[]{'|'});
				string[] dat = ret2[0].Split(new char[]{' '});
				print (ret2[2]  + " = " + modulos.GetComponentInChildren<UILabel>().text);
				//print (ret2[1]);
				if(ret2[2] == modulos.GetComponentInChildren<UILabel>().text)
                {
					print ("OK");
					miLista.Add (ret2[0],ret2[1]);//para cada id historial almaceno la fecha
					//string aux="";
					//miLista.TryGetValue("2014-10-21",out aux);
					//print(aux); //lista.Add(ret2[0]);
					list.AddItem(ret2[0]);
				}
				list.value = "";
				
			}
			//actualizarOpciones();
			
			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}
		
	}
}
