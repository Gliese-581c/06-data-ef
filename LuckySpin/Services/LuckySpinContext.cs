using Microsoft.EntityFrameworkCore;
using LuckySpin.Models; // Entity models are in this namespace    

namespace LuckySpin.Services
{
    public class LuckySpinContext : DbContext
    {
        public LuckySpinContext(DbContextOptions<LuckySpinContext> options) : base(options)
        {
        }

        //TODO: Add DbSet properties as entities Players, Games, and Spins


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure your entities here
            modelBuilder.Entity<Game>()
            .Navigation(g => g.Player)
            .AutoInclude();

            modelBuilder.Entity<Game>()
            .Navigation(g => g.Spins)
            .AutoInclude();

            modelBuilder.Entity<Spin>()
            .Navigation(s => s.Game)
            .AutoInclude();
        }
    }
}