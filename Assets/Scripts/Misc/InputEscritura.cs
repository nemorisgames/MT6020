using UnityEngine;
using System.Collections;

public class InputEscritura : MonoBehaviour {
	public UIInput[] inputs;
	public UILabel[] letras;
	public bool upperCase = false;
	// Use this for initialization
	void Start () {
		focus (inputs [0]);
	}

	public void escribir(string letra){
		print ("letra: " + letra);
		foreach (UIInput i in inputs) {
			if(i.isSelected){
				if(letra == "Delete"){
					if(i.value.Length > 0){
						i.value = i.value.Substring(0, i.value.Length - 1);
					}
				}
				else{
					if(letra == "Upper Case"){
						upperCase = !upperCase;
						foreach (UILabel l in letras){
							//if(i.value.Length == 1){
							l.text = upperCase?l.text.ToUpper():l.text.ToLower();
							//}
						}
					}
					else
						i.value += letra;
				}
			}
		}
	}

	public void focus(UIInput input){
		foreach (UIInput i in inputs){
			i.isSelected = (i == input);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
