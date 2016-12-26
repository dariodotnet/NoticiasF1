using F1WebCrawler.Core;
using F1WebCrawler.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace F1WebCrawler
{
    public class Crawler
    {
        private INoticia laF1Noticias;

        public async Task<IEnumerable<Noticia>> Noticias()
        {
            var noticias = new List<Noticia>();

            foreach (var noticia in await LaF1Es())
            {
                noticias.Add(noticia);
            }
            return noticias.OrderByDescending(x => x.Fecha);
        }

        private async Task<IEnumerable<Noticia>> LaF1Es()
        {
            var noticias = new List<Noticia>();
            var clienteWeb = new HttpClient();
            var html = await clienteWeb.GetStringAsync("http://www.laf1.es/");
            var htmlDocumento = new HtmlDocument();
            htmlDocumento.LoadHtml(html);

            var noticiasHtml =
                htmlDocumento.DocumentNode.Descendants("div")
                    .Where(x => x.GetAttributeValue("class", "").Contains("item"))
                    .ToList();

            foreach (var node in noticiasHtml)
            {
                if (node.GetAttributeValue("class", "").Equals("item item_subtitular"))
                {
                    noticias.Add(await LaF1ExtraerSubtitular(node));
                }
                if (node.GetAttributeValue("class", "").Equals("item item_titular") &&
                    node.Descendants("div").FirstOrDefault()
                    .Descendants("span").FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("media  en_bloque")) != null)
                {
                    noticias.Add(await LaF1ExtraerTitular(node));
                }
            }

            return noticias;
        }

        private async Task<Noticia> LaF1ExtraerTitular(HtmlNode node)
        {
            var noticia = new Noticia();

            noticia.Enlace = node.Descendants("div").FirstOrDefault()
                .Descendants("h2").FirstOrDefault()
                .Descendants("a").FirstOrDefault()
                .ChildAttributes("href").FirstOrDefault().Value;

            return noticia;
        }

        private async Task<Noticia> LaF1ExtraerSubtitular(HtmlNode node)
        {
            var noticia = new Noticia();

            noticia.Enlace = node.Descendants("h2").FirstOrDefault()
                    .Descendants("a").FirstOrDefault()
                    .ChildAttributes("href").FirstOrDefault().Value;

            return noticia;
        }

        private string LaF1ExtraerImagen(HtmlDocument documento)
        {
            return documento.DocumentNode.Descendants("div").FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("section_"))
                .Descendants("span").FirstOrDefault()
                .Descendants("a").FirstOrDefault()
                .Descendants("img").FirstOrDefault()
                .ChildAttributes("src").FirstOrDefault().Value;
        }

        public DateTime LaF1ExtraerFecha(string fecha)
        {
            string[] fechaSplit = fecha.Split(' ');
            if (fechaSplit[3] == "dic")
            {
                fechaSplit[3] = "12";
            }
            fecha = $"{fechaSplit[2]} /{fechaSplit[3]}/{fechaSplit[4]} {fechaSplit[6]}";
            return Convert.ToDateTime(fecha);
        }

        public async Task<string> LaF1ExtraerTitulo(string enlace)
        {
            string resultado = string.Empty;
            var clienteWeb = new HttpClient();
            var html = await clienteWeb.GetStringAsync(enlace);
            var documento = new HtmlDocument();
            documento.LoadHtml(html);

            resultado =
                documento.DocumentNode.Descendants("h1")
                    .FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("supertitular"))
                    .InnerText;

            return resultado;
        }

        public async Task<Noticia> LaF1ExtraerNoticia(string enlace)
        {
            var resultado = new Noticia();
            var clienteWeb = new HttpClient();
            var html = await clienteWeb.GetStringAsync(enlace);
            var documento = new HtmlDocument();
            documento.LoadHtml(html);

            laF1Noticias = new CrawlerLaF1Es(documento);


            resultado.Enlace = enlace;
            resultado.Titulo = laF1Noticias.GetTitle();
            resultado.Visto = false;

            var sb = new StringBuilder();
            var nodosTexto =
                documento.DocumentNode.Descendants("div")
                    .Where(x => x.GetAttributeValue("class", "").Equals("cuerpo  mb mt")).ToList();
            foreach (var node in nodosTexto)
            {
                sb.Append(node.InnerText);
                sb.Append(Environment.NewLine);
            }

            resultado.Contenido = sb.ToString();
            resultado.Imagen = laF1Noticias.GetImage(); //LaF1ExtraerImagen(documento);

            var fecha = documento.DocumentNode.Descendants("span")
                    .FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("fecha")).InnerText;

            resultado.Fecha = LaF1ExtraerFecha(fecha);

            return resultado;
        }

        private async Task<HtmlDocument> webDocument(string link)
        {
            var result = new HtmlDocument();
            var client = new HttpClient();
            var html = await client.GetStringAsync(link);
            result.LoadHtml(html);
            return result;
        }
    }
}