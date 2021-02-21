using ApiWithToken.Domain.Contexts;

namespace ApiWithToken.Domain.Repository
{
    public abstract class BaseRepository
    {
        protected readonly DataContext Context;
        protected BaseRepository(DataContext context)
        {
            this.Context = context;
        }
    }
}