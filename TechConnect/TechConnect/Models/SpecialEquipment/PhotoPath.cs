using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechConnect.Models.SpecialEquipment
{
    public class PhotoPath
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }

    }
}
