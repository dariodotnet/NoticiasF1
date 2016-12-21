using F1WebCrawler.Model;
using Microsoft.EntityFrameworkCore;

namespace NoticiasF1.Model
{
    public class Contexto : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=F1Noticias.db");
        }
    }
}
