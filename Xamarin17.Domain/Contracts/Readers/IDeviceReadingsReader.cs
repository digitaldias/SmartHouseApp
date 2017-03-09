using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin17.Domain.Entities;

namespace Xamarin17.Domain.Contracts.Readers
{
    public interface IDeviceReadingsReader 
    {
        Task<DeviceReading> GetLatestAsync(string locationName);


        Task<IEnumerable<Location>> GetLocationsAsync();


        Task<Goal> GetGoalAsync(string locationName);
    }
}
