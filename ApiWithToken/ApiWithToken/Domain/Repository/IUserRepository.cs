using System.Threading.Tasks;
using ApiWithToken.Domain.Entities;

namespace ApiWithToken.Domain.Repository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        User FindById(int userId);
        User FindByEmailAndPassword(string email,string password);
        void SaveRefreshToken(int userId, string refreshToken);
        User GetUserRefreshToken(string refreshToken);
        void RemoveRefreshToken(User user);
    }
}