using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Arreglos
{
    public partial class Form1 : Form
    {
        private List<Contacto> Contactos= new List<Contacto>();
        private int indice = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader lector = new StreamReader("Agenda.txt");
                string linea;
                while((linea=lector.ReadLine())!=null)
                {
                    int posicion;
                    Contacto persona = new Contacto();
                    posicion = linea.IndexOf("|");
                    persona.Nombre = linea.Substring(0, posicion);
                    linea=linea.Substring(posicion+1);
                    posicion =linea.IndexOf("|");
                    persona.Apellido =linea.Substring(0, posicion);
                    linea = linea.Substring(posicion+1);
                    posicion =linea.IndexOf("|");
                    persona.Telefono =linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf('|');
                    persona.Correo = linea.Substring(0, posicion);
                    Contactos.Add(persona);
                }
                lector.Close();
                actualizaVista();

            }
            catch(Exception ec) 
            {
                Console.WriteLine("Exception: " + ec.Message);
            }
            finally
            {
                Console.WriteLine("Ejecucion finalizada");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Contacto persona = new Contacto();
            persona.Nombre=tbNombre.Text;
            persona.Apellido=tbApellido.Text;
            persona.Telefono=tbTelefono.Text;
            persona.Correo=tbCorreo.Text;
            if(indice > -1)
            {
                Contactos[indice] = persona;
                indice = -1;
            }
            else
            {
                Contactos.Add(persona);
            }
           
            actualizaVista();
            limpiaCampos();
        }
        private void actualizaVista()
        {
            dgvContactos.DataSource = null;
            dgvContactos.DataSource = Contactos;
            dgvContactos.ClearSelection();
        }

        private void dgvContactos_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow renglon = dgvContactos.SelectedRows[0];
             indice =dgvContactos.Rows.IndexOf(renglon);
            Contacto persona = Contactos[indice];
            tbNombre.Text = persona.Nombre;
            tbApellido.Text = persona.Apellido;
            tbTelefono.Text=persona.Telefono;
            tbCorreo.Text = persona.Correo;

        }
        private void limpiaCampos()
        {
            tbNombre.Text = null;
            tbApellido.Text = null;
            tbTelefono.Text = null;
            tbCorreo.Text = null;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(indice > -1)
            {
                Contactos.RemoveAt(indice);
                actualizaVista();
                limpiaCampos();
                indice = -1;
            }
            else
            {
                MessageBox.Show("Selecciona el registro que deseas borrar");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            TextWriter Escribir = new StreamWriter("Agenda.txt");
            foreach(Contacto persona in Contactos)
            {
                Escribir.WriteLine(persona.Nombre+ "|"+ persona.Apellido+ "|"+persona.Telefono+ "|"+persona.Correo+ "|");
            }
            Escribir.Close();
            MessageBox.Show("Contactos Guardados");

        }
    }
}
