using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw6.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        public Student GetStudent(string index)
        {
            if (index == "s1234")
            {
                return new Student {IdStudent = 1, FirstName = "Jan", LastName = "Kowalski" };
            }
            return null;
        }

        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }
    }
}
