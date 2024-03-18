using Application.Mappings;
using Application.Utilities;
using BookingAgency.Ioc;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Application.Security;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    // Ruta al archivo XML de documentación
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Incluir el archivo XML de documentación en Swagger
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<JwtTokenGenerator>();
ConfigurationManager configuration = builder.Configuration;
var key = configuration.GetValue<string>("SecretKey");
builder.Services.AddAuthentication()
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            JwtTokenGenerator JwtTokenGenerator = new JwtTokenGenerator(configuration);
            var accessToken = context.Request.Headers["Authorization"];
            // Intenta validar el token JWT local
            if (!string.IsNullOrEmpty(accessToken) && JwtTokenGenerator.ValidateToken(accessToken))
            {
                context.Token = accessToken;
            }
            else
            {
                // Si ni el token JWT ni la clave API son válidos, rechaza la solicitud
                context.Fail("Acceso no autorizado");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConeccionSQL"), b => b.MigrationsAssembly("BookingAgency")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<RoomTotalCalculator>();
builder.Services.AddTransient<SendEmail>();
IocRepository.AddDependency(builder.Services);
IocServices.AddDependency(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
