using INSSBOT.Application.Interfaces;
using INSSBOT.Domain.Interfaces;
using INSSBOT.Domain.Model;
using System.Collections.Generic;

namespace INSSBOT.Application
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            return _usuarioService.Atualizar(usuario);
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            return _usuarioService.Cadastrar(usuario);
        }

        public Usuario ObterPorId(int id)
        {
            return _usuarioService.ObterPorId(id);
        }

        public Dictionary<long, Usuario> ObterTodos()
        {
            return _usuarioService.ObterTodos();
        }

        public void Remover(int id)
        {
            _usuarioService.Remover(id);
        }
    }
}
