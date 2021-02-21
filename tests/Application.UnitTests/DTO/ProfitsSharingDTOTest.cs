using Application.DTO;
using Xunit;

namespace Application.UnitTests.DTO
{
    /// <summary>
    /// Testes da classe ProfitsSharingDTO
    /// </summary>
    public class ProfitsSharingDTOTest
    {
        /// <summary>
        /// Cenários de teste do atributo EmployeeQuantityString
        /// </summary>
        /// <param name="count">Quantidade de funcionários</param>
        /// <param name="employeeQuantityStringExpected">Expectativa de retorno do atributo EmployeeQuantityString</param>
        [InlineData(1, "1")]
        [InlineData(1000, "1.000")]
        [InlineData(1000000, "1.000.000")]
        [Theory]
        public void Quando_ProfitSharingEmployees_Estiver_Preenchido_Entao_Retorna_EmployeeQuantityString_Formatado(int count, string employeeQuantityStringExpected)
        {
            var profitSharingEmployees = FixtureHelper.CreateCollection<ProfitSharingEmployeeDTO>(count, s => s.OmitAutoProperties());

            var profitsSharingDTO = new ProfitsSharingDTO(0, profitSharingEmployees);

            Assert.Equal(employeeQuantityStringExpected, profitsSharingDTO.EmployeeQuantityString);
        }

        /// <summary>
        /// Cenários de teste do atributo TotalSharingString
        /// </summary>
        /// <param name="count">Quantidade de funcionários</param>
        /// <param name="profitSharing">Participação no lucro</param>
        /// <param name="totalSharingStringExpected">Expectativa de retorno do atributo TotalSharingString</param>
        [InlineData(1, 0.01, "R$ 0,01")]
        [InlineData(1, 1, "R$ 1,00")]
        [InlineData(1, 1.01, "R$ 1,01")]
        [InlineData(1, 1000, "R$ 1.000,00")]
        [InlineData(1, 1000.01, "R$ 1.000,01")]
        [InlineData(1, 1000000, "R$ 1.000.000,00")]
        [InlineData(1, 1000000.01, "R$ 1.000.000,01")]
        [InlineData(2, 0.01, "R$ 0,02")]
        [InlineData(2, 1, "R$ 2,00")]
        [InlineData(2, 1.01, "R$ 2,02")]
        [InlineData(2, 1000, "R$ 2.000,00")]
        [InlineData(2, 1000.01, "R$ 2.000,02")]
        [InlineData(2, 1000000, "R$ 2.000.000,00")]
        [InlineData(2, 1000000.01, "R$ 2.000.000,02")]
        [Theory]
        public void Quando_TotalSharing_Estiver_Preenchido_Entao_Retorna_TotalSharingString_Formatado(int count, decimal profitSharing, string totalSharingStringExpected)
        {
            var profitSharingEmployees = FixtureHelper.CreateCollection<ProfitSharingEmployeeDTO>(count, s => s.With(o => o.ProfitSharing, () => profitSharing)
                                                                                                               .OmitAutoProperties());

            var profitsSharingDTO = new ProfitsSharingDTO(0, profitSharingEmployees);

            Assert.Equal(totalSharingStringExpected, profitsSharingDTO.TotalSharingString);
        }

        /// <summary>
        /// Cenários de teste do atributo AvaiableProfitString
        /// </summary>
        /// <param name="totalAvailable">Total do lucro disponibilizado</param>
        /// <param name="avaiableProfitStringExpected">Expectativa de retorno do atributo AvaiableProfitString</param>
        [InlineData(0.01, "R$ 0,01")]
        [InlineData(1, "R$ 1,00")]
        [InlineData(1.01, "R$ 1,01")]
        [InlineData(1000, "R$ 1.000,00")]
        [InlineData(1000.01, "R$ 1.000,01")]
        [InlineData(1000000, "R$ 1.000.000,00")]
        [InlineData(1000000.01, "R$ 1.000.000,01")]
        [Theory]
        public void Quando_AvaiableProfit_Estiver_Preenchido_Entao_Retorna_AvaiableProfitString_Formatado(decimal totalAvailable, string avaiableProfitStringExpected)
        {
            var profitsSharingDTO = new ProfitsSharingDTO(totalAvailable, null);

            Assert.Equal(avaiableProfitStringExpected, profitsSharingDTO.TotalAvaiableString);
        }

        /// <summary>
        /// Cenários de teste do atributo BalanceProfitString
        /// </summary>
        /// <param name="count">Quantidade de funcionários</param>
        /// <param name="profitSharing">Participação no lucro</param>
        /// <param name="totalAvailable">Total do lucro disponibilizado</param>
        /// <param name="balanceProfitStringExpected">Expectativa de retorno do atributo BalanceProfitString</param>
        [InlineData(1, 1, 1, "R$ 0,00")]
        [InlineData(1, 1.01, 1.02, "R$ 0,01")]
        [InlineData(1, 1, 2, "R$ 1,00")]
        [InlineData(1, 1000, 2000, "R$ 1.000,00")]
        [InlineData(1, 1000.01, 2000.02, "R$ 1.000,01")]
        [InlineData(1, 1000000, 2000000, "R$ 1.000.000,00")]
        [InlineData(1, 1000000.01, 2000000.02, "R$ 1.000.000,01")]
        [InlineData(2, 1, 2, "R$ 0,00")]
        [InlineData(2, 1.01, 2.03, "R$ 0,01")]
        [InlineData(2, 1, 3, "R$ 1,00")]
        [InlineData(2, 1000, 3000, "R$ 1.000,00")]
        [InlineData(2, 1000.01, 3000.03, "R$ 1.000,01")]
        [InlineData(2, 1000000, 3000000, "R$ 1.000.000,00")]
        [InlineData(2, 1000000.01, 3000000.03, "R$ 1.000.000,01")]
        [Theory]
        public void Quando_TotalSharing_E_TotalAvailable_Estiverem_Preenchidos_Entao_Retorna_BalanceProfitString_Formatado(int count, decimal profitSharing, decimal totalAvailable, string balanceProfitStringExpected)
        {
            var profitSharingEmployees = FixtureHelper.CreateCollection<ProfitSharingEmployeeDTO>(count, s => s.With(o => o.ProfitSharing, () => profitSharing)
                                                                                                               .OmitAutoProperties());

            var profitsSharingDTO = new ProfitsSharingDTO(totalAvailable, profitSharingEmployees);

            Assert.Equal(balanceProfitStringExpected, profitsSharingDTO.TotalBalanceAvailableString);
        }
    }
}
