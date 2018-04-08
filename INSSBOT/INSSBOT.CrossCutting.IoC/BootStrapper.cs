using INSSBOT.Application;
using INSSBOT.Application.Interfaces;
using INSSBOT.Domain.Interfaces;
using INSSBOT.Domain.Services;
using INSSBOT.Repository;
using SimpleInjector;

namespace INSSBOT.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            // Lifestyle.Transient => Uma instancia para cada solicitacao;
            // Lifestyle.Singleton => Uma instancia unica para a classe
            // Lifestyle.Scoped => Uma instancia unica para o request

            // App
            container.Register<IAposentadoriaAppService, AposentadoriaAppService>(Lifestyle.Singleton);
            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Singleton);

            // Domain Service
            container.Register<IAposentadoriaService, AposentadoriaService>(Lifestyle.Singleton);
            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Singleton);

            // Repository
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Singleton);
        }
    }
}
