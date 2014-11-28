using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Consultas
    {

        /*
         * Metodo de consulta SQL que verifica al Aministrador que para iniciar sesion cumpla con el RUT y las calves Solicitadas
         * @param rut Representa el RUT del Administrador.
         * @param clave Representa la clave del Administrador.
         **/
        public DataTable getLogin(string rut, string clave)
        {
            string query = "SELECT COUNT(*) FROM Usuario,Alumno WHERE Usuario.rut = '" + rut + "' AND Usuario.clave = '" + clave + "'AND Usuario.rut = Alumno.rutAlumno;";
            conexion conexion = new conexion("");
            return conexion.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que verifica si un alumno tiene una cita en cierta fecha.
         * @param fecha Representa la fecha de la cita.
         **/
        public DataTable getCita(string fecha)
        {
            string query = "SELECT Cita.fecha, Cita.hora, Usuario.nombre FROM Cita, Usuario WHERE Usuario.rut = Cita.rutAlumno AND Cita.fecha ='" + fecha + "' ORDER BY Cita.hora;";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que el alumno recibe los datos de la cita en un dia determinado.
         * @param rut Representa el RUT del Alumno.
         **/
        public DataTable getCitaAlumno(string rut)
        {
            string query = "SELECT Cita.fecha, Cita.hora, Usuario.nombre,Usuario.rut FROM Cita, Usuario WHERE Usuario.rut = Cita.rutAsistente AND Cita.rutAlumno ='" + rut + "' ORDER BY Cita.fecha;";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que muestra las carreras que estudia un Alumno.
         * @param rutAlumno Representa el rut del Alumno.
         **/
        public DataTable getCarrera(string rutAlumno)
        {
            string query = "SELECT Carrera.nombreCarrera FROM Carrera, Alumno WHERE Carrera.rutAlumno = Alumno.rutAlumno AND Alumno.rutAlumno ='" + rutAlumno + "';";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que consulta la cantidad de Carreras que estudia el alumno.
         * @param rut Representa el RUT del Asistente social.
         **/
        public DataTable getCantCarrera(string rutAlumno)
        {
            string query = "SELECT COUNT(*) FROM Carrera, Alumno WHERE Carrera.rutAlumno = Alumno.rutAlumno AND Alumno.rutAlumno ='" + rutAlumno + "';";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que consulta el rut del Asistente social a cargo de un motivo específico.
         * @param motivo Representa el motivo, la especialidad del Asistente Social, puede ser una carrera u otro detalle.
         **/
        public DataTable getAsistenteCita(string motivo)
        {
            string query = "SELECT AsistenteSocial.rutAsistente FROM AsistenteSocial WHERE AsistenteSocial.tipo ='" + motivo + "';";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
        * Metodo de consulta SQL que verifica la existencia de una cita específica ingresando el motivo, el rut, la hora y la fecha especifica.
        * @param motivo Representa la especificación del Asistente Social. Puede ser una carrera u otro detalle.
        * @param rut  Representa el rut del Asistente Social.
        * @param hora Representa la hora de la cita.
        * @param fecha Representa la fecha de la cita.
        **/
        public DataTable existeCita(string motivo, string rut, string hora, string fecha)
        {
            string query = "SELECT COUNT(Cita.rutAsistente) FROM AsistenteSocial, Cita WHERE AsistenteSocial.rutAsistente = Cita.rutAsistente AND AsistenteSocial.tipo ='" + motivo + "'AND AsistenteSocial.rutAsistente ='" + rut + "'AND Cita.hora ='" + hora + "'AND Cita.fecha = '" + fecha + "';";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que muestra la cantidad de citas de acuerdo al rut del asistente social.
         * @param codigo Representa el Rut del Asistente Social.
         **/
        public DataTable existeCita(string codigo)
        {
            string query = "SELECT COUNT(Cita.rutAsistente) FROM Cita WHERE Cita.rutAsistAlumhora = '" + codigo + "';";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que muestra el código de la cita dandose a conocer el rut del asistente social y del alumno, la fecha y hora de la cita.
         * @param rutAsist Representa el RUT del Asistente social.
         * @param rutAlum Representa el RUT del Alumno.
         * @param fecha Representa la fecha de la cita.
         * @param hora Representa la hora de la cita.
         **/
        public DataTable getCodCita(string rutAsist, string rutAlum, string fecha, string hora)
        {
            string query = "SELECT Cita.rutAsistAlumhora FROM AsistenteSocial, Cita WHERE Cita.rutAsistente ='" + rutAsist + "'AND Cita.rutAlumno ='" + rutAlum + "'AND Cita.hora ='" + hora + "'AND Cita.fecha = '" + hora + "';";
            conexion conect = new conexion("");
            return conect.QueryConRetorno(query);
        }

        /*
         * Metodo de consulta SQL que inserta un nuevo dato en la Tabla de la Cita. Ingresando un codigo, el rut del Asistente Social y del Alumno, la hora y fecha de la cita nueva.
         * @param codigo Representa el codigo de la cita.
         * @param rutAsis Representa el rut del Asistente Social.
         * @param rutAlum Representa el rut del Alumno.
         * @param hora Representa la hora de la cita.
         * @param fecha Representa la fecha de la cita.
         **/
        public void insertarCita(string codigo, string rutAsis, string rutAlum, string hora, string fecha)
        {
            string query = "INSERT INTO Cita (rutAsistAlumhora,fecha,rutAsistente,rutAlumno,hora) VALUES ('" + codigo + "','" + fecha + "','" + rutAsis + "','" + rutAlum + "','" + hora + "')";
            conexion conect = new conexion("");
            conect.QuerySinRetorno(query);
        }

        /*
         * Metodo de consulta SQL que Actualiza una tupla de la tabla Cita.
         * @param codigo Representa el codigo nuevo de la cita
         * @param hora Representa la nueva hora de la cita.
         * @param fecha Representa la nueva fecha de la cita.
         * @param cod Representa el codigo antiguo de la cita.
         **/
        public void ActualizarCita(string codigo, string hora, string fecha, string cod)
        {
            string query = "UPDATE Cita SET rutAsistAlumhora ='" + codigo + "', fecha ='" + fecha + "', hora ='" + hora + "' WHERE rutAsistAlumhora='" + cod + "';";
            conexion conect = new conexion("");
            conect.QuerySinRetorno(query);
        }

        /*
         * Metodo de consulta SQL que Elimina una cita dando a conocer el codigo de la cita.
         * @param codigo Representa el codigo de la cita.
         **/
        public void EliminarCita(string codigo)
        {
            string query = "DELETE FROM Cita WHERE Cita.rutAsistAlumhora='" + codigo + "';";
            conexion conect = new conexion("");
            conect.QuerySinRetorno(query);
        }

    }
}
