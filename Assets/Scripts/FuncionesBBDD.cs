using System;
using System.Data.SqlClient;
using System.IO;
using UnityEngine;

public class FuncionesBBDD : MonoBehaviour
{
    SqlConnection conexion;
    private string[] args;
    private int id = 0;

    //Conecta con la base de datos 
    public void conectar()
    {
        //192.168.4.59 proconsi
        //192.168.1.42 casa
        string connectionString = @"Data Source =192.168.4.59 ; user id = juego;password = root;Initial Catalog = VideoJuegosIristea;";
        conexion = new SqlConnection(connectionString);
    }

    //Inserto los datos del juego obtenidos
    public void insertarResultados(Resultados objeto)
    {
        //Extraigo el id pasado como argumento para realizar las consultas
        args = Environment.GetCommandLineArgs();
        id = int.Parse(args[1]);

        //Conectar a la base de datos
        conectar();

        //Abrir la conexion
        conexion.Open();

        //Instruccion sql para insertar 
        string query = "UPDATE Juegos SET resultados = @result FROM Juegos WHERE id = @id";

        //Ejecutamos la instruccion pasandole la conexion de la base de datos
        SqlCommand com = new SqlCommand(query, conexion);

        //Pasamos los datos a un string
        string json = JsonUtility.ToJson(objeto);

        //Limpiamos los parametros del sqlcomand
        com.Parameters.Clear();

        //Insertamos el id como parametro
        com.Parameters.AddWithValue("@id", id);

        //Insertamos el json string como parametro
        com.Parameters.AddWithValue("@result", json);

        com.ExecuteNonQuery();

        //Cerrar la conexion
        conexion.Close();

    }

    public Datos leerConfiguracion()
    {
        //Extraigo el id pasado como argumento para realizar las consultas
        args = Environment.GetCommandLineArgs();
        id = int.Parse(args[1]);

        //Instanciar el SqlConection llamado conexion 
        conectar();

        //Instruccion sql para leer la configuracion del ultimo estado
        string query = "SELECT (juego) FROM Juegos WHERE id = @id";

        conexion.Open();
 
        //Ejecutamos la instruccion pasandole la conexion de la base de datos
        SqlCommand command = new SqlCommand(query, conexion);

        //Insertamos el id como parametro
        command.Parameters.AddWithValue("@id", id);

        //Guardamos la configuracion del juego en resultados
        using (SqlDataReader reader = command.ExecuteReader())
        {
            Datos result = new Datos();
            string resultados = "";

            while (reader.Read())
            {

                resultados = reader.GetString(0);

            }

            //Convertimos los datos guardados en resultados en un json que tiene como estructura la clase Datos
            result = JsonUtility.FromJson<Datos>(resultados);

            //Cerramos la conexion
            conexion.Close();


            return result;


        }


    }

    public void insertarEstado()
    {
        //Extraigo el id pasado como argumento para realizar las consultas
        args = Environment.GetCommandLineArgs();
        id = int.Parse(args[1]);


        //Conectar a la base de datos
        conectar();
        //Abrir la conexion
        conexion.Open();

        //Instruccion sql para insertar 
        string query = "UPDATE Juegos SET estado = @state FROM Juegos WHERE id = @id";

        //Ejecutamos la instruccion pasandole la conexion de la base de datos
        SqlCommand com = new SqlCommand(query, conexion);

        //Limpiamos los parametros del sqlcomand
        com.Parameters.Clear();

        com.Parameters.AddWithValue("@id", id);

        //Insertamos el json string como parametro
        com.Parameters.AddWithValue("@state", 1);

        com.ExecuteNonQuery();

        //Cerrar la conexion
        conexion.Close();

    }
}
