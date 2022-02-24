using Battleships.Ships;
using Xunit;

namespace Battleships.Tests
{
    public class ShipTests
    {
        [Fact]
        public void ShipCtor_CreateStandardShip_ReturnsShip()
        {

            var name = "Parostatek";
            var size = 2;

            Ship ship = new Ship(name, size);

            Assert.True(ship.Name == name);
            Assert.True(ship.Size == size);
        }

        [Fact]
        public void RandomPosition_CreatesRandomPosition_SetsShipPropertyToRandomPosition()
        {

            Ship ship = new Ship("Testowy statek", 10);

            ship.RandomizePosition();

            var orientations = new Orientation[]{Orientation.Horizontal, Orientation.Vertical};

            Assert.Contains(ship.Position, orientations);

        }

    }
}