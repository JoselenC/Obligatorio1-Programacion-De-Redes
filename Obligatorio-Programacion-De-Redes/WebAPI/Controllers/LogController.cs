using System.Threading.Tasks;
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
        public async Task<ActionResult> Get()
        {
            var posts = await _logService.GetLogsAsync();
            return Ok(posts );
        }
        
         
        [HttpGet("{creationDate}")]
        public ActionResult GetByDate(string creationDate)
        {
            return Ok(_logService.GetByCreationDateAsync(creationDate));
        }
    }
}