using BoardingAppAPI.Models;
using BoardingAppAPI.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BoardingAppAPI
{
    public class BoardingAppContext : DbContext
    {
        [NotNull]
        public DbSet<DBUser>? Users { get; set; }

        [NotNull]
        public DbSet<DBActivity>? Activities { get; set; }

        public BoardingAppContext(DbContextOptions<BoardingAppContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
