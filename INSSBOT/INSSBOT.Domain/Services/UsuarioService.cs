using INSSBOT.Domain.Interfaces;
using INSSBOT.Domain.Model;
using System.Collections.Generic;

namespace INSSBOT.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            return _usuarioRepository.Atualizar(usuario);
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            return _usuarioRepository.Adicionar(usuario);
        }

        public Usuario ObterPorId(int id)
        {
            return _usuarioRepository.ObterPorId(id);
        }

        public Dictionary<long, Usuario> ObterTodos()
        {
            return _usuarioRepository.ObterTodos();
        }

        public void Remover(int id)
        {
            _usuarioRepository.Remover(id);
        }
    }
}
