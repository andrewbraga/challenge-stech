using System;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO;
using Application.ProfitSharing.Queries.GetProfitSharing;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    /// <summary>
    /// Operações da API de distribuição de lucros
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProfitsSharingController : ControllerBase
    {
        #region Private Properties

        /// <summary>
        /// Instância do Mediator
        /// </summary>
        private ISender Mediator { get; }

        /// <summary>
        /// Instância do Logger
        /// </summary>
        private ILogger<ProfitsSharingController> Logger { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="mediator">Instância do Mediator</param>
        /// <param name="logger">Instância do Logger</param>
        public ProfitsSharingController(ISender mediator, ILogger<ProfitsSharingController> logger)
        {
            Mediator = mediator;
            Logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retorna o cálculo da distribuição dos lucros aos funcionários
        /// </summary>
        /// <param name="totalAvailable">Total do lucro disponibilizado</param>
        /// <returns></returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfitsSharingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<ProfitsSharingDTO>> Get(decimal totalAvailable)
        {
            try
            {
                var query = new GetProfitSharingQuery(totalAvailable);

                var result = await Mediator.Send(query);

                if (!result.Succeeded) return BadRequest(result.ServiceError.Message);

                return Ok(result.Data);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, ServiceError.DefaultError.Message);
            }
        }

        #endregion
    }
}
