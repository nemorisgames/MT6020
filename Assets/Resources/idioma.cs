using UnityEngine;
using System.Collections;

public class idioma : MonoBehaviour {
	UIPopupList lista;
	public UILabel texto;
	// Use this for initialization
	void Start () {
		lista = GetComponent<UIPopupList> ();
		if (lista != null) {
			for(int i = 0; i < Localization.knownLanguages.Length; i++){
				lista.AddItem(Localization.knownLanguages[i]);
				print ("added " + Localization.knownLanguages[i]);
			}
		}
		escribirLenguaje ();
	}

	void escribirLenguaje(){
		if (texto != null) {
			texto.text = Localization.Get(Localization.language);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void cambiarLenguaje(){

		Localization.language = lista.value;
		escribirLenguaje ();
	}

	public void click(){
		//Localization.LoadCSV((TextAsset)Resources.Load("Localization",typeof(TextAsset)));
		if(Localization.language=="es")
			Localization.language="en";
		else
			Localization.language="es";
		print (Localization.language);
	}
}
