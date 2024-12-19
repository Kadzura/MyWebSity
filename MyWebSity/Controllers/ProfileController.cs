using Microsoft.AspNetCore.Mvc;
using MyWebSity.Models;

namespace MyWebSity.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userProfile = new UserProfile
            {
                FullName = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+1-555-123-4567",
                ProfilePicturePath = "/images/default_profile.png" // Default Image path
            };

            return View("~/Views/Account/Profile.cshtml", userProfile);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserProfile model)
        {
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploads");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }
                model.ProfilePicturePath = $"/images/uploads/{fileName}";

            }


            return View("~/Views/Account/Profile.cshtml", model);
        }
    }
}
