using System;

public class City
{
    public string Name { get; }

    public City(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}