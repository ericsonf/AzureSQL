using Microsoft.EntityFrameworkCore;
using RestBaseFormSQL.Core.Entities;

namespace RestBaseFormSQL.Infra.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }

        public DbSet<Person> Person { get; set; }
    }
}