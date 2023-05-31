using System.ComponentModel.DataAnnotations;

namespace AtonWebApi.DTO
{
    public class UserUpdateDTO
    {
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$")]
        public string? Name { get; set; }
        [Range(0, 2)]
        public int? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? UserLoginToUpdate { get; set; }
    }
}
