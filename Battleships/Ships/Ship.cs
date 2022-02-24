
using Battleships.Interfaces;

namespace Battleships.Ships;


public enum Orientation{ 
    Horizontal,
    Vertical
}


public class Ship : IShip
{
    private Random _random = new Random();
    public string Name { get; }
    public int Size { get; }
    public Orientation Position { get; set; }

    public bool ShouldSink { get; set; } = false;

    private int _hits;

    public Ship(string name, int size)
    {
        Name = name;
        Size = size;
        _hits = size;
    }

    public void Damage() //  Jeżeli uderzenie zatopi statek, metoda zwraca wartość true
    {
        _hits--;

        if(_hits <= 0)
        {
            ShouldSink = true;
            Console.WriteLine($"{Name} has sank!");
        }
    }

    public void RandomizePosition()
    {

        if(_random.Next(0, 2) == 0)
        {
            this.Position = Orientation.Horizontal;
        }
        else
        {
            this.Position = Orientation.Vertical;
        }
    }
}

