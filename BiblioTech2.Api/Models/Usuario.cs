using System;
using System.Collections.Generic;

namespace BiblioTech2.Api.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
