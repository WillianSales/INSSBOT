using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace INSSBOT.Domain.Model
{
    public class Contribuinte
    {
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Endereco { get; set; }
        public bool FuncionarioRural { get; set; }
        public int TempoContribuicao { get; set; }
        public int TempoInsalubridade { get; set; }
        public bool FuncionarioPublico { get; set; }
        public bool RecebeuInsalubridade { get; set; }
    }
}
