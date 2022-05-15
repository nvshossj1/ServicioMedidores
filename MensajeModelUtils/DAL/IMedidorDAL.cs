using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroModel.DAL
{
    public class IMedidorDAL : IMensajesDAL
    {

        //  para implementar un patron singleton
        //  Singleton es un patrón de diseño del tipo creacional cuyo propósito es garantizar la
        //  existencia de una sola instancia de una clase.Además el acceso a esa única instancia tiene que ser global.

        //1. El contructor tiene que ser private
        private IMedidorDAL()
        {

        }
        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static IMedidorDAL instancia;
        //3. Tener un metodo GetIntance, que devuelve una referencia al atributo
        public static IMensajesDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new IMedidorDAL();
            }
            return instancia;
        }

        private static string url = Directory.GetCurrentDirectory();

        private static string archivo = url + "/Lecturas.txt";
        public void IngresarLectura(Mensaje mensaje)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(mensaje.Medidor + "|" + mensaje.Fecha + "|" + mensaje.Lectura);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<Mensaje> ObtenerLecturas()
        {
            List<Mensaje> lista = new List<Mensaje>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Mensaje mensaje = new Mensaje()
                            {
                                Medidor = arr[0],
                                Fecha = arr[1],
                                Lectura = arr[2]
                            };
                            lista.Add(mensaje);
                        }
                    } while (texto != null);
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
