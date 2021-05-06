using System;
using BowlingBall;

namespace BowlingBallScoreCalculator
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                var game = new Game();
                AcceptScores(game);

                Console.WriteLine($"Total score = {game.GetScore()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while playing: {ex.Message}");
                throw;
            }
        }

        public static void AcceptScores(Game game)
        {
            int bowlOne, bowlTwo, bonus;
            for (int frame = 1; frame <= 10; frame++)
            {
                bowlOne = bowlTwo = bonus = 0;
                Console.WriteLine($"Please Enter your scores for Frame {frame}");
                Console.WriteLine($"Bowl 1:");
                bowlOne = int.Parse(Console.ReadLine());
                game.Roll(bowlOne);

                if (bowlOne < 10 || (frame == 10 && bowlOne == 10))
                {
                    Console.WriteLine($"Bowl 2:");
                    bowlTwo = int.Parse(Console.ReadLine());
                    game.Roll(bowlTwo);
                }

                if (frame == 10 && bowlOne + bowlTwo >= 10)
                {
                    Console.WriteLine($"Got an extra chance");
                    Console.WriteLine($"Bowl 3:");
                    bonus = int.Parse(Console.ReadLine());
                    game.Roll(bonus);
                }
            }
        }

    }
}