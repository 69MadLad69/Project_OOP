using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace Project_OOP
{
    public class DataBase
    {
        public List<BasicGameAccount> Accounts = new();
        private static readonly Random RandOpponent = new();
        private Hash _hash = new();

        public GameAccount ReturnToBasic(BasicGameAccount account)
        {
            GameAccount normalAccount = new GameAccount(account);
            Accounts.Remove(account);
            Accounts.Add(normalAccount);
            return normalAccount;
        }

        public PrimeAccount UpgradeToPrime(BasicGameAccount account)
        {
            PrimeAccount primeAccount = new PrimeAccount(account);
            Accounts.Remove(account);
            Accounts.Add(primeAccount);
            return primeAccount;
        }
        
        public PrimeDeluxeAccount UpgradeToPrimeDeluxe(BasicGameAccount account)
        {
            PrimeDeluxeAccount primeDeluxeAccount = new PrimeDeluxeAccount(account);
            Accounts.Remove(account);
            Accounts.Add(primeDeluxeAccount);
            return primeDeluxeAccount;
        }
        
        public BasicGameAccount ChooseRandomOpponent(String username)
        {
            List<BasicGameAccount> otherAccounts = Accounts.Where(account => account.UserName != username).ToList();
            int randI = RandOpponent.Next(0, otherAccounts.Count);
            return otherAccounts[randI];
        }
        
        public BasicGameAccount ChooseRandomNewOpponent(String playerUsername, String prevOpponentUsername)
        {
            List<BasicGameAccount> otherAccounts = Accounts.Where(account => account.UserName != playerUsername && account.UserName != prevOpponentUsername).ToList();
            int randI = RandOpponent.Next(0, otherAccounts.Count);
            return otherAccounts[randI];
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

        public BasicGameAccount FindAccount(string username)
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

        public void SaveAccountsToDataBase(List<BasicGameAccount> accounts)
        {
            var settings = new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.Auto };
            string serialaizedAccounts = JsonConvert.SerializeObject(accounts,Formatting.Indented ,settings);
            File.WriteAllText("Accounts.json",serialaizedAccounts);
        }

        // public void DeleteAccountFromDataBase(string username)
        // {
        //     BasicGameAccount account = FindAccount(username);
        //
        //     if (account != null)
        //     {
        //         Accounts.Remove(account);
        //         SaveAccountsToDataBase(Accounts);
        //     }
        // }

        public List<BasicGameAccount> LoadAllAccountsFromDataBase()
        {
            var settings = new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.Auto };

            string json = File.ReadAllText("Accounts.json");

            List<BasicGameAccount> accounts = JsonConvert.DeserializeObject<List<BasicGameAccount>>(json,settings) != null?
                JsonConvert.DeserializeObject<List<BasicGameAccount>>(json,settings): new List<BasicGameAccount>();
            return accounts;
        }
    }
}