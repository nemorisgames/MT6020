using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;

/// <summary>
/// Descripción breve de DataBase
/// </summary>
public class DataBase
{
    private string source;
    private MySqlConnection conexion;
   
    public DataBase()
    {
        source = "server= localhost;" +
                "database =mt6020;" +
                "Uid = root;" +
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
            //MessageBox.Show(e.ToString());
            MessageBox.Show(e.ToString(),"Mensaje",MessageBoxButton.OK,MessageBoxImage.Warning);
            conexion.Close();
           
        }
    }
    
    public DataTable Consultar(String consulta)
    {
        try
        {
            conexion.Open();
            DataTable resultado= new DataTable();
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            adaptador.Fill(resultado);
            conexion.Close();
            return resultado;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            conexion.Close();
            return null;
        }
    }

    public int LastID(string nombreTabla)
    {
        try
        {
            conexion.Open();
            DataTable resultado = new DataTable();
            MySqlCommand comando = new MySqlCommand("SELECT id FROM "+nombreTabla+" ORDER BY id DESC LIMIT 1", conexion);
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            adaptador.Fill(resultado);
            conexion.Close();
            return int.Parse(resultado.Rows[0][0].ToString());
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Warning);
            conexion.Close();
            return -1;
        }



        return 0;
    }
   
}

