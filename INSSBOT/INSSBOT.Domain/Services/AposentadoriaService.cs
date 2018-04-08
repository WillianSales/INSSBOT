using INSSBOT.Domain.Interfaces;
using INSSBOT.Domain.Model;
using System.Globalization;
using System.Text;

namespace INSSBOT.Domain.Services
{
    public class AposentadoriaService : IAposentadoriaService
    {
        public string CalcularTempo(Usuario usuario, int privadoIdadeMinimaMNova, int privadoTempoContribuicaoMNova, int privadoTempoContribuicaoMNovaMaximo,
                                    int privadoTempoContribuicaoM, int privadoIdadeMinimaFNova, int privadoTempoContribuicaoFNova, int privadoTempoContribuicaoFNovaMaximo,
                                    int privadoTempoContribuicaoF, int publicoTempoContribuicaoMNovaMaximo, int publicoIdadeMinimaMNova, int publicoTempoContribuicaoMNova,
                                    int publicoIdadeMinimaM, int publicoTempoContribuicaoM, int publicoTempoContribuicaoFNovaMaximo, int publicoIdadeMinimaFNova,
                                    int publicoTempoContribuicaoFNova, int publicoIdadeMinimaF, int publicoTempoContribuicaoF, int ruralIdadeMinimaMNova,
                                    int ruralTempoContribuicaoMNova, int ruralIdadeMinimaM, int ruralTempoContribuicaoM, int ruralIdadeMinimaFNova,
                                    int ruralTempoContribuicaoFNova, int ruralIdadeMinimaF, int ruralTempoContribuicaoF)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("RESUMO DAS INFORMAÇÕES");
            sb.AppendLine("Nome Completo: " + usuario.Contribuinte.Nome);
            sb.AppendLine("Data de Nascimento: " + usuario.Contribuinte.DataNascimento);
            sb.AppendLine("CPF: " + usuario.Contribuinte.CPF);
            sb.AppendLine("Sexo: " + usuario.Contribuinte.Sexo);
            sb.AppendLine("E-mail: " + usuario.Contribuinte.Email);
            sb.AppendLine("Celular: " + usuario.Contribuinte.Celular);
            sb.AppendLine("Endereço completo: " + usuario.Contribuinte.Endereco);
            sb.AppendLine("Funcionário rural: " + (usuario.Contribuinte.FuncionarioRural ? "Sim" : "Não"));
            sb.AppendLine("Funcionário público: " + (usuario.Contribuinte.FuncionarioPublico ? "Sim" : "Não"));
            sb.AppendLine("Recebeu insalubridade ou periculosidade: " + (usuario.Contribuinte.RecebeuInsalubridade ? "Sim" : "Não"));
            sb.AppendLine("Por quanto tempo (em meses): " + usuario.Contribuinte.TempoInsalubridade);
            sb.AppendLine("Por quanto tempo (em meses) contribuiu para o INSS: " + usuario.Contribuinte.TempoContribuicao);
            sb.AppendLine("**********************************");

            string resultadoRegraAtual = string.Empty;
            string resultadoRegraNova = string.Empty;
            int tempoContribuicao_Anos = (usuario.Contribuinte.TempoContribuicao / 12);


