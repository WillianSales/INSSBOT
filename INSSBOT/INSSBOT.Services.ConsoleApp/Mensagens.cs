using INSSBOT.Application.Interfaces;
using INSSBOT.Domain.Model;
using System;
using System.Configuration;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace INSSBOT.Services.ConsoleApp
{
    public class Mensagens
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IAposentadoriaAppService _aposentadoriaAppService;

        public Mensagens(IUsuarioAppService usuarioAppService, IAposentadoriaAppService aposentadoriaAppService)
        {
            _usuarioAppService = usuarioAppService;
            _aposentadoriaAppService = aposentadoriaAppService;
        }

        public void TratarPassos(int idUsuario, User sender, Chat chat, string mensagem)
        {
            Usuario user;
            Passo proximoPasso = new Passo();
            bool respostaValida = true;

            user = _usuarioAppService.ObterPorId(idUsuario);

            if (user == null)
            {
                user = new Usuario
                {
                    ID = idUsuario,
                    UsuarioTelegram = sender,
                    UsuarioChat = chat
                };

                proximoPasso = user.Passos.FirstOrDefault(x => x.Ordem == 1);
                user.PassoAtual = proximoPasso;
                _usuarioAppService.Cadastrar(user);
            }
            else
            {
                respostaValida = ArmazenarResposta(user, mensagem.ToUpper());
                proximoPasso = user.Passos.FirstOrDefault(x => x.Ordem == user.PassoAtual.Ordem + 1);
            }

            if (proximoPasso != null && respostaValida)
            {
                user.PassoAtual = proximoPasso;
                EnviarMensagem(user, user.PassoAtual.Descricao);
            }
            else
            {
                if (respostaValida)
                {
                    user.PassoAtual = user.Passos.FirstOrDefault(x => x.Ordem == 0);
                    string cal = _aposentadoriaAppService.CalcularTempo(user, int.Parse(ConfigurationManager.AppSettings["PrivadoIdadeMinimaMNova"]), int.Parse(ConfigurationManager.AppSettings["PrivadoTempoContribuicaoMNova"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PrivadoTempoContribuicaoMNovaMaximo"]), int.Parse(ConfigurationManager.AppSettings["PrivadoTempoContribuicaoM"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PrivadoIdadeMinimaFNova"]), int.Parse(ConfigurationManager.AppSettings["PrivadoTempoContribuicaoFNova"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PrivadoTempoContribuicaoFNovaMaximo"]), int.Parse(ConfigurationManager.AppSettings["PrivadoTempoContribuicaoF"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PublicoTempoContribuicaoMNovaMaximo"]), int.Parse(ConfigurationManager.AppSettings["PublicoIdadeMinimaMNova"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PublicoTempoContribuicaoMNova"]), int.Parse(ConfigurationManager.AppSettings["PublicoIdadeMinimaM"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PublicoTempoContribuicaoM"]), int.Parse(ConfigurationManager.AppSettings["PublicoTempoContribuicaoFNovaMaximo"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PublicoIdadeMinimaFNova"]), int.Parse(ConfigurationManager.AppSettings["PublicoTempoContribuicaoFNova"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["PublicoIdadeMinimaF"]), int.Parse(ConfigurationManager.AppSettings["PublicoTempoContribuicaoF"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["RuralIdadeMinimaMNova"]), int.Parse(ConfigurationManager.AppSettings["RuralTempoContribuicaoMNova"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["RuralIdadeMinimaM"]), int.Parse(ConfigurationManager.AppSettings["RuralTempoContribuicaoM"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["RuralIdadeMinimaFNova"]), int.Parse(ConfigurationManager.AppSettings["RuralTempoContribuicaoFNova"]),
                                                                        int.Parse(ConfigurationManager.AppSettings["RuralIdadeMinimaF"]), int.Parse(ConfigurationManager.AppSettings["RuralTempoContribuicaoF"]));


                    Bot.Api.SendTextMessageAsync(user.UsuarioChat.Id, cal, replyMarkup: new ReplyKeyboardRemove());
                    Bot.Api.SendTextMessageAsync(user.UsuarioChat.Id, $"Fim", replyMarkup: new ReplyKeyboardRemove());
                }
                else
                {
                    EnviarMensagem(user, "Resposta inváida");
                }
            }
        }

        private static void EnviarMensagem(Usuario user, string mensagem)
        {
            if (user.PassoAtual.Opcoes)
            {
                Bot.Api.SendTextMessageAsync(user.UsuarioChat.Id, mensagem, replyMarkup: user.PassoAtual.DescricaoBooleana);
            }
            else
            {
                Bot.Api.SendTextMessageAsync(user.UsuarioChat.Id, mensagem, replyMarkup: new ReplyKeyboardRemove());
            }
        }

        private static bool ArmazenarResposta(Usuario user, string mensagem)
        {
            int auxi = 0;
            DateTime aux;

            switch (user.PassoAtual.Ordem)
            {
                case 1:
                    user.Contribuinte.Nome = mensagem;
                    break;
                case 2:
                    if (DateTime.TryParse(mensagem, out aux))
                    {
                        int anos = DateTime.Now.Year - aux.Year;
                        if (DateTime.Now.Month < aux.Month || (DateTime.Now.Month == aux.Month && DateTime.Now.Day < aux.Day))
                            anos--;
                        user.Contribuinte.Idade = anos;
                        user.Contribuinte.DataNascimento = mensagem;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 3:
                    user.Contribuinte.CPF = mensagem;
                    break;
                case 4:
                    if (mensagem == "MASCULINO" || mensagem == "FEMININO")
                    {
                        user.Contribuinte.Sexo = mensagem;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 5:
                    user.Contribuinte.Email = mensagem;
                    break;
                case 6:
                    user.Contribuinte.Celular = mensagem;
                    break;
                case 7:
                    user.Contribuinte.Endereco = mensagem;
                    break;
                case 8:
                    if (mensagem == "SIM" || mensagem == "NÃO")
                    {
                        user.Contribuinte.FuncionarioRural = mensagem == "SIM" ? true : false;
                        if (mensagem == "SIM")
                        {
                            user.PassoAtual = user.Passos.FirstOrDefault(x => x.Ordem == user.PassoAtual.Ordem + 3);
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 9:
                    if (mensagem == "SIM" || mensagem == "NÃO")
                    {
                        user.Contribuinte.FuncionarioPublico = mensagem == "SIM" ? true : false;
                        if (mensagem == "SIM")
                        {
                            user.PassoAtual = user.Passos.FirstOrDefault(x => x.Ordem == user.PassoAtual.Ordem + 2);
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 10:
                    if (mensagem == "SIM" || mensagem == "NÃO")
                    {
                        user.Contribuinte.RecebeuInsalubridade = mensagem == "SIM" ? true : false;
                        if (mensagem == "NÃO")
                        {
                            user.PassoAtual = user.Passos.FirstOrDefault(x => x.Ordem == user.PassoAtual.Ordem + 1);
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 11:
                    if (int.TryParse(mensagem, out auxi))
                    {
                        user.Contribuinte.TempoInsalubridade = int.Parse(mensagem);
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 12:
                    if (int.TryParse(mensagem, out auxi))
                    {
                        user.Contribuinte.TempoContribuicao = int.Parse(mensagem);
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
