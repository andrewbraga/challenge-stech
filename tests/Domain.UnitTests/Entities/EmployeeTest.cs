using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Xunit;

namespace Domain.UnitTests.Entities
{
    /// <summary>
    /// Testes da classe Employee
    /// </summary>
    public class EmployeeTest
    {
        /// <summary>
        /// Cenários de teste do atributo ProfitSharing que lançam exceção
        /// </summary>
        /// <param name="salary">Salário bruto</param>
        /// <param name="admissionDate">Data de admissão</param>
        /// <param name="occupationArea">Área de atuação</param>
        [InlineData(null, "1900-01-01", "Diretoria")]
        [InlineData("R$ 0,01", null, "Diretoria")]
        [InlineData("R$ 0,01", "1900-01-01", null)]        
        [InlineData("", "1900-01-01", "Diretoria")]
        [InlineData("R$ 0,01", "", "Diretoria")]
        [InlineData("R$ 0,01", "1900-01-01", "")]
        [InlineData("a", "1900-01-01", "Diretoria")]
        [InlineData("R$ 0,01", "a", "Diretoria")]
        [InlineData("R$ 0,01", "1900-01-01", "a")]
        [Theory]
        public void Quando_Funcionario_Chamar_ProfitSharing_Lanca_Excecao(string salary, string admissionDate, string occupationArea)
        {
            var employee = new Employee()
            {
                Salary = salary,
                AdmissionDate = admissionDate,
                OccupationArea = occupationArea
            };

            Assert.Throws<ArgumentException>(() => employee.ProfitSharing);
        }

        /// <summary>
        /// Cenários de teste do atributo ProfitSharing que retornam valor do cálculo para estágiário
        /// </summary>
        /// <param name="admissionDurationScore">Peso por tempo de admissão</param>
        /// <param name="occupationAreaScore">Peso por área de atuação</param>
        [InlineData(1, 1, "Diretoria")]
        [InlineData(1, 2, "Contabilidade")]
        [InlineData(1, 2, "Financeiro")]
        [InlineData(1, 2, "Tecnologia")]
        [InlineData(1, 3, "Serviços Gerais")]
        [InlineData(1, 5, "Relacionamento com o Cliente")]
        [InlineData(2, 1, "Diretoria")]
        [InlineData(2, 2, "Contabilidade")]
        [InlineData(2, 2, "Financeiro")]
        [InlineData(2, 2, "Tecnologia")]
        [InlineData(2, 3, "Serviços Gerais")]
        [InlineData(2, 5, "Relacionamento com o Cliente")]
        [InlineData(3, 1, "Diretoria")]
        [InlineData(3, 2, "Contabilidade")]
        [InlineData(3, 2, "Financeiro")]
        [InlineData(3, 2, "Tecnologia")]
        [InlineData(3, 3, "Serviços Gerais")]
        [InlineData(3, 5, "Relacionamento com o Cliente")]
        [InlineData(5, 1, "Diretoria")]
        [InlineData(5, 2, "Contabilidade")]
        [InlineData(5, 2, "Financeiro")]
        [InlineData(5, 2, "Tecnologia")]
        [InlineData(5, 3, "Serviços Gerais")]
        [InlineData(5, 5, "Relacionamento com o Cliente")]
        [Theory]
        public void Quando_Estagiario_Ou_3_Salarios_Minimos_Chamar_ProfitSharing_Retorna_Valor_Calculo(int admissionDurationScore, int occupationAreaScore, string occupationArea)
        {
            const decimal MINIMUM_SALARY = 1100;

            var admission = admissionDurationScore == 2 ? DateTime.Today.AddYears(3) :
                            admissionDurationScore == 3 ? DateTime.Today.AddYears(8) :
                            admissionDurationScore == 5 ? DateTime.Today.AddYears(8).AddDays(1) :
                            DateTime.Today.AddYears(1);

            var employee = new Employee()
            {
                Salary = "R$ 1.100,00",
                AdmissionDate = admission.ToString("yyyy-MM-dd"),
                JobTitle = "Estagiário",
                OccupationArea = occupationArea
            };

            var profitSharingExpected = ((MINIMUM_SALARY * admissionDurationScore) + (MINIMUM_SALARY * occupationAreaScore)) / 1 * 12;

            Assert.Equal(profitSharingExpected, employee.ProfitSharing);
        }

