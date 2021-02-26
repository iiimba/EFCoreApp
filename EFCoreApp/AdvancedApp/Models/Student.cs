using System.Collections.Generic;

namespace AdvancedApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Course { get; set; }

        public IEnumerable<TeacherStudentJunction> Junctions { get; set; }
    }
}
