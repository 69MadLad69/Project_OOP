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
            string serialaizedAccounts = JsonConvert.SerializeObject(accounts,Formatting.Indented ,settings);
            File.WriteAllText("Accounts.json",serialaizedAccounts);
        }

        // public void DeleteAccountFromDataBase(string username)
        // {
        //     GameAccounts.BasicGameAccount account = FindAccount(username);
        //
        //     if (account != null)
        //     {
        //         Accounts.Remove(account);
        //         SaveAccountsToDataBase(Accounts);
        //     }
        // }

        public List<GameAccounts.BasicGameAccount> LoadAllAccountsFromDataBase()
        {
            var settings = new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.Auto };

            string json = File.ReadAllText("Accounts.json");

            List<GameAccounts.BasicGameAccount> accounts = JsonConvert.DeserializeObject<List<GameAccounts.BasicGameAccount>>(json,settings) != null?
                JsonConvert.DeserializeObject<List<GameAccounts.BasicGameAccount>>(json,settings): new List<GameAccounts.BasicGameAccount>();
            return accounts;
        }
    }
}