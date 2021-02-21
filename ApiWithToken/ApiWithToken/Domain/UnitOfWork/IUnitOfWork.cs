using System.Threading.Tasks;

namespace ApiWithToken.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Complate();
    }
}