using Battleships;
using Battleships.Interfaces;
using Battleships.Strategies;

List<IStrategy>? strategyOptions = new();

List<string> players = new(){ "Julia", "Damian" };


while (strategyOptions.Count < 2)
{
Console.Write(@$"Choose strategy style for player {players[1%(strategyOptions.Count+1)]}:
1. Dumb (Totally random)
2. Smart (Check for nearby ships in cross pattern on succesful hits)
");

    switch (Console.ReadKey().KeyChar)
    {
        case '1':
            strategyOptions.Add(new DumbStrategy());
        break;

        case '2':
            strategyOptions.Add(new SmarterStrategy());
        break;
    }

    Console.Clear();
}

var game = new Game(players[0], players[1], strategyOptions[0], strategyOptions[1]);
game.Play();

Console.ReadLine();
