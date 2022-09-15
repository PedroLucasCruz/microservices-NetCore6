﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShoppingApp.Identity.Models
{
    public class UsuarioRegistro
    {   [Required(ErrorMessage = "O campo{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisar ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
        [Compare("Senha", ErrorMessage = "As Senha não conferem.")]
        public string SenhaConfirmacao { get; set; }
        
    }

    public class UsuarioLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        

        [Required(ErrorMessage = "O campo {0} é obrigaório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

    }


    public class UsuarioRespostaLogin
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
