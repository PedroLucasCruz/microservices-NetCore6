using System.IdentityModel.Tokens.Jwt;

namespace GeekShoppingClient.Web.Configurations.IConfig
{
    public interface IJwtConfig
    {
         JwtSecurityToken ObterTokenFormatado(string jwtToken);
    }
}
