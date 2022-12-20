using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Project_OOP.Properties;

namespace Project_OOP
{
    public abstract class BasicGameAccount
    {
            protected static readonly int BasicGameId = 69068;
            private readonly DataBase _dataBase = new();
            private protected static readonly Writer Printer = new(); 
            public string UserName;
            public string Password;
            public AccountTypes AccountType = AccountTypes.Basic;
            [JsonRequired]
            private int _curRating = 1;

            protected int CurrentRating
            {
                get => _curRating;
                set
                {
                    _curRating = value;
                    if (_curRating < 1){
                        _curRating = 1;
                    }
                }
            }
            public readonly List<Game> GameList = new();

            protected BasicGameAccount(string userName, string password)
            {
                UserName = userName;
                Password = password;
            }

            protected BasicGameAccount()
            {
            }

            protected int LastId(int basicId)
            {
                foreach (var account in _dataBase.LoadAllAccountsFromDataBase())
                {
                    foreach (var game in account.GameList)
                    {
                        if (game.GameId > basicId)
                        {
                            basicId = game.GameId;
                        }
                    }
                }
                basicId++;
                return basicId;
            }

            public virtual void WinGame(String opponentName, GameTypes.BasicGame basicGame){
                CurrentRating += basicGame.RatingAmount;
                Game winGame = new Game(LastId(BasicGameId), basicGame.RatingAmount, opponentName, "Win", basicGame.GameType);
                GameList.Add(winGame);
            }

            public virtual void LoseGame(String opponentName,GameTypes.BasicGame basicGame){
                CurrentRating -= basicGame.RatingAmount;
                Game loseGame = new Game(LastId(BasicGameId), -basicGame.RatingAmount, opponentName, "Lose", basicGame.GameType);
                GameList.Add(loseGame);
            }

            public void DrawGame(String opponentName,GameTypes.BasicGame basicGame){
                CurrentRating += 0;
                Game drawGame = new Game(LastId(BasicGameId), 0, opponentName, "Draw", basicGame.GameType);
                GameList.Add(drawGame);
            }

            public virtual void PrintStats(){
                Console.WriteLine();
                Console.WriteLine(UserName+" stats:");
                Console.WriteLine("| Current rating: "+CurrentRating+" | Games played: "+GameList.Count+" | Account type: "+AccountType+" |");
                Printer.Seperator();
                foreach (var game in GameList){
                    Console.WriteLine("|\t Game ID:"+game.GameId+"  \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" \t|");
                }
                Printer.Seperator();
                Console.WriteLine();
            }
    }

        public class GameAccount : BasicGameAccount
        {
            public GameAccount(string userName, string password) : base(userName,password){
            }
        }
        
        public class PrimeAccount : BasicGameAccount
        {
            public PrimeAccount(){}

            public  PrimeAccount(string userName, string password) : base(userName,password)
            {
                AccountType = AccountTypes.Prime;
            }
            
            public PrimeAccount(BasicGameAccount account) : base(account.UserName,account.Password)
            {
                AccountType = AccountTypes.Prime;
                UserName = account.UserName;
                Password = account.Password;
                foreach (var game in account.GameList)
                {
                    GameList.Add(game);
                }
            }

            public override void WinGame(string opponentName, GameTypes.BasicGame basicGame){
                CurrentRating += (int)Math.Round(basicGame.RatingAmount+basicGame.RatingAmount*CheckWinStreak());
                Game winGame = new Game(LastId(BasicGameId), (int)Math.Round(basicGame.RatingAmount+Math.Round(basicGame.RatingAmount*CheckWinStreak())), opponentName, "Win", basicGame.GameType);
                GameList.Add(winGame);
            }

            public override void PrintStats(){
                Printer.TableWidth = 170;
                Console.WriteLine();
                Console.WriteLine(UserName+" stats:");
                Console.WriteLine("| Current rating: "+CurrentRating+" | Games played: "+GameList.Count+" | Account type: "+AccountType+" |");
                Console.WriteLine();
                Printer.Seperator();
                foreach (var game in GameList){
                    if (game.RatingAmount > 0)
                    {
                        int noBonusRating = (int)Math.Round(game.RatingAmount-Math.Round(game.RatingAmount - Math.Round(game.RatingAmount * CheckForPrint(game))) * CheckForPrint(game));
                        Console.WriteLine("|\t Game ID:"+game.GameId+"  \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+noBonusRating+" + "+(int)Math.Round(Math.Round(game.RatingAmount-game.RatingAmount*CheckForPrint(game))*CheckForPrint(game))+") \t |");
                    }
                    else{
                        Console.WriteLine("|\t Game ID:"+game.GameId+"  \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+game.RatingAmount+" + 0) \t |");
                    }
                }
                Printer.Seperator();
                Console.WriteLine();
                Printer.TableWidth = 161;
            }

