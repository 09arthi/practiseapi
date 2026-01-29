using Microsoft.EntityFrameworkCore;
using practiseapi.Model;

namespace practiseapi.Data
{
    public class ApiDbcontext:DbContext
    {

        public ApiDbcontext(DbContextOptions<ApiDbcontext>options):base(options) { 
        }
        
           public DbSet<empreg> empreg { get; set; }
        public DbSet<login> login { get; set; }


    }
}
