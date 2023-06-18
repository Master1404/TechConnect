using System.ComponentModel.DataAnnotations;

namespace TechConnect.Models
{
    public class User : IRecord<int>
    {
        // [Key]
         public int Id { get; set; }
       
        [Required(ErrorMessage = "Введи имя")]
        [MaxLength(10, ErrorMessage = "длина не больше 0")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "от 1 до 100")]
        public int Age { get; set; }
    }
}
