using UnityEngine;
using System.Collections;

public class revisarMail : MonoBehaviour {
	string mail;
	public string sprite;
	public string spritedefault;
	public string error;
	public bool correcto;
	// Use this for initialization
	void Start () {
		correcto = false;
	}
	
	// Update is called once per frame
	void Update () {
		mail = gameObject.GetComponent<UIInput> ().value;
		if (mail != "") {
			string[] arroba=mail.Split(new char[]{'@'});
			if(arroba.Length==2){
				string[] punto=arroba[1].Split(new char[]{'.'});
				if(punto.Length >= 2){
					if(punto[punto.Length - 1].Length == 0){
						gameObject.GetComponent<UISprite> ().spriteName = error;
						correcto=false;

					}
					else{
						if(punto[punto.Length-1]=="com"||punto[punto.Length-1]=="cl"){
							gameObject.GetComponent<UISprite> ().spriteName = sprite;
							correcto=true;
						}
						else{
							gameObject.GetComponent<UISprite> ().spriteName = error;
							correcto=false;

						}

					}
				}
				else{
					gameObject.GetComponent<UISprite> ().spriteName = error;
					correcto=false;

				}
			}
			else{
				gameObject.GetComponent<UISprite> ().spriteName = error;
				correcto=false;

			}
		}
	}
}
