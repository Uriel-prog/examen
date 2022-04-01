using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMEN
{
    public class ModeloPacientes
    {
        private int pacienteId;
        private string pacienteNombre;
        private string pacienteApellido;
        private int pacienteEdad;
        private string pacienteMotivoDeConsulta;

        public ModeloPacientes(){
        }


        public int PacienteId
        {
            get { return pacienteId; }
            set { pacienteId = value; }
        }
        public string PacienteNombre
        {
            get { return pacienteNombre; }
            set { pacienteNombre = value; }
        }

        public string PacienteApellido
        {
            get { return pacienteApellido; }
            set { pacienteApellido = value; }
        }

        public int PacienteEdad
        {
            get { return pacienteEdad; }
            set { pacienteEdad = value; }
        }

        public string PacienteMotivoDeConsulta
        {
            get { return pacienteMotivoDeConsulta; }
            set { pacienteMotivoDeConsulta = value; }
        }
    }
}
