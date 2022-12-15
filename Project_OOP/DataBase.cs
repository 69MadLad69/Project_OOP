using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace Project_OOP
{
    public class DataBase
    {
        public List<GameAccounts.BasicGameAccount> Accounts = new List<GameAccounts.BasicGameAccount>();
        private static readonly Random RandOpponent = new Random();

        public void LoadData()
        {
            GameTypes.CreateGame createGame = new GameTypes.CreateGame();
            GameAccounts.BasicGameAccount tilt = new GameAccounts.GameAccount("tilt", "notilt");
            GameAccounts.BasicGameAccount oleja = new GameAccounts.GameAccount("Oleja", "olejatilter");
            GameAccounts.BasicGameAccount bober = new GameAccounts.GameAccount("bober", "polska");
            GameAccounts.BasicGameAccount kirgo = new GameAccounts.PrimeAccount("kirgo", "katana");
            GameAccounts.BasicGameAccount chokopie = new GameAccounts.PrimeDeluxeAccount("chokopie", "polish");
            GameAccounts.BasicGameAccount scamenko = new GameAccounts.GameAccount("scamenko", "scam");
            DecideGameResult(createGame.CreateNormalGame(kirgo,bober),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(kirgo,oleja),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(kirgo,chokopie),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(kirgo),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(kirgo),GameResults.Win);
            DecideGameResult(createGame.CreateTrainingGame(kirgo,chokopie),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(chokopie),GameResults.Lose);
            DecideGameResult(createGame.CreatePvEGame(chokopie),GameResults.Lose);
            DecideGameResult(createGame.CreateNormalGame(chokopie,bober),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(chokopie,oleja),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(chokopie,bober),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(chokopie),GameResults.Lose);
            DecideGameResult(createGame.CreateNormalGame(tilt,kirgo),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(tilt,oleja),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(tilt,chokopie),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(tilt,scamenko),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(tilt,bober),GameResults.Win);
            Accounts.Add(tilt);
            Accounts.Add(oleja);
            Accounts.Add(bober);
            Accounts.Add(kirgo);
            Accounts.Add(chokopie);
            Accounts.Add(scamenko);
        }

        // public void SaveData()
        // {
        //     SaveAccountsToDataBase(Accounts);
        // }


        public GameAccounts.PrimeAccount UpgradeToPrime(GameAccounts.BasicGameAccount account)
        {
            GameAccounts.PrimeAccount primeAccount = new GameAccounts.PrimeAccount(account);
            Accounts.Remove(account);
            Accounts.Add(primeAccount);
            return primeAccount;
        }
        
        public GameAccounts.PrimeDeluxeAccount UpgradeToPrimeDeluxe(GameAccounts.BasicGameAccount account)
        {
            GameAccounts.PrimeDeluxeAccount primeDeluxeAccount = new GameAccounts.PrimeDeluxeAccount(account);
            Accounts.Remove(account);
            Accounts.Add(primeDeluxeAccount);
            return primeDeluxeAccount;
        }

        private static void DecideGameResult(GameTypes.BasicGame game, Enum gameResult){
            GameAccounts.BasicGameId++;
            if (gameResult.Equals(GameResults.Win)){
                game.Player.WinGame(game.Opponent.UserName, game);
                game.Opponent.LoseGame(game.Player.UserName, game);
            }else{
                if (gameResult.Equals(GameResults.Lose)){
                    game.Player.LoseGame(game.Opponent.UserName, game);
                    game.Opponent.WinGame(game.Player.UserName, game);
                }
            }
        }

        public GameAccounts.BasicGameAccount ChooseRandomOpponent(GameAccounts.BasicGameAccount player)
        {
            int randI = RandOpponent.Next(0, Accounts.Count);
            if (Accounts[randI] == player)
            {
                ChooseRandomOpponent(player);   
            }
            return Accounts[randI];
        }
        
        public GameAccounts.BasicGameAccount ChooseRandomNewOpponent(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount prevOpponent)
        {
            int randI = RandOpponent.Next(0, Accounts.Count);
            if (Accounts[randI] == player || Accounts[randI] == prevOpponent)
            {
                ChooseRandomOpponent(player);   
            }
            return Accounts[randI];
        }

        public bool CheckPassword(string username, string password)
        {
            foreach (var account in Accounts)
            {
                if (account.UserName == username)
                {
                    if (account.Password == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public GameAccounts.BasicGameAccount FindAccount(string username)
        {
            foreach (var account in Accounts)
            {
                if (account.UserName == username)
                {
                    return account;
                }
            }

            return null;
        }

        public void SaveAccountsToDataBase(List<GameAccounts.BasicGameAccount> accounts)
        {
            var settings = new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.Auto };
            string serialaizedAccounts = JsonConvert.SerializeObject(accounts,settings);
            File.WriteAllText("Accounts.json",serialaizedAccounts);
        }

        public void DeleteAccountFromDataBase(string username)
        {
            GameAccounts.BasicGameAccount account = FindAccount(username);

            if (account != null)
            {
                Accounts.Remove(account);
                SaveAccountsToDataBase(Accounts);
            }
        }

        public List<GameAccounts.BasicGameAccount> LoadAllAccountsFromDataBase()
        {
            var settings = new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.Auto };

            string json = File.ReadAllText("Accounts.json");

            List<GameAccounts.BasicGameAccount> accounts = JsonConvert.DeserializeObject<List<GameAccounts.BasicGameAccount>>(json,settings);

            return accounts;
        }
    }
}