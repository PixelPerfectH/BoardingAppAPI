using BoardingAppAPI.Models;
using BoardingAppAPI.Models.ActionResult;
using BoardingAppAPI.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ILogger<LevelController> _logger;
        private readonly BoardingAppContext _context;

        public LevelController(BoardingAppContext context, ILogger<LevelController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync(string userName, long levelId, CancellationToken cancellationToken)
        {
            var user = await _context.Users
            .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);

            var level = await _context.Levels
                .FirstOrDefaultAsync(l => l.Id == levelId, cancellationToken);

            if (user is null || level is null)
            {
                return NotFound();
            }

            var tasks = new List<DBTask>();

            foreach(var task in user.Tasks)
            {
                if (task.Level == levelId)
                {
                    tasks.Add(task);
                }
            }

            var levelResult = new LevelResult()
            {
                Level = levelId,
                Name = level.Name,
                Tasks = tasks
            };

            return Ok(levelResult);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddLevelAsync([FromBody] LevelModel model)
        {
            var level = new DBLevel()
            {
                Name = model.Name
            };

            _context.Levels.Add(level);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
