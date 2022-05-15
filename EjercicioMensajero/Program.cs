using EjercicioMensajero.Comunicacion;
using MensajeroModel;
using MensajeroModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjercicioMensajero
{
    class Program
    {
        private static IMensajesDAL mensajesDAL = ILecturaDAL.GetInstancia();
        
        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Que desea hacer?");
            Console.WriteLine("1. Ingresar \n2. Mostrar \n0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1": Ingresar(); 
                    break;
                case "2": Mostrar();
                    break;
                case "0": continuar = false;
                    break;
                default: Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }

        static void Main(string[] args)
        {
            //1. Iniciar el servidor socket en el puerto 3000
            //2. El puerto tiene que ser configurable App.config
            //3. Cuando reciba un cliente, tiene que solicitar a ese cliente el nombre
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            //1. Como atender mas de un cliente a la vez?
            //2. como evito que dos clientes ingresen a un archivo a la vez?
            //3. como evitar el bloqueo mutuo? 
            while (Menu()) ;
        }

        static void Ingresar()
        {
            Console.WriteLine("Ingrese medidor: ");
            string medidor = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese Fecha: ");
            string fecha = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese lectura: ");
            string lectura = Console.ReadLine().Trim();
            Mensaje mensaje = new Mensaje()
            {
                Medidor = medidor,
                Fecha = fecha,
                Lectura = lectura
            };
            //nuestro proyecto es ThreadSafe que significa que nos hacemos cargo de la concurrencia
            lock (mensajesDAL)
            { 
                mensajesDAL.IngresarLectura(mensaje);
            }
        }

        static void Mostrar()
        {
            List<Mensaje> mensajes = null;
            lock (mensajesDAL)
            {
                mensajes = mensajesDAL.ObtenerLecturas();
            }
            foreach (Mensaje mensaje in mensajes)
            {
                Console.WriteLine(mensaje);
            }
        }
    }
}
