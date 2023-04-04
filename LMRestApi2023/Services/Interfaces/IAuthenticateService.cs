using LMRestApi2023.Models;

namespace LMRestApi2023.Services.Interfaces
{
    public interface IAuthenticateService
    {
        LoggedUser Authenticate(string username, string password);
    }
}
