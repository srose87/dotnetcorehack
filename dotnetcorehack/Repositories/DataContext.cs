using dotnetcorehack.Models;
using Microsoft.EntityFrameworkCore;
 
namespace dotnetcorehack.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}