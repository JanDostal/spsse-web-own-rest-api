using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassesStudentsAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace ClassesStudentsAPI.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ClassesStudentsAPIContext _context;

        private Trida Trida { get; set; }

        private List<Student> Studenti { get; set; }

        private List<Trida> Tridy { get; set; }

        private DateTime Present { get; set; }

        private bool Exists { get; set; }

        public ClassesController(ClassesStudentsAPIContext context)
        {
            _context = context;
        }


        [HttpGet("{classId}/students/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Trida>>> GetNumberOfStudents(int classId)
        {

            Exists = _context.Tridy.AsNoTracking().Any(e => e.TridaId == classId);
            if (Exists == false)
            {
                return NotFound();
            }

            Studenti = await _context.Studenti.AsNoTracking().Where(x => x.TridaId == classId).ToListAsync();

            return Ok(Studenti.Count);

        }

        [HttpGet("ended")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Trida>>> GetClasses([FromQuery][Required] UrovenVzdelani educationLevel)
        {
            UrovenVzdelani? level = educationLevel;

            Present = DateTime.Now;

            Tridy = await _context.Tridy.AsNoTracking().Where(x => x.UrovenVzdelani == educationLevel && (Present >= x.DatumUkonceni) == true).ToListAsync();

            if (Tridy.Count == 0)
            {
                return NoContent();
            }

            return Ok(Tridy);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Trida>>> GetClasses(int? grade = null, UrovenVzdelani? educationLevel = null, string codeDesignation = null)
        {
            Present = DateTime.Now;

            if (grade == null && educationLevel == null && codeDesignation == null)
            {
                Tridy = await _context.Tridy.AsNoTracking().ToListAsync();
            }
            else if (grade == null && educationLevel == null && codeDesignation != null)
            {
                Tridy = await _context.Tridy.AsNoTracking().Where(x => x.KodoveOznaceni == codeDesignation).ToListAsync();
            }
            else if (grade == null && educationLevel != null && codeDesignation == null)
            {
                Tridy = await _context.Tridy.AsNoTracking().Where(x => x.UrovenVzdelani == educationLevel).ToListAsync();
            }
            else if (grade == null && educationLevel != null && codeDesignation != null)
            {
                Tridy = await _context.Tridy.AsNoTracking().Where(x => x.KodoveOznaceni == codeDesignation && x.UrovenVzdelani == educationLevel).ToListAsync();
            }
            else if (grade != null && educationLevel == null && codeDesignation == null)
            {
                Tridy = await _context.Tridy.AsNoTracking().Where(x => (Present >= x.DatumVzniku && Present < x.DatumUkonceni) && ((Present.Year - x.DatumVzniku.Year) == grade)).ToListAsync();
            }
            else if (grade != null && educationLevel == null && codeDesignation != null)
            {
                Tridy = await _context.Tridy.AsNoTracking().Where(x => (Present >= x.DatumVzniku && Present < x.DatumUkonceni) && ((Present.Year - x.DatumVzniku.Year) == grade) &&
                x.KodoveOznaceni == codeDesignation).ToListAsync();
            }
            else if (grade != null && educationLevel != null && codeDesignation == null)
            {
                Tridy = await _context.Tridy.AsNoTracking().Where(x => (Present >= x.DatumVzniku && Present < x.DatumUkonceni) && ((Present.Year - x.DatumVzniku.Year) == grade) &&
                x.UrovenVzdelani == educationLevel).ToListAsync();
            }
            else
            {
                Tridy = await _context.Tridy.AsNoTracking().Where(x => (Present >= x.DatumVzniku && Present < x.DatumUkonceni) && ((Present.Year - x.DatumVzniku.Year) == grade) &&
                x.UrovenVzdelani == educationLevel && x.KodoveOznaceni == codeDesignation).ToListAsync();
            }

            if (Tridy.Count == 0)
            {
                return NoContent();
            }

            return Ok(Tridy);

        }

        [HttpGet("{classId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Trida>> GetClass(int classId)
        {
            Trida = await _context.Tridy.AsNoTracking().Where(x => x.TridaId == classId).SingleOrDefaultAsync();

            if (Trida == null)
            {
                return NotFound();
            }

            return Ok(Trida);
        }

        [HttpPut("{classId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutClass(int classId, Trida trida)
        {
            Exists = _context.Tridy.AsNoTracking().Any(e => e.TridaId == classId);
            if (Exists == false)
            {
                return NotFound();
            }

            try
            {
             
                trida.TridaId = classId;

                if (trida.DatumUkonceni == default && trida.DatumVzniku == default && trida.KodoveOznaceni == default && trida.UrovenVzdelani == default)
                {
                    return NoContent();
                }   

                if ((int)trida.UrovenVzdelani > 3 || (int)trida.UrovenVzdelani < 1)
                {
                    throw new Exception();
                }

                _context.Entry(trida).State = EntityState.Modified;

                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Trida>> PostClass(Trida trida)
        {
            try
            {
                _context.Tridy.Add(trida);

                if (trida?.TridaId != 0)
                {
                    throw new Exception();
                }


                if (trida.DatumUkonceni == default && trida.DatumVzniku == default && trida.KodoveOznaceni == default && trida.UrovenVzdelani == default)
                {
                    throw new Exception();
                }

                if ((int)trida.UrovenVzdelani > 3 || (int)trida.UrovenVzdelani < 1)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();


            return CreatedAtAction("GetClass", new { classId = trida.TridaId }, trida);
        }

        [HttpDelete("{classId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTrida(int classId)
        {
            Trida = await _context.Tridy.FindAsync(classId);
            if (Trida == null)
            {
                return NotFound();
            }

            _context.Tridy.Remove(Trida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{classId}/students/adult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Student>>> GetAdultStudents(int classId)
        {

            Exists = _context.Tridy.AsNoTracking().Any(e => e.TridaId == classId);
            if (Exists == false)
            {
                return NotFound();
            }

            Present = DateTime.Now;

            var students = await _context.Studenti.AsNoTracking().ToArrayAsync();
            var adultStudents = new Collection<Student>();


            foreach (var item in students.AsEnumerable())
            {
                int age = Present.Year - item.DatumNarozeni.Year;
                if (item.DatumNarozeni > Present.AddYears(-age))
                {
                    age--;
                }

                if (item.TridaId == classId && age >= 18)
                {
                    adultStudents.Add(item);
                }


            }

            if (adultStudents.ToList().Count == 0)
            {
                return NoContent();
            }

            return Ok(adultStudents.ToList());

        }

        [HttpGet("{classId}/students")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(int classId)
        {
            Exists = _context.Tridy.AsNoTracking().Any(e => e.TridaId == classId);
            if (Exists == false)
            {
                return NotFound();
            }

            Studenti = await _context.Studenti.AsNoTracking().Where(x => x.TridaId == classId).ToListAsync();

            if (Studenti.Count == 0)
            {
                return NoContent();

            }
            return Ok(Studenti);
        }

    }
}
