using BusinessLogic.Services;
using LogsServerInterface;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("logs")]
    [ApiController]
    public class LogController: Controller
    {
        private ILogService _logService;
        
        public LogController(ILogService logService)
        {
            _logService = logService;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_logService.Get());
        }
    }
}