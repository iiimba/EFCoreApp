using System.ComponentModel.DataAnnotations;

namespace AdvancedApp.Models
{
    public class Person1
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Person2 Person2 { get; set; }
    }
}
