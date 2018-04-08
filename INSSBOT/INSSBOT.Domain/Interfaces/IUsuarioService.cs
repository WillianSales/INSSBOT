using INSSBOT.Domain.Model;
using System.Collections.Generic;

namespace INSSBOT.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Usuario Cadastrar(Usuario usuario);
        Usuario ObterPorId(int id);
        Dictionary<long, Usuario> ObterTodos();
        void Remover(int id);
        Usuario Atualizar(Usuario usuario);
    }
}
