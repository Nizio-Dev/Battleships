using Battleships.Interfaces;

namespace Battleships;

public class Game
{
    public List<Player> Players {get;} = new List<Player>();
    public int Turn {get; private set;}
    public int NumberOfTurns {get; private set;}
    public int NumberOfPlayers {get;} = 2;
    public int NumberOfShips{get; } = 5;

    public Player NextPlayer => Players[(Turn+1) % NumberOfPlayers];

    public Game(string firstPlayer, string secondPlayer, 
        IStrategy firstPlayerStrategy, IStrategy secondPlayerStrategy)
    {
        Players.Add(new Player(firstPlayer, firstPlayerStrategy));
        Players.Add(new Player(secondPlayer, secondPlayerStrategy));
    }

    private void NextTurn()
    {
        Turn = ++NumberOfTurns % 2;
    }

    public void Play() 
    {
        while (NextPlayer.Map.SunkenShips < NumberOfShips)
        {
            var player = Players[Turn];
            player.MakeMove();

            Console.WriteLine($"\nCurrent player: {player.Name}.");
            Console.WriteLine($"Enemy player has {NumberOfShips - player.Map.SunkenShips} ships.");

            player.Map.PrintMap();

            Console.WriteLine("\nPress enter to continue.");
            Console.ReadLine();
            Console.Clear();
            NextTurn();
        }

        Console.WriteLine($"{NextPlayer.Name} won!");


    }


}

