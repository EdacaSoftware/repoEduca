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
    public partial class Educa : Form
    {
        Consultas consu = new Consultas();
        public Educa()
        {
            InitializeComponent();
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                // Display a MsgBox asking the user to save changes or abort. 
                if (MessageBox.Show("Do you want to save changes to your text?", "My Application",
                   MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                    // Call method to save file...
                }
        }

        /*
         * Metodo que llama a consultar a la base de datos a través de un rut y la clave.
         * Dependiendo de lo solicitado, llevará a un menú específico. 
         * Considerar que este menú esta diseñado sólo para los Alumnos.
         **/
        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1 y textBox2 son el rut y la clave del alumno respectivamente
            DataTable dt = consu.getLogin(textBox1.Text, textBox2.Text);
            if (dt.Rows[0][0].ToString().Equals("1")){ //si entra en el if se encontro un alumno con ese rut y clave
                button1.Visible = false; //desabilita el boton inicio de sesion
                button1.Enabled = false;
                pictureBox1.Image = Image.FromFile(@"Educa\Base Acceso Agenda.png"); //cambia la pagina del menu de educa
                label1.Visible = false; //hace invicible al label que tiene escrito rut
                label2.Visible = false; //hace invicible al label que tiene escrito clave
                label3.Visible = true; //hace vicible al label que tiene escrito Solicitar hora con la asistente social
                textBox1.Visible = false; //desabilita el textBox1
                textBox1.Enabled = false;
                textBox2.Visible = false; //desabilita el textBox12
                textBox2.Enabled = false;
            }
            else //si entra en el else significa que no en contro al alumno consultado en la base de datos
            {
                pictureBox1.Image = Image.FromFile(@"Educa\error.png");//cambia a la pagina de error de inicio de sesion del educa
                button1.Visible = false; //desabilita el boton inicio de sesion
                button1.Enabled = false;
                label1.Visible = false; //hace invicible al label que tiene escrito rut
                label2.Visible = false; //hace invicible al label que tiene escrito clave
                textBox1.Visible = false; //desabilita el textBox1
                textBox1.Enabled = false;
                textBox2.Visible = false; //desabilita el textBox2
                textBox2.Enabled = false;
                button2.Enabled = true; //habilita el boton atras para poder volver a intentar iniciar sesion
                button2.Visible = true;
            }
        }

        /*
         * Metodo que abre el menu para solicitar, modificar o eliminar citas.
         * Dependiendo de lo solicitado, llevará a un formulario específico. 
         * Considerar que este menú esta diseñado sólo para los Alumnos.
         **/
        private void label3_Click(object sender, EventArgs e)
        {
            MenuSolicitud frm = new MenuSolicitud(textBox1.Text);
            frm.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"Educa\Base Inicio.png"); //cambia la pagina al inicio de sesion del educa
            button1.Visible = true; //habilita el boton inicio de sesion
            button1.Enabled = true;
            label1.Visible = true; //hace vicible al label que tiene escrito rut
            label2.Visible = true; //hace vicible al label que tiene escrito clave
            textBox1.Visible = true; //habilita el textBox1
            textBox1.Enabled = true;
            textBox2.Visible = true; //habilita el textBox1
            textBox2.Enabled = true;
            button2.Enabled = false; //desabilita el boton atras
            button2.Visible = false;
        }

        /*
         * Metodo que el RUT sólo permita numeros.
         **/
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /*
         * Método que a la clave sólo pueda colocar numeros y letras.
         **/
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /*
         * Metodo que Modifica el RUT una vez ingresado.
         * Formato Nuevo: XX.XXX.XXX-X
         **/
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string run = textBox1.Text;
            string aux = "";
            run.ToCharArray();
            if (run.Length == 9 && validarRut(run))
            {
                for (int i = 0; i < run.Length; i++)
                {
                    if (i == 1 || i == 4)
                    {
                        aux += run[i].ToString() + ".";
                    }
                    else if (i == 7)
                    {
                        aux += run[i].ToString() + "-";
                    }
                    else
                    {
                        aux += run[i].ToString();
                    }
                }
                textBox1.Text = aux;
            }
            else if (run.Length == 8 && validarRut(run))
            {
                for (int i = 0; i < run.Length; i++)
                {
                    if (i == 0 || i == 3)
                    {
                        aux += run[i].ToString() + ".";
                    }
                    else if (i == 6)
                    {
                        aux += run[i].ToString() + "-";
                    }
                    else
                    {
                        aux += run[i].ToString();
                    }
                }
                textBox1.Text = aux;
            }
        }

        /**
         * Metodo que valida el rut (Sistema Chileno).
         */
        public bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
    }
}
