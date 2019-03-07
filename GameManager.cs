using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public class GameManager
    {
        private bool m_isGameOver = false;
        private bool m_isRoundOver = false;
        private bool m_isNoOneWin = false;
        private bool m_isNewRound = false;
        private bool m_NewGame = false;
        private int m_Size;
        private PlayerInformation m_Player1 = new PlayerInformation(SoliderTypes.OSolider);
        private PlayerInformation m_Player2 = new PlayerInformation(SoliderTypes.XSolider);
        private GameBoard m_CheckersGameBoard;

        public enum eGameType
        {
            PlayerVsPlayer = 1,
            PlayerVsComputer = 2
        }

        public GameBoard CheckersGameBoard
        {
            get { return m_CheckersGameBoard; }
        }

        public bool IsRoundOver
        {
            get { return m_isRoundOver; }
            set { m_isRoundOver = value; }
        }

        public bool NewGame
        {
            get { return m_NewGame; }
            set { m_NewGame = value; }
        }

        public bool GameOver
        {
            get { return m_isGameOver; }
            set { m_isGameOver = value; }
        }

        public bool IsNoOneWin
        {
            get { return m_isNoOneWin; }
            set { m_isNoOneWin = value; }
        }

        public PlayerInformation Player1
        {
            get { return m_Player1; }
        }

        public PlayerInformation Player2
        {
            get { return m_Player2; }
        }

        public int SizeOfBoard
        {
            get { return m_Size; }
        }

        public bool NewRound
        {
            get { return m_isNewRound; }
            set { m_isNewRound = value; }
        }

        public void startTheGameOrRound(int i_size)
        {
            m_isGameOver = false;
            m_isRoundOver = false;
            m_isNoOneWin = false;
            m_isNewRound = false;
            m_NewGame = false;
            m_Size = i_size;
            m_CheckersGameBoard = new GameBoard(m_Size);
            initializPlayers();
            m_CheckersGameBoard.InitializeGameBoard(m_Player1, m_Player2);
            m_Player2.IsPlaying = true;
            ScreenDisplay.DrawBoardGame(m_CheckersGameBoard);
        }

        public PlayerInformation theFinalWinner()
        {
            PlayerInformation o_WinnerPlayer;
            if (m_Player1.Victories > m_Player2.Victories)
            {
                o_WinnerPlayer = m_Player1;
            }
            else if (m_Player2.Victories > m_Player1.Victories)
            {
                o_WinnerPlayer = m_Player2;
            }
            else
            {
                o_WinnerPlayer = null;
            }

            return o_WinnerPlayer;
        }

        public void calculatePointsForWinner(PlayerInformation i_WinnerPlayer)
        {
            if (i_WinnerPlayer.Equals(m_Player1))
            {
                m_Player1.Points = m_Player1.Points - m_Player2.Points;
                m_Player1.TotalPoints += m_Player1.Points;
                m_Player2.Points = 0;
            }
            else if (i_WinnerPlayer.Equals(m_Player2))
            {
                m_Player2.Points = m_Player2.Points - m_Player1.Points;
                m_Player2.TotalPoints += m_Player2.Points;
                m_Player1.Points = 0;
            }
            else //// no one winning
            {
                m_Player1.Points = 0;
                m_Player2.Points = 0;
            }
        }

        public PlayerInformation isTheRoundOverAndWhoIsTheWinnerOfTheRound()
        {
            PlayerInformation o_WinnerPlayer;
            GameMoves optionsMoves = new GameMoves();
            bool isPlayer1HasOptionsMoves = optionsMoves.checkIfThePlayersHaveLegalMoves(m_Player1, m_CheckersGameBoard);
            bool isPlayer2HasOptionsMoves = optionsMoves.checkIfThePlayersHaveLegalMoves(m_Player2, m_CheckersGameBoard);

            if ((m_Player1.AmountOfSoliders == 0 && m_Player2.AmountOfSoliders != 0) || (isPlayer2HasOptionsMoves && !(isPlayer1HasOptionsMoves)))
            {
                m_isRoundOver = true;
                o_WinnerPlayer = m_Player2;
                m_Player2.Victories++;
            }
            else if ((m_Player2.AmountOfSoliders == 0 && m_Player1.AmountOfSoliders != 0) || (isPlayer1HasOptionsMoves && !(isPlayer2HasOptionsMoves)))
            {
                m_isRoundOver = true;
                o_WinnerPlayer = m_Player1;
                m_Player1.Victories++;
            }
            else if ((m_Player2.AmountOfSoliders == 0 && m_Player1.AmountOfSoliders == 0) || (!(isPlayer1HasOptionsMoves) && !(isPlayer2HasOptionsMoves)))
            {
                m_isNoOneWin = true;
                m_isRoundOver = true;
                o_WinnerPlayer = null;
            }
            else
            {
                o_WinnerPlayer = null;
            }

            return o_WinnerPlayer;
        }

        public PlayerInformation turnsManger()
        {
            PlayerInformation o_currentPlayerTurn;
            if (m_Player1.IsPlaying)
            {
                m_Player1.IsPlaying = false;
                m_Player2.IsPlaying = true;
                o_currentPlayerTurn = m_Player2;
            }
            ////in situation which Player Two is Playing
            else
            {
                m_Player2.IsPlaying = false;
                m_Player1.IsPlaying = true;
                o_currentPlayerTurn = m_Player1;
            }

            return o_currentPlayerTurn;
        }

        private void initializPlayers()
        {
            m_Player1.IsHasToKill = false;
            m_Player2.IsHasToKill = false;
            m_Player1.ListOfSoliders.RemoveRange(0, m_Player1.AmountOfSoliders);
            m_Player2.ListOfSoliders.RemoveRange(0, m_Player2.AmountOfSoliders);

            switch (m_Size)
            {
                case 6:
                    m_Player1.Points = 6;
                    m_Player2.Points = 6;
                    m_Player1.AmountOfSoliders = 6;
                    m_Player2.AmountOfSoliders = 6;
                    break;
                case 8:
                    m_Player1.Points = 12;
                    m_Player2.Points = 12;
                    m_Player1.AmountOfSoliders = 12;
                    m_Player2.AmountOfSoliders = 12;
                    break;
                case 10:
                    m_Player1.Points = 10;
                    m_Player2.Points = 10;
                    m_Player1.AmountOfSoliders = 20;
                    m_Player2.AmountOfSoliders = 20;
                    break;
            }
        }

        public void updatePlayerInformation(PlayerInformation i_CurrentPlayer, int i_CurrentCol, int I_CurrentRow, int i_NewCol, int i_NewRow, int i_ColKilledSolider, int i_RowKilledSolider)
        {
            Point currentPlaceOfSolider = new Point(i_CurrentCol, I_CurrentRow);
            Point newPlaceOfSolider = new Point(i_NewCol, i_NewRow);
            i_CurrentPlayer.ListOfSoliders.Remove(currentPlaceOfSolider);
            i_CurrentPlayer.ListOfSoliders.Add(newPlaceOfSolider);

            if (i_ColKilledSolider != GameMoves.Error && i_RowKilledSolider != GameMoves.Error)
            {
                Point removeSolider = new Point(i_ColKilledSolider, i_RowKilledSolider);
                if (i_CurrentPlayer.Solider.Equals(m_Player1.Solider))
                {
                    if (m_CheckersGameBoard.GetCellInBoard(i_RowKilledSolider, i_ColKilledSolider) == m_Player2.Solider)
                    {
                        m_Player2.Points--;
                    }
                    else  //// if the enemy king 
                    {
                        m_Player2.Points -= 3;
                    }

                    m_Player2.AmountOfSoliders--;
                    m_Player2.ListOfSoliders.Remove(removeSolider);
                }
                else
                {
                    if (m_CheckersGameBoard.GetCellInBoard(i_RowKilledSolider, i_ColKilledSolider) == m_Player1.Solider)
                    {
                        m_Player1.Points--;
                    }
                    else //// if the enemy king 
                    {
                        m_Player1.Points -= 3;
                    }

                    m_Player1.AmountOfSoliders--;
                    m_Player1.ListOfSoliders.Remove(removeSolider);
                }
            }
        }
    }
}
