using System;
using System.Threading;

namespace Project_OOP
{
    public abstract class MainGame
    {
        private protected readonly char[][] TickTakToe = {"   ".ToCharArray(), "   ".ToCharArray(), "   ".ToCharArray()};
        private static readonly Random RandOrder = new Random();
        private protected readonly GameTypes.CreateGame CreateGame = new GameTypes.CreateGame();
        public virtual void Game(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent, GameTypesNames gameType)
        {
            switch (ChooseOrder()) 
            { 
                case 1: 
                { 
                    Console.WriteLine("As X plays - "+player.UserName); 
                    Console.WriteLine("As 0 plays - "+opponent.UserName);
                    Thread.Sleep(2000);
                    while (true) 
                    { 
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckDraw())
                        {
                            Console.WriteLine("\nIt`s a draw!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Draw);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Draw);
                                    break;
                                }
                            }
                            break;
                        }
                        if (Check0Win())
                        {
                            Console.Clear();
                            Console.WriteLine("\nPlayer "+opponent.UserName+" won!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Lose);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Lose);
                                    break;
                                }
                            }
                            break;
                        }
                        AskX(player);
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckXWin())
                        { 
                            Console.Clear();
                            Console.WriteLine("\nPlayer "+player.UserName+" won!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Win);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Win);
                                    break;
                                }
                            }
                            break;
                        }
                        if (CheckDraw())
                        {
                            Console.WriteLine("\nIt`s a draw!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Draw);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Draw);
                                    break;
                                }
                            } 
                            break;
                        }
                        Ask0(opponent);
                    } 
                    break;
                }
                
                case 2: 
                {
                    Console.WriteLine("As X plays - "+opponent.UserName); 
                    Console.WriteLine("As 0 plays - "+player.UserName);
                    Thread.Sleep(2000);
                    while (true) 
                    { 
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckDraw())
                        {
                            Console.WriteLine("\nIt`s a draw!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Draw);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Draw);
                                    break;
                                }
                            }
                            break;
                        }
                        if (Check0Win())
                        {
                            Console.Clear();
                            Console.WriteLine("\nPlayer "+player.UserName+" won!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Win);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Win);
                                    break;
                                }
                            }
                            break;
                        }
                        AskX(opponent);
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckXWin())
                        { 
                            Console.Clear();
                            Console.WriteLine("\nPlayer "+opponent.UserName+" won!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Lose);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Lose);
                                    break;
                                }
                            }
                            break;
                        }
                        if (CheckDraw())
                        {
                            Console.WriteLine("\nIt`s a draw!");
                            switch (gameType)
                            {
                                case GameTypesNames.Normal:
                                {
                                    DecideGameResult(CreateGame.CreateNormalGame(player,opponent),GameResults.Draw);
                                    break;
                                }

                                case GameTypesNames.Training:
                                {
                                    DecideGameResult(CreateGame.CreateTrainingGame(player,opponent),GameResults.Draw);
                                    break;
                                }
                            } 
                            break;
                        }
                        Ask0(player);
                    } 
                    break;
                }
            }
            ClearTickTakToe();
        }


        private protected int ChooseOrder()
        {
            int order = RandOrder.Next(1, 100);
            if (order < 50)
            {
                return 1;
            }

            return 2;
        }

        private protected bool CheckSpace(int row, int col)
        {
            if (TickTakToe[row][col] == ' ')
            {
                return true;
            }

            return false;
        }

        private protected bool CheckXWin()
        {
            int checker = 0;
            while (checker < 4)
            {
                switch (checker)
                {
                    case 0:
                    {
                        for (int i = 0; i < TickTakToe.Length; i++)
                        {
                            int xcounter = 0;
                            for (int j = 0; j < TickTakToe[0].Length; j++)
                            {
                                if (TickTakToe[i][j] == 'X')
                                {
                                    xcounter++;
                                }

                                if (xcounter == 3)
                                {
                                    return true;
                                }
                            }
                        }

                        break;
                    }

                    case 1:
                    {
                        for (int i = 0; i < TickTakToe.Length; i++)
                        {
                            int xcounter = 0;
                            for (int j = 0; j < TickTakToe[0].Length; j++)
                            {
                                if (TickTakToe[j][i] == 'X')
                                {
                                    xcounter++;
                                }

                                if (xcounter == 3)
                                {
                                    return true;
                                }
                            }
                        }

                        break;
                    }

                    case 2:
                    {
                        int xcounter = 0;
                        for (int i = 0; i < TickTakToe.Length;i++)
                        {
                            if (TickTakToe[i][i] == 'X')
                            {
                                xcounter++;
                            }

                            if (xcounter == 3)
                            {
                                return true;
                            }
                        }
                        break;
                    }

                    case 3:
                    {
                        int xcounter = 0;
                        for (int i = TickTakToe.Length-1; i >= 0; i--)
                        {
                            if (TickTakToe[i][i] == 'X')
                            {
                                xcounter++;
                            }

                            if (xcounter == 3)
                            {
                                return true;
                            }
                        }

                        break;
                    }
                }

                checker++;
            }

            return false;
        }

        private protected bool Check0Win()
        {
            int checker = 0;
            while (checker < 4)
            {
                switch (checker)
                {
                    case 0:
                    {
                        for (int i = 0; i < TickTakToe.Length; i++)
                        {
                            int ocounter = 0;
                            for (int j = 0; j < TickTakToe[0].Length; j++)
                            {
                                if (TickTakToe[i][j] == '0')
                                {
                                    ocounter++;
                                }

                                if (ocounter == 3)
                                {
                                    return true;
                                }
                            }
                        }

                        break;
                    }

                    case 1:
                    {
                        for (int i = 0; i < TickTakToe.Length; i++)
                        {
                            int ocounter = 0;
                            for (int j = 0; j < TickTakToe[0].Length; j++)
                            {
                                if (TickTakToe[j][i] == '0')
                                {
                                    ocounter++;
                                }

                                if (ocounter == 3)
                                {
                                    return true;
                                }
                            }
                        }

                        break;
                    }

                    case 2:
                    {
                        int ocounter = 0;
                        for (int i = 0; i < TickTakToe.Length; i++)
                        {
                            if (TickTakToe[i][i] == '0')
                            {
                                ocounter++;
                            }

                            if (ocounter == 3)
                            {
                                return true;
                            }
                        }

                        break;
                    }

                    case 3:
                    {
                        int ocounter = 0;
                        for (int i = TickTakToe.Length-1; i >= 0;i--)
                        {
                            if (TickTakToe[i][i] == '0')
                            {
                                ocounter++;
                            }

                            if (ocounter == 3)
                            {
                                return true;
                            }
                        }

                        break;
                    }
                }

                checker++;
            }

            return false;
        }

        private protected bool CheckDraw()
        {
            int drawcounter = 0;
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    if (!CheckSpace(i,j))
                    {
                        drawcounter++;
                    }
                }
            }

            if (drawcounter == TickTakToe.Length*TickTakToe[0].Length)
            {
                return true;
            }

            return false;
        }

        private protected void AskX(GameAccounts.BasicGameAccount player)
        {
            Console.WriteLine(player.UserName+" input row, where you want to place X (1-3):");
            int rowChoice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(player.UserName+" input column, where you want to place X (1-3):");
            int colChoice = Convert.ToInt32(Console.ReadLine());
            if (rowChoice > 3 || rowChoice < 1 || colChoice > 3 || colChoice < 1)
            {
                Console.WriteLine("Please input numbers in a range 1-3");
                AskX(player);
            }
            if (CheckSpace(rowChoice-1, colChoice-1))
            {
                TickTakToe[rowChoice - 1][colChoice - 1] = 'X';
            }
            else
            {
                Console.WriteLine("This place is taken. Please choose other.");
                AskX(player);
            }
        }

        private protected void Ask0(GameAccounts.BasicGameAccount player)
        {
            Console.WriteLine(player.UserName+" input row, where you want to place 0 (1-3):");
            int rowChoice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(player.UserName+" input column, where you want to place 0 (1-3):");
            int colChoice = Convert.ToInt32(Console.ReadLine());
            if (rowChoice > 3 || rowChoice < 1 || colChoice > 3 || colChoice < 1)
            {
                Console.WriteLine("Please input numbers in a range 1-3");
                AskX(player);
            }
            if (CheckSpace(rowChoice-1, colChoice-1))
            {
                TickTakToe[rowChoice - 1][colChoice - 1] = '0';
            }
            else
            {
                Console.WriteLine("This place is taken. Please choose other.");
                Ask0(player);
            }
        }

        private protected void ClearTickTakToe()
        {
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    TickTakToe[i][j] = ' ';
                }
            }
        }

        private protected void PrintTickTakToe()
        {
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    Console.Write(TickTakToe[i][j] + " | ");
                }
                Console.Write("\n");
            }
        }

        private protected static void DecideGameResult(GameTypes.BasicGame game, Enum gameResult)
        {
            GameAccounts.BasicGameId++;
            switch (gameResult)
            {
                case GameResults.Win:
                {
                    game.Player.WinGame(game.Opponent.UserName, game);
                    game.Opponent.LoseGame(game.Player.UserName, game);
                    break;
                }

                case GameResults.Lose:
                {
                    game.Player.LoseGame(game.Opponent.UserName, game);
                    game.Opponent.WinGame(game.Player.UserName, game);
                    break;
                }

                case GameResults.Draw:
                {
                    game.Player.DrawGame(game.Opponent.UserName,game);
                    game.Opponent.DrawGame(game.Player.UserName, game);
                    break;
                }
            }
            // if (gameResult.Equals(GameResults.Win))
            // {
            //     game.Player.WinGame(game.Opponent.UserName, game);
            //     game.Opponent.LoseGame(game.Player.UserName, game);
            // }
            // else
            // {
            //     if (gameResult.Equals(GameResults.Lose))
            //     {
            //         game.Player.LoseGame(game.Opponent.UserName, game);
            //         game.Opponent.WinGame(game.Player.UserName, game);
            //     }
            // }
        }
    }
        
    public class PvP : MainGame
    {
    }

    public class PvE : MainGame
    {
        int _row, _col, _order;
        private char _botSign, _playerSign;
        private int CheckStratRows()
        {
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                int takenCounter = 0;
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    if (TickTakToe[i][j] == _playerSign)
                    {
                        takenCounter++;
                    }
                }

                if (takenCounter == 0)
                {
                    return i;
                }
                _col = 0;
            }

            return -1;
        }
        
        private int CheckStratCols()
        {
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                int takenCounter = 0;
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    if (TickTakToe[j][i] == _playerSign)
                    {
                        takenCounter++;
                    }
                }

                if (takenCounter == 0)
                {
                    return i;
                }
                _row = 0;
            }

            return -1;
        }
        
        private bool CheckStratDiag()
        {
            int takenCounter = 0, i;
            for (i = 0; i < TickTakToe.Length; i++)
            {
                if (TickTakToe[i][i] == _playerSign)
                {
                    takenCounter++;
                    _row = 0;
                    break;
                }
            }
            
            if (takenCounter == 0)
            {
                return true;
            }
            
            return false;
        }
        
        private bool CheckStratPobichDiag()
        {
            int takenCounter = 0, i;
            for (i = TickTakToe.Length-1; i > 0; i--)
            {
                if (TickTakToe[i][i] == _playerSign)
                {
                    takenCounter++;
                    _row = TickTakToe.Length-1;
                    break;
                }
            }
            
            if (takenCounter == 0)
            {
                return true;
            }
            
            return false;
        }

        private void PlaceInRandomFreeSpace()
        {
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    if (CheckSpace(i,j))
                    {
                        TickTakToe[i][j] = _botSign;
                        break;
                    }
                }
            }
        }

        private void AskBot()
        {
            if (CheckStratRows() != -1)
            {
                _row = CheckStratRows();
                if (!CheckSpace(_row,_col))
                {
                    _col++;
                }
                TickTakToe[_row][_col] = _botSign;
            }
            else
            {
                _row = 0;
                _col = 0; 
                if (CheckStratCols() != -1)
                {
                    _col = CheckStratCols();
                    if (!CheckSpace(_row,_col))
                    {
                        _row++;
                    }
                    TickTakToe[_row][_col] = _botSign;
                }
                else
                {
                    _row = 0;
                    _col = 0;
                    if (CheckStratDiag())
                    {
                        _row++;
                        TickTakToe[_row][_row] = _botSign;
                    }
                    else
                    {
                        if (CheckStratPobichDiag())
                        {
                            TickTakToe[_row][_row] = _botSign;
                            _row--;
                        }
                        else
                        {
                            PlaceInRandomFreeSpace();
                        }
                    }
                }
            }
        }

        private bool CheckOpponentWinRows()
        {
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                int takenRowCounter = 0, notTakenCol = 0;
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    if (TickTakToe[i][j] == _playerSign)
                    {
                        takenRowCounter++;
                    }
                    else
                    { 
                        notTakenCol = j;   
                    }
                }

                if (takenRowCounter == 2 && CheckSpace(i, notTakenCol))
                {
                    TickTakToe[i][notTakenCol] = _botSign;
                    return true;
                }
            }

            return false;
        }

        private bool CheckOpponentWinCols()
        {
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                int takenColCounter = 0, notTakenRow = 0;
                for (int j = 0; j < TickTakToe[0].Length; j++)
                {
                    if (TickTakToe[j][i] == _playerSign)
                    {
                        takenColCounter++;
                    }
                    else
                    { 
                        notTakenRow = j;   
                    }
                }

                if (takenColCounter == 2 && CheckSpace(notTakenRow, i))
                {
                    TickTakToe[notTakenRow][i] = _botSign;
                    return true;
                }
            }

            return false;
        }

        private bool CheckOpponentWinMainDiagonal()
        {
            int takenDiagCounter = 0, notTakenDiag = 0;
            for (int i = 0; i < TickTakToe.Length; i++)
            {
                if (TickTakToe[i][i] == _playerSign)
                {
                    takenDiagCounter++;
                }
                else
                { 
                    notTakenDiag = i;   
                }
            }
            if (takenDiagCounter == 2 && CheckSpace(notTakenDiag,notTakenDiag))
            {
                TickTakToe[notTakenDiag][notTakenDiag] = _botSign;
                return true;
            }

            return false;
        }

        private bool CheckOpponentWinPobichnaDiagonal()
        {
            int takenDiagCounter = 0, notTakenDiag = 0;
            for (int i = TickTakToe.Length-1; i >= 0; i--)
            {
                if (TickTakToe[i][i] == _playerSign)
                {
                    takenDiagCounter++;
                }
                else
                { 
                    notTakenDiag = i;   
                }
            }
            if (takenDiagCounter == 2 && CheckSpace(notTakenDiag, notTakenDiag))
            {
                TickTakToe[notTakenDiag][notTakenDiag] = _botSign;
                return true;
            }

            return false;
        }

        public override void Game(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent, GameTypesNames gameType)
        {
            _order = ChooseOrder();
            switch (_order)
            {
                case 1:
                {
                    _botSign = '0';
                    _playerSign = 'X';
                    break;
                }

                case 2:
                {
                    _botSign = 'X';
                    _playerSign = '0';
                    break;
                }
            }
            switch (_order) 
            { 
                case 1: 
                { 
                    Console.WriteLine("As X plays - "+player.UserName); 
                    Console.WriteLine("As 0 plays - "+opponent.UserName);
                    Thread.Sleep(2000);
                    while (true) 
                    { 
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckDraw())
                        {
                            Console.WriteLine("\nIt`s a draw!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Draw);
                            break;
                        }
                        if (Check0Win())
                        {
                            Console.Clear();
                            PrintTickTakToe();
                            Console.WriteLine("\nPlayer "+opponent.UserName+" won!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Lose);
                            break;
                        }
                        AskX(player);
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckXWin())
                        { 
                            Console.Clear();
                            Console.WriteLine("\nPlayer "+player.UserName+" won!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Win);
                            break;
                        }
                        if (CheckDraw())
                        {
                            Console.WriteLine("\nIt`s a draw!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Draw);
                            break;
                        }
                        if (!CheckOpponentWinCols() && !CheckOpponentWinRows() && !CheckOpponentWinMainDiagonal() && !CheckOpponentWinPobichnaDiagonal())
                        {
                            AskBot();
                        }
                    } 
                    break;
                }
                
                case 2: 
                {
                    Console.WriteLine("As X plays - "+opponent.UserName); 
                    Console.WriteLine("As 0 plays - "+player.UserName);
                    Thread.Sleep(2000);
                    while (true) 
                    { 
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckDraw())
                        {
                            Console.WriteLine("\nIt`s a draw!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Draw);
                            break;
                        }
                        if (Check0Win())
                        {
                            Console.Clear();
                            PrintTickTakToe();
                            Console.WriteLine("\nPlayer "+player.UserName+" won!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Win);
                            break;
                        }
                        if (!CheckOpponentWinCols() && !CheckOpponentWinRows() && !CheckOpponentWinMainDiagonal() && !CheckOpponentWinPobichnaDiagonal())
                        {
                            AskBot();
                        }
                        Console.Clear();
                        PrintTickTakToe();
                        if (CheckXWin())
                        { 
                            Console.Clear();
                            PrintTickTakToe();
                            Console.WriteLine("\nPlayer "+opponent.UserName+" won!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Lose);
                            break;
                        }
                        if (CheckDraw())
                        {
                            Console.Clear();
                            PrintTickTakToe();
                            Console.WriteLine("\nIt`s a draw!");
                            DecideGameResult(CreateGame.CreatePvEGame(player),GameResults.Draw);
                            break;
                        }
                        Ask0(player);
                    } 
                    break;
                }
            }

            _row = 0;
            _col = 0;
            ClearTickTakToe();
        }
    }


    public enum GameResults{
        Win,
        Lose,
        Draw
    }
}