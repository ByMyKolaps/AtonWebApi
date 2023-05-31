using System.ComponentModel.DataAnnotations;

namespace AtonWebApi.DTO
{
    public class ChangeUserLoginDTO
    {
        [Required]
        public string OldLogin { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [Required]
        public string NewLogin { get; set; }
    }
}
