using System;

namespace F1WebCrawler.Model
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public string Imagen { get; set; }
        public string Enlace { get; set; }
    }
}
