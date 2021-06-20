using BusinessLogic.Services;
using LogsServerInterface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Fillter;

namespace WebAPI.Controllers
{
    [Route("logs")]
    [ApiController]
    [FilterExceptions]
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
        
         
        [HttpGet("{creationDate}")]
        public ActionResult GetByDate(string creationDate)
        {
            return Ok(_logService.GetByCreationDate(creationDate));
        }
    }
}