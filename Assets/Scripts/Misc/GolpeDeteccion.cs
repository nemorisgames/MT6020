using UnityEngine;
using System.Collections;

public class GolpeDeteccion : MonoBehaviour {
	/*ControlExcavadora controlExcavadora;
	Central central;
	// Use this for initialization
	void Start () {
		controlExcavadora = GameObject.FindWithTag ("Maquina").GetComponent<ControlExcavadora>();
		central = GameObject.FindWithTag ("Central").GetComponent<Central>();
	}

	void OnCollisionEnter(Collision collision) {
		//print ("detectado " + collision.gameObject.tag + ", " + collision.contacts[0].thisCollider.tag);
		ControlExcavadora.LugarMaquina l = ControlExcavadora.LugarMaquina.Brazo;
		switch (collision.contacts [0].thisCollider.tag) {
		case "MaquinaPosteriorIzq": l = ControlExcavadora.LugarMaquina.PosteriorIzquierdo; break;
		case "MaquinaPosteriorDer": l = ControlExcavadora.LugarMaquina.PosteriorDerecho; break;
		case "MaquinaMedioIzq": l = ControlExcavadora.LugarMaquina.Cabina; break;
		case "MaquinaMedioDer": l = ControlExcavadora.LugarMaquina.MedioDerecho; break;
		}
		controlExcavadora.golpe (collision.relativeVelocity.magnitude, collision.contacts[0].point, collision.gameObject.tag, l);

		if (collision.gameObject.CompareTag ("Camioneta")) {
			collision.gameObject.SendMessage ("recibirGolpe", collision.relativeVelocity.magnitude);
            if (collision.relativeVelocity.magnitude > 2f)
            {
                central.integridadCamioneta -= central.configuracion.DescuentoCamioneta;
                central.cantidadChoquesCamioneta++;
            }
		} else {
			if (collision.gameObject.CompareTag ("Obstaculo")) {
				if(collision.relativeVelocity.magnitude > 2f) central.cantidadChoquesZipper++;
			}
			else{
				if (collision.gameObject.CompareTag ("ObstaculoZipperPlastico")) {
					if(collision.relativeVelocity.magnitude > 2f) central.cantidadChoquesZipper++;
				}
				else{
					if (collision.gameObject.transform.root.name == "Camion") {
						if(collision.relativeVelocity.magnitude > 2f){
							central.cantidadChoquesCamion++;
							central.integridadCamion -= central.configuracion.DescuentoCamion;
						}
					}
					//else{
						//if(collision.relativeVelocity.magnitude > 1f) central.cantidadChoquesTunel++;
					//}
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}*/
}