            if (usuario.Contribuinte.FuncionarioRural)
            {
                resultadoRegraAtual = CalcularTempoRural(usuario, tempoContribuicao_Anos, false, ruralIdadeMinimaMNova, ruralTempoContribuicaoMNova, ruralIdadeMinimaM,
                                                         ruralTempoContribuicaoM, ruralIdadeMinimaFNova, ruralTempoContribuicaoFNova, ruralIdadeMinimaF, ruralTempoContribuicaoF);
                resultadoRegraNova = CalcularTempoRural(usuario, tempoContribuicao_Anos, true, ruralIdadeMinimaMNova, ruralTempoContribuicaoMNova, ruralIdadeMinimaM,
                                                         ruralTempoContribuicaoM, ruralIdadeMinimaFNova, ruralTempoContribuicaoFNova, ruralIdadeMinimaF, ruralTempoContribuicaoF);
            }
            if (usuario.Contribuinte.FuncionarioPublico)
            {
                resultadoRegraAtual = CalcularTempoPublico(usuario, tempoContribuicao_Anos, false, publicoTempoContribuicaoMNovaMaximo, publicoIdadeMinimaMNova, publicoTempoContribuicaoMNova,
                                                           publicoIdadeMinimaM, publicoTempoContribuicaoM, publicoTempoContribuicaoFNovaMaximo, publicoIdadeMinimaFNova, publicoTempoContribuicaoFNova,
                                                           publicoIdadeMinimaF, publicoTempoContribuicaoF);
                resultadoRegraNova = CalcularTempoPublico(usuario, tempoContribuicao_Anos, true, publicoTempoContribuicaoMNovaMaximo, publicoIdadeMinimaMNova, publicoTempoContribuicaoMNova,
                                                           publicoIdadeMinimaM, publicoTempoContribuicaoM, publicoTempoContribuicaoFNovaMaximo, publicoIdadeMinimaFNova, publicoTempoContribuicaoFNova,
                                                           publicoIdadeMinimaF, publicoTempoContribuicaoF);
            }
            else if (!usuario.Contribuinte.FuncionarioPublico && !usuario.Contribuinte.FuncionarioRural)
            {
                resultadoRegraAtual = CalcularTempoPrivado(usuario, tempoContribuicao_Anos, false, privadoIdadeMinimaMNova, privadoTempoContribuicaoMNova, privadoTempoContribuicaoMNovaMaximo,
                                                           privadoTempoContribuicaoM, privadoIdadeMinimaFNova, privadoTempoContribuicaoFNova, privadoTempoContribuicaoFNovaMaximo, privadoTempoContribuicaoF);
                resultadoRegraNova = CalcularTempoPrivado(usuario, tempoContribuicao_Anos, true, privadoIdadeMinimaMNova, privadoTempoContribuicaoMNova, privadoTempoContribuicaoMNovaMaximo,
                                                           privadoTempoContribuicaoM, privadoIdadeMinimaFNova, privadoTempoContribuicaoFNova, privadoTempoContribuicaoFNovaMaximo, privadoTempoContribuicaoF);
            }

            sb.AppendLine(resultadoRegraAtual);
            sb.AppendLine("**********************************");
            sb.AppendLine(resultadoRegraNova);
            return sb.ToString();
        }

        private static string CalcularTempoPrivado(Usuario usuario, int tempoContribuicao_Anos, bool novaRegra, int privadoIdadeMinimaMNova, int privadoTempoContribuicaoMNova,
                                                   int privadoTempoContribuicaoMNovaMaximo, int privadoTempoContribuicaoM, int privadoIdadeMinimaFNova,
                                                   int privadoTempoContribuicaoFNova, int privadoTempoContribuicaoFNovaMaximo, int privadoTempoContribuicaoF)
        {
            float tempoContribuicaoInsalubridade = 0;
            string resultado = string.Empty;
            float idadeAposentadoria;
            float insalubridade;
            float tempContrmin = 0;
            float tempContrmax = 0;

            if (usuario.Contribuinte.Sexo == "MASCULINO")
            {
                insalubridade = (float)1.4;
                if (novaRegra)
                {
                    idadeAposentadoria = privadoIdadeMinimaMNova;
                    tempContrmin = privadoTempoContribuicaoMNova;
                    tempContrmax = privadoTempoContribuicaoMNovaMaximo;
                }
                else
                {
                    idadeAposentadoria = privadoTempoContribuicaoM;
                }
            }
            else
            {
                insalubridade = (float)1.2;
                if (novaRegra)
                {
                    idadeAposentadoria = privadoIdadeMinimaFNova;
                    tempContrmin = privadoTempoContribuicaoFNova;
                    tempContrmax = privadoTempoContribuicaoFNovaMaximo;
                }
                else
                    idadeAposentadoria = privadoTempoContribuicaoF;
            }

            tempoContribuicaoInsalubridade = 0;
            if (usuario.Contribuinte.RecebeuInsalubridade)
            {
                var criarinsalubridade = (float)usuario.Contribuinte.TempoInsalubridade;
                tempoContribuicaoInsalubridade = (float)(criarinsalubridade * insalubridade) / 12;
            }

            if (novaRegra)
            {
                var totalTempoFaltaAposentarMin = tempContrmin - tempoContribuicao_Anos;
                var totalTempoFaltaAposentarMax = tempContrmax - tempoContribuicao_Anos;
                var totalIdadeFaltaAposentar = idadeAposentadoria - usuario.Contribuinte.Idade;
                totalTempoFaltaAposentarMin = totalTempoFaltaAposentarMin < 0 ? 0 : totalTempoFaltaAposentarMin;
                totalTempoFaltaAposentarMax = totalTempoFaltaAposentarMax < 0 ? 0 : totalTempoFaltaAposentarMax;
                totalIdadeFaltaAposentar = totalIdadeFaltaAposentar < 0 ? 0 : totalIdadeFaltaAposentar;

                if (tempoContribuicao_Anos >= tempContrmax && usuario.Contribuinte.Idade >= idadeAposentadoria)
                {
                    resultado = "Regra Nova: Parabéns... de acordo com a nova regra você já pode se aposentar com o valor integral do benefício.";
                }
                else if (tempoContribuicao_Anos >= tempContrmin && usuario.Contribuinte.Idade >= idadeAposentadoria)
                {
                    resultado = "Regra Nova: Parabéns... de acordo com a nova regra você já pode se aposentar com 60% do valor do benefício. Para aposentadoria com o valor integral ";
                    resultado += "faltam " + totalTempoFaltaAposentarMax + " anos de contribuição.";
                }
                else
                {
                    resultado += "Regra Nova: Faltam " + totalTempoFaltaAposentarMin + " anos de contribuição e mais " + totalIdadeFaltaAposentar + " anos de idade para sua aposentadoria com 60% do valor do benefício, para aposentadoria com o valor integral ";
                    resultado += "faltam " + totalTempoFaltaAposentarMax + " anos de contribuição.";
                }
            }
            else
            {
                float pontosTotais = (tempoContribuicao_Anos + tempoContribuicaoInsalubridade) + usuario.Contribuinte.Idade;
                if (pontosTotais >= idadeAposentadoria)
                {
                    resultado = "Regra Atual: Parabéns... de acordo com a regra atual você já pode se aposentar.";
                }
                else
                {
                    float tempoRestante = ((idadeAposentadoria - pontosTotais) / 2);
                    var values = tempoRestante.ToString(CultureInfo.InvariantCulture).Split('.');
                    if (!string.IsNullOrEmpty(values[0]) && values[0] != "0")
                    {
                        resultado += "Regra Atual: Faltam " + values[0] + " anos";
                        if (values.Length <= 1)
                        {
                            resultado += " para sua aposentadoria de acordo com a regra atual.";
                        }
                    }
                    if (values.Length > 1 && !string.IsNullOrEmpty(values[1]) && values[1] != "0")
                    {
                        resultado += " e " + values[1].Substring(0, 1) + " meses para sua aposentadoria de acordo com a regra atual.";
                    }
                }
            }

            return resultado;
        }

