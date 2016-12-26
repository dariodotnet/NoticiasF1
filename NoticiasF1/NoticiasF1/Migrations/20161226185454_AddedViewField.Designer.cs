using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NoticiasF1.Model;

namespace NoticiasF1.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20161226185454_AddedViewField")]
    partial class AddedViewField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("F1WebCrawler.Model.Noticia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contenido");

                    b.Property<string>("Enlace");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Imagen");

                    b.Property<string>("Titulo");

                    b.Property<bool?>("Visto");

                    b.HasKey("Id");

                    b.ToTable("Noticias");
                });
        }
    }
}
