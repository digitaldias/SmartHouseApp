using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin17.Domain.Entities
{
    public class Goal
    {
        public string Location { get; set; }

        public string Type { get; set; }

        public double Max { get; set; }

        public double Min { get; set; }

        public double TargetStart { get; set; }

        public double TargetEnd { get; set; }

        public bool IsBelowTarget(double temperature)
        {
            return temperature < TargetStart;
        }

        public bool IsBelowMin(double temperature)
        {
            return temperature < Min;
        }

        public bool IsInComfortZone(double temperature)
        {
            return temperature >= TargetStart && temperature <= TargetEnd;
        }

        public bool IsAboveComfortZone(double temperature)
        {
            return temperature > TargetEnd;
        }

        public bool IsAboveMax(double temperature)
        {
            return temperature > Max;
        }

        public bool IsOutsideOperationsRange(double temperature)
        {
            return IsBelowMin(temperature) || IsAboveMax(temperature);
        }
    }
}
