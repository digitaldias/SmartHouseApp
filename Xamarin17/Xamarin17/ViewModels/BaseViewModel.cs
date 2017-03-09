using Xamarin17.Data.AzureStorage;
using Xamarin17.Domain.Contracts.Readers;
using Xamarin17.Helpers;

namespace Xamarin17.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        public IDeviceReadingsReader DeviceReadingsReader = new DeviceReadingsReader();

        bool isBusy = false;
        string title = string.Empty;


        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

