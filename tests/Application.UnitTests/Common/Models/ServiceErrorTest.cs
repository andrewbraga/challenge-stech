using System;
using Application.Common.Models;
using Xunit;

namespace Application.UnitTests.Common.Models
{
    /// <summary>
    /// Testes da classe ServiceError
    /// </summary>
    public class ServiceErrorTest
    {
        /// <summary>
        /// Cenário de teste para método DefaultError
        /// </summary>
        [Fact]
        public void Quando_ServiceError_For_DefaultError_Retorna_Message_E_Code_Preenchido()
        {
            var serviceError = ServiceError.DefaultError;

            Assert.Equal("Ocorreu uma exceção.", serviceError.Message);
            Assert.Equal(999, serviceError.Code);
        }

        /// <summary>
        /// Cenário de teste para método Custom Message
        /// </summary>
        [Fact]
        public void Quando_ServiceError_For_CustomMessage_Retorna_Message_Personalizada()
        {
            var serviceError = ServiceError.CustomMessage("Test");

            Assert.Equal("Test", serviceError.Message);
            Assert.Equal(998, serviceError.Code);
        }

        /// <summary>
        /// Cenário de teste para os atributos Message e Code que não lança exceção
        /// </summary>
        [Fact]
        public void Quando_ServiceError_For_Instanciado_Com_Dados_Validos_Retorna_Message_E_Code_Preenchido()
        {
            var serviceError = new ServiceError("Test", 1);

            Assert.Equal("Test", serviceError.Message);
            Assert.Equal(1, serviceError.Code);
        }

        /// <summary>
        /// Cenários de teste para os atributos Message e Code que lança exceção
        /// </summary>
        /// <param name="message">Valor do atributo Message</param>
        /// <param name="code">Valor do atributo Code</param>
        [InlineData(null, 0)]
        [InlineData(null, 1)]
        [InlineData("", 0)]
        [InlineData("", 1)]
        [InlineData("Test", 0)]
        [Theory]
        public void Quando_ServiceError_For_Instanciado_Com_Dados_Invalidos_Lanca_Excecao(string message, int code)
        {
            Assert.Throws<ArgumentException>(() => new ServiceError(message, code));
        }
    }
}
