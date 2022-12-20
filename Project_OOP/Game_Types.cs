using System;
namespace Project_OOP
{
    public class GameTypes{
        private static readonly Random RandRating = new Random();
        public abstract class BasicGame{
            public GameTypesNames GameType;
            public int RatingAmount;
            public readonly BasicGameAccount Player;
            public readonly BasicGameAccount Opponent;

            protected BasicGame(BasicGameAccount player, BasicGameAccount opponent){
                RatingAmount = RandRating.Next(35);
                Player = player;
                Opponent = opponent;
            }
        }

        private class NormalGame : BasicGame{
            public NormalGame(BasicGameAccount player, BasicGameAccount opponent) : base(player, opponent)
            {
                GameType = GameTypesNames.Normal;
            }
        }

        private class PvEGame : BasicGame{
            public PvEGame(BasicGameAccount player, BasicGameAccount opponent) : base(player, opponent){
                GameType = GameTypesNames.PvE;
            }
        }

        private class TrainingGame : BasicGame{
            public TrainingGame(BasicGameAccount player, BasicGameAccount opponent) : base(player, opponent)
            {
                GameType = GameTypesNames.Training;
                RatingAmount = 0;
            }
        }

        public class CreateGame{
            public BasicGame CreateNormalGame(BasicGameAccount player, BasicGameAccount opponent){
                return new NormalGame(player, opponent);
            }
            
            public BasicGame CreateTrainingGame(BasicGameAccount player, BasicGameAccount opponent){
                return new TrainingGame(player, opponent);
            }
            
            public BasicGame CreatePvEGame(BasicGameAccount player){
                BasicGameAccount opponent = new BotAccount("Bots", "0000");
                return new PvEGame(player, opponent);
            }
            
        }
    }

    public enum GameTypesNames
    {
        Normal,
        PvE,
        Training
    }
}