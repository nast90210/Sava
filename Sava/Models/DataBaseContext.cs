using Microsoft.EntityFrameworkCore;
using Sava.Data;

namespace Sava.Models
{
    public sealed class DataBaseContext : DbContext
    {
        public DbSet<AudioFile> AudioFiles { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Person> Persons { get; set; }
        
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
            Database.EnsureCreated(); // создаем базу данных при первом обращении
        }
    }
}