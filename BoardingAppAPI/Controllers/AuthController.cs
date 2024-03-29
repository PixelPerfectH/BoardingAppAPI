﻿using BoardingAppAPI.Models.Auth;
using BoardingAppAPI.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly BoardingAppContext _context;

        public AuthController(BoardingAppContext context, ILogger<AuthController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> LoginByPasswordAsync([FromBody] LoginByPasswordModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.UserName == model.UserName, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.HashedPassword))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            var user = new DBUser()
            {
                UserName = model.UserName,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password),
                CreatedAt = DateTime.UtcNow,
                Tasks = GetActualTasks()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private static List<DBTask> GetActualTasks()
        {
            var task1 = new DBTask()
            {
                Name = "Task1",
                Description = "Description 1",
                IsActive = true,
                Level = 1
            };
            var task2 = new DBTask()
            {
                Name = "Task2",
                Description = "Description 2",
                IsActive = true,
                Level = 2
            };
            var task3 = new DBTask()
            {
                Name = "Task3",
                Description = "Description 3",
                IsActive = true,
                Level = 3
            };
            var task4 = new DBTask()
            {
                Name = "Task4",
                Description = "Description 4",
                IsActive = true,
                Level = 4
            };
            var task5 = new DBTask()
            {
                Name = "Task5",
                Description = "Description 5",
                IsActive = true,
                Level = 5
            };
            var tasks = new List<DBTask>()
            {
                task1,
                task2,
                task3,
                task4,
                task5
            };

            return tasks;
        }
    }
}
