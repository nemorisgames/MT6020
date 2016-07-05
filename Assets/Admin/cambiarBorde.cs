using UnityEngine;
using System.Collections;

public class cambiarBorde : MonoBehaviour {
	public string sprite;
	public string spritedefault;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<UIInput> ().isSelected) {
						gameObject.GetComponent<UISprite> ().spriteName = sprite;
		} else {
			gameObject.GetComponent<UISprite>().spriteName=spritedefault;
		}
	}
	public void cambiarSprite(){

	}
}
