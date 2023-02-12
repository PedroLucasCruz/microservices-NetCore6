﻿using GeekShoppingClient.Web.Models;

namespace GeekShoppingClient.Web.Services.IServices
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLoginModel> Login(UsuarioLogin usuarioLogin);
        Task<UsuarioRespostaLoginModel> Registro(UsuarioRegistro usuarioRegistro);
        void Logout();
        bool EstaAutenticado();
    }
}