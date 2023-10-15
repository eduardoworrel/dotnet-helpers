using Microsoft.EntityFrameworkCore;
using Entities;

namespace Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Request> Requests { get; set; } = null!;
}