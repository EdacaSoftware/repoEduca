using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;

public class conexion
{
    //hola soy un cambio
    //string con el motor que se utilizara
    string motor = "";
    mysql bdmysql = new mysql();
    public conexion(string motor)
    {
        this.motor = motor;
    }

    public DataTable QueryConRetorno(string query)
    {
        return bdmysql.EjecutarQuery(query);
    }

    public void QuerySinRetorno(string query)
    {
        bdmysql.EjecutarQuerySinRetorno(query);
    }

}