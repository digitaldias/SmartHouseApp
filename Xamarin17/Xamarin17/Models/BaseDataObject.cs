using System;
using Xamarin17.Helpers;

namespace Xamarin17.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }


        public string Id { get; set; }


        public DateTimeOffset CreatedAt { get; set; }


        public DateTimeOffset UpdatedAt { get; set; }


        public string AzureVersion { get; set; }
    }
}
