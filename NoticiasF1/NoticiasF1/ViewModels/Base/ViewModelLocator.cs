using Microsoft.Practices.Unity;

namespace NoticiasF1.ViewModels.Base
{
    public class ViewModelLocator
    {
        private readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();
            _container.RegisterType<NewViewModel>();

        }

        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
        public NewViewModel NewViewModel => _container.Resolve<NewViewModel>();
    }
}