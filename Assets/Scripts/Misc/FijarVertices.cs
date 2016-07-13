using UnityEngine;
using System.Collections;

public class FijarVertices : MonoBehaviour {
	public Transform objetoReferencia;
	Vector3 posicionInicialReferencia_;
	//public Vector2 rangoVertices = new Vector2 (0f, 4f);
	public float distanciaLimite = 10f;
	public ArrayList listaVertices = new ArrayList ();

	public Mesh mesh;
	public Vector3[] verts;
	public Vector3[] vertPos;
	// Use this for initialization
	void Start () {
		posicionInicialReferencia_ = objetoReferencia.localPosition;
		mesh = GetComponent<MeshFilter>().mesh;
		verts = mesh.vertices;
		int contadorVertices = 0;
		for (int i = 0; i < verts.Length; i++) {
			if(Mathf.Abs(verts[i].y - objetoReferencia.position.y) > distanciaLimite){
				contadorVertices++;
				listaVertices.Add (i);
			}
		}

		vertPos = new Vector3[contadorVertices];
		int contAux = 0;
		for (int i = 0; i < verts.Length; i++) {
			if(Mathf.Abs(verts[i].y - objetoReferencia.position.y) > distanciaLimite){
				vertPos[contAux] = transform.TransformPoint (verts[i]);
				contAux++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		int contAux = 0;
		for (int i = 0; i < verts.Length; i++) {
			if(listaVertices.Contains(i)){
				verts[i] = new Vector3( objetoReferencia.localPosition.x , objetoReferencia.localPosition.y, verts[i].z);
				contAux++;
			}
		}
		mesh.vertices = verts;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.UploadMeshData (false);
	}
}
