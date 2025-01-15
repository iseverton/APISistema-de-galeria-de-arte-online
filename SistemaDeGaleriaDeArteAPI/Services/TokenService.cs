using SistemaDeGaleriaDeArteAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SistemaDeGaleriaDeArteAPI.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SistemaDeGaleriaDeArteAPI.Interfaces;
using Microsoft.Extensions.Options;

namespace SistemaDeGaleriaDeArteAPI.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> JwtSettings )
    {
        _jwtSettings = JwtSettings.Value;
    }

    public string GerarToken(UserModel user)
    {
            var TokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.JwtKey);

            var TokenDescriptor = new SecurityTokenDescriptor
            {

                // criar os conteudos do token ( playLod ) 
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                }),

                // Definir tempo de duracao do token
                Expires = DateTime.UtcNow.AddHours(8),

                // encriptar token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);
        
    }
}
