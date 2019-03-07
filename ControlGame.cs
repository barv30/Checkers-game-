using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public class ControlGame
    {
        private GameManager m_manager = new GameManager();
        private int m_SizeofBoard;

        public ControlGame()
        {
            startGame();
        }

        public GameManager ControlTheGame
        {
            get { return m_manager; }
        }

        public void RunCheckersGame()
        {
            PlayerInformation thePlayerWhoWin = null;
            m_manager.startTheGameOrRound(m_SizeofBoard);

            while (!m_manager.IsRoundOver && !m_manager.GameOver)
            {
                PlayerInformation currentPlayer = m_manager.turnsManger();
                gettingPlayerMove(currentPlayer);
                if (!m_manager.IsRoundOver && !m_manager.GameOver)
                {
                    thePlayerWhoWin = m_manager.isTheRoundOverAndWhoIsTheWinnerOfTheRound();
                }
                else
                {
                    thePlayerWhoWin = winnerOfRoundInCaseOfQuitOrNewRound(currentPlayer);
                }
            }

            checkStatusOfGame(thePlayerWhoWin);
        }

        private void checkStatusOfGame(PlayerInformation i_thePlayerWhoWin)
        {
            if (m_manager.IsRoundOver)
            {
                EndRound(i_thePlayerWhoWin);

                if (m_manager.GameOver)
                {
                    EndGame();
                    if (m_manager.NewGame)
                    {
                        return;
                    }
                }
                else if (m_manager.NewRound)
                {
                    RunCheckersGame();
                }
                else
                {
                    afterEndRound(i_thePlayerWhoWin);
                    checkStatusOfGame(i_thePlayerWhoWin);
                }
            }
        }

        private PlayerInformation winnerOfRoundInCaseOfQuitOrNewRound(PlayerInformation i_CurrentPlayer)
        {
            PlayerInformation o_WinnerPlayer;
            if (i_CurrentPlayer.Equals(m_manager.Player1))
            {
                o_WinnerPlayer = m_manager.Player2;
                m_manager.Player2.Victories++;
            }
            else
            {
                o_WinnerPlayer = m_manager.Player1;
                m_manager.Player1.Victories++;
            }

            return o_WinnerPlayer;
        }

        private void startGame()
        {
            Console.WriteLine("Hello ! \nWelcome to Checkers game!");
            PlayerChoice.GetPlayerInfo(m_manager.Player1, m_manager.Player2, out m_SizeofBoard);
        }

        private void EndRound(PlayerInformation i_WinnerPlayer)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            if (i_WinnerPlayer == null && m_manager.IsNoOneWin)
            {
                Console.WriteLine("OH:(\n... NO ONE WINS... MAYBE NEXT ROUND!!!");
            }
            else
            {
                Console.WriteLine("The winner of this round is " + i_WinnerPlayer.PlayerName + "!!!");
            }

            m_manager.calculatePointsForWinner(i_WinnerPlayer);
            printPoints();
        }

        private void afterEndRound(PlayerInformation i_WinnerPlayer)
        {
            bool isValidInput = false;
            Console.WriteLine("press 'Q' to quit or 'R' for new round or 'G' for new game ");
            string playerMove = System.Console.ReadLine();
            while (!isValidInput)
            {
                if (PlayerChoice.checkQuitOrNewGameOrNewRound(this, m_manager, i_WinnerPlayer, playerMove))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Please enter again:");
                    playerMove = System.Console.ReadLine();
                }
            }
        }

        private void EndGame()
        {
            PlayerInformation winner = m_manager.theFinalWinner();
            if (winner != null)
            {
                Console.WriteLine("The Final Winner is " + winner.PlayerName + " with " + winner.Victories + " victories!!!");
            }
            else
            {
                Console.WriteLine("OH:(\n... NO ONE WINS THIS GAME!!!");
            }
        }

        private void printPoints()
        {
            Console.WriteLine(m_manager.Player1.PlayerName + " got " + m_manager.Player1.Points + " points in this game! - until now you have " + m_manager.Player1.TotalPoints + " points.");
            Console.WriteLine(m_manager.Player2.PlayerName + " got " + m_manager.Player2.Points + " points in this game! - until now you have " + m_manager.Player2.TotalPoints + " points.");
        }

        private void gettingPlayerMove(PlayerInformation i_CurrentPlayer)
        {
            int currentCol;
            int currentRow;
            int newCol;
            int newRow;
            GameMoves newMove = new GameMoves();
            int colKilledSolider = GameMoves.Error;
            int rowKilledSolider = GameMoves.Error;
            string currentMove;

            if (!i_CurrentPlayer.IsHuman)
            {
                newMove.makeMoveForComputerPlayer(i_CurrentPlayer, m_manager.CheckersGameBoard, out currentCol, out currentRow, out newCol, out newRow);
                PlayerChoice.ParseRowsAndColsToStringMove(out currentMove, currentCol, currentRow, newCol, newRow);
            }
            else
            {
                PlayerChoice.gettingValidMove(this, m_manager, i_CurrentPlayer, m_manager.CheckersGameBoard, out currentCol, out currentRow, out newCol, out newRow, out currentMove);

                while (!m_manager.GameOver && !m_manager.IsRoundOver && !(newMove.isLegalMove(i_CurrentPlayer, m_manager.CheckersGameBoard, currentCol, currentRow, newCol, newRow, ref colKilledSolider, ref rowKilledSolider, i_CurrentPlayer.IsHasToKill)))
                {
                    if (i_CurrentPlayer.IsHasToKill)
                    {
                        Console.WriteLine("You have to kill the enemy solider!\nPlease enter a move:");
                    }
                    else
                    {
                        Console.WriteLine("You should enter a valid move according the the checkers game\nPlease enter a move:");
                    }

                    PlayerChoice.gettingValidMove(this, m_manager, i_CurrentPlayer, m_manager.CheckersGameBoard, out currentCol, out currentRow, out newCol, out newRow, out currentMove);
                }

                if (m_manager.GameOver || m_manager.IsRoundOver)
                {
                    return;
                }
            }

            m_manager.updatePlayerInformation(i_CurrentPlayer, currentCol, currentRow, newCol, newRow, colKilledSolider, rowKilledSolider);
            ScreenDisplay.DrawBoardGame(m_manager.CheckersGameBoard);
            Console.WriteLine(i_CurrentPlayer.PlayerName + "'s move was (" + i_CurrentPlayer.Solider + ") " + currentMove);

            ////for keep killing
            Point placeAfterMove = new Point(newCol, newRow);
            Point newPlaceAfterKiliingAgain;
            while (keepKilling(i_CurrentPlayer, placeAfterMove, currentMove, out newPlaceAfterKiliingAgain))
            {
                placeAfterMove = newPlaceAfterKiliingAgain;
            }
        }

        private bool keepKilling(PlayerInformation i_CurrentPlayer, Point i_placeAfterMove, string lastMove, out Point newPlaceAfterKiliingAgain)
        {
            bool o_isKeepKilling = false;
            bool isPlayerWantToStopTheGame = false;
            List<Point> optionsToNewPlace;
            GameMoves newMove = new GameMoves();
            int nextCol;
            int nextRow;
            string nextMoveString;
            newPlaceAfterKiliingAgain = new Point(GameMoves.Error, GameMoves.Error);

            if (i_CurrentPlayer.IsHasToKill && newMove.checkIfHasKill(i_placeAfterMove, m_manager.CheckersGameBoard, i_CurrentPlayer.Solider, out optionsToNewPlace))
            {
                int colKilledSolider = GameMoves.Error;
                int rowKilledSolider = GameMoves.Error;

                if (i_CurrentPlayer.IsHuman)
                {
                    PlayerChoice.gettingValidMoveWhileKillingAgain(this, m_manager, i_CurrentPlayer, m_manager.CheckersGameBoard, i_placeAfterMove.X, i_placeAfterMove.Y, out nextCol, out nextRow, out nextMoveString, lastMove);

                    while (!m_manager.GameOver && !m_manager.IsRoundOver && !(newMove.isLegalMove(i_CurrentPlayer, m_manager.CheckersGameBoard, i_placeAfterMove.X, i_placeAfterMove.Y, nextCol, nextRow, ref colKilledSolider, ref rowKilledSolider, i_CurrentPlayer.IsHasToKill)))
                    {
                        Console.WriteLine("You have to kill the enemy solider!\nPlease enter a move:");
                        PlayerChoice.gettingValidMoveWhileKillingAgain(this, m_manager, i_CurrentPlayer, m_manager.CheckersGameBoard, i_placeAfterMove.X, i_placeAfterMove.Y, out nextCol, out nextRow, out nextMoveString, lastMove);
                    }

                    if (m_manager.GameOver || m_manager.IsRoundOver)
                    {
                        isPlayerWantToStopTheGame = true;
                        o_isKeepKilling = false;
                    }
                }
                else
                {
                    if (newMove.ifComputerKeepKilling(i_CurrentPlayer.Solider, i_placeAfterMove, m_manager.CheckersGameBoard, out nextCol, out nextRow, ref colKilledSolider, ref rowKilledSolider))
                    {
                        newMove.isLegalMove(i_CurrentPlayer, m_manager.CheckersGameBoard, i_placeAfterMove.X, i_placeAfterMove.Y, nextCol, nextRow, ref colKilledSolider, ref rowKilledSolider, i_CurrentPlayer.IsHasToKill);
                    }

                    PlayerChoice.ParseRowsAndColsToStringMove(out nextMoveString, i_placeAfterMove.X, i_placeAfterMove.Y, nextCol, nextRow);
                }

                if (!isPlayerWantToStopTheGame)
                {
                    m_manager.updatePlayerInformation(i_CurrentPlayer, i_placeAfterMove.X, i_placeAfterMove.Y, nextCol, nextRow, colKilledSolider, rowKilledSolider);
                    ScreenDisplay.DrawBoardGame(m_manager.CheckersGameBoard);

                    if (i_CurrentPlayer.IsHuman)
                    {
                        Console.WriteLine(i_CurrentPlayer.PlayerName + "'s move was (" + i_CurrentPlayer.Solider + ") " + nextMoveString);
                    }
                    else
                    {
                        Console.WriteLine(i_CurrentPlayer.PlayerName + "'s moves were (" + i_CurrentPlayer.Solider + ") :" + lastMove + " and then :" + nextMoveString);
                    }

                    newPlaceAfterKiliingAgain = new Point(nextCol, nextRow);
                    o_isKeepKilling = true;
                }
                else
                {
                    nextCol = GameMoves.Error;
                    nextRow = GameMoves.Error;
                    newPlaceAfterKiliingAgain = new Point(nextCol, nextRow);
                    o_isKeepKilling = false;
                }
            }

            return o_isKeepKilling;
        }
    }
}
