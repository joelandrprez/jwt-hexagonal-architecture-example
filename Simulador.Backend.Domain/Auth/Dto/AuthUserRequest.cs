using FluentValidation;
using Simulador.Backend.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Auth.Dto
{
    public class AuthUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthUserRequestValidator : AbstractValidator<AuthUserRequest>
    {
        public AuthUserRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(TextCommon.ErrorValidacionEmailUsuario);
            RuleFor(x => x.Password).NotNull().WithMessage(TextCommon.ErrorValidacionPasswordUsuario);
        }
    }
}
