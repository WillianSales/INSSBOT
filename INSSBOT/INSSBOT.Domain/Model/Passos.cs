using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace INSSBOT.Domain.Model
{
    public class Passo
    {
        public Guid Id { get; set; }
        public int Ordem { get; set; }
        public string Descricao { get; set; }
        public bool Opcoes { get; set; }
        public ReplyKeyboardMarkup DescricaoBooleana { get; set; }
    }
}
