using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroModel
{
    public class Mensaje
    {
        private string medidor;
        private string fecha;
        private string lectura;

        public string Medidor { get => medidor; set => medidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Lectura { get => lectura; set => lectura = value; }

        public override string ToString()
        {
            return medidor + " " + fecha + " " + lectura;
        }
    }
}
