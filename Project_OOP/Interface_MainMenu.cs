using System;
using System.Threading;
using Project_OOP.Properties;

namespace Project_OOP
{
    public class InterfaceMainMenu{
        private readonly DataBase _dataBase = new();
        private readonly MainGame _gamePvP = new PvP();
        private readonly MainGame _gamePvE = new PvE();
        private readonly Writer _writer = new();
        private readonly Creator _creator = new();
        public void MainMenu()
        {
            _dataBase.Accounts = _dataBase.LoadAllAccountsFromDataBase();
            _writer.PrintTitle("WELCOME TO TICTACTOE SHARP");
            _writer.PrintOptionsRow("1.Log in",
                                    "2.Sign up",
                                    "3.Exit");
            int choice = ParseChoiseToInt(Console.ReadLine());

            switch (choice)
            {
                case 1:{
                    LogInMenu();
                    break;
                }

                case 2:{
                    SignUpMenu();
                    break;
                }

                case 3:{
                    Environment.Exit(0);
                    break;
                }

                default:{
                    _writer.PrintTitle("Error! Wrong input! Please try again!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    MainMenu();
                    break;
                }
            }
        }

        private void SignUpMenu()
        {
            Console.Clear();
            _writer.PrintTitle("Please enter Username");
            String username = Console.ReadLine();
            if (!SignUpUsernameCheck(username))
            {
                _writer.PrintTitle("Error! Account with such Username already exists! Please input other Username!");
                Thread.Sleep(2500);
                SignUpMenu();
            }
            _writer.PrintTitle("Please enter Password");
            String password = Console.ReadLine();
            if (username != "" && password != "")
            {
                _creator.CreateAccount(username, password,ChooseAccountType());
                _writer.PrintTitle("You have successfully created new account!");
                Thread.Sleep(2000);
                Console.Clear();
                MainMenu();  
            }
            else
            {
                Console.Clear();
                _writer.PrintTitle("Error! You didn`t input Username or Password! Please try again!");
                Thread.Sleep(2000);
                SignUpMenu();
            }
        }
        
        private AccountTypes ChooseAccountType()
        {
            _writer.PrintTitle("Choose account version");
            _writer.PrintOptionsRow("1.Basic",
                "2.Prime",
                "3.PrimeDeluxe");
            while (true)
            {
                int typeChoise = ParseChoiseToInt(Console.ReadLine());
                switch (typeChoise)
                {
                    case 1:
                    {
                        return AccountTypes.Basic;
                    }

                    case 2:
                    {
                        return AccountTypes.Prime;
                    }

                    case 3:
                    {
                        return AccountTypes.PrimeDeluxe;
                    }
                }
            }
        }

        private bool SignUpUsernameCheck(string username)
        {
            foreach (var account in _dataBase.Accounts)
            {
                if (account.UserName == username.Trim())
                {
                    return false;
                }
            }
            return true;
        }

        private void LogInMenu()
        {
            Console.Clear();
            _writer.PrintTitle("Please enter Username");
            String username = Console.ReadLine();
            _writer.PrintTitle("Please enter Password");
            String password = Console.ReadLine();
            if (password != null && username != null)
            {
                if (_dataBase.CheckPassword(username.Trim(), password.Trim()))
                {
                    _writer.PrintTitle("You have successfully logged into "+username+" account!");
                    Thread.Sleep(2000);
                    AccountMenu(_dataBase.FindAccount(username));
                }
                else
                {
                    Thread.Sleep(2000);
                    Console.Clear();
                    _writer.PrintTitle("Error! Either Username or Password doesn`t exist in DataBase! Please try again!");
                    _writer.PrintOptionsRow("1.Try again", 
                                            "2.Return to main menu");
                    while (true)
                    {
                        int logInChoice = ParseChoiseToInt(Console.ReadLine());
                        switch (logInChoice)
                        {
                            case 1:
                            {
                                LogInMenu();
                                break;
                            }

                            case 2:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                _writer.PrintTitle("Error! You didn`t input Username or Password! Please try again!"); 
                LogInMenu();
            }
        }
        
        public void AccountMenu(BasicGameAccount account)
        {
            Console.Clear();
            _writer.PrintTitle("TICKTACKTOE SHARP");
            _writer.PrintOptionsRow("1.Play",
                                    "2.See stats",
                                    "3.Log out",
                                    "4.Shop",
                                    "5.Exit");
            int accountMenuChoice = ParseChoiseToInt(Console.ReadLine());
            switch (accountMenuChoice)
            {
                case 1:
                {
                    PlayMenu(account);
                    break;
                }

                case 2:
                {
                    Console.Clear();
                    if (account.AccountType == AccountTypes.Prime || account.AccountType == AccountTypes.PrimeDeluxe)
                    {
                        _writer.TableWidth = 170;
                    }
                    account.PrintStats();
                    _writer.PrintTitle("To return to Account menu input 1");
                    while (true)
                    {
                        int accountMenuReturnChoise = ParseChoiseToInt(Console.ReadLine());
                        if (accountMenuReturnChoise == 1)
                        {
                            Console.Clear();
                            _writer.TableWidth = 161;
                            AccountMenu(account);
                        }
                    }
                }

                case 3:
                {   
                    Console.Clear();
                    MainMenu();
                    break;
                }
                
                case 4:
                {
                    Console.Clear();
                    _writer.PrintTitle("Shop");
                    _writer.PrintOptionsRow("1.Prime",
                                            "2.PrimeDeluxe",
                                            "3.Go back");
                    while (true)
                    {
                        int upgradeMenuChoice = ParseChoiseToInt(Console.ReadLine());
                        switch (upgradeMenuChoice)
                        {
                            case 1:
                            {
                                account = _dataBase.UpgradeToPrime(account);
                                _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                                AccountMenu(account);
                                break;
                            }

                            case 2:
                            {
                                account = _dataBase.UpgradeToPrimeDeluxe(account);
                                _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                                AccountMenu(account);
                                break;
                            }

                            case 3:
                            {
                                Console.Clear();
                                AccountMenu(account);
                                break;
                            }
                        }
                    }
                }
                    
                case 5:{
                    Environment.Exit(0);
                    break;
                }

                default:{
                    _writer.PrintTitle("Error! Wrong input! Please try again!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    AccountMenu(account);
                    break;
                }
            }
        }

        private void AfterGameMenu(BasicGameAccount player, BasicGameAccount opponent, GameTypesNames gameType)
        {
            _writer.PrintTitle("The game ended, please choose what to do next");
            if (gameType != GameTypesNames.PvE)
            {
                _writer.PrintOptionsRow("1.Play again",
                                        "2.Play again against the same opponent",
                                        "3.Return to account menu");
            }
            else
            {
                _writer.PrintOptionsRow("1.Play again",
                                        "2.Return to account menu");
            }
            while (true)
            {
                int afterGameChoice = ParseChoiseToInt(Console.ReadLine());
                if (gameType != GameTypesNames.PvE)
                {
                    switch (afterGameChoice)
                    {
                        case 1:
                        {
                            if (_dataBase.Accounts.Count < 3)
                            {
                                _writer.PrintTitle("Error! Sorry, but there only one opponent you can play against. Wait for more players please.");
                                Console.Clear();
                                AfterGameMenu(player,opponent,gameType);
                            }
                            else
                            {
                                BasicGameAccount newOpponent = _dataBase.ChooseRandomNewOpponent(player.UserName,opponent.UserName);
                                _gamePvP.Game(player, newOpponent,gameType);
                                AfterGameMenu(player, newOpponent,gameType);
                            }
                            break;
                        }

                        case 2:
                        {
                            _gamePvP.Game(player,opponent,gameType);
                            AfterGameMenu(player,opponent,gameType);
                            break; 
                        }

                        case 3:
                        {   
                            Console.Clear();
                            AccountMenu(player);
                            break;
                        }
                    } 
                }
                else
                {
                    switch (afterGameChoice)
                    {
                        case 1:
                        {
                            _gamePvE.Game(player,opponent,gameType);
                            AfterGameMenu(player,opponent,gameType);
                            break; 
                        }

                        case 2:
                        {   
                            Console.Clear();
                            AccountMenu(player);
                            break;
                        }
                    }
                }
            }
        }

        private void PlayMenu(BasicGameAccount account){
            _writer.PrintTitle("Choose game mod");
            _writer.PrintOptionsRow("1.Normal",
                                    "2.Training",
                                    "3.PvE",
                                    "4.Go back to account menu");
            while (true)
            {
                int playMenuChoice = ParseChoiseToInt(Console.ReadLine());
                switch (playMenuChoice)
                {
                    case 1:
                    {
                        if (_dataBase.Accounts.Count < 2)
                        {
                            _writer.PrintTitle("Error! Sorry, but you`re the only player in the game. Wait for more players or choose PvE mode.");
                            Thread.Sleep(2000);
                            Console.Clear();
                            PlayMenu(account);
                        }

                        BasicGameAccount opponent = _dataBase.ChooseRandomOpponent(account.UserName);
                        _gamePvP.Game(account, opponent, GameTypesNames.Normal);
                        _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                        AfterGameMenu(account,opponent, GameTypesNames.Normal);
                        break;
                    }

                    case 2:
                    {
                        if (_dataBase.Accounts.Count < 2)
                        {
                            _writer.PrintTitle("Error! Sorry, but you`re the only player in the game. Wait for more players or choose PvE mode.");
                            Thread.Sleep(2000);
                            Console.Clear();
                            PlayMenu(account);
                        }
                        
                        BasicGameAccount opponent = _dataBase.ChooseRandomOpponent(account.UserName);
                        _gamePvP.Game(account, opponent, GameTypesNames.Training);
                        _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                        AfterGameMenu(account,opponent, GameTypesNames.Training);
                        break; 
                    }

                    case 3:
                    {
                        BasicGameAccount opponent = new BotAccount("Bot", "0000");
                        _gamePvE.Game(account, opponent, GameTypesNames.PvE);
                        _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                        AfterGameMenu(account,opponent, GameTypesNames.PvE);
                        break;
                    }

                    case 4:
                    {
                        Console.Clear();
                        AccountMenu(account);
                        break;
                    }
                }   
            }
        }

        private static int ParseChoiseToInt(string choiseString)
        { 
            int choice = 0;
            try
            {
                choice = int.Parse(choiseString ?? string.Empty);
            }
            catch (FormatException)
            {
            }

            return choice;
        }
    }
}