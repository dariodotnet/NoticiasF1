using NoticiasF1.ViewModels;
using NoticiasF1.Views.Base;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace NoticiasF1.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainView : PageBase
    {
        public MainViewModel ViewModel => DataContext as MainViewModel;

        public MainView()
        {
            this.InitializeComponent();
            this.Loaded += (o, s) => Listado.SelectionChanged += ViewModel.SelectionChanged;
        }
    }
}
