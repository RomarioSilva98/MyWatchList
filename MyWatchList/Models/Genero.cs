namespace MyWatchList.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int ObraId { get; set; }
        public Obra Obra { get; set; }
    }
}
