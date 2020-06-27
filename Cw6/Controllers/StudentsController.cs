using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw6.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();
            list.Add(new Student { IdStudent = 1, FirstName = "Jan", LastName = "Kowalski" });
            list.Add(new Student { IdStudent = 2, FirstName = "Andrzej", LastName = "Malewski" });



            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetStudents(int id)
        {
            return Ok(new Student { IdStudent = 1, FirstName = "Andrzej", LastName = "Malewski" });
        }
    }
}