using System.ComponentModel.DataAnnotations;

namespace AtonWebApi.DTO
{
    public class UserCreateDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Login { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$")]
        public string Name { get; set; }
        [Required]
        [Range(0, 2)]
        public int Gender { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public DateTime? Birhday { get; set; }

    }
}
