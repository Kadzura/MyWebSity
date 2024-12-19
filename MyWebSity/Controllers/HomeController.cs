using Microsoft.AspNetCore.Mvc;
using MyWebSity.Models;
using MyWebSity.Services;

namespace MyWebSity.Controllers
{
    public class HomeController : Controller
    {
        private readonly GeoIpService _geoIpService;

        public HomeController(GeoIpService geoIpService)
        {
            _geoIpService = geoIpService;
        }

        private async Task SetCityInfo()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress;
            var cityInfo = await _geoIpService.GetCityInfo(ipAddress);

            if (cityInfo == null)
            {
                ViewBag.CityInfo = new CityInfo()
                {
                    CityName = "Не определено",
                    Temperature = null,
                    WeatherDescription = null,
                    WindSpeed = null
                };
            }
            else
            {
                ViewBag.CityInfo = cityInfo;
            }
        }

        public IActionResult Index()
        {
            var resume = new ResumeData
            {
                // ... (other properties)
                Skills = new List<string> { "C#", ".NET Core", "ASP.NET", "SQL", "HTML", "CSS", "JavaScript" },
                Experience = new List<Experience>
                {
                    new Experience
                    {
                        Company = "Company Name 1",
                        Position = "Position 1",
                        StartDate = "01/2023",
                        EndDate = "06/2023",
                        Description = "Description of responsibilities in the first company."
                    },
                     // Add more Experience items here...
                    new Experience { Company = "Company Name 2", Position = "Position 2", StartDate = "07/2022", EndDate = "12/2022", Description = "Description of responsibilities in the second company." }

                },
                Education = new List<Education>
                {
                    new Education
                    {
                        Institution = "University Name",
                        Degree = "Degree 1",
                        Year = "2018-2022"
                    },
                      // Add more Education items here...
                    new Education { Institution = "High School Name", Degree = "High School Diploma", Year = "2016-2018" }
                },
                // ... (other properties)

            };
            return View(resume);
        }
    }
}
