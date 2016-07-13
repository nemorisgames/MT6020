using UnityEngine;
using System.Collections;

public class EscalarDistancia : MonoBehaviour {
	public Transform objetoReferencia;
	public float tasaEscala = 1f;
	public Vector3 ejeEscala;
	public float distancia_;
	float distanciaInicial_;
	Vector3 escalaInicial_;
	// Use this for initialization
	void Start () {
		escalaInicial_ = transform.localScale;
		distanciaInicial_ = Vector3.Distance (transform.position, objetoReferencia.position);
	}
	
	// Update is called once per frame
	void Update () {
		distancia_ = Vector3.Distance (transform.position, objetoReferencia.position);
		transform.localScale = new Vector3((ejeEscala.x==0f?transform.localScale.x:(distancia_ - distanciaInicial_) * tasaEscala * ejeEscala.x + escalaInicial_.x),(ejeEscala.y==0f?transform.localScale.y:(distancia_ - distanciaInicial_) * tasaEscala * ejeEscala.y + escalaInicial_.y), (ejeEscala.z==0f?transform.localScale.z:(distancia_ - distanciaInicial_) * tasaEscala * ejeEscala.z + escalaInicial_.z)) ;
	}
}
