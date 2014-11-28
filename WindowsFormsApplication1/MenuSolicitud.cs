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
    public partial class MenuSolicitud : Form
    {
        string rut;
        public MenuSolicitud(string run)
        {
            InitializeComponent();
            rut = run;
        }

        /**
         * Método que crea una nueva ventana "Solicitar Cita" al hacer click con el mismo nombre.
         **/
        private void button1_Click_1(object sender, EventArgs e)
        {
            MotivoCita f = new MotivoCita(rut);
            f.Show();
        }

        /**
         * Metodo que crea una nueva ventana de "Solicitar Cita" pero con función para modificar la cita.
         **/
        private void button2_Click_1(object sender, EventArgs e)
        {
            ModiElimiCita f = new ModiElimiCita(rut);
            f.Show();
        }

        /**
         * Metodo que crea una nueva ventana de "Solicitar Cita" pero con función de eliminiar una cita.
         **/
        private void button3_Click_1(object sender, EventArgs e)
        {
            ModiElimiCita f = new ModiElimiCita(rut, "");
            f.Show();
        }
    }
}
