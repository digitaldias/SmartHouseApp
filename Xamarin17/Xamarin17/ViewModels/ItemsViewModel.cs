using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin17.Domain.Entities;
using Xamarin17.Helpers;

namespace Xamarin17.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private IEnumerable<Location> _locations;
        private List<Goal> _goals;

        public ObservableRangeCollection<ItemDetailViewModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<ItemDetailViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Items.Clear();
                await EnsureBasedataLoaded();
                await PopulateItemsListAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task PopulateItemsListAsync()
        {
            var viewModels = new List<ItemDetailViewModel>();

            foreach (var location in _locations)
            {
                var locationReading = await DeviceReadingsReader.GetLatestAsync(location.Name);

                if (locationReading != null)
                    viewModels.Add(new ItemDetailViewModel(locationReading, _goals.Find(g => g.Location == locationReading.RoomName)));
            }            
            Items.AddRange(viewModels.OrderBy(vm => vm.Priority));
        }


        private async Task EnsureBasedataLoaded()
        {
            if (_locations == null)
            {
                _locations = await DeviceReadingsReader.GetLocationsAsync();

                _goals = new List<Goal>();
                foreach (var location in _locations)
                {
                    var goal = await DeviceReadingsReader.GetGoalAsync(location.Name);
                    if (goal != null)
                        _goals.Add(goal);
                }
            }
        }
    }
}