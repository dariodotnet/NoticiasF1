using System;

namespace F1WebCrawler.Core
{
    public interface INoticia
    {
        string GetTitle();
        string GetImage();
        string GetContent();
        DateTime GetDateTime();
    }
}