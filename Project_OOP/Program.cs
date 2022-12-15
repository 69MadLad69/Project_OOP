using System;

namespace Project_OOP
{
    internal class Program
    {
        public static void Main(string[] args){
            InterfaceMainMenu mainMenu = new InterfaceMainMenu();
            // oleja.PrintStats();
            // bober.PrintStats();
            // tilt.PrintStats();
            // kirgo.PrintStats();
            // chokopie.PrintStats();
            // scamenko.PrintStats();
            mainMenu.MainMenu();
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
    }
}