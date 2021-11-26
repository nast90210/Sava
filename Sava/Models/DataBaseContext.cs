using Microsoft.EntityFrameworkCore;
using Sava.Data;

namespace Sava.Models
{
    public sealed class DataBaseContext : DbContext
    {
        public DbSet<TempAudioFile> TempAudioFiles { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}