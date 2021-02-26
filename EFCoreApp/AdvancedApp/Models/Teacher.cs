using System.Collections.Generic;

namespace AdvancedApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public IEnumerable<TeacherStudentJunction> Junctions { get; set; }
    }
}
