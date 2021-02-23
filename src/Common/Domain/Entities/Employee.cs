using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    /// <summary>
    /// Funcionário
    /// </summary>
    public class Employee
    {
        #region Constants

        /// <summary>
        /// Peso 1
        /// </summary>
        private const int SCORE_1 = 1;

        /// <summary>
        /// Peso 2
        /// </summary>
        private const int SCORE_2 = 2;

        /// <summary>
        /// Peso 3
        /// </summary>
        private const int SCORE_3 = 3;

        /// <summary>
        /// Peso 5
        /// </summary>
        private const int SCORE_5 = 5;

        /// <summary>
        /// Mensagem de erro para atributo inválido
        /// </summary>
        private const string INVALID_ATTRIBUTE_MESSAGE = "Atributo com valor inválido.";

        #endregion

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
        /// Área de atuação
        /// </summary>
        [JsonPropertyName("area")]
        public string OccupationArea { get; set; }

        /// <summary>
        /// Cargo
        /// </summary>
        [JsonPropertyName("cargo")]
        public string JobTitle { get; set; }

        /// <summary>
        /// Salário bruto
        /// </summary>
        [JsonPropertyName("salario_bruto")]
        public string Salary { get; set; }

        /// <summary>
        /// Data de Admissão
        /// </summary>
        [JsonPropertyName("data_de_admissao")]
        public string AdmissionDate { get; set; }

        /// <summary>
        /// Participação do funcionário no lucro
        /// </summary>
        /// <remarks>Fórmula para calcular a participação do funcionário:</remarks>
        /// <remarks>(((SB * PTA) + (SB * PAA)) / PFS) * 12 (Meses do ano)</remarks>
        /// <remarks>SB = Salário Bruto</remarks>
        /// <remarks>PTA = Peso por tempo de admissão</remarks>
        /// <remarks>PAA = Peso</remarks>
        /// <remarks>PFS = Peso por faixa salarial</remarks>
        public decimal ProfitSharing
        {
            get
            {
                return ((SalaryDecimal * AdmissionDurationScore) + (SalaryDecimal * OccupationAreaScore)) / SalaryRangeScore * 12;
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Salário bruto
        /// </summary>
        private decimal SalaryDecimal 
        { 
            get 
            {
                if (!string.IsNullOrEmpty(Salary) && 
                    decimal.TryParse(Salary.Replace("R$ ", string.Empty).Replace(".", string.Empty).Replace(",", "."), out decimal salaryDecimal))
                    return salaryDecimal;
                else
                    throw new ArgumentException(INVALID_ATTRIBUTE_MESSAGE, nameof(Salary));
            } 
        }

        /// <summary>
        /// Data de Admissão
        /// </summary>
        private DateTime Admission
        {
            get
            {
                if (DateTime.TryParseExact(AdmissionDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime admission))
                    return admission;
                else
                    throw new ArgumentException(INVALID_ATTRIBUTE_MESSAGE, nameof(AdmissionDate));
            }
        }

        /// <summary>
        /// Peso por tempo de admissão
        /// </summary>
        /// <remarks>Peso 1: Até 1 ano de casa;</remarks>
        /// <remarks>Peso 2: Mais de 1 ano e menos de 3 anos;</remarks>
        /// <remarks>Peso 3: Acima de 3 anos e menos de 8 anos;</remarks>
        /// <remarks>Peso 5: Mais de 8 anos</remarks>
        private int AdmissionDurationScore
        {
            get
            {
                if (Admission.AddYears(1) > DateTime.Today && Admission.AddYears(3) < DateTime.Today) return SCORE_2;

                if (Admission.AddYears(3) > DateTime.Today && Admission.AddYears(8) < DateTime.Today) return SCORE_3;

                if (Admission.AddYears(8) > DateTime.Today) return SCORE_5;

                return SCORE_1;
            }
        }

        /// <summary>
        /// Pedo por área de atuação
        /// </summary>
        /// <remarks>Peso 1: Diretoria;</remarks>
        /// <remarks>Peso 2: Contabilidade, Financeiro, Tecnologia;</remarks>
        /// <remarks>Peso 3: Serviços Gerais;</remarks>
        /// <remarks>Peso 5: Relacionamento com o Cliente;</remarks>
        private int OccupationAreaScore
        {
            get
            {
                const string BOARD = "Diretoria";

                const string TECHNOLOGY = "Tecnologia";

                const string FINANCIAL = "Financeiro";

                const string ACCOUNTING = "Contabilidade";

                const string GENERAL_SERVICES = "Serviços Gerais";

                const string CUSTOMER_RELATIONSHIP = "Relacionamento com o Cliente";

                if(!string.IsNullOrEmpty(OccupationArea))
                {
                    if (OccupationArea.Equals(BOARD, StringComparison.InvariantCultureIgnoreCase)) return SCORE_1;

                    if (OccupationArea.Equals(ACCOUNTING, StringComparison.InvariantCultureIgnoreCase) ||
                        OccupationArea.Equals(FINANCIAL, StringComparison.InvariantCultureIgnoreCase) ||
                        OccupationArea.Equals(TECHNOLOGY, StringComparison.InvariantCultureIgnoreCase)) return SCORE_2;

                    if (OccupationArea.Equals(GENERAL_SERVICES, StringComparison.InvariantCultureIgnoreCase)) return SCORE_3;

                    if (OccupationArea.Equals(CUSTOMER_RELATIONSHIP, StringComparison.InvariantCultureIgnoreCase)) return SCORE_5;
                }

                throw new ArgumentException(INVALID_ATTRIBUTE_MESSAGE, nameof(OccupationArea));
            }
        }

        /// <summary>
        /// Peso por faixa salarial
        /// </summary>
        /// <remarks>Peso 1: Todos os estagiários e funcionários que ganham até 3 salários mínimos;</remarks>
        /// <remarks>Peso 2: Acima de 3 salários mínimos e menor que 5 salários mínimos;</remarks>
        /// <remarks>Peso 3: Acima de 5 salários mínimos e menor que 8 salários mínimos;</remarks>
        /// <remarks>Peso 5: Acima de 8 salários mínimos;</remarks>
        private int SalaryRangeScore
        {
            get
            {
                const string INTERN = "Estagiário";

                const decimal MINIMUM_SALARY = 1100;

                if (JobTitle.Equals(INTERN, StringComparison.InvariantCultureIgnoreCase) ||
                    SalaryDecimal <= (MINIMUM_SALARY * 3)) return SCORE_1;

                if (SalaryDecimal > (MINIMUM_SALARY * 3) && SalaryDecimal < (MINIMUM_SALARY * 5)) return SCORE_2;

                if (SalaryDecimal > (MINIMUM_SALARY * 5) && SalaryDecimal < (MINIMUM_SALARY * 8)) return SCORE_3;

                return SCORE_5;
            }
        }

        #endregion
    }
}
