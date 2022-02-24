using Battleships.Ships;

public class Field
{
    public int PositionX {get;}
    public int PositionY {get;}

    public char FieldSign {get; set;} = 'O';


    public Field(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }

    public Ship? Ship {get; set;}
}