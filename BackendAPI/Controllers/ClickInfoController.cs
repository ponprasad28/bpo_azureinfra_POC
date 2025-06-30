using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClickInfoController : ControllerBase
    {
        private readonly IClickInfoService _clickInfoService;

        public ClickInfoController(IClickInfoService clickInfoService)
        {
            _clickInfoService = clickInfoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClickInfo clickInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clickInfoService.AddAsync(clickInfo);
            return Ok(new { message = "ClickInfo saved successfully" });
        }
    }
}
