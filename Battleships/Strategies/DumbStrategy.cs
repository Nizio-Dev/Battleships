
using Battleships.Interfaces;

namespace Battleships.Strategies;


public class DumbStrategy : IStrategy
{

    public Player Player { get; set; }
    private Random _random = new Random();

    public void SetupHits()
    {

        Player.Hits = Enumerable.Range(0, Player.Map.GameMap.Count)
        .Select(x => 
            Enumerable.Range(0, Player.Map.GameMap.Count).ToList())
        .ToList();

    }

    public void Attack() // Złożoność obliczeniowa w najlepszym przypadku O(1), a w najgorszym O(N).
    {
        int x;
        do
        {
            x = _random.Next(0, Player.Hits.Count);
        }while(Player.Hits[x].Count <= 0); // Z listy list dostępnych pól do zbicia losuje którąś listę.
                                           // Odpowiada ona za pierwszą współrzędną.

        var y = _random.Next(0, Player.Hits[x].Count); // Wybiera liczbę z listy, jest to druga współrzędna

        Player.Map.Hit(x, Player.Hits[x][y]); // Uderza w punkt na mapie
        
        Player.Hits[x].Remove(Player.Hits[x][y]); // Usuwa liczbę z listy
   
    }
}
