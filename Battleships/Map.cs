using Battleships.Ships;

namespace Battleships;


public class Map
{
    public List<List<Field>> GameMap {get; set;} = new List<List<Field>>();

    public List<Ship> Ships {get; set;} = new List<Ship>();

    private Random _random = new Random();
    private int Size {get; set;}

    public int SunkenShips {get; set;} = 0;

    public Map(int size)
    {
        Size = size;
        SetupViewMap();
        SetupShips();
        PlaceShipsOnMap();
    }

    private void SetupViewMap() // Ustawia widoczną mapę oraz określa punkty, które zostały uderzone
    {
        for(var x=0; x<Size; x++)
        {
            GameMap.Add(new List<Field>());
            for(var y=0; y<Size; y++)
            {
                GameMap[x].Add(new Field(x,y));
            }
        }

    }

    private void SetupShips()
    {
        Ships = new List<Ship>
        {
            new Ship("Destroyer", 2),
            new Ship("Submarine", 3),
            new Ship("Cruiser", 3),
            new Ship("Battleship", 4),
            new Ship("Carrier", 5),
        };
    }


    private void PlaceShipsOnMap() // Ustawia statki
    {

        int coordX;
        int coordY;

        foreach(Ship ship in Ships)
        {
            ship.RandomizePosition();

            do
            {
                coordX = _random.Next(Size);
                coordY = _random.Next(Size); 
            }while(!CanPlaceShip(ship.Position, coordX, coordY, ship.Size));
                
            var directionX = 0;
            var directionY = 1;
            if (ship.Position == Orientation.Horizontal)
            {
                directionX = 1;
                directionY = 0;
            }

            for(var i=0; i<ship.Size; i++)
            {
                GameMap[coordX+(i*directionX)][coordY+(i*directionY)].Ship = ship;
            }



        }    
   
    }

    // Sprawdza, czy statek może zostać postawiony
    private bool CanPlaceShip(Orientation orientation, int startingX, int startingY, int shipSize) 
    {

        var directionX = 0;
        var directionY = 1;
        if (orientation == Orientation.Horizontal)
        {
            directionX = 1;
            directionY = 0;

            if(startingX+shipSize > Size)
            {
                return false;
            }
        }
        else
        {
            directionX = 0;
            directionY = 1;

            if(startingY+shipSize > Size)
            {
                return false;
            }
        }
    
        for(int i=0; i<shipSize; i++)
        {
            if(GameMap[startingX+(i*directionX)][startingY+(i*directionY)].Ship != null)
            {
                return false;
            };
        }
        
        return true;
    }

    public void PrintMap()
    {
        Console.Write("    ");
        for(var i=0; i<Size; i++)
        {
            Console.Write($"{(char)(65+i)} ");
        }
        Console.WriteLine();
        Console.Write("   _");
        for(var i=0; i<Size; i++)
        {
            Console.Write($"_ ");
        }
        Console.WriteLine();

        for(var x=0; x<Size; x++)
        {
            Console.Write($" {(0+x)} |");
            for(var y=0; y<Size; y++)
            {
                Console.Write($"{GameMap[y][x].FieldSign} ");
            }
            Console.WriteLine();
        }

    }


    // Po trafieniu uszkadza statek i dodaje odpowiedni znak w polu.
    public bool Hit(int x, int y) 
    {

        var ship = GameMap[x][y].Ship;
        Console.Write($"Hit at: ({(char)(65+x)},{y}): ");
        if(ship == null)
        {
            Console.WriteLine($" miss!");
            GameMap[x][y].FieldSign = '-';
            return false;
        }

        Console.WriteLine($" hit!");

        GameMap[x][y].FieldSign = 'X';
        ship.Damage();

        if (ship.ShouldSink)
        {
            SunkenShips++;
        }
        return true;
    }

}

