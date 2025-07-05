using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simulador.Backend.Application.Common;
using Simulador.Backend.Domain.Auth.Domain;
using Simulador.Backend.Domain.Auth.Dto;
using Simulador.Backend.Domain.Auth.Interfaces;
using Simulador.Backend.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Simulador.Backend.Application.Auth
{
    public class UserApp : BaseApp<UserApp>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        public UserApp(
            IUserRepository userRepository,
            IConfiguration config,
            ILogger logger) : base(logger)
        {
            _userRepository = userRepository;
            _logger = logger;
            _config = config;
        }

        public async Task<StatusResponse<UserResponse>> Login(AuthUserRequest userRequest,string? ip)
        {
            StatusResponse<UserResponse> response = new StatusResponse<UserResponse>();

            try
            {
                StatusResponse<User?> user = await this.ProcesoComplejo(() => _userRepository.FindByEmail(userRequest.Email));
                DateTime fechaProceso = DateTime.UtcNow;
                if (!user.Success)
                {
                    response.Errors = user.Errors;
                    response.Title = user.Title;
                    response.Detail = user.Detail;
                    response.Success = user.Success;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                    return response;
                }

                if (user.Data == null)
                {
                    response.Title = "Datos inválidos.";
                    response.Title = "Por favor, verifique los datos ingresados e intente nuevamente.";
                    response.Success = false;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_DATOS_NO_ENCONTRADOS;
                    return response;
                }

                if (user.Data.LoginAttempts >= MaestraConstante.CANTIDAD_INTENTOS_MAXIMO_PREDETERMINADO)
                {
                    response.Title = "Se han excedido los intentos permitidos.";
                    response.Title = "Ha superado el número máximo de intentos. Su cuenta ha sido bloqueada temporalmente.";
                    response.Success = false;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_DATOS_ENVIADOS;
                    return response;
                }

                bool passwordIsValid = this.VerifyPassword(user.Data, userRequest.Password);

                if (!passwordIsValid)
                {
                    await this.ProcesoSimple(() => _userRepository.UpdateLoginAttempts(user.Data.LoginAttempts + 1,user.Data.Id.ToString()),"");

                    response.Title = "Datos inválidos.";
                    response.Detail = $"Por favor, verifique los datos ingresados e intente nuevamente. solo le quedan {MaestraConstante.CANTIDAD_INTENTOS_MAXIMO_PREDETERMINADO - (user.Data.LoginAttempts )}";
                    response.Success = false;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_DATOS_ENVIADOS;
                    return response;
                }

                response.Data = UserMapper.ToDto(user.Data);
                response.Data.Token = this.GenerateToken(user.Data, fechaProceso);
                response.Code = MaestraConstante.CODIGO_RESPUESTA_EXITOSA;

                Session session = new Session();

                session.IpSession = ip;
                session.Token = response.Data.Token;
                session.IssueDate = fechaProceso;
                session.ExpirationDate = fechaProceso.AddHours(1);
                session.IsEnabled = true;
                session.UsersId = user.Data.Id;
                session.CreatedAt = fechaProceso;
                session.CreatedBy = user.Data.Email;

                StatusSimpleResponse sessionSave = await this.SaveSession(session);

                if (!sessionSave.Success)
                {
                    response.Errors = sessionSave.Errors;
                    response.Title = sessionSave.Title;
                    response.Detail = sessionSave.Detail;
                    response.Success = sessionSave.Success;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                    response.Data = null;
                    return response;
                }

                StatusSimpleResponse updateAttempts = await this.ProcesoSimple(() => _userRepository.UpdateLoginAttempts(0, user.Data.Id.ToString()), "");
                if (!updateAttempts.Success)
                {
                    response.Errors = updateAttempts.Errors;
                    response.Title = updateAttempts.Title;
                    response.Detail = updateAttempts.Detail;
                    response.Success = updateAttempts.Success;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                    return response;
                }


                return response;
            }
            catch (Exception ex)
            {
                response.Title = "Algo salió mal al procesar la operación. Intente más tarde o contacte al soporte.";
                response.Title = ex.Message;
                response.Success = false;
                response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                return response;
            }

        }

        public async Task<StatusSimpleResponse> SaveSession(Session session)
        {
            StatusSimpleResponse DisableSession = await this.ProcesoSimple(()=> _userRepository.DisableActiveSession(session.UsersId.Value,false),"");

            if (!DisableSession.Success)
            {
                DisableSession.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                return DisableSession;
            }

            return  await this.ProcesoSimple(() => _userRepository.SaveSession(session),"");
        }

        public async Task<StatusSimpleResponse> RequestPasswordReset(AuthUserRequest userRequest)
        {
            StatusSimpleResponse response = new StatusSimpleResponse();

            try
            {
                StatusResponse<User?> user = await this.ProcesoComplejo(() => _userRepository.FindByEmail(userRequest.Email));

                if (!user.Success)
                {
                    response.Errors = user.Errors;
                    response.Title = user.Title;
                    response.Detail = user.Detail;
                    response.Success = user.Success;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                    return response;
                }

                if (user.Data == null)
                {
                    response.Title = "Datos inválidos.";
                    response.Title = "Por favor, verifique los datos ingresados e intente nuevamente.";
                    response.Success = false;
                    response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_DATOS_NO_ENCONTRADOS;
                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Title = "Algo salió mal al procesar la operación. Intente más tarde o contacte al soporte.";
                response.Title = ex.Message;
                response.Success = false;
                response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                this._logger.LogError(ex, JsonConvert.SerializeObject(response));
                return response;
            }

        }

        public async Task<StatusSimpleResponse> Save(UserRequest userRequest,string session)
        {
            StatusSimpleResponse response = new StatusSimpleResponse();
            DateTime fechaProceso = DateTime.UtcNow;
            User user = new User();
            user = UserMapper.ToDomain(userRequest);
            user.CreatedAt = fechaProceso;
            user.UpdatedAt = fechaProceso;
            user.Password = HashPassword(user.Password);
            user.DateOfBirth = fechaProceso;
            user.LoginAttempts = MaestraConstante.CANTIDAD_INTENTOS_INICIAL_PREDETERMINADO;
            user.CreatedBy = session;

            response = await this.ProcesoSimple(() => _userRepository.Save(user),"");
            response.Title = MaestraConstante.MENSAJE_OPERACION_EXITOSA;
            response.Code = MaestraConstante.CODIGO_RESPUESTA_EXITOSA;
            return response;
        }

        public async Task<StatusResponse<UserResponse>> Update(AuthUserRequest userRequest)
        {
            StatusResponse<UserResponse> response = new StatusResponse<UserResponse>();

            return response;
        }

        public async Task<StatusResponse<UserResponse>> Get(AuthUserRequest userRequest)
        {
            StatusResponse<UserResponse> response = new StatusResponse<UserResponse>();

            return response;
        }

        private bool VerifyPassword(User user, string inputPassword)
        {
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, inputPassword);
            return result == PasswordVerificationResult.Success;
        }

        private static string HashPassword(string plainPassword)
        {
            var hasher = new PasswordHasher<object>();
            return hasher.HashPassword(null, plainPassword);
        }

        public string GenerateToken(User user,DateTime fechaProceso)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
