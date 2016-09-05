using UnityEngine;
using System.Collections;

public class GolpeDeteccion : MonoBehaviour {
	ControlCamion controlCamion;
	InGame inGame;
	// Use this for initialization
	void Start () {
		controlCamion = GameObject.FindWithTag ("Maquina").GetComponent<ControlCamion>();
		inGame = GameObject.FindWithTag ("InGame").GetComponent<InGame>();
	}

	void OnCollisionEnter(Collision collision) {
		print ("detectado " + collision.gameObject.tag + ", " + collision.contacts[0].thisCollider.tag);
		ControlCamion.LugarMaquina l = ControlCamion.LugarMaquina.Frontal;
		switch (collision.contacts [0].thisCollider.tag) {
		case "CamionMotorDer": l = ControlCamion.LugarMaquina.MotorDer; break;
		case "CamionMotorIzq": l = ControlCamion.LugarMaquina.MotorIzq; break;
		case "CamionTolvaDer": l = ControlCamion.LugarMaquina.TolvaDer; break;
		case "CamionTolvaIzq": l = ControlCamion.LugarMaquina.TolvaIzq; break;
		}
		controlCamion.golpe (collision.relativeVelocity.magnitude, collision.contacts[0].point, collision.gameObject.tag, l);

		/*if (collision.gameObject.CompareTag ("Camioneta")) {
			collision.gameObject.SendMessage ("recibirGolpe", collision.relativeVelocity.magnitude);
            if (collision.relativeVelocity.magnitude > 2f)
            {
                inGame.integridadCamioneta -= inGame.configuracion.DescuentoCamioneta;
                inGame.cantidadChoquesCamioneta++;
            }
		} else {*/
			if (collision.gameObject.CompareTag ("Obstaculo")) {
				if(collision.relativeVelocity.magnitude > 2f) inGame.cantidadChoquesZipper++;
			}
			else{
				if (collision.gameObject.CompareTag ("ObstaculoZipperPlastico")) {
					if(collision.relativeVelocity.magnitude > 2f) inGame.cantidadChoquesZipper++;
				}
				else{
					if (collision.gameObject.transform.root.name == "Buzon") {
						if(collision.relativeVelocity.magnitude > 2f){
							inGame.cantidadChoquesBuzon++;
							inGame.integridadCamion -= inGame.configuracion.DescuentoBuzonCarga;
						}
					}
					else{
						if(collision.relativeVelocity.magnitude > 1f) inGame.cantidadChoquesTunel++;
					}
				}
			}
		//}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
