using Microsoft.EntityFrameworkCore;
using Students_CRUD_WebAPI.Models;

namespace Students_CRUD_WebAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentModel> StudentModel { get; set; }
    }
}
