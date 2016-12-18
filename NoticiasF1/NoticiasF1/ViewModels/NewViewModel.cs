using F1WebCrawler;
using F1WebCrawler.Model;
using NoticiasF1.ViewModels.Base;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace NoticiasF1.ViewModels
{
    public class NewViewModel : ViewModelBase
    {
        private Noticia _noticiaActual;
        public Noticia NoticiaActual
        {
            get { return _noticiaActual; }
            set
            {
                _noticiaActual = value;
                RaisePropertyChanged();
            }
        }
        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override async Task OnNavigatedTo(NavigationEventArgs args)
        {
            if (args != null)
            {
                var crawler = new Crawler();
                var noticia = (Noticia)args.Parameter;
                noticia.Contenido = await crawler.LaF1ExtraerContenido(noticia.Enlace);
                NoticiaActual = noticia;
            }
        }
    }
}
