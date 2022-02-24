
using Battleships.Interfaces;

namespace Battleships.Strategies;


public class DumbStrategy : IStrategy
{

    public void Attack(Player player) // Złożoność obliczeniowa w najlepszym przypadku O(1), a w najgorszym O(N).
    {

        var random = new Random();

        int x;
        do
        {
            x = random.Next(0, player.Hits.Count);
        }while(player.Hits[x].Count <= 0); // Z listy list dostępnych pól do zbicia losuje którąś listę.
                                           // Odpowiada ona za pierwszą współrzędną.

        var y = random.Next(0, player.Hits[x].Count); // Wybiera liczbę z listy, jest to druga współrzędna

        player.Map.Hit(x, player.Hits[x][y]); // Uderza w punkt na mapie
        
        player.Hits[x].Remove(player.Hits[x][y]); // Usuwa liczbę z listy
   
    }
}
