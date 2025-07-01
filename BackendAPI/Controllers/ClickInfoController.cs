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

    public class ClickInfoController : ControllerBase
    {
        private readonly IClickInfoService _clickInfoService;
        private readonly IMapper _mapper;

        public ClickInfoController(IClickInfoService clickInfoService, IMapper mapper)
        {
            _clickInfoService = clickInfoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClickInfoDTO clickInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = _mapper.Map<ClickInfo>(clickInfo);

            await _clickInfoService.AddAsync(model);
            return Ok(new { message = "ClickInfo saved successfully" });
        }

        [HttpGet]
        public async Task<ActionResult<List<ClickInfoDTO>>> GetAll()
        {
            var models = await _clickInfoService.GetAllAsync();
            var dtos = _mapper.Map<List<ClickInfoDTO>>(models);
            return Ok(dtos);
        }
    }
}
