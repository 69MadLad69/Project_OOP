namespace Project_OOP
{
    public class Creator
    {
        private readonly DataBase _dataBase = new();
        public void CreateAccount(string username, string password)
        {
            if (username != null && password != null)
            {
                BasicGameAccount newAccount = new GameAccount(username.Trim(),password.Trim());
                _dataBase.Accounts.Add(newAccount);
                _dataBase.SaveAccountsToDataBase(_dataBase.Accounts);
            }
        }
    }
}