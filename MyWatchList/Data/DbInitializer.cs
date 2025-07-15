using MyWatchList.Models;

namespace MyWatchList.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Obras.Any()) return; // DB já populado

            var obras = new Obra[]
            {
                new Obra {
                    Titulo = "Naruto",
                    Sinopse = "Um jovem ninja sonha em se tornar Hokage.",
                    NotaMedia = 8.7f,
                    Genero = "Ação/Aventura",
                    PopularidadeDia = 1200,
                    PopularidadeSemana = 5600,
                    PopularidadeMes = 18000,
                    PopularidadeAno = 120000
                },
                new Obra {
                    Titulo = "Interestelar",
                    Sinopse = "Uma equipe de exploradores viaja por um buraco de minhoca no espaço.",
                    NotaMedia = 9.2f,
                    Genero = "Ficção Científica",
                    PopularidadeDia = 800,
                    PopularidadeSemana = 3000,
                    PopularidadeMes = 10000,
                    PopularidadeAno = 50000
                }
            };

            context.Obras.AddRange(obras);
            context.SaveChanges();
        }
    }
}