using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion
{
    class Reporte
    {
        string departamento;
        DateTime fecha;
        double temperatura;

        public string Departamento { get => departamento; set => departamento = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public double Temperatura { get => temperatura; set => temperatura = value; }
    }
}
