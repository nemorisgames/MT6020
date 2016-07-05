using UnityEngine;
using System.Collections;

public class editarDatosAdmin : MonoBehaviour {
	public GameObject usuario;
	public GameObject pass;
	public GameObject confirmarpass;
	public GameObject sexo;
	public GameObject nombre;
	public GameObject mail;
	public GameObject botonCrear;
	public GameObject botonCrearAlumno;
	public GameObject botonEliminar;
	public GameObject botonActualizar;
	public GameObject botonActualizarAlumno;
	public bool esInstructor=true;
	public GameObject popup;
	public GameObject instructores;
	public GameObject alumnos;
	// Use this for initialization
	void Start () {
		clickVolver ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void clickNuevoInstructor(){
		clickVolver ();
		usuario.SetActive (true);
		pass.SetActive (true);
		confirmarpass.SetActive (true);
		sexo.SetActive (true);
		mail.SetActive (true);
		nombre.SetActive (true);
		botonCrear.SetActive (true);
		esInstructor = true;
	}
	public void clickNuevoAlumno(){
		clickVolver ();
		usuario.SetActive (true);
		pass.SetActive (true);
		confirmarpass.SetActive (true);
		sexo.SetActive (true);
		mail.SetActive (true);
		nombre.SetActive (true);
		botonCrearAlumno.SetActive (true);
		esInstructor = false;
	}
	public void clickEditarAlumno(){
		clickVolver ();
		usuario.SetActive (true);
		pass.SetActive (true);
		confirmarpass.SetActive (true);
		sexo.SetActive (true);
		mail.SetActive (true);
		nombre.SetActive (true);
		botonActualizarAlumno.SetActive (true);
		esInstructor = false;
		alumnos.SetActive (true);

	

	}
	public void clickEditarInstructor(){
		clickVolver ();
		usuario.SetActive (true);
		pass.SetActive (true);
		confirmarpass.SetActive (true);
		sexo.SetActive (true);
		mail.SetActive (true);
		nombre.SetActive (true);
		botonActualizar.SetActive (true);
		esInstructor = true;
		instructores.SetActive (true);		
		
		
	}
	public void guardar(){
		StartCoroutine (actualizarEjecutar ());
	}
	public void guardarAlumno(){
		StartCoroutine (actualizarAlumnoEjecutar());
	}
	public void cargarInstructor(){
		StartCoroutine (datosEjecutar ());
	}
	public void cargarAlumno(){
		StartCoroutine (datosAlumnoEjecutar ());
	}
	public void clickVolver(){
		reset ();
		usuario.GetComponent<UIInput>().enabled=true;
		usuario.SetActive (false);
		pass.SetActive (false);
		confirmarpass.SetActive (false);
		sexo.SetActive (false);
		mail.SetActive (false);
		nombre.SetActive (false);
		botonCrear.SetActive (false);
		botonActualizar.SetActive (false);
		botonEliminar.SetActive (false);
		botonCrearAlumno.SetActive (false);
		instructores.SetActive (false);
		alumnos.SetActive (false);
		botonActualizarAlumno.SetActive (false);
	}
	public void registrar(){
		if (esInstructor) {
            //StartCoroutine(datosEjecutar());
            StartCoroutine(registrarseEjecutar());
        } else {
			//StartCoroutine(datosAlumnoEjecutar());
            StartCoroutine(registrarseAlumnoEjecutar());
        }
	}
	void reset(){
	 usuario.GetComponent<UIInput>().value ="";
		 pass.GetComponent<UIInput>().value ="" ;
		nombre.GetComponent<UIInput>().value ="" ;
		// sexo.GetComponent<UIPopupList>().value ="";
		 mail.GetComponent<UIInput>().value ="";
		//instructores.GetComponent<UIPopupList>().value="";
		//alumnos.GetComponent<UIPopupList>().value="";
	}
	IEnumerator registrarseEjecutar(){
		WWWForm form = new WWWForm();
		form.AddField( "username", usuario.GetComponent<UIInput>().value );
		form.AddField( "password", pass.GetComponent<UIInput>().value  );
		form.AddField( "nombre", nombre.GetComponent<UIInput>().value  );
		form.AddField( "sexo", sexo.GetComponent<UIPopupList>().value );
		//form.AddField( "edad", edad.value);
		//form.AddField( "estado", estado.text);
		form.AddField( "mail", mail.GetComponent<UIInput>().value  );
		//form.AddField( "direccion", direccion.value );
		if (usuario.GetComponent<UIInput>().value  != "") {
			if (pass.GetComponent<UIInput>().value  != confirmarpass.GetComponent<UIInput>().value ) {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Las contraseñas deben coincidir ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			} else {
				if (mail.gameObject.GetComponent<revisarMail> ().correcto) {
					popup.SetActive (true);
					popup.transform.FindChild ("Boton").gameObject.SetActive (false);
					popup.GetComponent<UILabel> ().text = "Registrando..";
					nombre.GetComponent<UIInput>().value  = "";
					confirmarpass.GetComponent<UIInput>().value  = "";
					mail.GetComponent<UIInput>().value  = "";
					
					
					WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/register.php", form);
					yield return download;
					if (download.error != null) {
						print ("Error downloading: " + download.error);
						//mostrarError("Error de conexion");
						yield break;
					} else {
						if(download.text!="ya creado"){
							//string retorno = download.text;
							print (download.text);
							popup.GetComponent<UILabel> ().text = "Registrado Correctamente ";
							popup.transform.FindChild ("Boton").gameObject.SetActive (true);
							nombre.GetComponent<UIInput>().value  = "";
							confirmarpass.GetComponent<UIInput>().value  = "";
							mail.GetComponent<UIInput>().value  = "";
							usuario.GetComponent<UIInput>().value="";
							pass.GetComponent<UIInput>().value="";
						}
						else{
							//string retorno = download.text;
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
	IEnumerator datosEjecutar(){
		popup.SetActive (true);
		popup.GetComponent<UILabel> ().text = "Cargando Datos ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWWForm form = new WWWForm ();
        string id = PlayerPrefs.GetString("idAdmin");// instructores.GetComponent<verNiveles> ().getIDADMIN ();
        print(PlayerPrefs.GetString("idAdmin"));
		form.AddField ("id", id);
		WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/obtenerAdministrador.php", form);
		yield return download;
		
		if (download.text != "-1") {
						string retorno = download.text;
						print (retorno);
						string[] ret = retorno.Split (new char[]{'*'});
						//print (ret[0]);
						nombre.GetComponent<UIInput> ().value = ret [0];
						mail.GetComponent<UIInput> ().value = ret [1];
						sexo.GetComponent<UIPopupList> ().value = ret [2];
						usuario.GetComponent<UIInput> ().value = ret [3];
						usuario.GetComponent<UIInput> ().enabled = false;
						popup.SetActive (true);
						popup.GetComponent<UILabel> ().text = "Datos Cargados Correctamente";
						popup.transform.FindChild ("Boton").gameObject.SetActive (true);
						//instructores.GetComponent<UIPopupList> ().value = "";
				} else {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Existieron Errores";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
				}
		
	}
	IEnumerator datosAlumnoEjecutar(){
		popup.SetActive (true);
		popup.GetComponent<UILabel> ().text = "Cargando Datos ";
		popup.transform.FindChild ("Boton").gameObject.SetActive (false);
		WWWForm form = new WWWForm ();
		string id;
		alumnos.GetComponent<verAlumnos> ().id.TryGetValue (alumnos.GetComponent<UIPopupList> ().value, out id);
		form.AddField ("id",id );
		WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/obtenerAlumno.php", form);
		yield return download;
		
		if (download.text!="-1") {
			string retorno=download.text;
			//print (retorno);
			string[] ret = retorno.Split (new char[]{'*'});
			//print (ret[0]);
			nombre.GetComponent<UIInput>().value=ret[0];
			mail.GetComponent<UIInput>().value=ret[1];
			sexo.GetComponent<UIPopupList>().value=ret[2];
			usuario.GetComponent<UIInput>().value=ret[3];
			usuario.GetComponent<UIInput>().enabled=false;
			popup.SetActive (true);
			popup.GetComponent<UILabel> ().text = "Datos Cargados Correctamente";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			//alumnos.GetComponent<UIPopupList>().value="";
		}
		
	}
	IEnumerator registrarseAlumnoEjecutar(){
		WWWForm form = new WWWForm();
		form.AddField( "username", usuario.GetComponent<UIInput>().value );
		form.AddField( "password", pass.GetComponent<UIInput>().value  );
		form.AddField( "nombre", nombre.GetComponent<UIInput>().value  );
		form.AddField( "sexo", sexo.GetComponent<UIPopupList>().value );
		form.AddField( "edad", "0");
		form.AddField( "estado", "Soltero");
		form.AddField( "mail", mail.GetComponent<UIInput>().value  );
		//form.AddField( "direccion", direccion.value );
		if (usuario.GetComponent<UIInput>().value  != "") {
			if (pass.GetComponent<UIInput>().value  != confirmarpass.GetComponent<UIInput>().value ) {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Las contraseñas deben coincidir ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			} else {
				if (mail.gameObject.GetComponent<revisarMail> ().correcto) {
					popup.SetActive (true);
					popup.transform.FindChild ("Boton").gameObject.SetActive (false);
					popup.GetComponent<UILabel> ().text = "Registrando..";

					
					
					WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/registerAlumno.php", form);
					yield return download;
					if (download.error != null) {
						print ("Error downloading: " + download.error);
						//mostrarError("Error de conexion");
						return false;
					} else {
						if(download.text!="ya creado"){
							//string retorno = download.text;
							print (download.text);
							popup.GetComponent<UILabel> ().text = "Registrado Correctamente ";
							popup.transform.FindChild ("Boton").gameObject.SetActive (true);
							nombre.GetComponent<UIInput>().value  = "";
							confirmarpass.GetComponent<UIInput>().value  = "";
							mail.GetComponent<UIInput>().value  = "";
							usuario.GetComponent<UIInput>().value="";
							pass.GetComponent<UIInput>().value="";
						}
						else{
							//string retorno = download.text;
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
	IEnumerator actualizarEjecutar(){
		//print (pass.value);
		//print (pass2.value);
		if (pass.GetComponent<UIInput>().value == "" || confirmarpass.GetComponent<UIInput>().value == "") {
			popup.SetActive (true);
			popup.GetComponent<UILabel> ().text = "Debe Introducir una Contraseña";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
		} else {
			if (pass.GetComponent<UIInput>().value!= confirmarpass.GetComponent<UIInput>().value) {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Las Contraseñas deben Coincidir ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			} else {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Guardando Datos ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (false);
				WWWForm form = new WWWForm ();
				string id=instructores.GetComponent<verNiveles> ().getIDADMIN ();
				form.AddField ("admin", id);
				form.AddField ("pass", pass.GetComponent<UIInput>().value);
				form.AddField ("nombre", nombre.GetComponent<UIInput>().value);
				form.AddField ("sexo", sexo.GetComponent<UIPopupList>().value);
				form.AddField ("mail", mail.GetComponent<UIInput>().value);
				WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/editarAdministrador.php", form);
				yield return download;
				if (download.text == "correcto") {
                    GameObject.FindGameObjectWithTag("Configuracion").GetComponent<Configuracion>().mailInstructor = mail.GetComponent<UIInput>().value;
                    pass.GetComponent<UIInput>().value="";
					confirmarpass.GetComponent<UIInput>().value="";
					popup.SetActive (true);
					popup.GetComponent<UILabel> ().text = "Datos Guardados Exitosamente";
					popup.transform.FindChild ("Boton").gameObject.SetActive (true);
					reset ();
				}
			}
		}
	}
	IEnumerator actualizarAlumnoEjecutar(){
		//print (pass.value);
		//print (pass2.value);
		if (pass.GetComponent<UIInput>().value == "" || confirmarpass.GetComponent<UIInput>().value == "") {
			popup.SetActive (true);
			popup.GetComponent<UILabel> ().text = "Debe Introducir una Contraseña";
			popup.transform.FindChild ("Boton").gameObject.SetActive (true);
		} else {
			if (pass.GetComponent<UIInput>().value!= confirmarpass.GetComponent<UIInput>().value) {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Las Contraseñas deben Coincidir ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (true);
			} else {
				popup.SetActive (true);
				popup.GetComponent<UILabel> ().text = "Guardando Datos ";
				popup.transform.FindChild ("Boton").gameObject.SetActive (false);
				WWWForm form = new WWWForm ();
				string id;
				alumnos.GetComponent<verAlumnos> ().id.TryGetValue (alumnos.GetComponent<UIPopupList> ().value, out id);
				form.AddField ("id", id);
				form.AddField ("pass", pass.GetComponent<UIInput>().value);
				form.AddField ("nombre", nombre.GetComponent<UIInput>().value);
				form.AddField ("sexo", sexo.GetComponent<UIPopupList>().value);
				form.AddField ("mail", mail.GetComponent<UIInput>().value);
				WWW download = new WWW (VariablesGlobales.direccion + "SimuladorMT6020/editarAlumno.php", form);
				yield return download;
				if (download.text == "correcto") {
					pass.GetComponent<UIInput>().value="";
					confirmarpass.GetComponent<UIInput>().value="";
					popup.SetActive (true);
					popup.GetComponent<UILabel> ().text = "Datos Guardados Exitosamente";
					popup.transform.FindChild ("Boton").gameObject.SetActive (true);
					reset ();
				}
			}
		}
	}
}
