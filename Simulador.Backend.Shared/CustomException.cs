using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Shared
{
    public class CustomException : Exception
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public List<string> Errores { get; set; }
        public CustomException(string titulo, Exception exception) : base(titulo, exception)
        {
            this.Codigo = 500;//Internal error
            this.Titulo = titulo;
        }

        public CustomException(string titulo, int codigo, Exception exception) : base(titulo, exception)
        {
            this.Codigo = codigo;
            this.Titulo = titulo;
        }

        public CustomException(string titulo, List<string> errores, Exception exception) : base(titulo, exception)
        {
            this.Titulo = titulo;
            this.Errores = errores;
        }
    }
}
