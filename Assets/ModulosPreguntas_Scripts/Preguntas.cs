using UnityEngine;
using System.Collections;

public class Preguntas : MonoBehaviour {
	//coordenadas preguntas
	//x1=-505 y1=307
	//x2=420 y2=307
	//x3=-505 y3=-63
	//x4= 420 y4=-63
	int posCorrecto;
	public GameObject pregunta;
	public bool Correcto=false;
	public GameObject resp1;
	public GameObject resp2;
	public GameObject resp3;
	public GameObject resp4;

	public UIToggle r1;
	public UIToggle r2;
	public UIToggle r3;
	public UIToggle r4;

	public void verRespuesta(){
		Correcto = false;
		if (r1.value && posCorrecto == 1)
			Correcto = true;
			else if(r2.value&&posCorrecto==2)
					Correcto = true;
				else if(r3.value&&posCorrecto==3)
						Correcto = true;
					else if(r4.value&&posCorrecto==4)
							Correcto = true;
	}
	// Use this for initialization
	void Start () {
	}
	public void setGrupo(int num){
		r1.group = num;
		r2.group = num;
		r3.group = num;
		r4.group = num;
	}

    public bool esNula()
    {
        return !(r1.value || r2.value || r3.value || r4.value);
    }

	public void setPreguntas(string preg,string p_1, string p_2, string p_3, string p_4, string corr){
		pregunta.AddComponent<UILocalize> ();
		resp1.AddComponent<UILocalize> ();
		resp2.AddComponent<UILocalize> ();
		resp3.AddComponent<UILocalize> ();
		resp4.AddComponent<UILocalize> ();

		pregunta.GetComponent<UILabel>().text = preg;
		resp1.GetComponent<UILabel>().text = p_1;
		resp2.GetComponent<UILabel>().text = p_2;
		resp3.GetComponent<UILabel>().text = p_3;
		resp4.GetComponent<UILabel>().text = p_4;

		print ((pregunta == null) + " " + (pregunta.GetComponent<UILocalize> () == null));
		pregunta.GetComponent<UILocalize>().key = preg.Replace("\n", "").Replace("\r", "").Replace((char)34,(char)39);
		resp1.GetComponent<UILocalize>().key = p_1.Replace("\n", "").Replace("\r", "").Replace((char)34,(char)39);
		resp2.GetComponent<UILocalize>().key = p_2.Replace("\n", "").Replace("\r", "").Replace((char)34,(char)39);
		resp3.GetComponent<UILocalize>().key = p_3.Replace("\n", "").Replace("\r", "").Replace((char)34,(char)39);
		resp4.GetComponent<UILocalize>().key = p_4.Replace("\n", "").Replace("\r", "").Replace((char)34,(char)39);

		if (corr == p_1)
			posCorrecto = 1;
		if (corr == p_2)
			posCorrecto = 2;
		if (corr == p_3)
			posCorrecto = 3;
		if (corr == p_4)
			posCorrecto = 4;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
