namespace Infrastructure.Persistence.Interfaces
{
    /// <summary>
    /// Interface da conexão de dados da aplicação
    /// </summary>
    public interface IRedisConnection
    {
        #region Public Methods

        /// <summary>
        /// Retorna o valor armazenado no contexto a partir da chave
        /// </summary>
        /// <param name="key">Chave</param>
        /// <returns>Valor armazenado no contexto</returns>
        string GetValueFromKey(string key);

        /// <summary>
        /// Armazena valor no contexto para uma chave
        /// </summary>
        /// <param name="key">Chave</param>
        /// <param name="value">Valor</param>
        void SetValueToKey(string key, string value);

        #endregion
    }
}
