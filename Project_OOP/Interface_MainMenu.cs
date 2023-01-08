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
        private readonly AccountCreator _accountCreator = new();
        private readonly Hash _hash = new();
        public void MainMenu()
        {
            _dataBase.Accounts = _dataBase.LoadAllAccountsFromDataBase();
            _writer.PrintTitle("WELCOME TO TICTACTOE SHARP");
            _writer.PrintOptionsRow("1.Log in",
                                    "2.Sign up",
                                    "3.Exit");
            int choice = IntValidator.ParseChoiceToInt(Console.ReadLine());

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
            if (!_dataBase.SignUpUsernameCheck(username))
            {
                _writer.PrintTitle("Error! Account with such Username already exists! Please input other Username!");
                Thread.Sleep(2500);
                SignUpMenu();
            }
            _writer.PrintTitle("Please enter Password");
            String password = Console.ReadLine();
            if (username != "" && password != "")
            {
                _accountCreator.CreateAccount(username, _hash.HashPassword(password), ChooseAccountType());
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
                int typeChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
                switch (typeChoice)
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
        

        private void LogInMenu()
        {
            Console.Clear();
            _writer.PrintTitle("Please enter Username");
            String username = Console.ReadLine();
            _writer.PrintTitle("Please enter Password");
            String password = Console.ReadLine();
            if (password != null && username != null)
            {
                if (_dataBase.CheckPassword(username.Trim(), _hash.HashPassword(password.Trim())))
                {
                    _writer.PrintTitle("You have successfully logged into "+username+" account!");
                    Thread.Sleep(2000);
                    Console.Clear();
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
                        int logInChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
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

        private void AccountMenu(BasicGameAccount account)
        {
            Console.Clear();
            _writer.PrintTitle("TICKTACKTOE SHARP");
            _writer.PrintOptionsRow("1.Play",
                                    "2.See stats",
                                    "3.Log out",
                                    "4.Shop",
                                    "5.Exit");
            int accountMenuChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
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
                    _writer.ChangeTableWidth(161);
                    if (account.AccountType == AccountTypes.Prime || account.AccountType == AccountTypes.PrimeDeluxe)
                    {
                        _writer.ChangeTableWidth(170);
                    }
                    account.PrintStats();
                    _writer.PrintTitle("To return to Account menu input 1");
                    while (true)
                    {
                        int accountMenuReturnChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
                        if (accountMenuReturnChoice == 1)
                        {
                            Console.Clear();
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
                    ShopMenu(account);
                    break;
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

        private void ShopMenu(BasicGameAccount account)
        {
            Console.Clear();
            _writer.PrintTitle("Shop");
            switch (account.AccountType)
            {
                case AccountTypes.Basic:
                {
                    _writer.PrintOptionsRow("1.Prime", "2.PrimeDeluxe", "3.Go back");
                    break;
                }

                case AccountTypes.Prime:
                {
                    _writer.PrintOptionsRow("1.Normal", "2.PrimeDeluxe", "3.Go back");
                    break;
                }

                case AccountTypes.PrimeDeluxe:
                {
                    _writer.PrintOptionsRow("1.Normal", "2.Prime", "3.Go back");
                    break;
                }
            }

            while (true)
            {
                AccountTypes? accountType = UpgradeChoice(account);
                switch (accountType)
                {
                    case AccountTypes.Basic:
                    {
                        account = _dataBase.ReturnToBasic(account);
                        _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                        AccountMenu(account);
                        break;
                    }

                    case AccountTypes.Prime:
                    {
                        account = _dataBase.UpgradeToPrime(account);
                        _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                        AccountMenu(account);
                        break;
                    }

                    case AccountTypes.PrimeDeluxe:
                    {
                        account = _dataBase.UpgradeToPrimeDeluxe(account);
                        _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
                        AccountMenu(account);
                        break;
                    }
                }
            }
        }

        private AccountTypes? UpgradeChoice(BasicGameAccount account)
        {
            int upgradeMenuChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
            switch (account.AccountType)
            {
                case AccountTypes.Basic:
                {
                    switch (upgradeMenuChoice)
                    {
                        case 1:
                        {
                            return AccountTypes.Prime;
                        }

                        case 2:
                        {
                            return AccountTypes.PrimeDeluxe;
                        }
                        
                        case 3:
                        {
                            Console.Clear();
                            AccountMenu(account);
                            break;
                        }
                    }
                    break;
                }

                case AccountTypes.Prime:
                {
                    switch (upgradeMenuChoice)
                    {
                        case 1:
                        {
                            Console.Clear();
                            _writer.PrintTitle("This AccountType is worse than owned. Do you really wish to change?");
                            _writer.PrintOptionsRow("1.Yes",
                                                    "2.No");
                            while (true)
                            {
                                int confirmChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
                                switch (confirmChoice)
                                {
                                    case 1:
                                    {
                                        return AccountTypes.Basic;
                                    }

                                    case 2:
                                    {
                                        ShopMenu(account);
                                        break;
                                    }
                                }
                            }
                        }

                        case 2:
                        {
                            return AccountTypes.PrimeDeluxe;
                        }
                        
                        case 3:
                        {
                            Console.Clear();
                            AccountMenu(account);
                            break;
                        }
                    }
                    break;
                }
                
                case AccountTypes.PrimeDeluxe:
                {
                    switch (upgradeMenuChoice)
                    {
                        case 1:
                        {
                            Console.Clear();
                            _writer.PrintTitle("This AccountType is worse than owned. Do you really wish to change?");
                            _writer.PrintOptionsRow("1.Yes",
                                                    "2.No");
                            while (true)
                            {
                                int confirmChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
                                switch (confirmChoice)
                                {
                                    case 1:
                                    {
                                        return AccountTypes.Basic;
                                    }

                                    case 2:
                                    {
                                        ShopMenu(account);
                                        break;
                                    }
                                }
                            }
                        }

                        case 2:
                        {
                            Console.Clear();
                            _writer.PrintTitle("This AccountType is worse than owned. Do you really wish to change?");
                            _writer.PrintOptionsRow("1.Yes",
                                                    "2.No");
                            while (true)
                            {
                                int confirmChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
                                switch (confirmChoice)
                                {
                                    case 1:
                                    {
                                        return AccountTypes.Prime;
                                    }

                                    case 2:
                                    {
                                        ShopMenu(account);
                                        break;
                                    }
                                }
                            }
                        }
                        
                        case 3:
                        {
                            Console.Clear();
                            AccountMenu(account);
                            break;
                        }
                    }
                    break;
                }
            }

            return null;
        }

        private void AfterGameMenu(BasicGameAccount player, BasicGameAccount opponent, GameTypesNames gameType)
        {
            _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
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
                int afterGameChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
                if (gameType != GameTypesNames.PvE)
                {
                    switch (afterGameChoice)
                    {
                        case 1:
                        {
                            if (_dataBase.Accounts.Count < 3)
                            {
                                _writer.PrintTitle("Error! Sorry, but there only one opponent you can play against. Wait for more players please.");
                                Thread.Sleep(2000);
                                Console.Clear();
                                AfterGameMenu(player,opponent,gameType);
                            }
                            else
                            {
                                BasicGameAccount newOpponent = _dataBase.ChooseRandomNewOpponent(player.UserName,opponent.UserName);
                                while (!ConfirmOpponent(player, newOpponent)){}
                                _gamePvP.Game(player, newOpponent,gameType);
                                AfterGameMenu(player, newOpponent,gameType);
                            }
                            break;
                        }

                        case 2:
                        {
                            while (!ConfirmOpponent(player, opponent)){}
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
                                    "4.Go back");
            while (true)
            {
                int playMenuChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
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
                        while (!ConfirmOpponent(account, opponent)){}
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
                        while (!ConfirmOpponent(account, opponent)){}
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

        private bool ConfirmOpponent(BasicGameAccount player,BasicGameAccount opponent)
        {
            Console.Clear();
            _writer.PrintTitle("User "+opponent.UserName+" please enter Password to accept duel");
            String password = Console.ReadLine();
            if (password != null)
            {
                if (_hash.HashPassword(password) == opponent.Password)
                {
                    _writer.PrintTitle(opponent.UserName+" have successfully accepted duel!");
                    Thread.Sleep(2000);
                    return true;
                }
            }
            Thread.Sleep(2000);
            Console.Clear();
            _writer.PrintTitle("Error! Password doesn`t exist in DataBase!");
            _writer.PrintOptionsRow("1.Try again", 
                                    "2.Decline duel");
            while (true)
            {
                int duelChoice = IntValidator.ParseChoiceToInt(Console.ReadLine());
                switch (duelChoice)
                {
                    case 1:
                    {
                        ConfirmOpponent(player, opponent);
                        break;
                    }

                    case 2:
                    {
                        Console.Clear();
                        _writer.PrintTitle("Opponent "+opponent.UserName+ " declined duel");
                        Thread.Sleep(2000);
                        AccountMenu(player);
                        break;
                    }
                }
            }
        }
    }
}