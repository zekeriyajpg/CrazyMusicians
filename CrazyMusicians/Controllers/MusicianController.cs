using CrazyMusicians.Data;
using CrazyMusicians.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CrazyMusicians.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicianController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Dependency Injection ile AppDbContext'i alıyoruz
        public MusicianController(AppDbContext context)
        {
            _context = context;
        }

        // Get All Musicians
        [HttpGet]
        public IActionResult GetAll()
        {
            var musicians = _context.Musicians.ToList();
            return Ok(musicians);
        }

        // Get Musician By ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var musician = _context.Musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound(new { Message = "Musician not found." });
            return Ok(musician);
        }

        // Search Musician By Name
        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            var result = _context.Musicians
                                 .Where(m => m.Name.Contains(name))
                                 .ToList();
            if (!result.Any()) return NotFound(new { Message = "No musicians match the search criteria." });
            return Ok(result);
        }

        // Add New Musician
        [HttpPost]
        public IActionResult Create([FromBody] Musician musician)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Yeni Id oluşturuyoruz
            musician.Id = _context.Musicians.Any() ? _context.Musicians.Max(m => m.Id) + 1 : 1;
            _context.Musicians.Add(musician);
            _context.SaveChanges(); // Veritabanına değişiklikleri kaydet

            return CreatedAtAction(nameof(GetById), new { id = musician.Id }, musician);
        }

        // Update Entire Musician (Put)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Musician updatedMusician)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var musician = _context.Musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound(new { Message = "Musician not found." });

            // Tüm alanları güncelliyoruz
            musician.Name = updatedMusician.Name;
            musician.Profession = updatedMusician.Profession;
            musician.FunFact = updatedMusician.FunFact;

            _context.SaveChanges(); // Veritabanına değişiklikleri kaydet
            return NoContent();
        }

        // Partially Update Musician (Patch)
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Musician> patchDoc)
        {
            if (patchDoc == null) return BadRequest(new { Message = "Patch document is required." });

            var musician = _context.Musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound(new { Message = "Musician not found." });

            // Patch işlemini uyguluyoruz
            patchDoc.ApplyTo(musician, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.SaveChanges(); // Veritabanına değişiklikleri kaydet
            return NoContent();
        }

        // Delete Musician
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var musician = _context.Musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound(new { Message = "Musician not found." });

            _context.Musicians.Remove(musician);
            _context.SaveChanges(); // Veritabanına değişiklikleri kaydet
            return NoContent();
        }
    }
}
