using System;
using System.Text.Json.Serialization;

namespace Application.DTO
{
    /// <summary>
    /// DTO da participção do funcionário no lucro
    /// </summary>
    public class ProfitSharingEmployeeDTO
    {
        #region Public Properties

        /// <summary>
        /// Matrícula
        /// </summary>
        [JsonPropertyName("matricula")]
        public string Registration { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        /// <summary>
        /// Valor da participação
        /// </summary>
        [JsonPropertyName("valor_da_participacao")]
        public string ProfitSharingString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Valor da participação
        /// </summary>
        [JsonIgnore]
        public decimal ProfitSharing { get; set; }

        #endregion
    }
}
