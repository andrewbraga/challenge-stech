using System;

namespace Application.Common.Models
{
    /// <summary>
    /// Erro do serviço
    /// </summary>
    public class ServiceError
    {
        #region Public Properties

        /// <summary>
        /// Mensagem de erro exibida ao usuário
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Código de erro do sistema
        /// </summary>
        public int Code { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem exibida para o usuário</param>
        /// <param name="code">Código do erro no sistema</param>
        public ServiceError(string message, int code)
        {
            const string ARGUMENT_EXCEPTION_MESSAGE = "Parâmetro com valor inválido.";

            if (string.IsNullOrEmpty(message)) throw new ArgumentException(ARGUMENT_EXCEPTION_MESSAGE, nameof(this.Message));

            if (code < 1) throw new ArgumentException(ARGUMENT_EXCEPTION_MESSAGE, nameof(this.Code));

            this.Message = message;
            this.Code = code;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Erro padrão quando ocorrer uma exceção
        /// </summary>
        public static ServiceError DefaultError => new ServiceError("Ocorreu uma exceção.", 999);

        /// <summary>
        /// Erro com mensagem customizada
        /// </summary>
        public static ServiceError CustomMessage(string errorMessage)
        {
            return new ServiceError(errorMessage, 998);
        }

        #endregion
    }

}
