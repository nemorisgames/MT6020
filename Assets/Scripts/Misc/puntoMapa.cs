using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class puntoMapa : MonoBehaviour {
	Transform maquina;
	ControlCamion controlCamion;
	public float escalaX = 3.21f;
	public float escalaY = 3.21f;

	UISprite sp;
	GameObject textura;
	GameObject textura2;
	bool enPisoBajo = false;
	// Use this for initialization
	void Start () {
		maquina = GameObject.FindWithTag ("Maquina").transform.FindChild ("Front");
		controlCamion = maquina.parent.gameObject.GetComponent<ControlCamion> ();
		sp = gameObject.GetComponent<UISprite> ();
		textura = transform.parent.parent.FindChild ("Texture").gameObject;
		Transform t2 = transform.parent.parent.FindChild ("Texture 1");
		if (t2 != null)
			textura2 = t2.gameObject;
	}

	public void activarPisoBajo(bool activar){
		textura.GetComponent<UITexture> ().color = new Color (1f, 1f, 1f, activar?0.3f:1f);
		textura2.GetComponent<UITexture> ().color = new Color (1f, 1f, 1f, activar?1f:0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		if (controlCamion.estado == ControlCamion.EstadoMaquina.encendida) {
			if(!sp.enabled){
				sp.enabled = true;
				textura.SetActive(true);
				if(textura2 != null) textura2.SetActive(true);
			}
		} else {
			if(sp.enabled){
				sp.enabled = false;
				textura.SetActive(false);
				if(textura2 != null) textura2.SetActive(false);
			}
		}
		if (SceneManager.GetActiveScene().name.Contains ("17") || SceneManager.GetActiveScene().name.Contains ("18")) {
			if(controlCamion.controlCamionMotor.gameObject.transform.position.y < -12f){
				if(!enPisoBajo){
					enPisoBajo = true;
					activarPisoBajo(true);
				}
			}
			else{
				if(enPisoBajo){
					enPisoBajo = false;
					activarPisoBajo(false);
				}
			}
		}
		transform.localPosition = new Vector3 (-maquina.position.z * escalaX, maquina.position.x * escalaY, 0f);
	}
}
