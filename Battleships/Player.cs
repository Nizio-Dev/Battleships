
using Battleships.Interfaces;

namespace Battleships;


public class Player
{

    private const int _mapSize = 10;

    public string Name { get; }

    public IStrategy Strategy {get; private set;}

    public List<List<int>> Hits {get; set;} = new List<List<int>>();

    public Map Map = new Map(_mapSize);

    public Player(string name, IStrategy strategy)
    {
        Name = name;
        Strategy = strategy;
        Strategy.Player = this;
        Strategy.SetupHits();
    }

    public void MakeMove()
    {
        Strategy.Attack();
    }
}
