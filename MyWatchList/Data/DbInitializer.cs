using MyWatchList.Models;

namespace MyWatchList.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Obras.Any()) return;

            var obras = new Obra[]
            {
                
            };

            context.Obras.AddRange(obras);
            context.SaveChanges();
        }
    }
}
