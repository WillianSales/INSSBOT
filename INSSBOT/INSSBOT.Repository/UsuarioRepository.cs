using INSSBOT.Domain.Interfaces;
using INSSBOT.Domain.Model;
using System.Collections.Generic;

namespace INSSBOT.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private Dictionary<long, Usuario> _usuarioTelegram;

        public UsuarioRepository()
        {
            _usuarioTelegram = new Dictionary<long, Usuario>();
        }

        public Usuario Adicionar(Usuario obj)
        {
            _usuarioTelegram.Add(obj.ID, obj);

            return obj;
        }

        public Usuario Atualizar(Usuario obj)
        {
            _usuarioTelegram[obj.ID] = obj;

            return obj;
        }

        public Usuario ObterPorId(int id)
        {
            if (_usuarioTelegram.TryGetValue(id, out Usuario usuario))
            {
                return usuario;
            }

            return null;
        }

        public Dictionary<long, Usuario> ObterTodos()
        {
            return _usuarioTelegram;
        }

        public void Remover(int id)
        {
            _usuarioTelegram.Remove(id);
        }
    }
}
