using INSSBOT.Domain.Model;
using System.Collections.Generic;

namespace INSSBOT.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Dictionary<long, Usuario> ObterTodos();
        Usuario ObterPorId(int id);
        Usuario Adicionar(Usuario obj);
        Usuario Atualizar(Usuario obj);
        void Remover(int id);
    }
}
