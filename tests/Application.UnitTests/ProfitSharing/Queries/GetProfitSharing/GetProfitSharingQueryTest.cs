using System.Threading;
using Application.Common.Models;
using Application.DTO;
using Application.ProfitSharing.Queries.GetProfitSharing;
using Xunit;

namespace Application.UnitTests.ProfitSharing.Queries.GetProfitSharing
{
    /// <summary>
    /// Testes da classe GetProfitSharingQuery
    /// </summary>
    public class GetProfitSharingQueryTest
    {
        /// <summary>
        /// Cenários de teste do método Handle
        /// </summary>
        /// <param name="hasEmployee">Indica que existe funcionário na base de dados</param>
        /// <param name="allProfitSharingIsValid">Indica que todos os valores das participações nos lucros dos funcionários são válidos</param>
        /// <param name="availableGreaterThanOrEqualSharing">Indica que o valor do lucro disponibilizado é maior ou igual ao valor distribuido aos funcionários</param>
        /// <param name="succeed">Indica se o retorno é sucesso</param>
        [InlineData(false, false, false, false)]
        [InlineData(false, false, true, false)]
        [InlineData(true, false, false, false)]
        [InlineData(true, false, true, false)]
        [InlineData(false, true, false, false)]
        [InlineData(false, true, true, false)]
        [InlineData(true, true, false, false)]
        [InlineData(true, true, true, true)]
        [Theory]
        public void Quando_Handle_Chamado_Retorna_Resultado_Servico(bool hasEmployee, bool allProfitSharingIsValid, bool availableGreaterThanOrEqualSharing, bool succeed)
        {
            var totalAvailable = succeed ? 1 : 0;
            var data = succeed ? new ProfitsSharingDTO() : null;
            var serviceError = succeed ? null : ServiceError.DefaultError;
            var getProfitSharingQuery = new GetProfitSharingQuery(totalAvailable);
            var getProfitSharingQueryHandler = new GetProfitSharingQueryHandler();
            var result = getProfitSharingQueryHandler.Handle(getProfitSharingQuery, CancellationToken.None).Result;

            Assert.Equal(succeed, result.Succeeded);
            Assert.Equal(data, result.Data);
            Assert.Equal(serviceError, result.ServiceError);
            Assert.Equal(!hasEmployee, (result.ServiceError != null && result.ServiceError.Code == 1));
            Assert.Equal(!hasEmployee, (result.ServiceError != null && result.ServiceError.Message == "Não existe funcionário na base de dados."));
            Assert.Equal((hasEmployee && !allProfitSharingIsValid), (result.ServiceError != null && result.ServiceError.Code == 2));
            Assert.Equal((hasEmployee && !allProfitSharingIsValid), (result.ServiceError != null && result.ServiceError.Message == "Existe funcionário com valor inválido da participação no lucro."));
            Assert.Equal((hasEmployee && allProfitSharingIsValid && !availableGreaterThanOrEqualSharing), (result.ServiceError != null && result.ServiceError.Code == 3));
            Assert.Equal((hasEmployee && allProfitSharingIsValid && !availableGreaterThanOrEqualSharing), (result.ServiceError != null && result.ServiceError.Message == "Valor disponibilizado é menor do que a soma das participações no lucro dos funcionários."));
        }
    }
}
