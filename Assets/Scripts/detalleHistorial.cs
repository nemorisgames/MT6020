using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class detalleHistorial : MonoBehaviour {
	string idHistorial;
	Dictionary<string,string> miLista=new Dictionary<string,string>();
	List<string>lista=new List<string>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public IEnumerator verHistorialEjecutar(){
		
		WWWForm form = new WWWForm();
		form.AddField( "id", idHistorial );
		
		
		WWW download = new WWW( VariablesGlobales.direccion + "SimuladorMT6020/detalleHistorial.php", form);
		yield return download;
		//print(download.text);
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			//print ("hola");
			string retorno = download.text;
			//print (retorno);
			string[] ret = retorno.Split(new char[]{'|'});

			
			//comprueba si lo que devuelve es informacion de alguien que existe
			
		}
		
	}
}
