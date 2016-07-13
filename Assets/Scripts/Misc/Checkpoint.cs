using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public int indice = 0;
	public GameObject objetivo;
	public string mensaje;
	InGame inGame;
	float cooldownError = 0f;
	Rigidbody maquina;
	// Use this for initialization
	void Start () {
		inGame = GameObject.FindWithTag ("InGame").GetComponent<InGame> ();
		maquina = GameObject.FindWithTag ("Maquina").transform.FindChild ("Trasero_B").gameObject.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider c){
		print ("toque");
		if (c.gameObject.name != "AdvertenciaAmarillaAdelante" && c.gameObject.name != "AdvertenciaRojaAdelante" && c.gameObject.name != "AdvertenciaAmarillaAtras" && c.gameObject.name != "AdvertenciaRojaAtras" && c.gameObject.transform.root.gameObject.CompareTag ("Maquina")) {
			//TO DO: solucionar esto para mt6020
            if(true) {// inGame.verificarAvance){
				if(cooldownError < Time.time){
					print (maquina.transform.InverseTransformDirection(maquina.velocity) + " " + c.gameObject.transform.parent.name);
					//if((inGame.avanceEnBalde && c.gameObject.transform.parent.name == "CollidersBalde" || inGame.avanceEnBalde && c.gameObject.transform.parent.name == "Pala") && maquina.transform.InverseTransformDirection(maquina.velocity).z > 0f ){
                    if ((true && c.gameObject.transform.parent.name == "Colliders_D") && maquina.transform.InverseTransformDirection(maquina.velocity).z > 0f)
                    {
                        print("avance balde");
						transform.parent.gameObject.SendMessage ("checkpointTocado", indice);
						if(objetivo != null)
							objetivo.SendMessage(mensaje);
					}
					/*else{
						//if(!inGame.avanceEnBalde && c.gameObject.transform.parent.name == "ColliderBack" && maquina.transform.InverseTransformDirection(maquina.velocity).z < 0f){
							print ("avance motor");
							transform.parent.gameObject.SendMessage ("checkpointTocado", indice);
							if(objetivo != null)
								objetivo.SendMessage(mensaje);
						}
						else{
							transform.parent.gameObject.SendMessage ("direccionErronea");
							cooldownError = Time.time + 3f;
						}
					}*/
				}
			}
			/*else{
				transform.parent.gameObject.SendMessage ("checkpointTocado", indice);
				if(objetivo != null)
					objetivo.SendMessage(mensaje);
			}*/
			print ("enviado " + c.gameObject.name);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
