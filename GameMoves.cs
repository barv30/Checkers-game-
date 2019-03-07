using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public class GameMoves
    {
        private const int k_Error = -1;

        public static int Error
        {
            get { return k_Error; }
        }

        private bool isLegalSlantMove(char i_TypeSoliderOfPlayer, GameBoard i_Board, out char state, int currentCol, int currentRow, int newCol, int newRow)
        {
            bool o_IsLegalSlantMove = false;
            state = i_Board.GetCellInBoard(currentRow, currentCol);

            if (state != SoliderTypes.Empty)
            {
                if (checkIfTheNextCellValid(i_Board, newCol, newRow))
                {
                    if (i_TypeSoliderOfPlayer.Equals(SoliderTypes.OSolider) && (state.Equals(SoliderTypes.KingO) || state.Equals(SoliderTypes.OSolider)) ||
                    (i_TypeSoliderOfPlayer.Equals(SoliderTypes.XSolider) && (state.Equals(SoliderTypes.KingX) || state.Equals(SoliderTypes.XSolider))))
                    {
                        if (state == SoliderTypes.OSolider)
                        {
                            if ((newCol == currentCol + 1 || newCol == currentCol - 1) && (newRow == currentRow + 1))
                            {
                                o_IsLegalSlantMove = true;
                            }
                        }
                        else if (state == SoliderTypes.XSolider)
                        {
                            if ((newCol == currentCol + 1 || newCol == currentCol - 1) && (newRow == currentRow - 1))
                            {
                                o_IsLegalSlantMove = true;
                            }
                        }
                        else if (state == SoliderTypes.KingO || (state == SoliderTypes.KingX))
                        {
                            if ((newCol == currentCol + 1 || newCol == currentCol - 1) && (newRow == currentRow - 1 || newRow == currentRow + 1))
                            {
                                o_IsLegalSlantMove = true;
                            }
                        }
                    }
                }
            }

            return o_IsLegalSlantMove;
        }

        private bool isLegalKillingSoliderMove(char i_TypeSoliderOfPlayer, GameBoard i_Board, out char state, int currentCol, int currentRow, int newCol, int newRow, ref int o_RowOfKilledSolider, ref int o_ColOfKilledSolider)
        {
            bool o_IsLegalKillingSoliderMove = false;
            state = i_Board.GetCellInBoard(currentRow, currentCol);

            if (state != SoliderTypes.Empty)
            {
                if (checkIfTheNextCellValid(i_Board, newCol, newRow))
                {
                    if (i_TypeSoliderOfPlayer.Equals(SoliderTypes.OSolider) && (state.Equals(SoliderTypes.KingO) || state.Equals(SoliderTypes.OSolider)) ||
                    (i_TypeSoliderOfPlayer.Equals(SoliderTypes.XSolider) && (state.Equals(SoliderTypes.KingX) || state.Equals(SoliderTypes.XSolider))))
                    {
                        if (state == SoliderTypes.OSolider || state == SoliderTypes.KingO)
                        {
                            if (newRow == currentRow + 2)
                            {
                                if (newCol == currentCol + 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow + 1, currentCol + 1);
                                    if (enemy == SoliderTypes.XSolider || enemy == SoliderTypes.KingX)
                                    {
                                        o_ColOfKilledSolider = currentCol + 1;
                                        o_RowOfKilledSolider = currentRow + 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                                else if (newCol == currentCol - 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow + 1, currentCol - 1);
                                    if (enemy == SoliderTypes.XSolider || enemy == SoliderTypes.KingX)
                                    {
                                        o_ColOfKilledSolider = currentCol - 1;
                                        o_RowOfKilledSolider = currentRow + 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                            }
                            else if (newRow == currentRow - 2 && state == SoliderTypes.KingO)
                            {
                                if (newCol == currentCol + 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow - 1, currentCol + 1);
                                    if (enemy == SoliderTypes.XSolider || enemy == SoliderTypes.KingX)
                                    {
                                        o_ColOfKilledSolider = currentCol + 1;
                                        o_RowOfKilledSolider = currentRow - 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                                else if (newCol == currentCol - 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow - 1, currentCol - 1);
                                    if (enemy == SoliderTypes.XSolider || enemy == SoliderTypes.KingX)
                                    {
                                        o_ColOfKilledSolider = currentCol - 1;
                                        o_RowOfKilledSolider = currentRow - 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                            }
                        }
                        else if (state == SoliderTypes.XSolider || state == SoliderTypes.KingX)
                        {
                            if (newRow == currentRow - 2)
                            {
                                if (newCol == currentCol + 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow - 1, currentCol + 1);
                                    if (enemy == SoliderTypes.OSolider || enemy == SoliderTypes.KingO)
                                    {
                                        o_ColOfKilledSolider = currentCol + 1;
                                        o_RowOfKilledSolider = currentRow - 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                                else if (newCol == currentCol - 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow - 1, currentCol - 1);
                                    if (enemy == SoliderTypes.OSolider || enemy == SoliderTypes.KingO)
                                    {
                                        o_ColOfKilledSolider = currentCol - 1;
                                        o_RowOfKilledSolider = currentRow - 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                            }
                            else if (newRow == currentRow + 2 && state == SoliderTypes.KingX)
                            {
                                if (newCol == currentCol + 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow + 1, currentCol + 1);
                                    if (enemy == SoliderTypes.OSolider || enemy == SoliderTypes.KingO)
                                    {
                                        o_ColOfKilledSolider = currentCol + 1;
                                        o_RowOfKilledSolider = currentRow + 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                                else if (newCol == currentCol - 2)
                                {
                                    char enemy = i_Board.GetCellInBoard(currentRow + 1, currentCol - 1);
                                    if (enemy == SoliderTypes.OSolider || enemy == SoliderTypes.KingO)
                                    {
                                        o_ColOfKilledSolider = currentCol - 1;
                                        o_RowOfKilledSolider = currentRow + 1;
                                        o_IsLegalKillingSoliderMove = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return o_IsLegalKillingSoliderMove;
        }

        public bool isLegalMove(PlayerInformation i_Player, GameBoard i_Board, int currentCol, int currentRow, int newCol, int newRow, ref int colOfKiledSolider, ref int rowOfKilledSolider, bool i_isHaveToKill)
        {
            char currentState;
            char newState;
            bool o_IsLegalMove = false;

            if (i_isHaveToKill)
            {
                if (isLegalKillingSoliderMove(i_Player.Solider, i_Board, out currentState, currentCol, currentRow, newCol, newRow, ref rowOfKilledSolider, ref colOfKiledSolider))
                {
                    isBecomeKing(i_Player, i_Board, currentState, out newState, newRow);
                    makeMove(i_Board, newState, currentCol, currentRow, newCol, newRow, rowOfKilledSolider, colOfKiledSolider);
                    o_IsLegalMove = true;
                }
                else
                {
                    o_IsLegalMove = false;
                }
            }
            else if (isLegalSlantMove(i_Player.Solider, i_Board, out currentState, currentCol, currentRow, newCol, newRow))
            {
                isBecomeKing(i_Player, i_Board, currentState, out newState, newRow);
                makeMove(i_Board, newState, currentCol, currentRow, newCol, newRow, rowOfKilledSolider, colOfKiledSolider);
                o_IsLegalMove = true;
            }
            else
            {
                o_IsLegalMove = false;
            }

            return o_IsLegalMove;
        }

        private void isBecomeKing(PlayerInformation i_Player, GameBoard i_Board, char currentState, out char newState, int newRow)
        {
            newState = currentState;

            if (currentState != SoliderTypes.KingX && currentState != SoliderTypes.KingO)
            {
                if (currentState == SoliderTypes.OSolider)
                {
                    if (newRow == i_Board.SizeBoard - 1)
                    {
                        i_Player.Points += 3;
                        newState = SoliderTypes.KingO;
                    }
                }
                else if (currentState == SoliderTypes.XSolider)
                {
                    if (newRow == 0)
                    {
                        i_Player.Points += 3;
                        newState = SoliderTypes.KingX;
                    }
                }
            }
        }

        private void makeMove(GameBoard i_Board, char soliderType, int currentCol, int currentRow, int newCol, int newRow, int rowOfKiledSolider, int colOfKiledSolider)
        {
            if (rowOfKiledSolider != k_Error && colOfKiledSolider != k_Error)
            {
                i_Board.SetCellInBoard(rowOfKiledSolider, colOfKiledSolider, SoliderTypes.Empty);
            }

            i_Board.SetCellInBoard(currentRow, currentCol, SoliderTypes.Empty);
            i_Board.SetCellInBoard(newRow, newCol, soliderType);
        }

        private bool checkIfTheNextCellValid(GameBoard i_Board, int i_X, int i_Y)
        {
            bool o_IsTheNextCellValid = true;
            if ((i_X < 0 || i_X >= i_Board.SizeBoard) || (i_Y < 0 || i_Y >= i_Board.SizeBoard))
            {
                o_IsTheNextCellValid = false;
            }

            if (o_IsTheNextCellValid && i_Board.GetCellInBoard(i_Y, i_X) != SoliderTypes.Empty)
            {
                o_IsTheNextCellValid = false;
            }

            return o_IsTheNextCellValid;
        }

        public bool checkIfThePlayersHaveLegalMoves(PlayerInformation i_CurrentPlayer, GameBoard i_Board)
        {
            i_CurrentPlayer.IsHasToKill = false;
            List<Point> optionsOfNewPlaceForKill;
            List<Point> optionsOfNewPlaceForSlant;
            bool isHasKill = false;
            bool o_isHaveLegalMoves = false;
            foreach (Point place in i_CurrentPlayer.ListOfSoliders)
            {
                if (isTheSpecipicSoliderHasLegelMoves(place, i_CurrentPlayer, i_Board, out optionsOfNewPlaceForSlant, out optionsOfNewPlaceForKill, ref isHasKill))
                {
                    if (isHasKill)
                    {
                        i_CurrentPlayer.IsHasToKill = true;
                    }

                    o_isHaveLegalMoves = true;
                }
            }

            return o_isHaveLegalMoves;
        }

        public void makeMoveForComputerPlayer(PlayerInformation i_CurrentPlayer, GameBoard i_Board, out int o_currentCol, out int o_currentRow, out int o_newCol, out int o_newRow)
        {
            i_CurrentPlayer.IsHasToKill = false;
            List<Point> optionsOfNewPlaceForSlant;
            List<Point> optionsOfNewPlaceForKill;
            List<Point> optionsOfPlacesWhichCanKill = new List<Point>();
            bool isMoveDone = false;
            bool isHasKill = false;
            int rowOfKiledSolider = k_Error;
            int colOfKilledSolider = k_Error;
            Point chooseTheSoliderPlaceToMove = new Point(k_Error, k_Error);
            Point chooseTheNewPlace = new Point(k_Error, k_Error);

            foreach (Point place in i_CurrentPlayer.ListOfSoliders)
            {
                if (isTheSpecipicSoliderHasLegelMoves(place, i_CurrentPlayer, i_Board, out optionsOfNewPlaceForSlant, out optionsOfNewPlaceForKill, ref isHasKill))
                {
                    if (isHasKill)
                    {
                        optionsOfPlacesWhichCanKill.Add(place);
                        i_CurrentPlayer.IsHasToKill = true;
                    }
                }
            }

            if (optionsOfPlacesWhichCanKill.Count > 0)
            {
                chooseTheSoliderPlaceToMove = randomPoint(optionsOfPlacesWhichCanKill);
                while (!isMoveDone)
                {
                    if (isTheSpecipicSoliderHasLegelMoves(chooseTheSoliderPlaceToMove, i_CurrentPlayer, i_Board, out optionsOfNewPlaceForSlant, out optionsOfNewPlaceForKill, ref isHasKill))
                    {
                        chooseTheNewPlace = randomMoveForSpecipicPlace(optionsOfNewPlaceForKill);
                        isMoveDone = isLegalMove(i_CurrentPlayer, i_Board, chooseTheSoliderPlaceToMove.X, chooseTheSoliderPlaceToMove.Y, chooseTheNewPlace.X, chooseTheNewPlace.Y, ref colOfKilledSolider, ref rowOfKiledSolider, i_CurrentPlayer.IsHasToKill);
                    }
                }
            }
            else
            {
                chooseTheSoliderPlaceToMove = randomPoint(i_CurrentPlayer.ListOfSoliders);
                while (!isMoveDone)
                {
                    if (isTheSpecipicSoliderHasLegelMoves(chooseTheSoliderPlaceToMove, i_CurrentPlayer, i_Board, out optionsOfNewPlaceForSlant, out optionsOfNewPlaceForKill, ref isHasKill))
                    {
                        chooseTheNewPlace = randomMoveForSpecipicPlace(optionsOfNewPlaceForSlant);
                        isMoveDone = isLegalMove(i_CurrentPlayer, i_Board, chooseTheSoliderPlaceToMove.X, chooseTheSoliderPlaceToMove.Y, chooseTheNewPlace.X, chooseTheNewPlace.Y, ref colOfKilledSolider, ref rowOfKiledSolider, i_CurrentPlayer.IsHasToKill);
                    }
                    else
                    {
                        chooseTheSoliderPlaceToMove = randomPoint(i_CurrentPlayer.ListOfSoliders);
                    }
                }
            }

            o_currentCol = chooseTheSoliderPlaceToMove.X;
            o_currentRow = chooseTheSoliderPlaceToMove.Y;
            o_newRow = chooseTheNewPlace.Y;
            o_newCol = chooseTheNewPlace.X;
        }

        private bool isTheSpecipicSoliderHasLegelMoves(Point place, PlayerInformation i_CurrentPlayer, GameBoard i_Board, out List<Point> optionsOfNewPlaceForSlant, out List<Point> optionsOfNewPlaceForKill, ref bool isHasKill)
        {
            bool isHasSlant = false;
            bool o_isHasMove = false;
            isHasSlant = checkIfHasSlant(place, i_Board, i_CurrentPlayer.Solider, out optionsOfNewPlaceForSlant);
            isHasKill = checkIfHasKill(place, i_Board, i_CurrentPlayer.Solider, out optionsOfNewPlaceForKill);
            o_isHasMove = isHasKill || isHasSlant;
            return o_isHasMove;
        }

        private bool checkIfHasSlant(Point i_Place, GameBoard i_Board, char i_SoliderType, out List<Point> o_OptionsOfNewPlace)
        {
            bool o_IfHasSlant;
            List<Point> removeOptionsMoves = new List<Point>();
            o_OptionsOfNewPlace = new List<Point>();
            Point option;
            bool isLegal = false;
            char state;

            if (i_SoliderType == SoliderTypes.OSolider)
            {
                option = new Point(i_Place.X - 1, i_Place.Y + 1);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 1, i_Place.Y + 1);
                o_OptionsOfNewPlace.Add(option);
            }
            else if (i_SoliderType == SoliderTypes.XSolider)
            {
                option = new Point(i_Place.X - 1, i_Place.Y - 1);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 1, i_Place.Y - 1);
                o_OptionsOfNewPlace.Add(option);
            }
            else ////  in situation : i_SoliderType = SoliderTypes.KingO or i_SoliderType = SoliderTypes.KingX 
            {
                option = new Point(i_Place.X - 1, i_Place.Y - 1);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 1, i_Place.Y - 1);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X - 1, i_Place.Y + 1);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 1, i_Place.Y + 1);
                o_OptionsOfNewPlace.Add(option);
            }

            foreach (Point optionPlace in o_OptionsOfNewPlace)
            {
                isLegal = isLegalSlantMove(i_SoliderType, i_Board, out state, i_Place.X, i_Place.Y, optionPlace.X, optionPlace.Y);
                if (!isLegal)
                {
                    removeOptionsMoves.Add(optionPlace);
                }
            }

            foreach (Point removePlace in removeOptionsMoves)
            {
                o_OptionsOfNewPlace.Remove(removePlace);
            }

            if (o_OptionsOfNewPlace.Count == 0)
            {
                o_IfHasSlant = false;
            }
            else
            {
                o_IfHasSlant = true;
            }

            return o_IfHasSlant;
        }

        public bool checkIfHasKill(Point i_Place, GameBoard i_Board, char i_SoliderType, out List<Point> o_OptionsOfNewPlace)
        {
            bool o_IfHasKill;
            List<Point> removeOptionsMoves = new List<Point>();
            int colOfKilledSolider = k_Error;
            int rowOfKilledSolider = k_Error;

            o_OptionsOfNewPlace = new List<Point>();
            Point option;
            bool isLegal = false;
            char state;

            if (i_Board.GetCellInBoard(i_Place.Y, i_Place.X) == SoliderTypes.OSolider)
            {
                option = new Point(i_Place.X - 2, i_Place.Y + 2);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 2, i_Place.Y + 2);
                o_OptionsOfNewPlace.Add(option);
            }
            else if (i_Board.GetCellInBoard(i_Place.Y, i_Place.X) == SoliderTypes.XSolider)
            {
                option = new Point(i_Place.X - 2, i_Place.Y - 2);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 2, i_Place.Y - 2);
                o_OptionsOfNewPlace.Add(option);
            }
            else  ////in situation: i_SoliderType = SoliderTypes.KingO or i_SoliderType = SoliderTypes.KingX
            {
                option = new Point(i_Place.X - 2, i_Place.Y - 2);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 2, i_Place.Y - 2);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X - 2, i_Place.Y + 2);
                o_OptionsOfNewPlace.Add(option);
                option = new Point(i_Place.X + 2, i_Place.Y + 2);
                o_OptionsOfNewPlace.Add(option);
            }

            foreach (Point optionPlace in o_OptionsOfNewPlace)
            {
                isLegal = isLegalKillingSoliderMove(i_SoliderType, i_Board, out state, i_Place.X, i_Place.Y, optionPlace.X, optionPlace.Y, ref rowOfKilledSolider, ref colOfKilledSolider);
                if (!isLegal)
                {
                    removeOptionsMoves.Add(optionPlace);
                }
            }

            foreach (Point removePlace in removeOptionsMoves)
            {
                o_OptionsOfNewPlace.Remove(removePlace);
            }

            if (o_OptionsOfNewPlace.Count == 0)
            {
                o_IfHasKill = false;
            }
            else
            {
                o_IfHasKill = true;
            }

            return o_IfHasKill;
        }

        private Point randomPoint(List<Point> i_listOfSoliders)
        {
            Random rnd = new Random();
            Point choosenPoint;
            int index = rnd.Next(0, i_listOfSoliders.Count);
            choosenPoint = i_listOfSoliders[index];
            return choosenPoint;
        }

        private Point randomMoveForSpecipicPlace(List<Point> i_optionOfMoves)
        {
            Random rnd = new Random();
            Point choosenMove;
            int index = rnd.Next(0, i_optionOfMoves.Count);
            choosenMove = i_optionOfMoves[index];
            return choosenMove;
        }

        public bool ifComputerKeepKilling(char i_SoliderType, Point i_CurrentPlace, GameBoard i_Board, out int o_NextX, out int o_NextY, ref int o_KilledSoliderCol, ref int o_KilledSoliderRow)
        {
            bool o_IfComputerKeepKilling;
            char state;
            List<Point> optionsToKill = new List<Point>();
            bool isHasToKillFromCurrentPlace = checkIfHasKill(i_CurrentPlace, i_Board, i_SoliderType, out optionsToKill);

            if (isHasToKillFromCurrentPlace)
            {
                Point nextPlaceAfterkill = randomMoveForSpecipicPlace(optionsToKill);
                o_NextX = nextPlaceAfterkill.X;
                o_NextY = nextPlaceAfterkill.Y;
                o_IfComputerKeepKilling = isLegalKillingSoliderMove(i_SoliderType, i_Board, out state, i_CurrentPlace.X, i_CurrentPlace.Y, o_NextX, o_NextY, ref o_KilledSoliderCol, ref o_KilledSoliderRow);
            }
            else
            {
                o_NextX = k_Error;
                o_NextY = k_Error;
                o_IfComputerKeepKilling = false;
            }

            return o_IfComputerKeepKilling;

        }
    }
}
