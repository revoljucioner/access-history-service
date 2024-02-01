using AccessManager.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace AccessHistoryService.Providers
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<EmployeeInfoDbModel> Employee { get; set; }

        public DbSet<DepartmentDbModel> Department { get; set; }

        public DbSet<RoomDbModel> Room { get; set; }

        public DbSet<EventDbModel> Event { get; set; }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}