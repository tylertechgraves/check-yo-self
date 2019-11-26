using Microsoft.EntityFrameworkCore;

namespace paycheck_calculator_web.Server
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }        
    }
}
