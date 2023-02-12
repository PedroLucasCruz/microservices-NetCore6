
using GeekShoppingClient.Web.Configurations.IConfig;
using System.IdentityModel.Tokens.Jwt;

namespace GeekShoppingClient.Web.Configurations
{
    public class JwtConfig : IJwtConfig
    {

        public JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}
