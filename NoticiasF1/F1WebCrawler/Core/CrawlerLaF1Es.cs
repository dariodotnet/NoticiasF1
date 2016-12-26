using HtmlAgilityPack;
using System;
using System.Linq;

namespace F1WebCrawler.Core
{
    public class CrawlerLaF1Es : INoticia
    {
        private HtmlDocument _document;

        public CrawlerLaF1Es(HtmlDocument document)
        {
            _document = document;
        }

        public string GetTitle()
        {
            var result = string.Empty;

            result = _document.DocumentNode.Descendants("h1")
                .FirstOrDefault(x => x.GetAttributeValue("class", "")
                .Equals("supertitular")).InnerText;

            if (string.IsNullOrEmpty(result))
                throw new ArgumentNullException("Title", "Error scanning DOM.");

            return result;
        }

        public string GetImage()
        {
            var result = string.Empty;

            result = _document.DocumentNode.Descendants("div").FirstOrDefault(x => x.GetAttributeValue("class", "").Equals("section_"))
                .Descendants("span").FirstOrDefault()
                .Descendants("a").FirstOrDefault()
                .Descendants("img").FirstOrDefault()
                .ChildAttributes("src").FirstOrDefault().Value;

            if (string.IsNullOrEmpty(result))
                throw new ArgumentNullException("Image", "Error scanning DOM.");

            return result;
        }

        public string GetContent()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime()
        {
            throw new NotImplementedException();
        }
    }
}