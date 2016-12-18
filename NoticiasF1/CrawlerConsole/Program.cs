using F1WebCrawler;
using System;
using System.Threading.Tasks;

namespace CrawlerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            StartCrawlerAsync();

            Console.ReadLine();
        }

        private static async Task StartCrawlerAsync()
        {
            try
            {
                var crawler = new Crawler();

                var noticias = await crawler.Noticias();

                foreach (var noticia in noticias)
                {
                    Console.WriteLine(noticia.Fecha);
                    Console.WriteLine(noticia.Titulo);
                    Console.WriteLine(noticia.Imagen);
                    Console.WriteLine();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }
    }
}
