using F1WebCrawler.Model;
using System.Collections.ObjectModel;

namespace NoticiasF1.FakeData
{
    public class FakeItems
    {
        public ObservableCollection<Noticia> Noticias => new ObservableCollection<Noticia>()
        {
            new Noticia()
            {
                Titulo = "Bernie: \"Las mujeres son más competentes que los hombres\"",
                Imagen = "http://www.laf1.es/sites/default/files/styles/big/public/imagenes/panels/ecclestone-bernie-mujer-f1-laf1_1_0.jpg"
            },
            new Noticia()
            {
                Titulo = "Ecclestone: \"El problema es que escuchamos a los equipos\"",
                Imagen = "http://www.laf1.es/sites/default/files/styles/thumbnail/public/imagenes/noticia/fia-ecclestone-equipos-soymotor.jpg"
            },
            new Noticia()
            {
                Titulo = "Kehm: \"No haremos comentarios sobre la salud de Schumacher\"",
                Imagen = "http://www.laf1.es/sites/default/files/styles/big/public/imagenes/panels/schumacher-kehm-salud-soymotor_1.jpg"
            },
            new Noticia()
            {
                Titulo = "Ecclestone: \"El problema es que escuchamos a los equipos\"",
                Imagen = "http://www.laf1.es/sites/default/files/styles/large/public/imagenes/noticia/bernie-ecclestone-2017-soymotor.jpg"
            }
        };

        public Noticia NoticiaActual => new Noticia()
        {
            Titulo = "Bernie: \"Las mujeres son más competentes que los hombres\"",
            Imagen = "http://www.laf1.es/sites/default/files/styles/big/public/imagenes/panels/ecclestone-bernie-mujer-f1-laf1_1_0.jpg",
            Contenido = "Haciendo pruebas de contenido para la visualizacion de una noticia."
        };
    }
}