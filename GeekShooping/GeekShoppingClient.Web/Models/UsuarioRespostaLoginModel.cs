namespace GeekShoppingClient.Web.Models
{
    public class UsuarioRespostaLoginModel
    {
        public string AcessToken { get; set; }
        public double ExpireIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }

    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; } //valor do tipo, caso seja permissão dentro da propriedade tipo logo a baixo será os valores que o usuário possui
        public string Type { get; set; } //Um tipo que pode ser permissão
    }
}
