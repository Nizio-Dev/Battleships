
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

    private int _targetHitX = 0;
    private int _targetHitY = 0;

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
        /*
        int mapSize = Player.Map.GameMap.Count;

        List<List<int>> hits = Player.Hits;

         hits = Enumerable.Range(0, mapSize)
        .Select(x => 
            Enumerable.Range(0, mapSize).Where(i => i % 2 == 0).ToList())
        .ToList();

        for (int i = 0; i < hits.Count; i+=2)
        {
            hits[i] = hits[i].Select(c => c+1).ToList();
        }

        Player.Hits = hits;*/
    }

    public void Attack()
    {

        CheckForShips();

        if (!_knownShipLocation)
        {
            HitNextUnknown();
            _direction = Direction.Up;
            return;
        }



        Move();

    

    }

    private void CheckForShips()
    {

    }

    private void Move()
    {

        DirectionBoundary();

        switch (_direction)
        {
            case Direction.Up:
                Hit(0, -1);
            break;
            case Direction.Down:
                Hit(0, 1);
            break;
            case Direction.Left:
                Hit(-1, 0);
            break;
            case Direction.Right:
                Hit(1, 0);
            break;
        }
    }


    private void Hit(int vectorX, int vectorY)
    {
        int coordX = _targetHitX+vectorX;
        int coordY = _targetHitY+vectorY;

        Console.WriteLine($"{coordX} {coordY}");

        if(Player.Map.Hit(coordX, coordY))
        {
            _consuctiveHits++;
            if(Player.Map.GameMap[coordX][coordY].Ship.Health <= 0)
            {
                _knownShipLocation = false;
            }
        }
        else
        {
            _targetHitX = _initialHitX;
            _targetHitY = _initialHitY;
            SetDirection();
            DirectionBoundary();
        }
        Player.Hits[coordX].Remove(coordY);
    }


    private void SetDirection()
    {
        switch (_direction)
        {
            case Direction.Up:
                _direction = Direction.Down;
                break;

            case Direction.Down:
                _direction = Direction.Left;
                break;

            case Direction.Left:
                _direction = Direction.Right;
                break;

            case Direction.Right:
                _direction = Direction.Up;
                break;
        }
    }


        private void DirectionBoundary()
    {

        var mapSize = Player.Map.GameMap.Count;
        switch (_direction)
        {
            case Direction.Up:
                if(_targetHitY+1 > mapSize) _direction = Direction.Down;
            break;

            case Direction.Down:
                if(_targetHitY-1 <= 0) _direction = Direction.Left;
            break;

            case Direction.Left:
                if(_targetHitX-1 <= 0) _direction = Direction.Right;
            break;

            case Direction.Right:
                if(_targetHitX+1 > mapSize) _direction = Direction.Up;
            break;    
        }
    }


    private void HitNextUnknown()
    {

        int x;
        do
        {
            x = _random.Next(0, Player.Hits.Count);
        }while(Player.Hits[x].Count <= 0); // Z listy list dostępnych pól do zbicia losuje którąś listę.
                                           // Odpowiada ona za pierwszą współrzędną.

        var YValue = _random.Next(0, Player.Hits[x].Count); // Wybiera liczbę z listy, jest to druga współrzędna

        var y = Player.Hits[x][YValue];

        if(Player.Map.Hit(x, y)) // Uderza w punkt na mapie
        {
            _knownShipLocation = true;
            _initialHitX = x;
            _initialHitY =  y;
            _targetHitX = x;
            _targetHitY = y;
            _consuctiveHits++;
            if(Player.Map.GameMap[x][y].Ship.Health <= 0)
            {
                _knownShipLocation = false;
            }
        } 
        
        Player.Hits[x].Remove(y); // Usuwa liczbę z listy

        /*var currentCol = Player.Hits.First(i => i.Count > 0);

        var currentColIdx =  Player.Hits.IndexOf(currentCol);

        Console.WriteLine($"{currentColIdx} - {currentCol[0]}");

        if(Player.Map.Hit(currentColIdx, currentCol[0]))
        {
            _knownShipLocation = true;
            _initialHitX = currentColIdx;
            _initialHitY =  currentCol[0];
        }
        else
        {
            _knownShipLocation = false;
        }

        currentCol.RemoveAt(0);*/
    }

    private void FindHitField()
    {
        
    }

}
