using UnityEngine;
using System;
using MySql.Data.MySqlClient;


/// <summary>
/// Descripción breve de DataBase
/// </summary>
public class DataBase
{
    private string source;
    private MySqlConnection conexion;
    public bool conectado = false;
   
    public DataBase()
    {
        source = "server= localhost;" +
                "database =generico;" +
                "Uid =root;" +
                "Pooling=false;" +
                "password= ";
        conexion = new MySqlConnection(source);
    }
    public void EjecutarConsultar(String consulta)
    {
        try
        {
            conexion.Open();
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
           conexion.Close();
           
        }
    }
    
    public MySqlDataReader Consultar(String consulta)
    {
        try
        {
            conexion.Open();
            MySqlCommand comando = conexion.CreateCommand();
            
            comando.CommandText = consulta;
            MySqlDataReader datos = comando.ExecuteReader();
       
          /*  while (datos.Read())
            {
                string usuario = (string)datos["name"];
                Debug.Log("Usuario " + usuario);
            }*/
            
            return datos;
            conexion.Close();
        }
        catch (Exception e)
        {
            conexion.Close();
            return null;
        }

        
    }

   
}

