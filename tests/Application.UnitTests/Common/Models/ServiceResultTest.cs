using Application.Common.Models;
using Application.DTO;
using Xunit;

namespace Application.UnitTests.Common.Models
{
    /// <summary>
    /// Testes da classe ServiceResult
    /// </summary>
    public class ServiceResultTest
    {
        /// <summary>
        /// Cenários de teste para atributo Data
        /// </summary>
        /// <param name="notNull">Indica que o atributo dATA está nulo</param>
        [InlineData(true)]
        [InlineData(false)]
        [Theory]
        public void Quando_ServiceResult_For_Sucesso_Retorna_Data_Preenchido(bool notNull)
        {
            var data = notNull ? new ProfitsSharingDTO() : (ProfitsSharingDTO)null;

            var serviceResult = ServiceResult.Success<ProfitsSharingDTO>(data);

            Assert.True(serviceResult.Succeeded);
            Assert.Equal(notNull, serviceResult.Data != null);
        }

        /// <summary>
        /// Cenários de teste para atributos ServiceError
        /// </summary>
        /// <param name="notNull">Indica que o atributo ServiceError está nulo</param>
        [InlineData(true)]
        [InlineData(false)]
        [Theory]
        public void Quando_ServiceResult_For_Falha_Retorna_ServiceError_Preenchido(bool notNull)
        {
            const string TEST_MESSAGE = "Test";
            const int TEST_CODE = 1;

            var message = notNull ? TEST_MESSAGE : "Ocorreu uma exceção.";
            var code = notNull ? TEST_CODE : 999;
            var serviceError = notNull ? new ServiceError(TEST_MESSAGE, TEST_CODE) : (ServiceError)null;

            var serviceResult = ServiceResult.Failed<ProfitsSharingDTO>(serviceError);

            Assert.NotNull(serviceResult.ServiceError);
            Assert.Equal(code, serviceResult.ServiceError.Code);
            Assert.Equal(message, serviceResult.ServiceError.Message);
        }

        /// <summary>
        /// Cenários de teste para atributo Succeed
        /// </summary>
        /// <param name="succeed">Valor do atributo Succeed</param>
        [InlineData(true)]
        [InlineData(false)]
        [Theory]
        public void Quando_ServiceResult_For_Instanciado_Retorna_Succeed_Preenchido(bool succeed)
        {           
            var serviceResult = succeed ? ServiceResult.Success<ProfitsSharingDTO>(null) : ServiceResult.Failed<ProfitsSharingDTO>(null);

            Assert.Equal(succeed, serviceResult.ServiceError == null);
        }
    }
}
