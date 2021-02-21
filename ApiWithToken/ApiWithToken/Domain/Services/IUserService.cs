using ApiWithToken.Domain.Entities;
using ApiWithToken.Domain.Response;

namespace ApiWithToken.Domain.Services
{
    public interface IUserService
    {
        UserResponse Add(User user );
        UserResponse FindById(int userId);
        UserResponse FindEmailAndPassword(string email, string password);
        void SaveRefreshToken(int userId, string refreshToken);
        UserResponse GetUserRefreshToken(string refreshToken);
        void RemoveRefreshToken(User user);
    }
}