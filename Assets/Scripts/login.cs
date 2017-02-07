
using UnityEngine;
using System.Collections;

public class login : MonoBehaviour
{
    public UIInput rut;
    public UIInput clave;
    public GameObject cosasLogin;
    public GameObject obOpciones;
    public GameObject popup;
    public GameObject obAdmin;

    public GameObject teclado;

    Configuracion conf;
    // Use this for initialization
    void Start()
    {
        GameObject confi = GameObject.FindGameObjectWithTag("Configuracion");
        conf = confi.GetComponent<Configuracion>();
        //print (conf.logeado);
        if (conf.logeado)
        {
            if (conf.usuario == "Administrador")
            {
                obAdmin.SetActive(true);
                cosasLogin.SetActive(false);
            }
            else
            {
                if (teclado != null) teclado.SetActive(false);
                obOpciones.SetActive(true);
                cosasLogin.SetActive(false);
            }
        }

        /*print("pantallas: " + Display.displays.Length);
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        Display.displays[0].SetParams(1366, 768, 0, 0);
        Display.displays[1].SetParams(1920, 1080, 0, 0);
        Display.displays[2].SetParams(1920, 1080, 0, 0);
        Display.displays[3].SetParams(1920, 1080, 0, 0);
        Display.displays[4].SetParams(800, 480, 0, 0);

        Display.displays[5].SetParams(1920, 1080, 0, 0);*/
        Screen.SetResolution(9600, 1080, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cerrarSimulador()
    {
        Application.Quit();
    }

    public void loguearOperador()
    {
        StartCoroutine(loginEjecutar(true));
    }
    public void loguear()
    {
        StartCoroutine(loginEjecutar(false));
    }
    public void logoutOperador()
    {
        GameObject confi = GameObject.FindGameObjectWithTag("Configuracion");
        Configuracion conf = confi.GetComponent<Configuracion>();
        conf.alumno = "";
        cosasLogin.SetActive(true);
        obOpciones.SetActive(false);
        rut.value = "";
        clave.value = "";
        if (teclado != null) teclado.SetActive(true);
    }
    public IEnumerator loginEjecutar(bool operador)
    {
        if (operador && teclado != null) teclado.SetActive(false);
        WWWForm form = new WWWForm();
        form.AddField("username", rut.value);
        form.AddField("password", clave.value);
        form.AddField("admin", (rut.value=="Administrador"?"admin":"instructor"));
        popup.SetActive(true);
        popup.transform.FindChild("Boton").gameObject.SetActive(false);
        popup.GetComponent<UILabel>().text = "Iniciando Sesion..";

        //print (VariablesGlobales.direccion + "SimuladorMT6020/login" + (operador ? "Usuario" : "") + ".php");
        WWW download = new WWW(VariablesGlobales.direccion + "SimuladorMT6020/login" + (operador ? "Usuario" : "") + ".php", form);
        yield return download;
        if (download.error != null)
        {
            print("Error downloading: " + download.error);
            //mostrarError("Error de conexion");
			yield return false;
        }
        else
        {
            string retorno = download.text;
            print(retorno);
            string[] ret = retorno.Split(new char[] { '|' });
            for (int i = 0; i < ret.Length; i++)
            {
                print(ret[i]);
            }
            if (ret[0] == "correcto")
            {
                if (!operador)
                    PlayerPrefs.SetString("idAdmin", ret[1]);
                //SceneManager.LoadScene ("OpcionesAdmin");
                popup.SetActive(false);

                GameObject confi = GameObject.FindGameObjectWithTag("Configuracion");
                Configuracion conf = confi.GetComponent<Configuracion>();
                if (!operador)
                {
                    conf.logeado = true;
                    conf.pass = clave.value;
                    conf.usuario = rut.value;
                    conf.mailInstructor = ret[2];
                    //popup.GetComponent<UILabel>().text="Bienvenido "+rut.value;
                    //popup.transform.FindChild ("Boton").gameObject.SetActive (true);

                }
                else
                {

                    conf.alumno = ret[1];
                }

                StartCoroutine(revisarAlumnoVinculado(operador));
            }
            else
            {
                //if(operador) teclado.SetActive(true);
                popup.GetComponent<UILabel>().text = "Datos Incorrectos";
                popup.transform.FindChild("Boton").gameObject.SetActive(true);
            }
            //comprueba si lo que devuelve es informacion de alguien que existe

        }

    }

    public void cerrandoPopup()
    {
        if (conf.alumno != rut.value)
        {
            if (teclado != null) teclado.SetActive(true);
        }
    }

    public IEnumerator revisarAlumnoVinculado(bool operador)
    {
        print("revisando alumno vinculado");
        print(PlayerPrefs.GetString("idAdmin"));
        if (PlayerPrefs.GetString("idAdmin") != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("id", PlayerPrefs.GetString("idAdmin"));
            //print ("id admin " + PlayerPrefs.GetString ("idAdmin"));

            WWW download = new WWW(VariablesGlobales.direccion + "SimuladorMT6020/verAlumnos.php", form);
            yield return download;
            //print(download.text);
            if (download.error != null)
            {
                print("Error downloading: " + download.error);
                if (operador && teclado != null) teclado.SetActive(true);
                //mostrarError("Error de conexion");
				yield return false;
            }
            else
            {

                string retorno = download.text;
                print (retorno);
                string[] ret = retorno.Split(new char[] { '*' });
                bool alumnoVinculado = false;
                for (int i = 0; i < ret.Length - 1; i++)
                {
                    print (ret[i]);
                    string[] ret2 = ret[i].Split(new char[] { '|' });
                    print (ret2[0]);
                    print (ret2[1]);
                    print("probando " + conf.alumno + "==" + ret2[0]);
                    if (conf.alumno == ret2[0])
                    {
                        alumnoVinculado = true;
                    }
                }

                if (conf.alumno != "" && conf.alumno != null && !alumnoVinculado)
                {
                    WWWForm formVincular = new WWWForm();
                    formVincular.AddField("idAlumno", conf.alumno);
                    formVincular.AddField("idAdmin", PlayerPrefs.GetString("idAdmin"));
                    print("vincular: " + conf.alumno + " " + PlayerPrefs.GetString("idAdmin"));
                    WWW downloadVincular = new WWW(VariablesGlobales.direccion + "SimuladorMT6020/vincularAlumno.php", formVincular);
                    yield return downloadVincular;
                    print(downloadVincular.text);
                }

            }
        }
        if (!operador)
        {
            if (rut.value == "Administrador")
            {
                obAdmin.SetActive(true);
                cosasLogin.SetActive(false);
            }
            else
            {
                obOpciones.SetActive(true);
                cosasLogin.SetActive(false);
            }
        }
        else
        {
            if (rut.value != "")
            {
                cosasLogin.SetActive(false);
                obOpciones.SetActive(true);
            }
            else
                if (operador) if (teclado != null) teclado.SetActive(true);
        }

        rut.value = "";
        clave.value = "";
        yield break;
    }
}
