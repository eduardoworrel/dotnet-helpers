using Microsoft.EntityFrameworkCore;
public class AppContext : DbContext{
    public DbSet<AppFile> Files {get;set;}
    public AppContext(DbContextOptions<AppContext> options) : base(options){}

}