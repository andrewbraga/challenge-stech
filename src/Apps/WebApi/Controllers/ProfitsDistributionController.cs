using System;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfitsDistributionController : BaseApiController
    {
        private readonly ILogger<ProfitsDistributionController> _logger;

        public ProfitsDistributionController(ILogger<ProfitsDistributionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResult<ProfitsDistributionDTO>>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
