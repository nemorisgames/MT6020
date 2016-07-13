using UnityEngine;
using System.Collections;

public class ControlCheckpoints : MonoBehaviour {
	public GameObject[] checkpoints;
	public UILabel contadorLabel;
	int nCheckpoints = 0;
	public int indiceActual = 0;
	public int nVueltas = 0;

	public int[] ignorarCheckpointsIndice;

	public Transform flecha;
    public ArrayList tiempos;

	public int nVueltasObjetivo = 1;
    float tiempoInicialVueltaAnterior;
	InGame inGame;
	// Use this for initialization
	void Start () {
        tiempos = new ArrayList();
        nCheckpoints = checkpoints.Length;
		inGame = GameObject.FindWithTag ("InGame").GetComponent<InGame>();
	}

	public void checkpointTocado(int indice){
		print ("recibido " + indice);
		if (indice != indiceActual)
			return;
		bool indiceEncontrado = true;
        if (nVueltas <= 0 && indice == 0)
        {
            tiempoInicialVueltaAnterior = Time.time;
            print("tiempo inicial " + tiempoInicialVueltaAnterior);
        } 
        while (indiceEncontrado) {
			checkpoints [indiceActual].SetActive (false);
			indiceActual++;
			if (indiceActual >= nCheckpoints) {
                nVueltas++;
				contadorLabel.text = "" + nVueltas;
				indiceActual = 0;
                tiempos.Add(Time.time - tiempoInicialVueltaAnterior);
                print("tiempo vuelta " + nVueltas + ": " + (Time.time - tiempoInicialVueltaAnterior));
                tiempoInicialVueltaAnterior = Time.time;
                if (nVueltas >= nVueltasObjetivo) {
                    //TO DO: arreglar esto para mt6020
					//inGame.condicionesTerminoListas ();
				}
			}
			checkpoints [indiceActual].SetActive (true);
			if(ignorarCheckpointsIndice.Length > 0 && nVueltas < nVueltasObjetivo - 1){
				bool salirWhile = true;
				for(int i = 0; i < ignorarCheckpointsIndice.Length; i++){
					if(indiceActual == ignorarCheckpointsIndice[i]){
						salirWhile = false;
					}
				}
				indiceEncontrado = !salirWhile;
		    }
		    else indiceEncontrado = false;
		}
	}

	public void retrocederCheckpoint(){
		checkpoints [indiceActual].SetActive (false);
		indiceActual -= 3; 
		checkpointTocado (indiceActual);
	}

	public void direccionErronea(){
		//inGame.mostrarMensajeDireccionErronea ();
	}
	
	// Update is called once per frame
	void Update () {
		if (flecha != null) {
			flecha.LookAt(checkpoints[indiceActual].transform);
		}
	}
}
