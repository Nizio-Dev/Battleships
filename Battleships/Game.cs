using Battleships.Interfaces;

namespace Battleships;

public class Game
{
    public List<Player> Players {get;} = new List<Player>();
    private int _turn = 0;
    public int Turn {get => _turn;}

    private int _numberOfTurns = 0;
    public int NumberOfTurns {get;}
    public int NumberOfPlayers {get;} = 2;
    public int NumberOfShips{get; } = 5;
    public IStrategy Strategy {get;}

    public Player NextPlayer => Players[(Turn+1) % NumberOfPlayers];

    public Game(string firstPlayer, string secondPlayer, IStrategy strategy)
    {
        Players.Add(new Player(firstPlayer));
        Players.Add(new Player(secondPlayer));
        Strategy = strategy;
    }

    private void NextTurn()
    {
        _turn = ++_numberOfTurns % 2;
    }

    public void Play() 
    {


        while (NextPlayer.Map.SunkenShips < NumberOfShips)
        {
            var player = Players[Turn];
            Strategy.Attack(Players[Turn]);
            
            player.Map.PrintMap();

            Console.WriteLine($"\nCurrent player: {player.Name}.");
            Console.WriteLine($"Enemy player has {NumberOfShips - player.Map.SunkenShips} ships.");

            Console.WriteLine("\nPress enter to continue.");
            Console.ReadLine();
            Console.Clear();
            NextTurn();
        }

        Console.WriteLine($"{NextPlayer.Name} won!");


    }


}

