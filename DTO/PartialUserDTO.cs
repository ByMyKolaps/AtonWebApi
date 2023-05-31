using AtonWebApi.Models;

namespace AtonWebApi.DTO
{
    public class PartialUserDTO
    {
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; }
        public PartialUserDTO(User user)
        {
            Name = user.Name;
            Gender = user.Gender;
            Birthday = user.Birthday.GetValueOrDefault();
            IsActive = user.RevokedOn == null ? true : false;
        }
    }
}
