using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw6.Services
{
    public interface IStudentDbService
    {
        IEnumerable<Student> GetStudents();

        Student GetStudent(string index);
    }
}
