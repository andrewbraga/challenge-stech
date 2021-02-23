using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO;
using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using MediatR;

namespace Application.ProfitSharing.Queries.GetProfitSharing
{
    /// <summary>
    /// Query para consultar a distribuição do lucro
    /// </summary>
    public class GetProfitSharingQuery : IRequest<ServiceResult<ProfitsSharingDTO>>
    {
        #region Public Properties

        /// <summary>
        /// Total do lucro disponibilizado
        /// </summary>
        public decimal TotalAvailable { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="totalAvailable">Total do lucro disponibilizado</param>
        public GetProfitSharingQuery(decimal totalAvailable)
        {
            TotalAvailable = totalAvailable;
        }

        #endregion
    }

    /// <summary>
    /// Handle da query para consultar a distribuição do lucro
    /// </summary>
    public class GetProfitSharingQueryHandler : IRequestHandler<GetProfitSharingQuery, ServiceResult<ProfitsSharingDTO>>
    {
        #region Constants

        /// <summary>
        /// 
        /// </summary>
        private const string EMPLOYEE_KEY = "Funcionarios";

        #endregion

        #region Private Properties

        /// <summary>
        /// Conexão de dados da aplicação
        /// </summary>
        private IRedisConnection Connection { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="connection">Conexão de dados da aplicação</param>
        public GetProfitSharingQueryHandler(IRedisConnection connection)
        {
            Connection = connection;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ServiceResult<ProfitsSharingDTO>> Handle(GetProfitSharingQuery request, CancellationToken cancellationToken)
        {
            var employees = JsonSerializer.Deserialize<IEnumerable<Employee>>(Connection.GetValueFromKey(EMPLOYEE_KEY));

            if (!employees.Any())
            {
                var serviceError = new ServiceError("Não existe funcionário na base de dados.", 1);

                return ServiceResult.Failed<ProfitsSharingDTO>(serviceError);
            }

            if (employees.Any(e => e.ProfitSharing <= 0)) 
            {
                var serviceError = new ServiceError("Existe funcionário com valor inválido da participação no lucro.", 2);

                return ServiceResult.Failed<ProfitsSharingDTO>(serviceError);
            }

            if (request.TotalAvailable < employees.Sum(e => e.ProfitSharing))
            {
                var serviceError = new ServiceError("Valor disponibilizado é menor do que a soma das participações no lucro dos funcionários.", 3);

                return ServiceResult.Failed<ProfitsSharingDTO>(serviceError);
            }

            var profitSharingEmployees = employees.Select(e => new ProfitSharingEmployeeDTO()
            {
                Registration = e.Registration,
                Name = e.Name,
                ProfitSharing = e.ProfitSharing
            });

            var data = new ProfitsSharingDTO(request.TotalAvailable, profitSharingEmployees);

            return ServiceResult.Success<ProfitsSharingDTO>(data);
        }

        #endregion
    }
}
