using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechConnect.Models.SpecialEquipment
{
    public class SpecialVehicleViewModel
    {
        [Required(ErrorMessage = "Введите заголовок")]
        [MaxLength(70, ErrorMessage = "Длина должна быть не более 70 символов")]
        public string Title { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Цена должна быть неотрицательным числом")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public AdsCategory Category { get; set; }

        [MaxLength(500, ErrorMessage = "Длина описания должна быть не более 500 символов")]
        public string Description { get; set; }
      //  public List<byte[]> Photos { get; set; }
       // public bool HasPhotos { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^\+\d{2} \d{3} \d{3} \d{2} \d{2}$", ErrorMessage = "Неверный формат номера телефона")]
        public string PhoneNumber { get; set; }
    }
}
