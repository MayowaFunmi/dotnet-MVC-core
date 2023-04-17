using System.ComponentModel.DataAnnotations;

namespace MvcCoreTutorial.Models.Domain
{
    public class Person
    {
        public int Id { get; set; }
        [Required]

        public String? Name { get; set; }
        [Required]
        [EmailAddress]
        public String? Email { get; set; }

    }
}
