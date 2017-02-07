using UnityEngine;
using System.Collections;

public class registrar : MonoBehaviour {
	public UIInput rut;
	public UIInput clave;
	public UIInput clave2;
	public UIInput nombre;
	public UIInput edad;
	public UIInput direccion;
	public UIInput mail;
	public UILabel estado;
	public UILabel sexo;
	public GameObject popup;

	public GameObject[] cosasLogin;
	public GameObject[] cosasRegistro;

    public volver volver;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void registrarseOperador(){
		StartCoroutine(registrarseEjecutar(true));
	}
	public void registrarse(){
		StartCoroutine(registrarseEjecutar(false));
	}
	public IEnumerator registrarseEjecutar(bool operador){
		WWWForm form = new WWWForm();
		form.AddField( "username", rut.value );
		form.AddField( "password", clave.value );
		form.AddField( "nombre", nombre.value );
		form.AddField( "sexo", sexo.text);
		//if (operador) {
			form.AddField ("edad", edad.value);
			form.AddField ("estado", estado.text);
			form.AddField ("direccion", direccion.value);
		//}
		form.AddField( "mail", mail.value );
		if (rut.value != "") {
						if (clave.value != clave2.value) {
								popup.SetActive (true);
								popup.GetComponent<UILabel> ().text = "Las contraseñas deben coincidir ";
								popup.transform.FindChild ("Boton").gameObject.SetActive (true);
						} else {
								if (mail.gameObject.GetComponent<revisarMail> ().correcto) {
										popup.SetActive (true);
										popup.transform.FindChild ("Boton").gameObject.SetActive (false);
										popup.GetComponent<UILabel> ().text = "Registrando..";
										nombre.value = "";
										clave2.value = "";
										mail.value = "";


										WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/register"+(operador?"Alumno":"")+".php", form);
										yield return download;
										if (download.error != null) {
												print ("Error downloading: " + download.error);
												//mostrarError("Error de conexion");
						yield return false;
										} else {
												if(download.text!="ya creado"){
													string retorno = download.text;
													print (download.text);
													popup.GetComponent<UILabel> ().text = "Registrado Correctamente ";
													foreach(GameObject g in cosasLogin)
														g.SetActive(true);
													foreach(GameObject g in cosasRegistro)
														g.SetActive(false);
													popup.transform.FindChild ("Boton").gameObject.SetActive (true);
                                                    volver.click();
                                                }
												else{
													string retorno = download.text;
													print (download.text);
													popup.GetComponent<UILabel> ().text = "Debe Cambiar el Nombre de Usuario ";
													popup.transform.FindChild ("Boton").gameObject.SetActive (true);
												}
												//comprueba si lo que devuelve es informacion de alguien que existe

										}
								} else {
										popup.SetActive (true);
										popup.GetComponent<UILabel> ().text = "Mail Inválido";
										popup.transform.FindChild ("Boton").gameObject.SetActive (true);
								}
						}
				} else {
					popup.SetActive (true);
					popup.GetComponent<UILabel> ().text = "Debe Ingresar un Nombre de Usuario";
					popup.transform.FindChild ("Boton").gameObject.SetActive (true);
				}
	}
}
