

namespace Battleships.Interfaces;

public interface IStrategy
{

    public Player Player { get; set; }

    public void Attack();

}

