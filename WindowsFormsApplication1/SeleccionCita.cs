using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class SeleccionCita : Form
    {
        Consultas consu = new Consultas();
        string m;
        string r;
        string r2;
        string oldcodigo;
        public SeleccionCita(string motivo, string rut)
        {
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            m = motivo;
            r = rut;
            button1.Enabled = true;
            button1.Visible = true;
        }

        public SeleccionCita(string rut, string cod, string v)
        {
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            r = rut;
            r2 = v;
            oldcodigo = cod;
            button2.Enabled = true;
            button2.Visible = true;
        }

        /*
         * Metodo que permite modificar una cita.
         **/
        private void button2_Click(object sender, EventArgs e)
        {
            string codigo;
            DataTable noCita = consu.existeCita(oldcodigo);
            codigo = "" + r2 + "" + r + "" + comboBox1.Text + "" + dateTimePicker1.Text;
            MessageBox.Show(codigo + ", " + r2 + ", " + r + ", " + comboBox1.Text + ", " + dateTimePicker1.Text);

            if (noCita.Rows[0][0].ToString().Equals("1"))
            {
                consu.ActualizarCita(codigo, comboBox1.Text, dateTimePicker1.Text, oldcodigo);
                MessageBox.Show("Se ha modificado la cita con exito!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Esa hora ya está ocupada, seleccione otra hora");
            }
        }

        /*
         * Metodo que permite crear una nueva cita.
         **/
        private void button1_Click_1(object sender, EventArgs e)
        {
            string codigo;
            DataTable dtc = consu.getAsistenteCita(m);
            DataTable noCita = consu.existeCita(m, dtc.Rows[0][0].ToString(), comboBox1.Text, dateTimePicker1.Text);
            codigo = "" + dtc.Rows[0][0].ToString() + "" + r + "" + comboBox1.Text + "" + dateTimePicker1.Text;
            MessageBox.Show(codigo + ", " + dtc.Rows[0][0].ToString() + ", " + r + ", " + comboBox1.Text + ", " + dateTimePicker1.Text);

            if (noCita.Rows[0][0].ToString().Equals("1"))
            {
                MessageBox.Show("Esa hora ya está ocupada, seleccione otra hora");
            }
            else
            {
                consu.insertarCita(codigo, dtc.Rows[0][0].ToString(), r, comboBox1.Text, dateTimePicker1.Text);
                MessageBox.Show("Se ha creado la cita con exito!");
                this.Close();
            }
        }

        /*
         * Metodo que impide la escritura en el combobox que solicita la cita.
         **/
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        /*
         * Metodo que impide la seleccion de los dias sabados y domingos al seleccionar o cambiar las citas.
         **/
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value.Date;

            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                MessageBox.Show("No se permiten fin de semanas");
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateTimePicker1.Text = dateTimePicker1.Value.AddDays(1).ToString();
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    dateTimePicker1.Text = dateTimePicker1.Value.AddDays(2).ToString();
                }
            }
        }


    }
}
