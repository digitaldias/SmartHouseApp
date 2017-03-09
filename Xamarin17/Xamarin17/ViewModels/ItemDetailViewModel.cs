using System;
using Xamarin.Forms;
using Xamarin17.Domain.Entities;

namespace Xamarin17.ViewModels
{    
    public class ItemDetailViewModel : BaseViewModel
    {
        public DeviceReading Item { get; set; }
        double _temperature = 25;
        double _humidity = 20;
        DateTime _sampleTime = DateTime.Now.AddDays(-1);
        private readonly Goal _goal;

        public ItemDetailViewModel(DeviceReading item = null, Goal goal = null)
        {
            Title = item.RoomName ?? "Unknown Room";
            Item = item;
            _temperature = item.Temperature;
            _humidity = item.Humidity;
            _goal = goal;
        }


        public int Priority
        {
            get
            {
                if (_goal.IsInComfortZone(_temperature))
                    return 4;
                else if (_goal.IsOutsideOperationsRange(_temperature) || Item.SampleAge >= TimeSpan.FromMinutes(15))
                    return 1;
                else if (_goal.IsAboveComfortZone(_temperature))
                    return 2;

                return 3;
            }
        }


        public string Status
        {
            get
            {
                if (_goal.IsInComfortZone(_temperature))
                    return "All is good";

                else if (_goal.IsOutsideOperationsRange(_temperature))
                    return "Temperature outside comfort zone";

                else if (Item.SampleAge >= TimeSpan.FromMinutes(15))
                    return "Sample is too old. Check sensor";

                else if (_goal.IsAboveComfortZone(_temperature))
                    return "It's too hot";

                return "It's too cold";
            }
        }


        public ImageSource Icon
        {
            get
            {
                var icon = new Image { Aspect = Aspect.AspectFit };

                if (_goal.IsInComfortZone(_temperature))
                    icon.Source = ImageSource.FromFile("ok.png");
                else if (_goal.IsOutsideOperationsRange(_temperature) || Item.SampleAge >= TimeSpan.FromMinutes(15))
                    icon.Source = ImageSource.FromFile("Alert.png");
                else if (_goal.IsAboveComfortZone(_temperature))
                    icon.Source = ImageSource.FromFile("burning_hot.png");
                else
                    icon.Source = ImageSource.FromFile("Snowflake.png");

                return icon.Source;
            }
        }


        public Color BackgroundColor
        {
            get
            {
                if (_goal.IsInComfortZone(_temperature))
                    return Color.FromRgb(153, 255, 51);

                if (_goal.IsOutsideOperationsRange(_temperature) || Item.SampleAge >= TimeSpan.FromMinutes(15))
                    return Color.FromRgb(255, 255, 204);

                if (_goal.IsAboveComfortZone(_temperature))
                    return Color.FromRgb(255, 102, 102);

                return Color.FromRgb(102, 255, 255); // light blue
            }
        }


        public double Temperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature, value); }
        }


        public double Humidity
        {
            get { return _humidity; }
            set { SetProperty(ref _humidity, value); }
        }


        public DateTime SampleTime
        {
            get { return Item.SampleTime; }
            set
            {
                Item.SampleTime = value;
                OnPropertyChanged();
                OnPropertyChanged("SampleAge");
            }
        }


        public string SampleAge
        {
            get
            {
                if (Item.SampleAge > TimeSpan.FromDays(1))
                    return $"{Item.SampleAge.Days} days, {Item.SampleAge.Hours} hours ago";

                if(Item.SampleAge > TimeSpan.FromHours(1))
                    return $"{Item.SampleAge.Hours} hours and {Item.SampleAge.Minutes} minutes ago";

                return $"{Item.SampleAge.Minutes} Minutes and {Item.SampleAge.Seconds} seconds ago";
            }
        }


        public Goal Goal { get { return _goal; } }
    }
}