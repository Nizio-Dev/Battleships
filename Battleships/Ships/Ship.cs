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

    public int Health {get; private set; }

    public Ship(string name, int size)
    {
        Name = name;
        Size = size;
        Health = size;
    }

    public void Damage() //  Jeżeli uderzenie zatopi statek, metoda zwraca wartość true
    {
        Health--;

        if(Health <= 0)
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

