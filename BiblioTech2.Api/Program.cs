using Scalar.AspNetCore;
using BiblioTech2.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);  // inicializa una nueva instancia, encargada de leer la configuración de la aplicación (appsettings.json, variables de entorno, etc.)
                                                   // y de configurar los servicios que la aplicación necesita para funcionar correctamente)

// Add services to the container.

builder.Services.AddControllers(); // llama a la carpeta Controllers y registra todos los controladores que encuentre en ella, para que puedan ser utilizados por la aplicación
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); // scalar 

var app = builder.Build(); // toma la configuracion y los servicios que de declararon posteriormente y construye la aplicación,


builder.Services.AddDbContext<BibliotecaContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("<BiblioTechConnection")));// pasando al estado para recibir solicitudes HTTP y generar respuestas


// parte dos Pipeline de la aplicación, que se encarga de procesar las solicitudes HTTP entrantes y generar las respuestas correspondientes

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection(); //si la solicitud entrante es HTTP, la redirige a HTTPS para garantizar una comunicación segura entre el cliente y el servidor

app.UseAuthorization(); //se encarga de verificar si el usuario que realiza la solicitud tiene los permisos necesarios para acceder a los recursos protegidos de la aplicación.

app.MapControllers(); //mapea las rutas de los controladores registrados en la aplicación, para que puedan ser accedidos a través de las solicitudes HTTP entrantes

app.Run(); // arranca la aplicación y comienza a escuchar las solicitudes HTTP entrantes, procesándolas a través del pipeline configurado anteriormente.
