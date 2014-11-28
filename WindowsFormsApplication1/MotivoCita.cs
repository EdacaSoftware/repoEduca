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
    public partial class MotivoCita : Form
    {
        Consultas consu = new Consultas();
        string r;
        public MotivoCita(string rut)
        {
            InitializeComponent();
            DataTable dt = consu.getCarrera(rut);
            DataTable dt2 = consu.getCantCarrera(rut);
            int cantidad = Int32.Parse(dt2.Rows[0][0].ToString());
            for (int i = 0; i < cantidad; i++)
            {
                // Agrega carreras del alumno.
                comboBox1.Items.Add(dt.Rows[i][0].ToString());

            }
            r = rut;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionCita fsc = new SeleccionCita(comboBox1.Text, r);
            fsc.Show();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
