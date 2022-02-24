

using Battleships.Ships;

namespace Battleships.Interfaces;

public interface IShip
{
    public string Name { get; }
    public int Size { get;}
    public Orientation Position { get; set; }

    public bool ShouldSink { get; set; }
    public void Damage();
}

