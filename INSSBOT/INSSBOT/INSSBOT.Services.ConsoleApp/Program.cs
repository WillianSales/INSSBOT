using System;

namespace INSSBOT.Services.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Bot.Api.OnMessage += Bot_OnMessage;
            Bot.Api.OnMessageEdited += Bot_OnMessage;

            Bot.Api.StartReceiving();

            Console.WriteLine("Serviço iniciado!");

            Console.ReadLine();

            Bot.Api.StopReceiving();
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
            {
                Console.WriteLine(e.Message.Text);

                Bot.Api.SendTextMessageAsync(e.Message.Chat.Id, $"Teste resposta {e.Message.Text}");
            }
        }
    }
}
