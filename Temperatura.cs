using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion
{
    class Temperatura
    {
        int codigoDepartamento;
        double medicion;
        DateTime fecha;

        public int CodigoDepartamento { get => codigoDepartamento; set => codigoDepartamento = value; }
        public double Medicion { get => medicion; set => medicion = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
    }
}
