using System.Threading.Tasks;

namespace Xamarin17.Domain.Contracts.Readers
{
    public interface IDataReader<TEntity>
    {
        Task<TEntity> GetLatestAsync();
    }
}
