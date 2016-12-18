using NoticiasF1.ViewModels.Base;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NoticiasF1.Views.Base
{
    public class PageBase : Page
    {

        private ViewModelBase _vm;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _vm = (ViewModelBase)this.DataContext;
            _vm.SetAppFrame(this.Frame);
            _vm.OnNavigatedTo(e);

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += EntryPage_BackRequested;
        }

        private void EntryPage_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if (Frame != null)
            {
                if (Frame.CanGoBack)
                {
                    e.Handled = true;
                    Frame.GoBack();
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _vm.OnNavigatedFrom(e);
        }
    }
}