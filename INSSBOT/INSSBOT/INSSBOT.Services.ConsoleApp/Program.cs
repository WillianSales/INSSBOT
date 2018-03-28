using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSSBOT.Services.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://localhost:12345
            using (WebApp.Start<Startup>("http://localhost:80"))
            {
                // Register WebHook
                // You should replace {YourHostname} with your Internet accessible hosname
                Bot.Api.SetWebhookAsync("https://26fda9b6.ngrok.io").Wait();

                Console.WriteLine("Server Started");

                // Stop Server after <Enter>
                Console.ReadLine();

                // Unregister WebHook
                Bot.Api.DeleteWebhookAsync().Wait();
            }


            teste();

        }

        private async void teste ()
        {
            //   var botClient = new ]Bot.Api.TelegramBotClient("your API access Token");
            var me = await Bot.Api.GetMeAsync();
            System.Console.WriteLine($"Hello! My name is {me.FirstName}");
        }
    }
}
