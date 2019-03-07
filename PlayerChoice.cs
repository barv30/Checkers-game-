using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public static class PlayerChoice
    {
        public static void GetPlayerInfo(PlayerInformation io_PlayerOne, PlayerInformation io_PlayerTwo, out int o_SizeBoard)
        {
            Console.WriteLine("Please enter Player1 name: (20 letters, no spaces)");
            io_PlayerOne.PlayerName = gettingValidName();
            Console.WriteLine("Please choose a size board (6,8,10)");
            o_SizeBoard = gettingValidSize();
            Console.WriteLine("Whould you like to play against the computer or against a human friend? (press '1' for human friend '2' for computer");
            GameManager.eGameType choiceOFplay;
            choiceOFplay = (GameManager.eGameType)gettingValidChoicePlayerTwo();
            if (choiceOFplay == GameManager.eGameType.PlayerVsComputer)
            {
                io_PlayerTwo.IsHuman = false;
                io_PlayerTwo.PlayerName = "ComputerPlayer";
            }
            else
            {
                io_PlayerTwo.IsHuman = true;
                Console.WriteLine("Please enter Player2 name: (20 letters, no spaces)");
                io_PlayerTwo.PlayerName = gettingValidName();
            }
        }

        private static int gettingValidChoicePlayerTwo()
        {
            int o_ValidChoice;
            bool isValid;
            isValid = int.TryParse(Console.ReadLine(), out o_ValidChoice);
            while (!isValid || (o_ValidChoice != 1 && o_ValidChoice != 2))
            {
                Console.WriteLine("You should enter number - '1' for huuman friend end '2' for the computer \nPlease enter a new number:");
                isValid = int.TryParse(Console.ReadLine(), out o_ValidChoice);
            }

            return o_ValidChoice;
        }

        private static int gettingValidSize()
        {
            int o_ValidSize;
            const int k_MinSize = 6;
            const int k_MidSize = 8;
            const int k_MaxSize = 10;
            bool isValid;
            isValid = int.TryParse(Console.ReadLine(), out o_ValidSize);
            while (!isValid || (o_ValidSize != k_MinSize && o_ValidSize != k_MidSize && o_ValidSize != k_MaxSize))
            {
                Console.WriteLine("You should enter size - 6 or 8 or 10! \nPlease enter a new size:");
                isValid = int.TryParse(Console.ReadLine(), out o_ValidSize);
            }

            return o_ValidSize;
        }

        private static string gettingValidName()
        {
            const int k_MaxLength = 20;
            const string k_Space = " ";
            string o_ValidName;
            o_ValidName = Console.ReadLine();
            while (o_ValidName.Length > k_MaxLength || o_ValidName.Contains(k_Space) || o_ValidName == k_Space)
            {
                Console.WriteLine("You should enter a name which is not cointain a white space and the max length is 20 letter\nPlease enter a new name:");
                o_ValidName = Console.ReadLine();
            }

            return o_ValidName;
        }

        public static void gettingValidMoveWhileKillingAgain(ControlGame i_controlGame, GameManager i_game, PlayerInformation i_CurrentPlayer, GameBoard i_GameBoard, int i_ColSelectCurrent, int i_RowSelectCurrent, out int o_ColSelectFuture, out int o_RowSelectFuture, out string nextMove, string currentMove)
        {
            string playerMove;
            Console.WriteLine("Start your move with" + currentMove[3] + currentMove[4]);
            Console.WriteLine(i_CurrentPlayer.PlayerName + "'s turn (" + i_CurrentPlayer.Solider + ") : ");
            playerMove = Console.ReadLine();
            bool isContinueTheGame = true;
            while (isContinueTheGame && CheckValidInputString(i_GameBoard, playerMove) != true || playerMove[0] != currentMove[3] || playerMove[1] != currentMove[4])
            {
                Console.WriteLine(i_CurrentPlayer.PlayerName + "'s turn (" + i_CurrentPlayer.Solider + ") : ");
                playerMove = System.Console.ReadLine();
                if (checkQuitOrNewGameOrNewRound(i_controlGame, i_game, i_CurrentPlayer, playerMove))
                {
                    isContinueTheGame = false;
                }
                else
                {
                    Console.WriteLine("For while we continue the game...");
                }
            }

            if (isContinueTheGame)
            {
                int ColSelectCurrent;
                int RowSelectCurrent;
                ParseInputStringToRowsAndCols(playerMove, out ColSelectCurrent, out RowSelectCurrent, out o_ColSelectFuture, out o_RowSelectFuture);
                nextMove = playerMove;
            }
            else
            {
                o_ColSelectFuture = GameMoves.Error;
                o_RowSelectFuture = GameMoves.Error;
                nextMove = null;
            }
        }

        public static void gettingValidMove(ControlGame i_controlGame, GameManager i_Game, PlayerInformation i_CurrentPlayer, GameBoard i_GameBoard, out int o_ColSelectCurrent, out int o_RowSelectCurrent, out int o_ColSelectFuture, out int o_RowSelectFuture, out string currentMove)
        {
            string playerMove;
            bool haveToInsertNewInput = true;
            bool isCountinuePlay = true;
            Console.WriteLine(i_CurrentPlayer.PlayerName + "'s turn (" + i_CurrentPlayer.Solider + ") : ");
            playerMove = Console.ReadLine();

            while (haveToInsertNewInput)
            {
                if (playerMove.Length == 1)
                {
                    if (CheckExitKeyPress(playerMove[0]))
                    {
                        Console.WriteLine("press 'Q' to quit or 'R' for new round or 'G' for new game ");
                        playerMove = System.Console.ReadLine();
                        if (checkQuitOrNewGameOrNewRound(i_controlGame, i_Game, i_CurrentPlayer, playerMove))
                        {
                            haveToInsertNewInput = false;
                            isCountinuePlay = false;
                        }
                        else
                        {
                            Console.WriteLine("For while we countinue the game");
                        }
                    }
                }
                else if (CheckValidInputString(i_GameBoard, playerMove))
                {
                    haveToInsertNewInput = false;
                }

                if (haveToInsertNewInput)
                {
                    Console.WriteLine(i_CurrentPlayer.PlayerName + "'s turn (" + i_CurrentPlayer.Solider + ") : ");
                    playerMove = System.Console.ReadLine();
                }
            }

            if (isCountinuePlay)
            {
                currentMove = playerMove;
                ParseInputStringToRowsAndCols(playerMove, out o_ColSelectCurrent, out o_RowSelectCurrent, out o_ColSelectFuture, out o_RowSelectFuture);
            }
            else //// want to Quit
            {
                o_ColSelectCurrent = GameMoves.Error;
                o_RowSelectCurrent = GameMoves.Error;
                o_ColSelectFuture = GameMoves.Error;
                o_RowSelectFuture = GameMoves.Error;
                currentMove = null;
            }
        }

        public static bool checkQuitOrNewGameOrNewRound(ControlGame i_controlGame, GameManager i_Game, PlayerInformation i_CurrentPlayer, string i_playerMove)
        {
            bool o_IsCheckQuitOrNewGameOrNewRound = false;

            if (i_playerMove.Length == 1)
            {
                if (CheckExitKeyPress(i_playerMove[0]))
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("You choose to quit the game\nBYE BYE");
                    i_Game.IsRoundOver = true;
                    i_Game.NewRound = false;
                    i_Game.GameOver = true;

                    o_IsCheckQuitOrNewGameOrNewRound = true;
                }
                else if (checkNewRound(i_playerMove[0]))
                {
                    i_Game.IsRoundOver = true;
                    i_Game.NewRound = true;

                    o_IsCheckQuitOrNewGameOrNewRound = true;
                }
                else if (checkNewGame(i_playerMove[0]))
                {
                    i_Game.IsRoundOver = true;
                    i_Game.GameOver = true;
                    i_Game.NewGame = true;
                    o_IsCheckQuitOrNewGameOrNewRound = true;
                }
                else /// the player enter illegal Char
                {
                    Console.WriteLine("You enter unvalid input...");
                    o_IsCheckQuitOrNewGameOrNewRound = false;
                }
            }

            return o_IsCheckQuitOrNewGameOrNewRound;
        }

        private static bool checkNewRound(char i_InputChar)
        {
            bool isNewRound = i_InputChar == 'R' || i_InputChar == 'r';
            return isNewRound;
        }

        private static bool checkNewGame(char i_InputChar)
        {
            bool isNewGame = i_InputChar == 'G' || i_InputChar == 'g';
            return isNewGame;
        }

        public static bool CheckExitKeyPress(char i_InputChar)
        {
            bool isExit = i_InputChar == 'Q' || i_InputChar == 'q';
            return isExit;
        }

        public static void ParseRowsAndColsToStringMove(out string o_MoveString, int i_ColSelectCurrent, int i_RowSelectCurrent, int i_ColSelectFuture, int i_RowSelectFuture)
        {
            const int k_LenOfString = 5;
            StringBuilder newStringMove = new StringBuilder();
            char[] charArr = new char[k_LenOfString];

            charArr[0] = Convert.ToChar(i_ColSelectCurrent + 'A');
            charArr[1] = Convert.ToChar(i_RowSelectCurrent + 'a');
            charArr[2] = '>';
            charArr[3] = Convert.ToChar(i_ColSelectFuture + 'A');
            charArr[4] = Convert.ToChar(i_RowSelectFuture + 'a');

            for (int i = 0; i < k_LenOfString; i++)
            {
                newStringMove.Append(charArr[i]);
            }

            o_MoveString = newStringMove.ToString();
        }

        public static void ParseInputStringToRowsAndCols(string i_MoveString, out int o_ColSelectCurrent, out int o_RowSelectCurrent, out int o_ColSelectFuture, out int o_RowSelectFuture)
        {
            // index 2 of the string contains '>'
            o_ColSelectCurrent = Convert.ToInt32(i_MoveString[0] - 'A');
            o_RowSelectCurrent = Convert.ToInt32(i_MoveString[1] - 'a');
            o_ColSelectFuture = Convert.ToInt32(i_MoveString[3] - 'A');
            o_RowSelectFuture = Convert.ToInt32(i_MoveString[4] - 'a');
        }

        public static bool CheckValidInputString(GameBoard i_GameBoard, string i_InputString)
        {
            const int k_NumOfMoveString = 5; // the string should be in format: COLrow>COLrow, in total 5 chars)
            bool isValid = true;

            if (i_InputString.Length != k_NumOfMoveString)
            {
                isValid = false;
            }
            else
            {
                if (i_InputString[2] != '>')
                {
                    isValid = false;
                }
                else
                {
                    for (int i = 0; i < k_NumOfMoveString; i += 2)
                    {
                        if (i == 2)
                        {
                            i++;
                        }

                        if (i_InputString[i] < 'A' || i_InputString[i] > Convert.ToChar(i_GameBoard.SizeBoard + 'A') || i_InputString[i + 1] < 'a' || i_InputString[i + 1] > Convert.ToChar(i_GameBoard.SizeBoard + 'a'))
                        {
                            isValid = false;
                        }
                    }
                }
            }

            if (isValid == false)
            {
                string illegalMsg = string.Format("{0}Invalid move! Try again.{0}", Environment.NewLine);
                Console.WriteLine(illegalMsg);
            }

            return isValid;
        }
    }
}
