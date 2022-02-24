
using Battleships.Interfaces;

namespace Battleships;


public class Player
{

    private const int _mapSize = 10;

    public bool WasLastHitSuccessful { get; set; } = false;
    public string Name { get; }

    private IStrategy _strategy;

    public List<List<int>> Hits {get; set;}

    public Map Map = new Map(_mapSize);

    public Player(string name)
    {
        Name = name;
        SetupHits();
    }

    private void SetupHits()
    {
            Hits = Enumerable.Range(0, _mapSize)
            .Select(x => 
                Enumerable.Range(0, _mapSize).ToList())
            .ToList();
    }


}
