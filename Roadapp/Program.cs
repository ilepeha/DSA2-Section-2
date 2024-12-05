using System;

class Program
{
    static void Main(string[] args)
    {
        var network = new RoadNetwork();

        // Define cities
        var cities = new[]
        {
            "Varna", "Burgas", "Dobrich", "Silistra", "Razgrad",
            "Tyrgowishte", "Shumen", "Veliko Trynovo", "Sliven",
            "Yambol", "Kazanlyk", "Stara Zagora"
        };

        foreach (var city in cities)
        {
            network.AddCity(city);
        }

        // Define first-class roads
        network.AddRoad("Varna", "Burgas", 130, 120);
        network.AddRoad("Varna", "Shumen", 90, 100);
        network.AddRoad("Shumen", "Razgrad", 40, 90);
        network.AddRoad("Razgrad", "Tyrgowishte", 35, 90);
        network.AddRoad("Tyrgowishte", "Veliko Trynovo", 70, 120);
        network.AddRoad("Veliko Trynovo", "Sliven", 150, 120);
        network.AddRoad("Sliven", "Yambol", 30, 100);
        network.AddRoad("Yambol", "Burgas", 80, 120);
        network.AddRoad("Yambol", "Kazanlyk", 120, 100);
        network.AddRoad("Kazanlyk", "Stara Zagora", 30, 90);

        Console.WriteLine("Enter the start city:");
        var startCity = Console.ReadLine()?.Trim();

        Console.WriteLine("Enter the end city:");
        var endCity = Console.ReadLine()?.Trim();

        if (!string.IsNullOrWhiteSpace(startCity) && !string.IsNullOrWhiteSpace(endCity))
        {
            network.DisplayPathAndTime(startCity, endCity);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter valid city names.");
        }
    }
}