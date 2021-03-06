﻿using F1WebCrawler;
using F1WebCrawler.Model;
using Microsoft.EntityFrameworkCore;
using NoticiasF1.Model;
using NoticiasF1.ViewModels.Base;
using NoticiasF1.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
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
            await MigrarBaseDeDatos();
            BarraDeProgesoVisibilidad = true;
            await CargarNoticias();
            BarraDeProgesoVisibilidad = false;
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

            using (var db = new Contexto())
            {
                Noticias = new ObservableCollection<Noticia>(await
                    db.Noticias.Where(x => x.Fecha >= DateTime.Now.AddDays(-1))
                    .OrderByDescending(x => x.Fecha).ToListAsync());

                if (!Noticias.Any())
                {
                    await ExtraerNoticiasLaF1Es();
                }
            }

        }

        private async Task ExtraerNoticiasLaF1Es()
        {
            var crawler = new Crawler();
            var enlaceNoticias = await crawler.Noticias();

            try
            {
                using (var db = new Contexto())
                {
                    foreach (var enlaceNoticia in enlaceNoticias)
                    {
                        var existe = await db.Noticias.AnyAsync(x => x.Enlace == enlaceNoticia.Enlace);
                        if (!existe)
                        {
                            var noticia = await crawler.LaF1ExtraerNoticia(enlaceNoticia.Enlace);
                            db.Noticias.Add(noticia);
                            await db.SaveChangesAsync();
                        }
                    }
                    Noticias = new ObservableCollection<Noticia>(await db.Noticias
                        .Where(x => x.Fecha >= DateTime.Now.AddDays(-1))
                        .OrderByDescending(x => x.Fecha).ToListAsync());
                }
            }
            catch (Exception exception)
            {
                var dialog = new MessageDialog($"Error al recolectar noticias de LaF1.es: {exception.Message}");
                await dialog.ShowAsync();
                throw;
            }
        }

        private async Task MigrarBaseDeDatos()
        {
            using (var db = new Contexto())
            {
                await db.Database.MigrateAsync();
            }
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
            await ExtraerNoticiasLaF1Es();
            BarraDeProgesoVisibilidad = false;
        }

        #endregion
    }
}
