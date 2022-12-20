namespace Project_OOP
{
    public class Creator
    {
        private readonly DataBase _dataBase = new();
        public void CreateAccount(string username, string password, AccountTypes accountType)
        {
            if (username != null && password != null)
            {
                BasicGameAccount newAccount = new GameAccount(username,password);
                switch (accountType)
                {
                    case AccountTypes.Basic:
                    {
                        newAccount = new GameAccount(username.Trim(),password.Trim());
                        break;
                    }

                    case AccountTypes.Prime:
                    {
                        newAccount = new PrimeAccount(username.Trim(),password.Trim());
                        break;
                    }

                    case AccountTypes.PrimeDeluxe:
                    {
                        newAccount = new PrimeDeluxeAccount(username.Trim(), password.Trim());
                        break;
                    }

                }

                _dataBase.Accounts = _dataBase.LoadAllAccountsFromDataBase();
                _dataBase.Accounts.Add(newAccount);
                _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
            }
        }
    }
}