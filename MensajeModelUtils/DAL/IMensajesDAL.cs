using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroModel.DAL
{
    public interface IMensajesDAL
    {
        void IngresarLectura(Mensaje mensaje);
        List<Mensaje> ObtenerLecturas();

    }


}
