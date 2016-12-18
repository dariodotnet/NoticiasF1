using F1WebCrawler.Model;
using System.Collections.Generic;

namespace NoticiasF1.Model
{
    public static class NoticiasDisponibles
    {
        public static bool NoticiasCargadas { get; set; }
        public static IEnumerable<Noticia> Noticias { get; set; }
    }
}