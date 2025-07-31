namespace MyWatchList.Models
{
    public class Foto
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int ObraId { get; set; }
        public Obra Obra { get; set; }
    }
}
