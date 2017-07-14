using UnityEngine;
using System.Collections;

public class ColisionDetector : MonoBehaviour {
	//public VidaControl vida;
	public GameObject polvo;
	public Collider[] colliders;
	//rango aproximado 1000 a 125000
	float fuerzaMinima_ = 5f;

	void Start(){
		if (colliders == null || colliders.Length == 0) {
			colliders = new Collider[1];
			colliders[0] = gameObject.GetComponent<Collider>();
		}
	}

	void OnCollisionEnter(Collision collision) {
		bool encontrado_ = false;
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i] == collision.contacts[0].thisCollider)
					encontrado_ = true;
		}
		if(!encontrado_) return;
		//Instantiate(polvo, collision.contacts[0].point, Quaternion.identity);
    }
	
	void OnCollisionStay(Collision collision) {
		/*if (collision.relativeVelocity.magnitude > fuerzaMinima_)
			foreach (ContactPoint contact in collision.contacts) 
				Instantiate(polvo, contact.point, Quaternion.identity);*/
		bool encontrado_ = false;
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i] == collision.contacts[0].thisCollider)
				encontrado_ = true;
		}
		if(!encontrado_) return;
		if (collision.relativeVelocity.magnitude > fuerzaMinima_){
			foreach (ContactPoint contact in collision.contacts) {

				//print ("factor " + gameObject.tag + " " + collision.gameObject.tag + " : " +  GlobalVariables.getFactor(gameObject.tag, collision.gameObject.tag));
				float choqueMag = 0.1f;
				//vida.SendMessage("onGolpe",  Mathf.Clamp((choqueMag), 0f, 300f));
//				print ("" + Mathf.Clamp((choqueMag), 0f, 300f));

			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		bool encontrado_ = false;
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i] == GetComponent<Collider>()){
				encontrado_ = true;
				//print ("trigger " +i+ GetComponent<Collider>().name);
			}
		}
		if(!encontrado_) return;

		//Instantiate(polvo, other.transform.position, Quaternion.identity);
		if (other.tag == "PesoBalde")
			transform.SetParent (other.transform);
	}
}
