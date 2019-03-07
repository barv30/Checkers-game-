using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public class GameBoard
    {
        private readonly int r_WidthOfBoard;
        private BoardCell[,] m_GameBoard;

        public int SizeBoard
        {
            get { return r_WidthOfBoard; }
        }

        public GameBoard(int i_WidthOfBoard)
        {
            r_WidthOfBoard = i_WidthOfBoard;
            startMatrix();
        }

        public void SetCellInBoard(int i_IndexY, int i_IndexX, char i_Value)
        {
            m_GameBoard[i_IndexY, i_IndexX].CellState = i_Value;
        }

        public char GetCellInBoard(int i_IndexY, int i_IndexX)
        {
            return m_GameBoard[i_IndexY, i_IndexX].CellState;
        }

        public BoardCell[,] BoardGame
        {
            get { return m_GameBoard; }
        }

        private void startMatrix()
        {
            m_GameBoard = new BoardCell[r_WidthOfBoard, r_WidthOfBoard];
            for (int i = 0; i < r_WidthOfBoard; i++)
            {
                for (int j = 0; j < r_WidthOfBoard; j++)
                {
                    m_GameBoard[i, j] = new BoardCell();
                }
            }
        }

        public void InitializeGameBoard(PlayerInformation i_PlayerOne, PlayerInformation i_PlayerTwo)
        {
            int boardFirstHalf = (r_WidthOfBoard / 2) - 1;
            int boardSecondHalf = (r_WidthOfBoard / 2) + 1;
            Point place;

            for (int i = 0; i < boardFirstHalf; i++)
            {
                for (int j = 0; j < r_WidthOfBoard; j++)
                {
                    if (IsNumEven(i + j) == true)
                    {
                        m_GameBoard[i, j].CellState = m_GameBoard[i, j].Empty;
                    }
                    else
                    {
                        m_GameBoard[i, j].CellState = i_PlayerOne.Solider;
                        place = new Point(j, i);
                        i_PlayerOne.ListOfSoliders.Add(place);
                    }
                }
            }

            for (int i = boardFirstHalf; i <= (boardFirstHalf + 1); i++)
            {
                for (int j = 0; j < r_WidthOfBoard; j++)
                {
                    m_GameBoard[i, j].CellState = m_GameBoard[i, j].Empty;
                }
            }

            for (int i = boardSecondHalf; i < r_WidthOfBoard; i++)
            {
                for (int j = 0; j < r_WidthOfBoard; j++)
                {
                    if (IsNumEven(i + j) == true)
                    {
                        m_GameBoard[i, j].CellState = m_GameBoard[i, j].Empty;
                    }
                    else
                    {
                        m_GameBoard[i, j].CellState = i_PlayerTwo.Solider;
                        place = new Point(j, i);
                        i_PlayerTwo.ListOfSoliders.Add(place);
                    }
                }
            }
        }

        private bool IsNumEven(int i_IsNumEven)
        {
            return (i_IsNumEven % 2) == 0;
        }
    }
}
