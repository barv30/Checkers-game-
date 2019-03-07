using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public class Program
    {
        public static void Main()
        {
            ControlGame checkersGame = new ControlGame();
            checkersGame.RunCheckersGame();
            while (checkersGame.ControlTheGame.NewGame)
            {
                checkersGame = new ControlGame();
                checkersGame.RunCheckersGame();
            }
        }
    }
}
