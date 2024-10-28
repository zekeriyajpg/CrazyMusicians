using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Models
{
    public class Musician
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Profession { get; set; }

        [StringLength(200)]
        public string FunFact { get; set; }
    }
}
