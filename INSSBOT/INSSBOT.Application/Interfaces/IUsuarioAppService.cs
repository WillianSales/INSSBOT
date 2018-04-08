using INSSBOT.Domain.Model;
using System.Collections.Generic;

namespace INSSBOT.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Usuario Cadastrar(Usuario usuario);
        Usuario ObterPorId(int id);
        Dictionary<long, Usuario> ObterTodos();
        void Remover(int id);
        Usuario Atualizar(Usuario usuario);
    }
}