        private static string CalcularTempoPublico(Usuario usuario, int tempoContribuicao_Anos, bool novaRegra, int publicoTempoContribuicaoMNovaMaximo, int publicoIdadeMinimaMNova,
                                                   int publicoTempoContribuicaoMNova, int publicoIdadeMinimaM, int publicoTempoContribuicaoM, int publicoTempoContribuicaoFNovaMaximo,
                                                   int publicoIdadeMinimaFNova, int publicoTempoContribuicaoFNova, int publicoIdadeMinimaF, int publicoTempoContribuicaoF)
        {
            string resultado = string.Empty;
            float tempContrmax = 0;
            var tempContr = 0;
            float idadeAposentadoria = 0;

            if (usuario.Contribuinte.Sexo == "MASCULINO")
            {
                tempContrmax = publicoTempoContribuicaoMNovaMaximo;

                if (novaRegra)
                {
                    idadeAposentadoria = publicoIdadeMinimaMNova;
                    tempContr = publicoTempoContribuicaoMNova;
                }
                else
                {
                    idadeAposentadoria = publicoIdadeMinimaM;
                    tempContr = publicoTempoContribuicaoM;
                }
            }
            else
            {
                tempContrmax = publicoTempoContribuicaoFNovaMaximo;
                if (novaRegra)
                {
                    idadeAposentadoria = publicoIdadeMinimaFNova;
                    tempContr = publicoTempoContribuicaoFNova;
                }
                else
                {
                    idadeAposentadoria = publicoIdadeMinimaF;
                    tempContr = publicoTempoContribuicaoF;
                }
            }

            if (novaRegra)
            {
                var totalTempoFaltaAposentarMin = tempContr - tempoContribuicao_Anos;
                var totalTempoFaltaAposentarMax = tempContrmax - tempoContribuicao_Anos;
                var totalIdadeFaltaAposentar = idadeAposentadoria - usuario.Contribuinte.Idade;
                totalTempoFaltaAposentarMin = totalTempoFaltaAposentarMin < 0 ? 0 : totalTempoFaltaAposentarMin;
                totalTempoFaltaAposentarMax = totalTempoFaltaAposentarMax < 0 ? 0 : totalTempoFaltaAposentarMax;
                totalIdadeFaltaAposentar = totalIdadeFaltaAposentar < 0 ? 0 : totalIdadeFaltaAposentar;

                if (tempoContribuicao_Anos >= tempContrmax && usuario.Contribuinte.Idade >= idadeAposentadoria)
                {
                    resultado = "Regra Nova: Parabéns... de acordo com a nova regra você já pode se aposentar com o valor integral do benefício.";
                }
                else if (tempoContribuicao_Anos >= tempContr && usuario.Contribuinte.Idade >= idadeAposentadoria)
                {
                    resultado = "Regra Nova: Parabéns... de acordo com a nova regra você já pode se aposentar com 70% do valor do benefício. Para aposentadoria com o valor integral ";
                    resultado += "faltam " + totalTempoFaltaAposentarMax + " anos de contribuição.";
                }
                else
                {
                    resultado += "Regra Nova: Faltam " + totalTempoFaltaAposentarMin + " anos de contribuição e mais " + totalIdadeFaltaAposentar + " anos de idade para sua aposentadoria com 60% do valor do benefício, para aposentadoria com o valor integral ";
                    resultado += "faltam " + totalTempoFaltaAposentarMax + " anos de contribuição.";
                }
            }
            else
            {
                if (tempoContribuicao_Anos >= tempContr && usuario.Contribuinte.Idade >= idadeAposentadoria)
                {
                    if (novaRegra)
                        resultado = "Regra Nova: Parabéns... de acordo com a nova regra você já pode se aposentar.";
                    else
                        resultado = "Regra Atual: Parabéns... de acordo com a regra atual você já pode se aposentar.";
                }
                else
                {
                    var totalTempoFaltaAposentar = tempContr - tempoContribuicao_Anos;
                    var totalIdadeFaltaAposentar = idadeAposentadoria - usuario.Contribuinte.Idade;
                    totalTempoFaltaAposentar = totalTempoFaltaAposentar < 0 ? 0 : totalTempoFaltaAposentar;
                    totalIdadeFaltaAposentar = totalIdadeFaltaAposentar < 0 ? 0 : totalIdadeFaltaAposentar;

                    if (novaRegra)
                        resultado += "Regra Nova: Faltam " + totalTempoFaltaAposentar + " anos de contribuição e mais " + totalIdadeFaltaAposentar + " anos de idade para sua aposentadoria de acordo com a nova regra.";
                    else
                        resultado += "Regra Atual: Faltam " + totalTempoFaltaAposentar + " anos de contribuição e mais " + totalIdadeFaltaAposentar + " anos de idade para sua aposentadoria de acordo com a regra atual.";
                }
            }
            return resultado;
        }

