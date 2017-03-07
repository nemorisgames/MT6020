using UnityEngine;
using System.Collections;

public class ChecklistLista : MonoBehaviour {
	public Transform rootChecklist;
	public GameObject checklistOpcionPrefab;
	GameObject[] checklistOpciones;

	[HideInInspector]
	public int listaSelec = 0;

	public UILabel tituloLabel;

	public string[] titulos;
	public int[] respuestas1;
	public string[] checkLists1;
	public int[] respuestas2;
	public string[] checkLists2;
	public int[] respuestas3;
	public string[] checkLists3;
	public int[] respuestas4;
	public string[] checkLists4;

	// Use this for initialization
	void Start () {
		respuestas1 = new int[checkLists1.Length];
		respuestas2 = new int[checkLists2.Length];
		respuestas3 = new int[checkLists3.Length];
		respuestas4 = new int[checkLists4.Length];
	}

	void resetLista(){
		if (checklistOpciones == null || checklistOpciones.Length <= 0)
			return;
		int contOpciones = 0;
		switch (listaSelec) {
		case 0: contOpciones = checkLists1.Length; break;
		case 1: contOpciones = checkLists2.Length; break;
		case 2: contOpciones = checkLists3.Length; break;
		case 3: contOpciones = checkLists4.Length; break;
		}

		for (int i = 0; i < contOpciones; i++) {
			Destroy(checklistOpciones[i]);
		}
		checklistOpciones = null;
	}

	public void verLista1(){
		resetLista ();
		crearLista (0);
	}
	public void verLista2(){
		resetLista ();
		crearLista (1);
	}
	public void verLista3(){
		resetLista ();
		crearLista (2);
	}
	public void verLista4(){
		resetLista ();
		crearLista (3);
	}

	public void guardarRespuestaVerde(int indice){
		switch (listaSelec) {
		case 0: respuestas1[indice] = 1; break;
		case 1: respuestas2[indice] = 1; break;
		case 2: respuestas3[indice] = 1; break;
		case 3: respuestas4[indice] = 1; break;
		}
	}

	public void guardarRespuestaRojo(int indice){
		switch (listaSelec) {
		case 0: respuestas1[indice] = 2; break;
		case 1: respuestas2[indice] = 2; break;
		case 2: respuestas3[indice] = 2; break;
		case 3: respuestas4[indice] = 2; break;
		}
	}

	void crearLista(int indice){
//		print ("crearLista " + indice);
		listaSelec = indice;
		tituloLabel.text = titulos [listaSelec];
		string[] checkLists = null;
		int[] respuestas = null;
		switch (indice) {
		case 0: checkLists = checkLists1; respuestas = respuestas1; break;
		case 1: checkLists = checkLists2; respuestas = respuestas2; break;
		case 2: checkLists = checkLists3; respuestas = respuestas3; break;
		case 3: checkLists = checkLists4; respuestas = respuestas4; break;
		}

		print (indice + " " + checkLists.Length + " " + respuestas1.Length);

		checklistOpciones = new GameObject[checkLists.Length];
		for (int i = 0; i < checkLists.Length; i++) {
//			print("creando " + i);
			checklistOpciones[i] = (GameObject)Instantiate(checklistOpcionPrefab);
			checklistOpciones[i].transform.parent = rootChecklist;
			checklistOpciones[i].transform.localScale = Vector3.one;
			checklistOpciones[i].transform.localPosition = new Vector3(0, -20f - 40f * i, 0f);
			checklistOpciones[i].GetComponent<ChecklistOpcion>().inicializar(listaSelec, i, checkLists[i], respuestas[i]);
		}
		//checklistOpciones
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
