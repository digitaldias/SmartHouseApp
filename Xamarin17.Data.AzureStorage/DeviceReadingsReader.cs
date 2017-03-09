using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using System.Threading.Tasks;
using Xamarin17.Domain.Contracts.Readers;
using Xamarin17.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Xamarin17.Data.AzureStorage
{
    public class DeviceReadingsReader : StorageTableBase, IDeviceReadingsReader
    {
        
        private readonly CloudTable _deviceReadingsTable;
        private readonly CloudTable _locationsTable;
        private readonly CloudTable _goalsTable;


    public DeviceReadingsReader()
    {
        _deviceReadingsTable = TableClient.GetTableReference("DeviceReadings");
        _locationsTable = TableClient.GetTableReference("Locations");
        _goalsTable = TableClient.GetTableReference("TemperatureGoals");
    }


        public async Task<Goal> GetGoalAsync(string locationName)
        {
            var query = new TableQuery().Where($"PartitionKey eq '{locationName}'").Take(1);
            var continuationToken = new TableContinuationToken();

            var queryResult = await _goalsTable.ExecuteQuerySegmentedAsync(query, continuationToken);

            if(queryResult.Results.Any())
            {
                var dynamicEntity = queryResult.Results.First();

                return new Goal {
                    Location = dynamicEntity.PartitionKey,
                    Type = dynamicEntity.RowKey,
                    Min = dynamicEntity.Properties["Min"].DoubleValue.Value,
                    Max = dynamicEntity.Properties["Max"].DoubleValue.Value,
                    TargetStart = dynamicEntity.Properties["TargetStart"].DoubleValue.Value,
                    TargetEnd = dynamicEntity.Properties["TargetEnd"].DoubleValue.Value
                };
            }
            return null;
        }


        public async Task<DeviceReading> GetLatestAsync(string locationName)
        {
            var query = new TableQuery().Where($"PartitionKey eq '{locationName}'").Take(1);
            var continuationToken = new TableContinuationToken();

            var queryResult = await _deviceReadingsTable.ExecuteQuerySegmentedAsync(query, continuationToken);

            if (queryResult.Results.Any())
            {
                var dynamicEntity = queryResult.Results.First();

                return new DeviceReading {
                    RoomName = dynamicEntity.PartitionKey, 
                    Humidity = dynamicEntity.Properties["Humidity"].DoubleValue.Value,
                    Temperature = dynamicEntity.Properties["Temperature"].DoubleValue.Value,
                    SampleTime = dynamicEntity.Timestamp.LocalDateTime
                };
            }
            return new DeviceReading();
        }


        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            var query = new TableQuery();
            var continuationToken = new TableContinuationToken();

            var queryResult = await _locationsTable.ExecuteQuerySegmentedAsync(query, continuationToken);

            var locations = new List<Location>();
            if(queryResult.Results.Any())
            {
                foreach(var dynamicEntity in queryResult.Results)
                {
                    locations.Add(new Location { Name = dynamicEntity.PartitionKey });
                }
            }
            return locations;
        }
    }
}
