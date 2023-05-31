using System.ComponentModel.DataAnnotations;

namespace AtonWebApi.DTO
{
    public class DeleteUserDTO
    {
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public bool IsCompletely { get; set; }
    }
}
