using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO;
using Application.ProfitSharing.Queries.GetProfitSharing;
using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
        [InlineData(false, false, false, false, false)]
        [InlineData(false, false, false, true, false)]
        [InlineData(true, false, false, true, false)]
        [InlineData(true, false, true, true, false)]
        [InlineData(true, true, false, true, false)]
        [InlineData(true, true, true, true, true)]
        [Theory]
        public async Task Quando_Handle_Chamado_Retorna_Resultado_ServicoAsync(bool hasEmployee, bool allProfitSharingIsValid, bool availableGreaterThanOrEqualSharing, bool noException, bool succeed)
        {
            var applicationDb = Substitute.For<IApplicationDb>();

            var employees = new List<Employee>();

            if (hasEmployee && !allProfitSharingIsValid)
            {
                employees.Add(new Employee()
                {
                    OccupationArea = "Diretoria",
                    JobTitle = "Diretor",
                    Salary = "R$ 0,00",
                    AdmissionDate = DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd")
                });
            }

            if (hasEmployee && allProfitSharingIsValid && !availableGreaterThanOrEqualSharing)
            {
                employees.Add(new Employee()
                {
                    OccupationArea = "Diretoria",
                    JobTitle = "Diretor",
                    Salary = "R$ 1,00",
                    AdmissionDate = DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd")
                });
            }

            if (hasEmployee && allProfitSharingIsValid && availableGreaterThanOrEqualSharing)
            {
                employees.Add(new Employee()
                {
                    OccupationArea = "Diretoria",
                    JobTitle = "Diretor",
                    Salary = "R$ 0,01",
                    AdmissionDate = DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd")
                });
            }

            if (noException) 
                applicationDb.GetValueFromKey(Arg.Any<string>()).Returns(JsonSerializer.Serialize(employees));
            else
                applicationDb.GetValueFromKey(Arg.Any<string>()).ThrowsForAnyArgs<Exception>();

            var totalAvailable = succeed ? 1 : 0;
            var getProfitSharingQuery = new GetProfitSharingQuery(totalAvailable);

            var getProfitSharingQueryHandler = new GetProfitSharingQueryHandler(applicationDb);

            if (noException)
            {
                var result = await getProfitSharingQueryHandler.Handle(getProfitSharingQuery, CancellationToken.None);

                Assert.Equal(succeed, result.Succeeded);
                Assert.Equal(succeed, result.Data != null && result.ServiceError == null);
                Assert.Equal(!succeed, result.Data == null && result.ServiceError != null);
                Assert.Equal(!hasEmployee, (result.ServiceError != null && result.ServiceError.Code == 1));
                Assert.Equal(!hasEmployee, (result.ServiceError != null && result.ServiceError.Message == "Não existe funcionário na base de dados."));
                Assert.Equal((hasEmployee && !allProfitSharingIsValid), (result.ServiceError != null && result.ServiceError.Code == 2));
                Assert.Equal((hasEmployee && !allProfitSharingIsValid), (result.ServiceError != null && result.ServiceError.Message == "Existe funcionário com valor inválido da participação no lucro."));
                Assert.Equal((hasEmployee && allProfitSharingIsValid && !availableGreaterThanOrEqualSharing), (result.ServiceError != null && result.ServiceError.Code == 3));
                Assert.Equal((hasEmployee && allProfitSharingIsValid && !availableGreaterThanOrEqualSharing), (result.ServiceError != null && result.ServiceError.Message == "Valor disponibilizado é menor do que a soma das participações no lucro dos funcionários."));
            }
            else
                await Assert.ThrowsAsync<Exception>(async () => await getProfitSharingQueryHandler.Handle(getProfitSharingQuery, CancellationToken.None));
        }
    }
}
