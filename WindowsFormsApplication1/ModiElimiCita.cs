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
    public partial class ModiElimiCita : Form
    {
        string rut;
        string cod = "";
        Consultas consu = new Consultas();
        public ModiElimiCita(string r)
        {
            InitializeComponent();
            DataTable dt = consu.getCitaAlumno(r);
            dataGridView1.DataSource = dt;
            rut = r;
            button1.Enabled = true;
            button1.Visible = true;
        }

        public ModiElimiCita(string r, string v)
        {
            InitializeComponent();
            DataTable dt = consu.getCitaAlumno(r);
            dataGridView1.DataSource = dt;
            rut = r;
            button2.Enabled = true;
            button2.Visible = true;
        }

        /*
         * Metodo que obtine el codigo de la cita atra ves un string
         * @param codigo: un string que contiene el codigo que se utilaza para obtener el codigo de la cita.
         **/
        public string getCodigo(string codigo)
        {
            string aux = "";
            codigo.ToCharArray();
            for (int i = 0; i < (codigo.Length - 18); i++)
            {
                aux = aux + codigo[i].ToString();
            }
            string aux2 = "";
            string aux3 = "";
            string aux4 = "";
            int cont = 0;
            for (int i = codigo.Length - 18; i < (codigo.Length - 8); i++)
            {
                if(codigo[i].ToString().Equals("/"))
                {
                    cont++;
                }
                else if(cont ==0)
                {
                    aux2 = aux2 + codigo[i].ToString();
                }
                else if(cont ==1)
                {
                    aux3 = aux3 + codigo[i].ToString();
                }
                else
                {
                    aux4 = aux4 + codigo[i].ToString();
                }
            }
            aux = aux + aux4 + "-" + aux3 + "-" + aux2;

            return aux;
        }

        /*
         * Metodo que permite elimnar una cita de la base de datos
         **/
        private void button2_Click_1(object sender, EventArgs e)
        {
            int posicion = dataGridView1.CurrentRow.Index;
            string ra = dataGridView1.Rows[posicion].Cells[3].Value.ToString();
            string codigo = getCodigo((ra + rut + dataGridView1.Rows[posicion].Cells[1].Value.ToString() + dataGridView1.Rows[posicion].Cells[0].Value.ToString()));
            consu.EliminarCita(codigo);
            MessageBox.Show("Se ha eliminado la cita con exito");
            this.Close();
        }

        /*
         * Metodo que envia a una nueva ventana que permitira al alumno modificar su cita para otro dia
         **/
        private void button1_Click_1(object sender, EventArgs e)
        {
            int posicion = dataGridView1.CurrentRow.Index;
            string ra = dataGridView1.Rows[posicion].Cells[3].Value.ToString();
            cod = getCodigo((ra + rut + dataGridView1.Rows[posicion].Cells[1].Value.ToString() + dataGridView1.Rows[posicion].Cells[0].Value.ToString()));
            SeleccionCita fs = new SeleccionCita(rut, cod, ra);
            fs.Show();
        }
    }
}
