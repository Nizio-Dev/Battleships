
using Battleships.Interfaces;
namespace Battleships.Strategies;

enum Direction
{
    Up, Down,
    Left, Right
}


public class SmarterStrategy : IStrategy
{

    private Direction _direction = Direction.Up;

    private int _initialHitX = 0;
    private int _initialHitY = 0;

    private bool _hasHit = false;

    private bool _knownShipLocation = false;

    private bool _hasSankShipLastTurn = false;

    private Random _random = new Random();

    private int _consuctiveHits = 0;
    public Player Player {get; set;}


    public void SetupHits()
    {
        
        Player.Hits = Enumerable.Range(0, Player.Map.GameMap.Count)
        .Select(x => 
            Enumerable.Range(0, Player.Map.GameMap.Count).ToList())
        .ToList();
    }

    public void Attack()
    {
        if (!_knownShipLocation || _hasSankShipLastTurn)
        {
            HitNextUnknown();  

        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        _hasHit = false;

        while (!_hasHit)
        {

            switch (_direction)
            {
                case Direction.Up:
                    TryHit(_initialHitX, _initialHitY-_consuctiveHits);
                break;
                case Direction.Down:
                    TryHit(_initialHitX, _initialHitY+_consuctiveHits);
                break;
                case Direction.Left:
                    TryHit(_initialHitX-_consuctiveHits, _initialHitY);
                break;
                case Direction.Right:
                    TryHit(_initialHitX+_consuctiveHits, _initialHitY);
                break;
            }
        }
    }


    private void TryHit(int coordX, int coordY)
    {

        if (!IsHitValid(coordX, coordY))
        {
            SetDirection();
            _consuctiveHits=1;
            return;
        }
        
        Hit(coordX, coordY);
        _hasHit = true;
        return;

    }

    private void Hit(int coordX, int coordY)
    {

        if(Player.Map.Hit(coordX, coordY))
        {
            if(Player.Map.GameMap[coordX][coordY].Ship.Health <= 0)
            {
                _knownShipLocation = false;
            }
            Player.Hits[coordX].Remove(coordY);
            _consuctiveHits++;
        }
        else
        {
            _consuctiveHits = 0;
        }
    
    }

    private void SetDirection()
    {
        switch (_direction)
        {
            case Direction.Up:
                _direction = _consuctiveHits > 1 ? Direction.Down : Direction.Right;
                break;

            case Direction.Down:
                _direction = _consuctiveHits > 1 ? Direction.Up : Direction.Left;
                break;

            case Direction.Left:
                _direction = _consuctiveHits > 1 ? Direction.Right : Direction.Up;
                break;

            case Direction.Right:
                _direction = _consuctiveHits > 1 ? Direction.Left : Direction.Down;
                break;
        }
    }


    private bool IsHitValid(int targettedX, int targettedY)
    {

        var map = Player.Map.GameMap;

        if(targettedX >= map.Count || targettedX < 0 ||  targettedY >= map.Count || targettedY < 0)
        {
            return false;
        }
        if(map[targettedX][targettedY].FieldSign != 'O')
        {
            return false;
        }
        return true;

    }

    private void HitNextUnknown()
    {
        int x;
        do
        {
            x = _random.Next(0, Player.Hits.Count);
        }while(Player.Hits[x].Count <= 0);

        var YValue = _random.Next(0, Player.Hits[x].Count);

        var y = Player.Hits[x][YValue];

        if(Player.Map.Hit(x, y))
        {
            _knownShipLocation = true;
            _initialHitX = x;
            _initialHitY = y;
            _consuctiveHits++;
            if(Player.Map.GameMap[x][y].Ship.Health <= 0)
            {
                _hasSankShipLastTurn = true;
            }
        } 
        
        Player.Hits[x].Remove(y);
    }


}
