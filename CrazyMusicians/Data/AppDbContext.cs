using CrazyMusicians.Models;

namespace CrazyMusicians.Data
{
    public class AppDbContext
    {
        public List<Musician> Musicians { get; set; } = new List<Musician>
    {
        new Musician { Id = 1, Name = "Ahmet Çalgı", Profession = "Ünlü Çalgı Çalar", FunFact = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
        new Musician { Id = 2, Name = "Zeynep Melodi", Profession = "Popüler Melodi Yazarı", FunFact = "Şarkıları yanlış anlaşılıyor ama çok popüler" }
        // Diğer müzisyenleri buraya ekleyin...
    };

    }
}
