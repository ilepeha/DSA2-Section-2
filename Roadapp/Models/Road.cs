using System;

public class Road
{
    public City From { get; }
    public City To { get; }
    public double Distance { get; } // in kilometers
    public int MaxSpeed { get; } // in km/h

    public Road(City from, City to, double distance, int maxSpeed)
    {
        From = from ?? throw new ArgumentNullException(nameof(from));
        To = to ?? throw new ArgumentNullException(nameof(to));
        Distance = distance > 0 ? distance : throw new ArgumentOutOfRangeException(nameof(distance));
        MaxSpeed = maxSpeed > 0 ? maxSpeed : throw new ArgumentOutOfRangeException(nameof(maxSpeed));
    }

    public double TravelTime => Distance / MaxSpeed; // in hours
}