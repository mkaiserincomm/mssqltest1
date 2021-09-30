using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace mssqltest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly CategoryContext _context;
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger, CategoryContext context)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation(new EventId(1001,"Constructor"),"RedisControler VersionController");
        }

        // GET: api/version
        [HttpGet]
        public string GetVersion() => Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

    }
}
