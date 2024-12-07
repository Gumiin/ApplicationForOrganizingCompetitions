
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Image { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Country { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? TelephoneNumber { get; set; } = string.Empty;
    }
}
