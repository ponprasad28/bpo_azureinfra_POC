using AutoMapper;
using DataAccess.Models;
using DataAccess.Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Authorize(Policy = "AccessAsUser")]
    [ApiController]
    [Route("api/[controller]")]
    public class LogInfoController : ControllerBase
    {
        private readonly ILogInfoService _logInfoService;
        private readonly IMapper _mapper;
        public LogInfoController(ILogInfoService logInfoService, IMapper mapper)
        {
            _logInfoService = logInfoService;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogInfoDTO logInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = _mapper.Map<LogInfo>(logInfo);

            await _logInfoService.AddAsync(model);
            return Ok(new { message = "LogInfo saved successfully" });
        }

        [HttpGet]
        public async Task<ActionResult<List<LogInfoDTO>>> GetAll()
        {
            var models = await _logInfoService.GetAllAsync();
            var dtos = _mapper.Map<List<LogInfoDTO>>(models);
            return Ok(dtos);
        }

    }
}