        /// <summary>
        /// Cenários de teste do atributo ProfitSharing que retornam valor do cálculo
        /// </summary>
        /// <param name="admissionDurationScore">Peso por tempo de admissão</param>
        /// <param name="occupationAreaScore">Peso por área de atuação</param>
        /// <param name="salaryRangeScore">Peso por faixa salarial</param>
        [InlineData(1, 1, 2, "Diretoria")]
        [InlineData(1, 1, 3, "Diretoria")]
        [InlineData(1, 1, 5, "Diretoria")]
        [InlineData(1, 2, 2, "Contabilidade")]
        [InlineData(1, 2, 2, "Financeiro")]
        [InlineData(1, 2, 2, "Tecnologia")]
        [InlineData(1, 2, 3, "Contabilidade")]
        [InlineData(1, 2, 3, "Financeiro")]
        [InlineData(1, 2, 3, "Tecnologia")]
        [InlineData(1, 2, 5, "Contabilidade")]
        [InlineData(1, 2, 5, "Financeiro")]
        [InlineData(1, 2, 5, "Tecnologia")]
        [InlineData(1, 3, 2, "Serviços Gerais")]
        [InlineData(1, 3, 3, "Serviços Gerais")]
        [InlineData(1, 3, 5, "Serviços Gerais")]
        [InlineData(1, 5, 2, "Relacionamento com o Cliente")]
        [InlineData(1, 5, 3, "Relacionamento com o Cliente")]
        [InlineData(1, 5, 5, "Relacionamento com o Cliente")]
        [InlineData(2, 1, 2, "Diretoria")]
        [InlineData(2, 1, 3, "Diretoria")]
        [InlineData(2, 1, 5, "Diretoria")]
        [InlineData(2, 2, 2, "Financeiro")]
        [InlineData(2, 2, 2, "Tecnologia")]
        [InlineData(2, 2, 2, "Contabilidade")]
        [InlineData(2, 2, 3, "Financeiro")]
        [InlineData(2, 2, 3, "Tecnologia")]
        [InlineData(2, 2, 3, "Contabilidade")]
        [InlineData(2, 2, 5, "Financeiro")]
        [InlineData(2, 2, 5, "Tecnologia")]
        [InlineData(2, 2, 5, "Contabilidade")]
        [InlineData(2, 3, 2, "Serviços Gerais")]
        [InlineData(2, 3, 3, "Serviços Gerais")]
        [InlineData(2, 3, 5, "Serviços Gerais")]
        [InlineData(2, 5, 2, "Relacionamento com o Cliente")]
        [InlineData(2, 5, 3, "Relacionamento com o Cliente")]
        [InlineData(2, 5, 5, "Relacionamento com o Cliente")]
        [InlineData(3, 1, 2, "Diretoria")]
        [InlineData(3, 1, 3, "Diretoria")]
        [InlineData(3, 1, 5, "Diretoria")]
        [InlineData(3, 2, 2, "Financeiro")]
        [InlineData(3, 2, 2, "Tecnologia")]
        [InlineData(3, 2, 2, "Contabilidade")]
        [InlineData(3, 2, 3, "Financeiro")]
        [InlineData(3, 2, 3, "Tecnologia")]
        [InlineData(3, 2, 3, "Contabilidade")]
        [InlineData(3, 2, 5, "Financeiro")]
        [InlineData(3, 2, 5, "Tecnologia")]
        [InlineData(3, 2, 5, "Contabilidade")]
        [InlineData(3, 3, 2, "Serviços Gerais")]
        [InlineData(3, 3, 3, "Serviços Gerais")]
        [InlineData(3, 3, 5, "Serviços Gerais")]
        [InlineData(3, 5, 2, "Relacionamento com o Cliente")]
        [InlineData(3, 5, 3, "Relacionamento com o Cliente")]
        [InlineData(3, 5, 5, "Relacionamento com o Cliente")]
        [InlineData(5, 1, 2, "Diretoria")]
        [InlineData(5, 1, 3, "Diretoria")]
        [InlineData(5, 1, 5, "Diretoria")]
        [InlineData(5, 2, 2, "Financeiro")]
        [InlineData(5, 2, 2, "Tecnologia")]
        [InlineData(5, 2, 2, "Contabilidade")]
        [InlineData(5, 2, 3, "Financeiro")]
        [InlineData(5, 2, 3, "Tecnologia")]
        [InlineData(5, 2, 3, "Contabilidade")]
        [InlineData(5, 2, 5, "Financeiro")]
        [InlineData(5, 2, 5, "Tecnologia")]
        [InlineData(5, 2, 5, "Contabilidade")]
        [InlineData(5, 3, 2, "Serviços Gerais")]
        [InlineData(5, 3, 3, "Serviços Gerais")]
        [InlineData(5, 3, 5, "Serviços Gerais")]
        [InlineData(5, 5, 2, "Relacionamento com o Cliente")]
        [InlineData(5, 5, 3, "Relacionamento com o Cliente")]
        [InlineData(5, 5, 5, "Relacionamento com o Cliente")]
        [Theory]
        public void Quando_Funcionario_Chamar_ProfitSharing_Retorna_Valor_Calculo(int admissionDurationScore, int occupationAreaScore, int salaryRangeScore, string occupationArea)
        {
            const decimal MINIMUM_SALARY = 1100;

            var salary = salaryRangeScore == 2 ? (MINIMUM_SALARY * 5) :
                         salaryRangeScore == 3 ? (MINIMUM_SALARY * 8) :
                         salaryRangeScore == 5 ? ((MINIMUM_SALARY * 8) + 1) :
                         MINIMUM_SALARY * 3;

            var admission = admissionDurationScore == 2 ? DateTime.Today.AddYears(3) :
                            admissionDurationScore == 3 ? DateTime.Today.AddYears(8) :
                            admissionDurationScore == 5 ? DateTime.Today.AddYears(8).AddDays(1) :
                            DateTime.Today.AddYears(1);

            var employee = new Employee()
            {
                Salary = salary.ToString(),
                AdmissionDate = admission.ToString("yyyy-MM-dd"),
                OccupationArea = occupationArea
            };

            var profitSharingExpected = ((salary * admissionDurationScore) + (salary * occupationAreaScore)) / salaryRangeScore * 12;

            Assert.Equal(profitSharingExpected, employee.ProfitSharing);
        }
    }
}
