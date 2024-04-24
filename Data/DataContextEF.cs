using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HelloWorld.Models;

namespace HelloWorld.Data
{   public class DataContextEF : DbContext
    {
        private IConfiguration _config;
        public DataContextEF(IConfiguration config)
        {
            // _config = config;
            _config = config;
        }
        //You can take everything one class has, and use another class to inherit things from it.
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //When we create 'DataContextEF' is created, it invokes/uses 'DbContext' as our class inherits from
            //it. Whenever 'DbContext' is called, it automatically invokes the method that lives in it,
            //'onConfiguring'.
            if (!optionsBuilder.IsConfigured)
            {
                //We're checking to see if it has already been configured or not. This variable tells us
                //whether or not we've already called it.
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            //When overriding, we must use the same name. (Duh!)
            modelBuilder.Entity<Computer>()
                .HasKey(Computer => Computer.ComputerId);
                //.HasNoKey();

                // .ToTable("Computer", "TutorialAppSchema");
                // .ToTable("TableName", "SchemaName");
        }
        //Entity Framework.
        //
    }
}
