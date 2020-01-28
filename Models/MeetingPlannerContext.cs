using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MeetingScheduler.Models
{
  // >dotnet ef migration add testMigration in AspNet5MultipleProject
  public class MeetingPlannerContext : DbContext
  {
    public MeetingPlannerContext(DbContextOptions<MeetingPlannerContext> options) : base(options)
    {
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Days> Groups { get; set; }
  }
}