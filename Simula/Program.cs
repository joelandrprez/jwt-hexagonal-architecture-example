using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Simulador.Backend.Application.Auth;
using Simulador.Backend.Application.Common;
using Simulador.Backend.Application.Subscriptions;
using Simulador.Backend.Domain.Auth.Interfaces;
using Simulador.Backend.Domain.Config.Dto;
using Simulador.Backend.Domain.Subscriptions.Interfaces;
using Simulador.Backend.Infraestructure.Auth;
using Simulador.Backend.Infraestructure.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.json", optional: true);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
DatabaseConfig databaseConfig = configuration.GetSection("Database").Get<DatabaseConfig>();


// Add services to the container.
builder.Services.AddSingleton<DatabaseConfig>(databaseConfig);
builder.Services.AddTransient<UserApp>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<SubscriptionApp>();
builder.Services.AddTransient<ISubcriptionRepository, SubcriptionRepository>();

builder.Services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:4200") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = false,
        //comment this and add this line to fool the validation logic
        SignatureValidator = delegate (string token, TokenValidationParameters parameters)
        {
            var jwt = new JsonWebToken(token);
            if (parameters.ValidateIssuer && parameters.ValidIssuer != jwt.Issuer)
                return null;
            return jwt;
        },
        ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
        ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
        RequireExpirationTime = true,
        ValidateLifetime = true, //TODO: Habilitar la validez del token enviado desde el frontend
        ClockSkew = TimeSpan.Zero,
    };

    o.Events = new JwtBearerEvents()
    {
        OnTokenValidated = c =>
        {
            Console.WriteLine("User successfully authenticated");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = c =>
        {
            //c.NoResult();

            c.Response.StatusCode = 401;
            c.Response.ContentType = "text/plain";

            return c.Response.CompleteAsync();
            //if (IsDevelopment)
            //{
            //    return c.Response.WriteAsync(c.Exception.ToString());
            //}
            //return c.Response.WriteAsync("An error occured processing your authentication.");
        }
    };

    o.IncludeErrorDetails = true;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
