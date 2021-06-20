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
        public ActionResult GetLogByDate([FromQuery]string creationDate)
        {
            return Ok(_logService.GetByCreationDate(creationDate));
        }
        
        [HttpGet("post")]
        public ActionResult GetLogByPost([FromQuery]string name)
        {
            return Ok(_logService.GetByPost(name));
        }
        
        [HttpGet("theme")]
        public ActionResult GetLogByTheme([FromQuery]string name)
        {
            return Ok(_logService.GetByTheme(name));
        }
        
        [HttpGet("type")]
        public ActionResult GetLogByType([FromQuery]string type)
        {
            return Ok(_logService.GetByType(type));
        }
    }
}