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
                Bot.Api.SetWebhookAsync("https://26fda9b6.ngrok.io").Wait();

                Console.WriteLine("Server Started");

                Console.ReadLine();

                Bot.Api.DeleteWebhookAsync().Wait();
            }
        }
    }
}
