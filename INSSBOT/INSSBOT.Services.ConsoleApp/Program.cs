using INSSBOT.Application.Interfaces;
using SimpleInjector;
using System;

namespace INSSBOT.Services.ConsoleApp
{
    class Program
    {
        private static IUsuarioAppService _usuarioAppService;
        private static IAposentadoriaAppService _aposentadoriaAppService;
        private static Container _container;
        private static Mensagens _mensagens;

        static void Main(string[] args)
        {
            IniciarInstancias();

            Bot.Api.OnMessage += Bot_OnMessage;
            Bot.Api.OnMessageEdited += Bot_OnMessage;

            Bot.Api.StartReceiving();

            Console.WriteLine("Serviço iniciado!");

            Console.ReadLine();

            Bot.Api.StopReceiving();
        }

        private static void IniciarInstancias()
        {
            _container = SimpleInjectorInitializer.Initialize();
            _usuarioAppService = _container.GetInstance<IUsuarioAppService>();
            _aposentadoriaAppService = _container.GetInstance<IAposentadoriaAppService>();

            _mensagens = new Mensagens(_usuarioAppService, _aposentadoriaAppService);
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
                _mensagens.TratarPassos(e.Message.From.Id, e.Message.From, e.Message.Chat, e.Message.Text);
        }
      
    }
}
