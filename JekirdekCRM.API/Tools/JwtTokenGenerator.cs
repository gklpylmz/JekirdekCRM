using JekirdekCRM.DTO.AuthDtos;
using JekirdekCRM.DTO.TokenDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JekirdekCRM.API.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(LoginResponseDto loginResponseDto)
        {
            var claims = new List<Claim>();

            if (loginResponseDto.Username != null)
            {
                claims.Add(new Claim("Username", loginResponseDto.Username));
            }
            if (loginResponseDto.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, loginResponseDto.Role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signInCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(issuer:JwtTokenDefaults.ValidIssuer,
                audience: JwtTokenDefaults.ValidAudience, claims:claims,notBefore:DateTime.UtcNow,expires:expireDate,
                signingCredentials:signInCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(tokenHandler.WriteToken(token),expireDate);
        }
    }
}