            private double CheckWinStreak(){
                int streakCounter = 0;
                int i = GameList.Count-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }

            private double CheckForPrint(Game game){
                int streakCounter = 0;
                int i = GameList.IndexOf(game)-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }
        }
        
        public class PrimeDeluxeAccount : BasicGameAccount
        {
            public PrimeDeluxeAccount()
            {
            }

            public PrimeDeluxeAccount(string userName, string password) : base(userName,password){
                AccountType = AccountTypes.PrimeDeluxe;
            }

            public PrimeDeluxeAccount(BasicGameAccount account) : base(account.UserName, account.Password)
            {
                AccountType = AccountTypes.PrimeDeluxe;
                UserName = account.UserName;
                Password = account.Password;
                foreach (var game in account.GameList)
                {
                    GameList.Add(game);
                }
            }

            public override void WinGame(string opponentName, GameTypes.BasicGame basicGame){
                CurrentRating += (int)Math.Round(basicGame.RatingAmount+basicGame.RatingAmount*CheckWinStreak());
                Game winGame = new Game(LastId(BasicGameId), (int)Math.Round(basicGame.RatingAmount+basicGame.RatingAmount*CheckWinStreak()), opponentName, "Win", basicGame.GameType);
                GameList.Add(winGame);
            }

            public override void LoseGame(string opponentName, GameTypes.BasicGame basicGame){
                CurrentRating -= (int)Math.Round(basicGame.RatingAmount-basicGame.RatingAmount*0.25);
                Game loseGame = new Game(LastId(BasicGameId), -(int)Math.Round(basicGame.RatingAmount-basicGame.RatingAmount*0.25), opponentName, "Lose", basicGame.GameType);
                GameList.Add(loseGame);
            }
            
            public override void PrintStats(){
                Printer.TableWidth = 170;
                Console.WriteLine();
                Console.WriteLine(UserName+" stats:");
                Console.WriteLine("| Current rating: "+CurrentRating+" | Games played: "+GameList.Count+" | Account type: "+AccountType+" |");
                Console.WriteLine();
                Printer.Seperator();
                foreach (var game in GameList){
                    if (game.RatingAmount > 0){
                        int noBonusRating = (int)Math.Round(game.RatingAmount-Math.Round(game.RatingAmount - Math.Round(game.RatingAmount * CheckForPrint(game))) * CheckForPrint(game));
                        Console.WriteLine("|\t Game ID:"+game.GameId+"  \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+noBonusRating+" + "+(int)Math.Round(Math.Round(game.RatingAmount-game.RatingAmount*CheckForPrint(game))*CheckForPrint(game))+") \t |");
                    }
                    else{
                        Console.WriteLine("|\t Game ID:"+game.GameId+"  \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+(int)Math.Round(-game.RatingAmount+game.RatingAmount*-0.33)+" - "+(int)Math.Round(game.RatingAmount*-0.33)+") \t |");
                    }
                }
                Printer.Seperator();
                Console.WriteLine();
                Printer.TableWidth = 161;
            }

            private double CheckForPrint(Game game){
                int streakCounter = 0;
                int i = GameList.IndexOf(game)-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }

            private double CheckWinStreak(){
                int streakCounter = 0;
                int i = GameList.Count-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }
        }
        
        public class BotAccount : BasicGameAccount
        {
            public BotAccount(string userName, string password) : base(userName,password)
            {
                AccountType = AccountTypes.Bot;
            }
        }
        public class Game{
            public readonly int GameId;
            public readonly int RatingAmount;
            public readonly string OpponentName;
            public readonly string GameResult;
            public readonly GameTypesNames GameType;

            public Game(int gameId, int ratingAmount, string opponentName, string gameResult, GameTypesNames gameType){
                GameId = gameId;
                RatingAmount = ratingAmount;
                OpponentName = opponentName;
                GameResult = gameResult;
                GameType = gameType;
            }
        }

        public enum AccountTypes
        {
            Basic,
            Prime,
            PrimeDeluxe,
            Bot
        }
}