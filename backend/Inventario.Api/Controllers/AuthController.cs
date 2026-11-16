using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Inventario.Api.DTOs;
using Inventario.Api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AutenticationService _autenticationService;
    private readonly IConfiguration _configuration;

    public AuthController(AutenticationService autenticationService, IConfiguration configuration)
    {
        _autenticationService = autenticationService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var credencialesValidas = await _autenticationService.ValidarCredenciales(loginDTO.Correo, loginDTO.Clave);

        if (!credencialesValidas)
        {
            return Unauthorized(new { mensaje = "Credenciales inválidas." });
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, loginDTO.Correo)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );  

        var tokenGenerado = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { mensaje = "Inicio de sesión exitoso.", token = tokenGenerado });
    }
}