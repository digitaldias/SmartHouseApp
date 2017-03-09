using System;

namespace Xamarin17.Domain.Entities
{
    public class DeviceReading
    {
        public string RoomName { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public DateTime SampleTime { get; set; }

        public TimeSpan SampleAge
        {
            get
            {
                return DateTime.Now - SampleTime;
            }
        }

        public string SampleAgeText
        {
            get
            {
                if (SampleAge > TimeSpan.FromDays(1))
                    return $"{SampleAge.Days} days, {SampleAge.Hours} hours ago";

                if (SampleAge > TimeSpan.FromHours(1))
                    return $"{SampleAge.Hours} hours and {SampleAge.Minutes} minutes ago";

                return $"{SampleAge.Minutes} Minutes and {SampleAge.Seconds} seconds ago";
            }
        }
    }
}