        private static string CalcularTempoRural(Usuario usuario, int tempoContribuicao_Anos, bool novaRegra, int ruralIdadeMinimaMNova, int ruralTempoContribuicaoMNova, int ruralIdadeMinimaM,
                                                 int ruralTempoContribuicaoM, int ruralIdadeMinimaFNova, int ruralTempoContribuicaoFNova, int ruralIdadeMinimaF, int ruralTempoContribuicaoF)
        {
            string resultado = string.Empty;
            var tempContr = 0;
            float idadeAposentadoria = 0;

            if (usuario.Contribuinte.Sexo == "MASCULINO")
            {
                if (novaRegra)
                {
                    idadeAposentadoria = ruralIdadeMinimaMNova;
                    tempContr = ruralTempoContribuicaoMNova;
                }
                else
                {
                    idadeAposentadoria = ruralIdadeMinimaM;
                    tempContr = ruralTempoContribuicaoM;
                }
            }
            else
            {
                if (novaRegra)
                {
                    idadeAposentadoria = ruralIdadeMinimaFNova;
                    tempContr = ruralTempoContribuicaoFNova;
                }
                else
                {
                    idadeAposentadoria = ruralIdadeMinimaF;
                    tempContr = ruralTempoContribuicaoF;
                }
            }

            if (tempoContribuicao_Anos >= tempContr && usuario.Contribuinte.Idade >= idadeAposentadoria)
            {
                if (novaRegra)
                    resultado = "Regra Nova: Parabéns... de acordo com a nova regra você já pode se aposentar.";
                else
                    resultado = "Regra Atual: Parabéns... de acordo com a regra atual você já pode se aposentar.";
            }
            else
            {
                var totalTempoFaltaAposentar = tempContr - tempoContribuicao_Anos;
                var totalIdadeFaltaAposentar = idadeAposentadoria - usuario.Contribuinte.Idade;

                totalTempoFaltaAposentar = totalTempoFaltaAposentar < 0 ? 0 : totalTempoFaltaAposentar;
                totalIdadeFaltaAposentar = totalIdadeFaltaAposentar < 0 ? 0 : totalIdadeFaltaAposentar;

                if (novaRegra)
                    resultado += "Regra Nova: Faltam " + totalTempoFaltaAposentar + " anos de contribuição e mais " + totalIdadeFaltaAposentar + " anos de idade para sua aposentadoria de acordo com a nova regra.";
                else
                    resultado += "Regra Atual: Faltam " + totalTempoFaltaAposentar + " anos de contribuição e mais " + totalIdadeFaltaAposentar + " anos de idade para sua aposentadoria de acordo com a regra atual.";
            }

            return resultado;
        }
    }
}
