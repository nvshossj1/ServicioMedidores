using MensajeroModel;
using MensajeroModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioMensajero.Comunicacion
{
    public class HebraCliente
    {
        private IMensajesDAL mensajesDAL = ILecturaDAL.GetInstancia();
        public ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese Medidor: ");
            string medidor = clienteCom.Leer();
            clienteCom.Escribir("Ingrese Fecha: ");
            string texto = clienteCom.Leer();
            clienteCom.Escribir("Ingrese lectura: ");
            string lectura = clienteCom.Leer();
            clienteCom.Escribir("Ok");
            Mensaje mensaje = new Mensaje()
            {
                Medidor = medidor,
                Fecha = texto,
                Lectura = lectura
            };
            lock (mensajesDAL)
            {
                mensajesDAL.IngresarLectura(mensaje);
                List<Mensaje> mensajes = mensajesDAL.ObtenerLecturas();
                {
                    Console.WriteLine(mensaje);
                }
                clienteCom.Desconectar();
            }
        }
    }
}
