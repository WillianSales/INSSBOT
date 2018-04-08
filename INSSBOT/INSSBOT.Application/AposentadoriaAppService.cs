using INSSBOT.Application.Interfaces;
using INSSBOT.Domain.Interfaces;
using INSSBOT.Domain.Model;

namespace INSSBOT.Application
{
    public class AposentadoriaAppService : IAposentadoriaAppService
    {
        private readonly IAposentadoriaService _usuarioService;
        public AposentadoriaAppService(IAposentadoriaService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public string CalcularTempo(Usuario usuario, int privadoIdadeMinimaMNova, int privadoTempoContribuicaoMNova, int privadoTempoContribuicaoMNovaMaximo,
                                    int privadoTempoContribuicaoM, int privadoIdadeMinimaFNova, int privadoTempoContribuicaoFNova, int privadoTempoContribuicaoFNovaMaximo,
                                    int privadoTempoContribuicaoF, int publicoTempoContribuicaoMNovaMaximo, int publicoIdadeMinimaMNova, int publicoTempoContribuicaoMNova,
                                    int publicoIdadeMinimaM, int publicoTempoContribuicaoM, int publicoTempoContribuicaoFNovaMaximo, int publicoIdadeMinimaFNova,
                                    int publicoTempoContribuicaoFNova, int publicoIdadeMinimaF, int publicoTempoContribuicaoF, int ruralIdadeMinimaMNova,
                                    int ruralTempoContribuicaoMNova, int ruralIdadeMinimaM, int ruralTempoContribuicaoM, int ruralIdadeMinimaFNova,
                                    int ruralTempoContribuicaoFNova, int ruralIdadeMinimaF, int ruralTempoContribuicaoF)
        {
            return _usuarioService.CalcularTempo(usuario, privadoIdadeMinimaMNova, privadoTempoContribuicaoMNova, privadoTempoContribuicaoMNovaMaximo,
                                                 privadoTempoContribuicaoM, privadoIdadeMinimaFNova, privadoTempoContribuicaoFNova, privadoTempoContribuicaoFNovaMaximo,
                                                 privadoTempoContribuicaoF, publicoTempoContribuicaoMNovaMaximo, publicoIdadeMinimaMNova, publicoTempoContribuicaoMNova,
                                                 publicoIdadeMinimaM, publicoTempoContribuicaoM, publicoTempoContribuicaoFNovaMaximo, publicoIdadeMinimaFNova,
                                                 publicoTempoContribuicaoFNova, publicoIdadeMinimaF, publicoTempoContribuicaoF, ruralIdadeMinimaMNova,
                                                 ruralTempoContribuicaoMNova, ruralIdadeMinimaM, ruralTempoContribuicaoM, ruralIdadeMinimaFNova,
                                                 ruralTempoContribuicaoFNova, ruralIdadeMinimaF, ruralTempoContribuicaoF);
        }

     
    }
}
