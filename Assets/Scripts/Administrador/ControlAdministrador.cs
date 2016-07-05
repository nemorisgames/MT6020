using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlAdministrador : MonoBehaviour {
	Configuracion configuracion;
	// Use this for initialization
	void Start () {
		GameObject g = GameObject.FindWithTag ("Configuracion");
		if (g != null)
			configuracion = g.GetComponent<Configuracion> ();
	}

	public void setCalidadBaja(){
		QualitySettings.SetQualityLevel(3, true);
	}
	
	public void setCalidadMedia(){
		QualitySettings.SetQualityLevel(4, true);
	}
	
	public void setCalidadAlta(){
		QualitySettings.SetQualityLevel(5, true);
	}
	
	public void reset(){
		//gameObject.SendMessage ("apagarLeds");
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void salirSimulacion(){
		configuracion.finalizar ();
		//configuracion.guardarHistorial ();
		//gameObject.SendMessage ("apagarLeds");
		//SceneManager.LoadScene ("Login");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
