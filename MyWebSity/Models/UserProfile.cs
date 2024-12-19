namespace MyWebSity.Models
{
    public class UserProfile
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicturePath { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
