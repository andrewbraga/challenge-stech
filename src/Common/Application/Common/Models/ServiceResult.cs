namespace Application.Common.Models
{
    /// <summary>
    /// Resposta padrão para requisições do serviço
    /// </summary>
    /// <typeparam name="T">Tipo de dado do resultado do serviço</typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        #region Public Properties

        /// <summary>
        /// Dados do resultado do serviço
        /// </summary>
        public T Data { get; set; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="data">Dados do resultado do serviço</param>
        public ServiceResult(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="serviceError">Dados do erro do serviço</param>
        public ServiceResult(ServiceError serviceError) : base(serviceError)
        {

        }

        #endregion
    }

    /// <summary>
    /// Resposta padrão para requisições do serviço
    /// </summary>
    public class ServiceResult
    {
        #region Public Properties

        /// <summary>
        /// Indica sucesso no resultado do serviço 
        /// </summary>
        public bool Succeeded => this.ServiceError == null;

        /// <summary>
        /// Dados do erro do serviço
        /// </summary>
        public ServiceError ServiceError { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="serviceError">Dados do erro do serviço</param>
        public ServiceResult(ServiceError serviceError)
        {
            if (serviceError == null)
            {
                serviceError = ServiceError.DefaultError;
            }

            ServiceError = serviceError;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public ServiceResult() { }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Retorna instância do resultado do serviço como falha
        /// </summary>
        /// <typeparam name="T">Tipo de dado do resultado do serviço</typeparam>
        /// <param name="serviceError">Dados do erro do serviço</param>
        /// <returns>Instância do resultado do serviço</returns>
        public static ServiceResult<T> Failed<T>(ServiceError serviceError)
        {
            return new ServiceResult<T>(serviceError);
        }

        /// <summary>
        /// Retorna instância do resultado do serviço como sucesso
        /// </summary>
        /// <typeparam name="T">Tipo de dado do resultado do serviço</typeparam>
        /// <param name="data">Dados do resultado do serviço</param>
        /// <returns>Instância do resultado do serviço</returns>
        public static ServiceResult<T> Success<T>(T data)
        {
            return new ServiceResult<T>(data);
        }

        #endregion
    }
}
