using UnityEngine;
using System.Collections;

public class EditarDatosUsuario : MonoBehaviour {
	public UIInput pass;
	public UIInput pass2;
	public UIInput nombre;
	public UIInput mail;
	public UILabel sexo;
	public UILabel usuario;
	public GameObject popup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void actualizar(){
		StartCoroutine (actualizarEjecutar ());

	}
	public void obtenerDatos(){
		StartCoroutine (datosEjecutar ());
	}
	IEnumerator datosEjecutar(){
		popup.SetActive (true);
		popup.GetComponent<UILabel> ().text = "Cargando Datos ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWWForm form = new WWWForm ();
		form.AddField ("id", PlayerPrefs.GetString ("idAdmin"));
		WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/obtenerAdministrador.php", form);
		yield return download;

		if (download.text!="-1") {
			string retorno=download.text;
			//print (retorno);
			string[] ret = retorno.Split (new char[]{'*'});
			//print (ret[0]);
			nombre.value=ret[0];
			mail.value=ret[1];
			sexo.text=ret[2];
			usuario.text=PlayerPrefs.GetString ("idAdmin");
			popup.SetActive (true);
			popup.GetComponent<UILabel> ().text = "Datos Cargados Correctamente";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
		}

	}
	IEnumerator actualizarEjecutar(){
		//print (pass.value);
		//print (pass2.value);
		if (pass.value == "" || pass2.value == "") {
						popup.SetActive (true);
						popup.GetComponent<UILabel> ().text = "Debe Introducir una Contraseña";
						popup.transform.FindChild ("Boton").gameObject.SetActive (true);
				} else {
						if (pass.value != pass2.value) {
								popup.SetActive (true);
								popup.GetComponent<UILabel> ().text = "Las Contraseñas deben Coincidir ";
								popup.transform.FindChild ("Boton").gameObject.SetActive (true);
						} else {
								popup.SetActive (true);
								popup.GetComponent<UILabel> ().text = "Guardando Datos ";
								popup.transform.FindChild ("Boton").gameObject.SetActive (false);
								WWWForm form = new WWWForm ();
								form.AddField ("admin", PlayerPrefs.GetString ("idAdmin"));
								form.AddField ("pass", pass.value);
								form.AddField ("nombre", nombre.value);
								form.AddField ("sexo", sexo.text);
								form.AddField ("mail", mail.value);
								WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/editarAdministrador.php", form);
								yield return download;
								if (download.text == "correcto") {
                                        GameObject.FindGameObjectWithTag("Configuracion").GetComponent<Configuracion>().mailInstructor = mail.GetComponent<UIInput>().value;
                                        pass.value="";
										pass2.value="";
										popup.SetActive (true);
										popup.GetComponent<UILabel> ().text = "Datos Guardados Exitosamente";
										popup.transform.FindChild ("Boton").gameObject.SetActive (true);
								}
						}
				}
	}

}
