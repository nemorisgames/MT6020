using UnityEngine;
using System;
using System.Collections;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class ControlBaseDatos : MonoBehaviour {
    DataBase db = new DataBase();
    // Use this for initialization
    void Start () {
        //datos = db.Consultar("SELECT * FROM module");
        //loginSupervisor("Administrador", "admin");
    }

/*    public void login(string username, string password, bool operador, Action<ArrayList, string[]> callback)
    {
        MySqlDataReader datos;
        if (!operador)
        {
            if (username == "Administrador" && password == "admin")
            {
                datos = db.Consultar("select id, user, pass, rut, email from admin where user = \"" + username + "\" and pass = \"" + password + "\"");
            }
            else
                datos = db.Consultar("select id, user, pass, rut, email from supervisor where user = \"" + username + "\" and pass = \"" + password + "\"");
        }
        else
            datos = db.Consultar("select id, user, pass, rut, email from user where user = \"" + username + "\" and pass = \"" + password + "\"");
        StartCoroutine(espera(datos, new string[5] { "id", "user", "pass", "rut", "email" }, callback));
    }

    public void verAlumnos(string idSupervisor, Action<ArrayList, string[]> callback)
    {
        MySqlDataReader datos;
        datos = db.Consultar("select a.name, a.lastname , a.id from user a, supervisor_user b where a.id = b.fk_user and b.fk_supervisor  = " + idSupervisor);
        print("select a.name, a.lastname , a.id from user a, supervisor_user b where a.id = b.fk_user and b.fk_supervisor  = " + idSupervisor);

        StartCoroutine(espera(datos, new string[3] { "name", "lastname", "id" }, callback));
    }

    public void vincularAlumno(string idUser, string idSupervisor) 
    {
        db.EjecutarConsultar("INSERT INTO supervisor_user(fk_supervisor, fk_user) values( " + idUser + ", " + idSupervisor + ")");
        print("INSERT INTO supervisor_user(fk_supervisor, fk_user) values( " + idUser + ", " + idSupervisor + ")");
    }*/

    /*void procesarLoginAdministrador(ArrayList resultado, string[] nombreCampos)
    {
        if (resultado.Count > 0)
        {
            (((string[])resultado[0])[0]);
        }
    }*/

 /*   IEnumerator espera(MySqlDataReader datos, string[] campos, Action<ArrayList, string[]> callback)
    {
        ArrayList resultado = new ArrayList();
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //print(datos.IsClosed);
            //if (datos != null && datos.HasRows)
            //{
            if (datos != null && datos.HasRows)
            {
                while (datos.Read())
                {
                    print("filas encontradas");
                    string[] fila = new string[campos.Length];
                    for(int j = 0; j < campos.Length; j++)
                    {
                        fila[j] = datos[campos[j]].ToString();
                        print(fila[j]);
                    }
                    resultado.Add(fila);
                }
                callback(resultado, campos);
                Debug.Log("Información recibida " + callback.ToString());
                return false;
            }
            //}
        }
        Debug.Log("Información no recibida " + callback.ToString());
        callback(resultado, campos);
    }*/

    // Update is called once per frame
    void Update () {
	
	}
}
