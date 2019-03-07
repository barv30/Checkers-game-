using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public static class ScreenDisplay
    {
        public static void DrawBoardGame(GameBoard i_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int sizeOfBoard = i_Board.SizeBoard;
            Console.Write("  ");
            for (int i = 0; i < sizeOfBoard; i++)
            {
                Console.Write("  " + Convert.ToChar('A' + i) + " ");
            }

            Console.Write(Environment.NewLine);

            for (int j = 0; j < sizeOfBoard; j++)
            {
                Console.Write("   ");
                for (int k = 0; k < sizeOfBoard; k++)
                {
                    Console.Write("====");
                }

                Console.Write(Environment.NewLine);
                Console.Write(Convert.ToChar('a' + j) + " ");

                for (int l = 0; l < sizeOfBoard; l++)
                {
                    Console.Write("| " + i_Board.GetCellInBoard(j, l) + " ");
                }

                Console.Write("|" + Environment.NewLine);
            }

            Console.Write("   ");
            for (int k = 0; k < sizeOfBoard; k++)
            {
                Console.Write("====");
            }

            Console.WriteLine(Environment.NewLine);
        }
    }
}
