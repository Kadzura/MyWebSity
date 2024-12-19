using MyWebSity.Models;

namespace MyWebSity.Services
{
    public interface ISqlUserService
    {
        ApplicationUser GetUserByEmail(string email);
        ApplicationUser CreateUser(string email, string password);
        bool ValidateUser(string email, string password);
    }   
}
