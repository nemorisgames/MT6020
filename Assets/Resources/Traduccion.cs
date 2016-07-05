using UnityEngine;
using System.Collections;

public class Traduccion : MonoBehaviour {
	/*para cargar el localize: Localization.LoadCSV((TextAsset)Resources.Load("Localization",typeof(TextAsset)));*/
	// Use this for initialization
	void Start () {
		//Localization.LoadCSV((TextAsset)Resources.Load("GoogleFU",typeof(TextAsset)));
		Localization.Load((TextAsset)Resources.Load("Localization",typeof(TextAsset)));
		string todos = "";//almancenara todos los keys;
		string todos_Natural="";//el texto original
		Transform[] objetos;
		objetos=  GetComponentsInChildren<Transform>(true);
		print (objetos.Length);
		foreach (Transform label in objetos) {
			if(label.gameObject.GetComponent<UILabel>()!=null){//si es un label
				if(label.gameObject.GetComponent<UILocalize>()==null){					
					//si no tiene el uilocalize lo agrego
					/*en el caso de que no funcione la traduccion, poner el key antes de agregar el uilocalize*/
					label.gameObject.AddComponent<UILocalize>();					
					
				}
				//le pongo la clave que le corresponde
				string key=label.gameObject.GetComponent<UILabel>().text;
				//todos_Natural+=key.Replace("\r", "|").Replace("\n", "|")+"*";
				todos_Natural+=key.Replace("\n","|").Replace("\n","").Replace("\r","").Replace((char)34,(char)39);
				todos_Natural+="#";
				key=key.Replace("\n", "").Replace("\r", "").Replace((char)34,(char)39);//le quito todos los enter y/o retornos
				//key=key.Replace("\n", "").Replace((char)34,(char)39);//le quito todos los enter y/o retornos
				todos+=key+"#";
				label.gameObject.GetComponent<UILabel>().text=key;
				
			}
		}
		string[] separados = todos.Split (new char[]{'#'});
		print ("Keys para el localize: ");
		todos = "";
		foreach (string x in separados) {
			todos+= x+"\n";
		}
		print (todos);
		separados = todos_Natural.Split (new char[]{'#'});
		todos_Natural="";
		print ("Valor para el localize: ");
		foreach (string x in separados) {
			//print (x);
			todos_Natural+= x+"\n";
		}
		print (todos_Natural);
		Localization.language="es";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
