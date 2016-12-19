using F1WebCrawler;
using F1WebCrawler.Model;
using NoticiasF1.Model;
using NoticiasF1.ViewModels.Base;
using NoticiasF1.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NoticiasF1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Propiedades enlazadas

        private string _helloWorld;
        public string HelloWorld
        {
            get { return _helloWorld; }
            set { _helloWorld = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Noticia> _noticias;
        public ObservableCollection<Noticia> Noticias
        {
            get { return _noticias; }
            set
            {
                _noticias = value;
                RaisePropertyChanged();
            }
        }

        private bool _barraDeProgesoVisibilidad;

        public bool BarraDeProgesoVisibilidad
        {
            get { return _barraDeProgesoVisibilidad; }
            set
            {
                _barraDeProgesoVisibilidad = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Eventos

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override async Task OnNavigatedTo(NavigationEventArgs args)
        {
            if (!NoticiasDisponibles.NoticiasCargadas)
            {
                BarraDeProgesoVisibilidad = true;
                await CargarNoticias();
                BarraDeProgesoVisibilidad = false;
            }
            else
            {
                CargaEscalonada();
            }
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null)
            {
                var noticia = (Noticia)e.AddedItems.FirstOrDefault();
                AppFrame.Navigate(typeof(NewView), noticia);
            }
        }

        #endregion

        #region Metodos

        private async Task CargarNoticias()
        {
            var crawler = new Crawler();
            NoticiasDisponibles.Noticias = await crawler.Noticias();
            CargaEscalonada();
            NoticiasDisponibles.NoticiasCargadas = true;
        }

        private void CargaEscalonada()
        {
            Noticias = new ObservableCollection<Noticia>(NoticiasDisponibles.Noticias);
        }

        #endregion

        #region Comandos

        private ICommand refrescarListadoCommand;

        public ICommand RefrescarListadoCommand
        {
            get
            {
                return refrescarListadoCommand = refrescarListadoCommand ?? new DelegateCommand(RefrescarListadoExecute);
            }
        }

        private async void RefrescarListadoExecute()
        {
            BarraDeProgesoVisibilidad = true;
            await CargarNoticias();
            BarraDeProgesoVisibilidad = false;
        }

        #endregion
    }
}
