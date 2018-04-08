using INSSBOT.Domain.Model;

namespace INSSBOT.Domain.Interfaces
{
    public interface IAposentadoriaService
    {
        string CalcularTempo(Usuario usuario, int privadoIdadeMinimaMNova, int privadoTempoContribuicaoMNova, int privadoTempoContribuicaoMNovaMaximo,
                                    int privadoTempoContribuicaoM, int privadoIdadeMinimaFNova, int privadoTempoContribuicaoFNova, int privadoTempoContribuicaoFNovaMaximo,
                                    int privadoTempoContribuicaoF, int publicoTempoContribuicaoMNovaMaximo, int publicoIdadeMinimaMNova, int publicoTempoContribuicaoMNova,
                                    int publicoIdadeMinimaM, int publicoTempoContribuicaoM, int publicoTempoContribuicaoFNovaMaximo, int publicoIdadeMinimaFNova,
                                    int publicoTempoContribuicaoFNova, int publicoIdadeMinimaF, int publicoTempoContribuicaoF, int ruralIdadeMinimaMNova,
                                    int ruralTempoContribuicaoMNova, int ruralIdadeMinimaM, int ruralTempoContribuicaoM, int ruralIdadeMinimaFNova,
                                    int ruralTempoContribuicaoFNova, int ruralIdadeMinimaF, int ruralTempoContribuicaoF);
    }
}
