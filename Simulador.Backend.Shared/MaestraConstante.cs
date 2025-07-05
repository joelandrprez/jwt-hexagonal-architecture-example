using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Shared
{
    public class MaestraConstante
    {
        public static readonly string APP_VERSION = "0.1.0";
        public static readonly string MENSAJE_OPERACION_EXITOSA = "Operación realizada con éxito.";
        public static readonly string MENSAJE_OPERACION_FALLIDA = "Ocurrió un error al procesar la operación. Por favor, inténtalo nuevamente.";


        public static readonly int CODIGO_RESPUESTA_EXITOSA = 200;
        public static readonly int CODIGO_RESPUESTA_ERROR_GENERAL = 500;
        public static readonly int CODIGO_RESPUESTA_ERROR_DATOS_ENVIADOS = 400;
        public static readonly int CODIGO_RESPUESTA_ERROR_DATOS_NO_ENCONTRADOS = 404;

        public static readonly int CANTIDAD_INTENTOS_INICIAL_PREDETERMINADO = 0;
        public static readonly int CANTIDAD_INTENTOS_MAXIMO_PREDETERMINADO = 10;

    }
}
