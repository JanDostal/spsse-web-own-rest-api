using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassesStudentsAPI.Models;
using System.Collections.ObjectModel;

namespace ClassesStudentsAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ClassesStudentsAPIContext _context;

        private List<Student> Studenti { get; set; }

        private DateTime Present { get; set; }

        private Student Student { get; set; }


        public StudentsController(ClassesStudentsAPIContext context)
        {
            _context = context;
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> GetStudent(int studentId)
        {
            Student = await _context.Studenti.AsNoTracking().Where(x => x.StudentId == studentId).SingleOrDefaultAsync();

            if (Student == null)
            {
                return NotFound();
            }

            return Ok(Student);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> GetStudents(int? age = null, Pohlavi? gender = null)
        {
            List<Student> globalStudents;

            if (age == null && gender == null)
            {
                Studenti = await _context.Studenti.AsNoTracking().ToListAsync();
                globalStudents = Studenti;
            }
            else if (age == null && gender != null)
            {
                Studenti = await _context.Studenti.AsNoTracking().Where(x => x.Pohlavi == gender).ToListAsync();

                globalStudents = Studenti;

            }
            else if (age != null && gender == null)
            {
                Present = DateTime.Now;

                var students = await _context.Studenti.AsNoTracking().ToArrayAsync();
                var filteredStudents = new Collection<Student>();

                foreach (var item in students.AsEnumerable())
                {
                    int studentAge = Present.Year - item.DatumNarozeni.Year;

                    if (item.DatumNarozeni> Present.AddYears(-studentAge))
                    {   
                        studentAge--;
                    }

                    if (age == studentAge)
                    {
                        filteredStudents.Add(item);
                    }


                }

                globalStudents = filteredStudents.ToList();

            }
            else
            {
                Present = DateTime.Now;

                var students = await _context.Studenti.AsNoTracking().ToArrayAsync();
                var filteredStudents = new Collection<Student>();

                foreach (var item in students.AsEnumerable())
                {
                    int studentAge = Present.Year - item.DatumNarozeni.Year;

                    if (item.DatumNarozeni > Present.AddYears(-studentAge))
                    {
                        studentAge--;
                    }

                    if (age == studentAge && item.Pohlavi == gender)
                    {
                        filteredStudents.Add(item);
                    }
                }

                globalStudents = filteredStudents.ToList();
            }

            if (globalStudents.Count == 0)
            {
                return NoContent();
            }

            return Ok(globalStudents);

        }

       
        [HttpPut("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStudent(int studentId, Student student)
        {
            bool exists = _context.Studenti.AsNoTracking().Any(e => e.StudentId == studentId);
            if (exists == false)
            {
                return NotFound();
            }

            try
            {

                student.StudentId = studentId;

                if ((int)student.Pohlavi != 1 && (int)student.Pohlavi != -1)
                {
                    throw new Exception();
                }

                _context.Entry(student).State = EntityState.Modified;


                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {

            try
            {
                _context.Studenti.Add(student);

                if (student?.StudentId != 0)
                {
                    throw new Exception();
                }

                if ((int)student.Pohlavi != 1 && (int)student.Pohlavi != -1)
                {
                    throw new Exception();
                }
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest();
            }


            return CreatedAtAction("GetStudent", new { studentId = student.StudentId }, student);
        }

        [HttpDelete("{studentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            Student = await _context.Studenti.FindAsync(studentId);
            if (Student == null)
            {
                return NotFound();
            }

            _context.Studenti.Remove(Student);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
