using System;

namespace homework.OOP;

internal static class PropertiesProgram
{
    public static void Main1(string[] args)
    {
        Renderer renderer = new();
        Player2 player2 = new(20, 8);
        renderer.Draw(player2);
    }
}

class Player2
{
    public Player2(int positionX, int positionY, char sign = '*')
    {
        PositionX = positionX;
        PositionY = positionY;
        
        Sign = sign;
    }
    
    public int PositionX { get; private set; }

    public int PositionY { get; private set; }
    
    public char Sign { get; private set; }
}

class Renderer
{
    public void Draw(Player2 player2)
    {
        Console.CursorVisible = false;
        
        Console.SetCursorPosition(player2.PositionX, player2.PositionY);
        Console.Write(player2.Sign);
        
        Console.ReadKey(true);
    }
}