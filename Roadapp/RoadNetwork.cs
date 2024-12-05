using System;
using System.Collections.Generic;
using System.Linq;

public class RoadNetwork
{
    private readonly List<City> _cities = new();
    private readonly List<Road> _roads = new();

    public void AddCity(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("City name cannot be null or empty.", nameof(name));

        if (_cities.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"City '{name}' already exists in the network.");

        _cities.Add(new City(name));
    }

    public void AddRoad(string from, string to, double distance, int maxSpeed)
    {
        var sourceCity = _cities.Find(c => c.Name.Equals(from, StringComparison.OrdinalIgnoreCase));
        var destinationCity = _cities.Find(c => c.Name.Equals(to, StringComparison.OrdinalIgnoreCase));

        if (sourceCity == null || destinationCity == null)
            throw new InvalidOperationException("Both cities must exist in the network before adding a road.");

        _roads.Add(new Road(sourceCity, destinationCity, distance, maxSpeed));
        _roads.Add(new Road(destinationCity, sourceCity, distance, maxSpeed));
    }

    public (List<string> Path, double Time) FindQuickestPath(string start, string end)
    {
        var startCity = _cities.Find(c => c.Name.Equals(start, StringComparison.OrdinalIgnoreCase));
        var endCity = _cities.Find(c => c.Name.Equals(end, StringComparison.OrdinalIgnoreCase));

        if (startCity == null || endCity == null)
            throw new InvalidOperationException("Both cities must exist in the network.");

        var distances = _cities.ToDictionary(city => city, _ => double.MaxValue);
        var previous = new Dictionary<City, City>();
        var unvisited = new HashSet<City>(_cities);

        distances[startCity] = 0;

        while (unvisited.Count > 0)
        {
            var current = unvisited.OrderBy(city => distances[city]).First();
            unvisited.Remove(current);

            if (current == endCity) break;

            var neighbors = _roads.Where(road => road.From == current && unvisited.Contains(road.To));
            foreach (var road in neighbors)
            {
                var time = distances[current] + road.TravelTime;
                if (time < distances[road.To])
                {
                    distances[road.To] = time;
                    previous[road.To] = current;
                }
            }
        }

        var path = new List<string>();
        for (var city = endCity; city != null; city = previous.GetValueOrDefault(city))
        {
            path.Add(city.Name);
        }

        path.Reverse();
        return (path, distances[endCity]);
    }

    public void DisplayPathAndTime(string start, string end)
    {
        try
        {
            var (path, time) = FindQuickestPath(start, end);
            Console.WriteLine($"Quickest path from {start} to {end}:");
            Console.WriteLine(string.Join(" -> ", path));
            Console.WriteLine($"Total travel time: {time:F2} hours");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}