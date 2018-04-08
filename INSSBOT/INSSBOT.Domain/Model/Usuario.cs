using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace INSSBOT.Domain.Model
{
    public class Usuario
    {
        public int ID { get; set; }
        public User UsuarioTelegram { get; set; }
        public Chat UsuarioChat { get; set; }
        public List<Passo> Passos { get; set; }
        public Passo PassoAtual { get; set; }
        public Contribuinte Contribuinte { get; set; }

        public Usuario()
        {
            this.Contribuinte = new Contribuinte();
            this.Passos = new List<Passo>
            {
                new Passo { Descricao = "", Ordem = 0, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Informe seu Nome Completo:", Ordem = 1, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Informe sua Data de Nascimento:", Ordem = 2, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Informe seu CPF:", Ordem = 3, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Informe seu Sexo:", Ordem = 4, Id = Guid.NewGuid(), Opcoes = true, DescricaoBooleana = CriarOpcoes(new List<string>() { "Masculino", "Feminino" }) },
                new Passo { Descricao = "Informe seu E-mail:", Ordem = 5, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Informe um Celular com DDD para contato:", Ordem = 6, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Informe seu Endereço completo:", Ordem = 7, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Funcionário rural?", Ordem = 8, Id = Guid.NewGuid(), Opcoes = true, DescricaoBooleana = CriarOpcoes(new List<string>() { "Sim", "Não" }) },
                new Passo { Descricao = "Funcionário público?", Ordem = 9, Id = Guid.NewGuid(), Opcoes = true, DescricaoBooleana = CriarOpcoes(new List<string>() { "Sim", "Não" }) },
                new Passo { Descricao = "Recebeu insalubridade ou periculosidade?", Ordem = 10, Id = Guid.NewGuid(), Opcoes = true, DescricaoBooleana = CriarOpcoes(new List<string>() { "Sim", "Não" }) },
                new Passo { Descricao = "Por quanto tempo (em meses)?", Ordem = 11, Id = Guid.NewGuid(), Opcoes = false },
                new Passo { Descricao = "Por quanto tempo (em meses) contribuiu para o INSS?", Ordem = 12, Id = Guid.NewGuid(), Opcoes = false }
            };
        }

        private ReplyKeyboardMarkup CriarOpcoes(List<string> opcoes)
        {
            dynamic rkm = new ReplyKeyboardMarkup();
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            foreach (var item in opcoes)
            {
                cols.Add(new KeyboardButton("" + item));
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
            }

            rkm.Keyboard = rows.ToArray();
            return rkm;
        }
    }
}
