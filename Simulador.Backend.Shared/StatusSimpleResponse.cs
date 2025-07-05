using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Shared
{
    public class StatusSimpleResponse
    {
        public Guid Id { get; set; }

        private int _codigo;
        public int Code
        {
            get
            {
                if (!this._success && this._codigo == 200)
                    return 500;

                return this._codigo;
            }
            set
            {
                this._codigo = value;
            }
        }
        private bool _success;
        public bool Success
        {
            get { return this._success; }
            set
            {
                this._success = value;
            }
        }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }


        public StatusSimpleResponse() : this(true, "")
        {
            this.Id = Guid.NewGuid();
            this.Code = 200;//Ok;
            this.Title = null;
            this.Detail = null;
        }

        public StatusSimpleResponse(bool satisfactorio, string titulo)
        {
            this.Id = Guid.NewGuid();
            this.Code = 200;//Ok;
            this.Success = satisfactorio;
            this.Title = titulo;
        }

        public StatusSimpleResponse(bool satisfactorio, string titulo, string detalle)
        {
            this.Id = Guid.NewGuid();
            this.Code = 200;//Ok;
            this.Success = satisfactorio;
            this.Title = titulo;
            this.Detail = detalle;
        }

        public StatusSimpleResponse(bool satisfactorio, string titulo, string detalle, int codigo)
        {
            this.Id = Guid.NewGuid();
            this.Code = codigo;
            this.Success = satisfactorio;
            this.Title = titulo;
            this.Detail = detalle;
        }

        public StatusSimpleResponse(string titulo, Dictionary<string, List<string>> errores)
        {
            this.Id = Guid.NewGuid();
            this.Code = 200;//Ok;
            this.Success = false;
            this.Title = titulo;
            this.Detail = null;
            this.Errors = errores;
        }

        public StatusSimpleResponse(string titulo, string detalle, Dictionary<string, List<string>> errores)
        {
            this.Id = Guid.NewGuid();
            this.Code = 200;//Ok;
            this.Success = false;
            this.Title = titulo;
            this.Detail = detalle;
            this.Errors = errores;
        }
    }
}
