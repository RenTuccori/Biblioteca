using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Biblioteca.WebAPI.Tests.Auth;

public static class AuthHelpers
{
    public static string GenerateTestJwt(string username, string role, IEnumerable<string>? permisos = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test-secret-key-12345678901234567890"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, "1"),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(ClaimTypes.Role, role)
        };
        if (permisos != null)
        {
            claims.AddRange(permisos.Select(p => new Claim("permiso", p)));
        }
        var token = new JwtSecurityToken(
            issuer: "Biblioteca.WebAPI",
            audience: "Biblioteca.Clients",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}