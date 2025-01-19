using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{

    //We use our database context to save things into our database
    public class SpendSmartDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : base(options) 
        {
            
        }
    }
}
