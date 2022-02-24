using Battleships;
using Battleships.Interfaces;
using Battleships.Strategies;

IStrategy? strategyOption = null;

string optionsPrompt = @"
Choose strategy style:
1. Dumb (Totally random)
2. Smart (Check for nearby ships in cross pattern on succesful hits)
";

while (strategyOption == null)
{
    Console.Write(optionsPrompt);

    switch (Console.ReadKey().KeyChar)
    {
        case '1':
            strategyOption = new DumbStrategy();
        break;

        case '2':
            strategyOption = new SmarterStrategy();
        break;
    }

    Console.Clear();
}


var game = new Game("Martyna", "Damian", strategyOption);

game.Play();

Console.ReadLine();
