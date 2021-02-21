﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Application.DTO
{
    /// <summary>
    /// DTO do resultado da distribuição do lucro
    /// </summary>
    public class ProfitsSharingDTO
    {
        #region Public Properties

        /// <summary>
        /// Participações
        /// </summary>
        [JsonPropertyName("participacoes")]
        public IEnumerable<ProfitSharingEmployeeDTO> ProfitSharingEmployees { get; }

        /// <summary>
        /// Total de funcionários
        /// </summary>
        [JsonPropertyName("total_funcionarios")]
        public string EmployeeQuantityString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Total distribuído
        /// </summary>
        [JsonPropertyName("total_distribuido")]
        public string TotalSharingString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Total disponibilizado
        /// </summary>
        [JsonPropertyName("total_disponibilizado")]
        public string TotalAvaiableString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Saldo Total disponibilizado
        /// </summary>
        [JsonPropertyName("saldo_total_disponibilizado")]
        public string TotalBalanceAvailableString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Total disponibilizado
        /// </summary>
        private decimal TotalAvailable { get; }

        /// <summary>
        /// Total de funcionários
        /// </summary>
        private int EmployeeQuantity
        {
            get
            {
                return ProfitSharingEmployees.Count();
            }
        }

        /// <summary>
        /// Total distribuído
        /// </summary>
        private decimal TotalSharing
        {
            get
            {
                return ProfitSharingEmployees.Sum(e => e.ProfitSharing);
            }
        }

        /// <summary>
        /// Saldo Total disponibilizado
        /// </summary>
        [JsonIgnore]
        private decimal TotalBalanceAvailable
        {
            get
            {
                return TotalAvailable - TotalSharing;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="totalAvailable">Total do lucro disponibilizado</param>
        /// <param name="profitSharingEmployees">Dados das participações dos funcionários no lucro</param>
        public ProfitsSharingDTO(decimal totalAvailable, IEnumerable<ProfitSharingEmployeeDTO> profitSharingEmployees)
        {
            TotalAvailable = totalAvailable;
            ProfitSharingEmployees = profitSharingEmployees;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public ProfitsSharingDTO()
        {

        }

        #endregion
    }
}
