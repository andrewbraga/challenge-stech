using System;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfitsSharingController : BaseApiController
    {
        private readonly ILogger<ProfitsSharingController> _logger;

        public ProfitsSharingController(ILogger<ProfitsSharingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<ProfitsSharingDTO> Get()
        {
            throw new NotImplementedException();
        }
    }
}
