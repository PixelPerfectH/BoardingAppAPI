using BoardingAppAPI.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly BoardingAppContext _context;

        public TaskController(BoardingAppContext context, ILogger<TaskController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLevelsAsync(string userName, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);

            if (user is null)
            {
                return NotFound();
            }

            var levels = new Dictionary<int, List<DBTask>>();

            foreach (var task in user.Tasks)
            {
                var levelsList = new List<DBTask>(); ;

                if (levels.ContainsKey(task.Level))
                {
                    levelsList = levels[task.Level];

                }

                levelsList.Add(task);
                levels[task.Level] = levelsList;
            }

            return Ok(levels);
        }
    }
}
