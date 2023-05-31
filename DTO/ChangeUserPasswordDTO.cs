using System.ComponentModel.DataAnnotations;

namespace AtonWebApi.DTO
{
    public class ChangeUserPasswordDTO
    {
        [Required]
        public string UserLogin { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [Required]
        public string OldPassword { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [Required]
        public string NewPassword { get; set; }
    }
}
