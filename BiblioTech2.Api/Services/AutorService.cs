using BiblioTech2.Api.Data;
using BiblioTech2.Api.DTOs.Autores;
using BiblioTech2.Api.Exceptions;
using BiblioTech2.Api.Models;
using BiblioTech2.Api.Services;
using BiblioTech2.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

//encargado de programar y cumpliir cada uno de los metodos falta traer
//los serviicion si en la interface llamamos un metodo y no lo llalmamos
//nos va a da error

namespace BiblioTech2.Api.Services

{
    public class AutorService(BibliotecaContext db) : IAutorService

    {

        public async Task<List<AutorDto>> listarAsync()
        {
            return await db.Autores
                .OrderBy(a => a.Nombre)
                .Select(a => new AutorDto
                {
                    id = a.Id,
                    Nombre = a.Nombre,
                    Nacionalidad = a.Nacionalidad,
                    CantidadLibros = a.Libros.Count
                })
                .ToListAsync();
        }
        public async Task<AutorDto> ObtenerAsync(int id)
        {
            return await db.Autores
                .Where(a => a.Id == id)
                .Select(a => new AutorDto
                {
                id = a.Id,
                    Nombre = a.Nombre,
                    Nacionalidad = a.Nacionalidad,
                    CantidadLibros = a.Libros.Count
                })
                .FirstOrDefaultAsync();
        } 


        public async Task<AutorDto> CrearAsync(AutorGuardarDtocs dto)
        {
            var autor = new Autor
            {
                Nombre = dto.Nombre.Trim(),
                Nacionalidad = string.IsNullOrWhiteSpace(dto.Nacinalidad) ? null : dto.Nacinalidad.Trim()
            };
            db.Autores.Add(autor);
            await db.SaveChangesAsync();
            return new AutorDto { id = autor.Id, Nombre = autor.Nombre, Nacionalidad = autor.Nacionalidad };
        }

        public async Task ActualizarAsync(int id, AutorGuardarDtocs dto)
        {
            var autor = await db.Autores.FindAsync(id)
            autor.Nombre = dto.Nombre.Trim();
            autor.Nacionalidad = string.IsNullOrWhiteSpace(dto.Nacinalidad) ? null : dto.Nacinalidad.Trim();

            await db.SaveChangesAsync();
        }


        public async Task EliminarAsync ( int id)
        {
            var autor = await db.Autores.FindAsync(id);

            var cantidadLibros = await db.Libros.ContainsAsync(async listarAsync => l.Autores.any(a => a.id == id));
            if (cantidadLibros > 0)

                db.Autores.Remove(autor);
            await db.SaveChangesAsync();
        }
    }


