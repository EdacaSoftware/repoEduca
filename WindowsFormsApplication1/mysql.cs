using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using MySql.Data;
using MySql.Data.MySqlClient;



public class mysql
{
    private MySqlConnection conexion = new MySqlConnection();
    private MySqlCommand comando = new MySqlCommand();
    private MySqlDataReader lector;
    private MySqlTransaction transaccion;

    private MySqlDataAdapter adaptador;

    public mysql()
    {
        conexion.ConnectionString = "Server=svnserver.disc.ucn.cl;user id=userig12; password=GJ8vmk+jHK;Database=dbuser12;";
    }

    public MySqlDataAdapter adap(MySqlCommand comando)
    {
        MySqlDataAdapter adapa = new MySqlDataAdapter(comando);
        return adapa;
    }

    //**********************************************************'
    //*Nombre método: EjecutarQuerySinRetorn
    //*@Param: La query (consulta) que se desea realizar
    //**********************************************************'
    public void EjecutarQuerySinRetorno(string Query)
    {
        try
        {
            conexion.Open();
            //se abre la conexión
            comando.CommandText = Query;
            //se le asocia la query al comando
            comando.Connection = conexion;
            //se le asigna la conexión al comando
            comando.ExecuteNonQuery();
            //se ejecuta
            conexion.Close();
            //se cierra la conexión
        }
        catch (Exception ex)
        {
            throw new Exception("Ocurrió un error al ejecutar la query (insert/update/delete)." + ex.Message);
        }
    }
    //**********************************************************'
    //*Nombre método: EjecutarQuery
    //*Retorna un DataTable con las tuplas que devuelva la query
    //*@Param: La query (consulta) que se desea realizar
    //**********************************************************'
    public DataTable EjecutarQuery(string Query)
    {
        try
        {
            DataTable dt = new DataTable();
            //se crea un nuevo datatable
            conexion.Open();
            //se abre la conexión
            comando.CommandText = Query;
            //se le asigna la query al comando
            comando.Connection = conexion;
            //se le asigna la conexión al comando
            transaccion = conexion.BeginTransaction();
            adaptador = adap(comando);
            //se le asocia el comando al adaptador
            adaptador.Fill(dt);
            //el adaptador se encarga de poblar el datatable
            conexion.Close();
            //se cierra la conexión
            return dt;
            //se retorna el datatable con las tuplas ya cargadas
        }
        catch (Exception ex)
        {
            throw new Exception("Ocurrió un error al obtener a los datos." + ex.Message);
        }


    }
}