using Application.DTO;
using Xunit;

namespace Application.UnitTests.DTO
{
    /// <summary>
    /// Testes da classe ProfitSharingEmployeeDTO
    /// </summary>
    public class ProfitSharingEmployeeDTOTest
    {
        /// <summary>
        /// Cenários de teste do atributo ProfitSharing
        /// </summary>
        /// <param name="profitSharing">Participação no lucro</param>
        /// <param name="profitSharingStringExpected">Expectativa de retorno do atributo ProfitSharingString</param>
        [InlineData(0.01, "R$ 0,01")]
        [InlineData(1, "R$ 1,00")]
        [InlineData(1.01, "R$ 1,01")]
        [InlineData(1000, "R$ 1.000,00")]
        [InlineData(1000.01, "R$ 1.000,01")]
        [InlineData(1000000, "R$ 1.000.000,00")]
        [InlineData(1000000.01, "R$ 1.000.000,01")]
        [Theory]
        public void Quando_ProfitSharing_Estiver_Preenchido_Entao_Retorna_ProfitSharingString_Formatado(decimal profitSharing, string profitSharingStringExpected)
        {
            var profitSharingEmployeeDTO = new ProfitSharingEmployeeDTO() { ProfitSharing = profitSharing };

            Assert.Equal(profitSharingStringExpected, profitSharingEmployeeDTO.ProfitSharingString);
        }
    }
}
