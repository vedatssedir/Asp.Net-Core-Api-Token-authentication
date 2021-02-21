using System;
using System.Linq;
using System.Threading.Tasks;
using ApiWithToken.Domain.Contexts;
using ApiWithToken.Domain.Entities;
using ApiWithToken.Security.Token;
using Microsoft.Extensions.Options;

namespace ApiWithToken.Domain.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly TokenOptions _tokenOptions;


        public UserRepository(DataContext context, IOptions<TokenOptions> tokenOptions) : base(context)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public async Task AddUser(User user)
        {
            await Context.Users.AddAsync(user);
        }

        public User FindById(int userId)
        {
            var user = Context.Users.Find(userId);
            return user;
        }

        public User FindByEmailAndPassword(string email, string password)
        {
            var user = Context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            var data = FindById(userId);
            data.RefreshToken = refreshToken;
            data.RefreshTokenEndDate = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
        }

        public User GetUserRefreshToken(string refreshToken)
        {
            return Context.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
        }

        public void RemoveRefreshToken(User user)
        {
            var data = Context.Users.Find(user.Id);
            data.RefreshToken = null;
            data.RefreshTokenEndDate = null;
        }
    }
}