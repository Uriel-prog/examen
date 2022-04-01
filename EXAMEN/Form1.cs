using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXAMEN;
using Npgsql;


namespace EXAMEN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void configuracion()
        {
            DataBasePacientes dataBasePacientes = new DataBasePacientes();
            try
            {
                var estado = dataBasePacientes.crearDb();
                MessageBox.Show("Se ha creado la base de datos");

                if (estado.State == System.Data.ConnectionState.Closed)
                {
                    dataBasePacientes.CrearTablaDb();
                    MessageBox.Show("Se ha creado la tabla correctamente");
                }

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LlenarDataGridView(List<ModeloPacientes> modeloList)
        {
            if (modeloList != null)
            {
                dataGridView.DataSource = modeloList;

            }

        }

        private void ejecutar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                configuracion();
                radioButton1.Enabled = false;
            }
            else if (radioButton2.Checked)
            {
                DataBasePacientes dataBasePacientes = new DataBasePacientes();
                ModeloPacientes modelpacientes = new ModeloPacientes();
                modelpacientes.PacienteNombre = textBox1.Text;
                modelpacientes.PacienteApellido = textBox2.Text;
                modelpacientes.PacienteEdad = int.Parse(textBox3.Text);
                modelpacientes.PacienteMotivoDeConsulta = textBox4.Text;
                dataBasePacientes.insertar(modelpacientes);
                MessageBox.Show("Se inserto correctamente");
                

            }
            else if (radioButton3.Checked)
            {
                DataBasePacientes datos = new DataBasePacientes();
                try
                {
                    List<ModeloPacientes> modeloList = datos.ConsultaDos(int.Parse(textBox3.Text));
                    LlenarDataGridView(modeloList);    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error" + ex);
                }
            }
            else if (radioButton4.Checked)
            {

            }else if (radioButton5.Checked) { 
            }
        }
    }
}
