using System;
using Infrastructure.Persistence;
using NSubstitute;
using StackExchange.Redis;
using Xunit;
using NSubstitute.ExceptionExtensions;
using MediatR;
using Application.ProfitSharing.Queries.GetProfitSharing;
using Application.Common.Models;
using Application.DTO;
using WebApi.Controllers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.UnitTests.Persistence
{
    public class ProfitsSharingControllerTest
    {

        /// <summary>
        /// Cenários de teste do método Get que retorna 200 (OK)
        /// </summary>
        [Fact]
        public async Task Quando_Get_Chamado_Retorna_200OKAsync()
        {
            var mediator = Substitute.For<ISender>();
            var logger = Substitute.For<ILogger<ProfitsSharingController>>();

            mediator.Send(Arg.Any<GetProfitSharingQuery>()).Returns(ServiceResult.Success<ProfitsSharingDTO>(new ProfitsSharingDTO()));

            var profitsSharingController = new ProfitsSharingController(mediator, logger);
            var result = await profitsSharingController.Get(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<ProfitsSharingDTO>(((OkObjectResult)result.Result).Value);
            Assert.NotNull((ProfitsSharingDTO)((OkObjectResult)result.Result).Value);
        }

        /// <summary>
        /// Cenários de teste do método Get que retorna 400 (Bad Request)
        /// </summary
        /// <param name="code">Código de erro do sistema</param>
        /// <param name="message">Mensagem de erro</param>
        /// <returns></returns>
        [InlineData(1,"Não existe funcionário na base de dados.")]
        [InlineData(2,"Existe funcionário com valor inválido da participação no lucro.")]
        [InlineData(3,"Valor disponibilizado é menor do que a soma das participações no lucro dos funcionários.")]
        [InlineData(998,"Mensagem de teste")]
        [Theory]
        public async Task Quando_Get_Chamado_Retorna_400BadRequest(int code, string message)
        {
            var mediator = Substitute.For<ISender>();
            var logger = Substitute.For<ILogger<ProfitsSharingController>>();
            var serviceError = new ServiceError(message, code);

            mediator.Send(Arg.Any<GetProfitSharingQuery>()).Returns(ServiceResult.Failed<ProfitsSharingDTO>(serviceError));

            var profitsSharingController = new ProfitsSharingController(mediator, logger);
            var result = await profitsSharingController.Get(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(message, ((ObjectResult)result.Result).Value);
        }

        /// <summary>
        /// Cenários de teste do método Get que retorna 500 (Internal Server Error)
        /// </summary>
        [Fact]
        public async Task Quando_Get_Chamado_Retorna_500InternalServerError()
        {
            var mediator = Substitute.For<ISender>();
            var logger = Substitute.For<ILogger<ProfitsSharingController>>();

            mediator.Send(Arg.Any<GetProfitSharingQuery>()).Throws(new Exception());

            var profitsSharingController = new ProfitsSharingController(mediator, logger);
            var result = await profitsSharingController.Get(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, ((ObjectResult)result.Result).StatusCode);
            Assert.Equal("Ocorreu uma exceção.", ((ObjectResult)result.Result).Value);
        }
    }
}
