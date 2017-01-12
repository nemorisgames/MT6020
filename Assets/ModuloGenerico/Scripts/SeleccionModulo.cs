using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class SeleccionModulo : MonoBehaviour {

    DataBase db = new DataBase();
    MySqlDataReader datos;
    MySqlDataReader datos2;
    public UIPopupList CBSeleccionTipo;
    string nombre;

    // Use this for initialization
    void Start()
    {
        //datos = db.Consultar("SELECT * FROM module");
        
        //StartCoroutine(espera(datos));
        StartCoroutine(wait());
    }

    public void crearModulo()
    {
        PlayerPrefs.SetInt("idModulo", 0);
        SceneManager.LoadScene("Information");
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    IEnumerator espera(MySqlDataReader datos)
    {

        for (int i = 0; i < 500; i++)
        {
            yield return new WaitForSeconds(0.1f);
            print(datos.IsClosed);
            if (datos != null && datos.HasRows)
            {
                if (datos != null && datos.HasRows)
                {
                    while (datos.Read())
                    {
                        nombre = (string)datos["name"];
                        print(nombre);
                        CBSeleccionTipo.AddItem(nombre);
                    }

                }
                Debug.Log("Información recibida");
                return false;
            }
        }
        Debug.Log("Información no recibida");
    }

    IEnumerator wait(){
        WWW download = new WWW(db.direccion + "seleccionModulo.php");
        yield return download;

        string retorno = download.text;
        print(retorno);
        string[] ret = retorno.Split(new char[]{'|'});
        for(int i=0;i<ret.Length-1;i++){
            string[] ret2=ret[i].Split(new char[]{'*'});
            print ("asd: " + ret2[0]);
            print ("asdds: " + ret2[1]);
            nombre = (string)ret2[1];
            print("nombre: " + nombre);
            CBSeleccionTipo.AddItem(nombre);
        }
    }
}
