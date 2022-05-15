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

namespace EjercicioMensajero.Comunicacion
{
    public class HebraServidor
    {
        private IMensajesDAL mensajesDAL = ILecturaDAL.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket serverSocket = new ServerSocket(puerto);
            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("Servidor: Esperando Cliente: ");
                    Socket socket = serverSocket.ObtenerCliente();
                    Console.WriteLine("Servidor: Cliente conectado");

                    ClienteCom clienteCom = new ClienteCom(socket);
                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("Error, no se a logrado levantar el server en {0}", puerto);
            }
        }
    }
}
