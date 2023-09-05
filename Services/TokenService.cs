using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtBearer.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtBearer.Services;

public class TokenService
{
    public string Generate(User user)
    {
        // Cria uma instancia do JwtSecurityTokenHandler
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",", user.Roles))
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        // Gera um Token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        //Retorna o Token em formato String
        return tokenHandler.WriteToken(token);
    }
}