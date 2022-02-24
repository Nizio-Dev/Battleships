
using Battleships.Interfaces;

namespace Battleships.Strategies;


public class SmarterStrategy : IStrategy
{

    private int _xDirection = 0;
    private int _YDirection = 0;

    private int _numberOfActions = 0;
    private bool _wasLastHitSuccesful = false;
    
    private int _initialHitX = -1;
    private int _initialHitY = -1;

    private Random _random = new Random();

    private int _consuctiveHits = 0;
    public Player Player {get; set;}

    public void Attack()
    {
        if(_consuctiveHits == 0)
        {
            RandomHit();
        }
    }

    private void RandomHit()
    {
        int x;
        do
        {
            x = _random.Next(0, Player.Hits.Count);
        }while(Player.Hits[x].Count <= 0); 

        var y = _random.Next(0, Player.Hits[x].Count); 

        var hit = Player.Map.Hit(x, Player.Hits[x][y]);

        if (hit)
        {
            _initialHitX = x;
            _initialHitY = Player.Hits[x][y];
            _YDirection = 1;
        }

        Player.Hits[x].Remove(Player.Hits[x][y]);
    }

    private void RotateDirection()
    {
        
    }

    private void FindHitField()
    {
        
    }

}
