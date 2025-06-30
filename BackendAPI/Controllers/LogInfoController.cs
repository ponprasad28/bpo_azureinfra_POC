using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LogInfoController : ControllerBase
    {
        private readonly ILogInfoService _logInfoService;

        public LogInfoController(ILogInfoService logInfoService)
        {
            _logInfoService = logInfoService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogInfo logInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _logInfoService.AddAsync(logInfo);
            return Ok(new { message = "LogInfo saved successfully" });
        }

        [HttpGet]
        public async Task<ActionResult<List<LogInfo>>> GetAll()
        {
            var logs = await _logInfoService.GetAllAsync();
            return Ok(logs);
        }

    }
}
