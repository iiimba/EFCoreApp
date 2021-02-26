using System.ComponentModel.DataAnnotations;

namespace AdvancedApp.Models
{
    public class Person2
    {
        [Key]
        public int Id { get; set; }

        public string OldName { get; set; }

        public int Person1Id { get; set; }

        public Person1 Person1 { get; set; }
    }
}
