using System;
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
        [InlineData(null, null, null)]
        [Theory]
        public void Quando_Funcionario_Chamar_ProfitSharing_Lanca_Excecao(string salary, string admissionDate, string occupationArea)
        {
            var employee = new Employee()
            {
                Salary = salary,
                AdmissionDate = admissionDate,
                OccupationArea = occupationArea
            };

            Assert.Throws<Exception>(() => employee.ProfitSharing);
        }

        /// <summary>
        /// Cenários de teste do atributo ProfitSharing que retornam valor do cálculo
        /// </summary>
        /// <param name="salary">Salário bruto</param>
        /// <param name="admissionDate">Data de admissão</param>
        /// <param name="occupationArea">Área de atuação</param>
        /// <param name="profitSharingExpected">Participação no lucro esperada</param>
        [InlineData("R$ 1,00", "", "", 1)]
        [Theory]
        public void Quando_Funcionario_Chamar_ProfitSharing_Retorna_Valor_Calculo(string salary, string admissionDate, string occupationArea, decimal profitSharingExpected)
        {
            var employee = new Employee()
            {
                Salary = salary,
                AdmissionDate = admissionDate,
                OccupationArea = occupationArea
            };

            Assert.Equal(profitSharingExpected, employee.ProfitSharing);
        }
    }
}
