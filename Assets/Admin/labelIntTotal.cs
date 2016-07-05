using UnityEngine;
using System.Collections;

public class labelIntTotal : MonoBehaviour {
	public UIInput postder;
	public UIInput post;
	public UIInput postizq;
	public UIInput balde;
	public UIInput cabina;
	public UIInput medioder;
	float porcentaje=0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (post.value != "" && postder.value != "" && postizq.value != "" && medioder.value != "" && cabina.value != "" && balde.value != "") {
						porcentaje = (float.Parse (post.value) + float.Parse (postder.value) + float.Parse (postizq.value)
								+ float.Parse (balde.value) + float.Parse (cabina.value) + float.Parse (medioder.value)) * 1f / 6f;
						gameObject.GetComponent<UILabel> ().text = "INTEGRIDAD TOTAL: " + (int)(100 - porcentaje);
				} else {
						gameObject.GetComponent<UILabel> ().text = "INTEGRIDAD TOTAL: 0 " ;
				}
	}
}
