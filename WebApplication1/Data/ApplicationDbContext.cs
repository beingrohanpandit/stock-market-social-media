using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

// In the below ApplicationDbContext is the parent class where as the DbContext is the Base Class
// This isBasically inheritance.
public class ApplicationDbContext: DbContext
{
    // This is a Constructor and shortcut to create this is ctor
    public ApplicationDbContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions) // This means we get the dbContextOptions from base class and pass into the constructor
    {
            
    }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}