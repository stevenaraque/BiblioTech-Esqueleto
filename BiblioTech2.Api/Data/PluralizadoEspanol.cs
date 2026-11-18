using Microsoft.EntityFrameworkCore.Design;

namespace BiblioTech2.Api.Data;


public class PluralizadorEspanol : IPluralizer
{
    private const string Vocales = "aeiouáéíóú";

    // Libro -> Libros, Autor -> Autores, Prestamo -> Prestamos
    public string Pluralize(string nombre)
    {
        if (string.IsNullOrEmpty(nombre) || nombre.EndsWith('s')) return nombre;
        if (nombre.EndsWith('z')) return nombre[..^1] + "ces";
        return Vocales.Contains(char.ToLowerInvariant(nombre[^1])) ? nombre + "s" : nombre + "es";
    }

    // Libros -> Libro, Autores -> Autor, Socios -> Socio
    public string Singularize(string nombre)
    {
        if (string.IsNullOrEmpty(nombre) || !nombre.EndsWith('s')) return nombre;

        // "...es" precedido de consonante: Autores -> Autor
        if (nombre.Length > 3 && nombre.EndsWith("es") && !Vocales.Contains(char.ToLowerInvariant(nombre[^3])))
            return nombre[..^2];

        return nombre[..^1];
    }
}

/// <summary>
/// EF Core busca esta clase en el proyecto al ejecutar el Scaffold
/// y usa nuestro pluralizador en lugar del de inglés.
/// </summary>
public class DisenoEfCore : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection services)
        => services.AddSingleton<IPluralizer, PluralizadorEspanol>();
}