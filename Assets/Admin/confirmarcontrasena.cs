using UnityEngine;
using System.Collections;

public class confirmarcontrasena : MonoBehaviour {
	public string sprite;
	public string spritedefault;
	public string error;
	public UIInput pass;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(pass.value!=gameObject.GetComponent<UIInput>().value&&gameObject.GetComponent<UIInput>().value!=""){
			gameObject.GetComponent<UISprite> ().spriteName = error;

		}
		else{
			if (gameObject.GetComponent<UIInput> ().isSelected) {
				gameObject.GetComponent<UISprite> ().spriteName = sprite;
			} else {
				gameObject.GetComponent<UISprite>().spriteName=spritedefault;
			}
		}
	}
	public void cambiarSprite(){
		
	}
}

