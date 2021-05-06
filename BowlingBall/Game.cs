using BowlingBall.Domain;
using BowlingBall.Domain.Enum;
using BowlingBall.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingBall
{
    public class Game
    {
        private const int MaxFrameCount = 10;

        /// <summary>
        /// Number of pins that each Frame should start with
        /// </summary>
        private const int StartingPinCount = 10;

        private readonly List<Frame> Frames = new List<Frame>();

        private Frame CurrentFrame = null;
        private List<int> _Rolls = new List<int>();
        private int score = 0;
        public void Roll(int pins)
        {
            if (Frames.Count > MaxFrameCount && CurrentFrame != null && CurrentFrame.IsClosed)
            {
                throw new InvalidOperationException("You've played enough for today! Consider calling Score()");
            }
            var frameType = FrameType.Unknown;
            var lastFrame = Frames.Count == MaxFrameCount ;

            if (!Frames.Any() || CurrentFrame.IsClosed)
            {
                _Rolls.Add(pins);

                if (_Rolls.Count == 1 && _Rolls[0] == 10)   // Strike in First attempt
                {
                    frameType = FrameType.Strike;
                }
                else if (_Rolls.Count == 2 && _Rolls[0] + _Rolls[1] == 10) // Spare attempt
                {
                    frameType = FrameType.Spare;
                }
                else if (_Rolls.Count == 2)  // Normal 
                {
                    frameType = FrameType.Normal;
                }

                if(CurrentFrame != null && lastFrame && CurrentFrame.IsStrike && CurrentFrame.Knocks.Count<= 3)
                {
                    CurrentFrame.Knocks.Add(pins);
                }

                if (CurrentFrame != null && lastFrame && CurrentFrame.IsSpare && CurrentFrame.Knocks.Count <= 2)
                {
                    CurrentFrame.Knocks.Add(pins);
                }
            }

            if (frameType != FrameType.Unknown && !lastFrame)
            {
                CurrentFrame = FrameFactory.CreateFrame(frameType, StartingPinCount, lastFrame);
                Frames.Add(CurrentFrame);
                foreach (var roll in _Rolls)
                {
                    CurrentFrame.RegisterRoll(roll);
                }
                _Rolls.Clear();
            }

        }

        public int GetScore()
        {
            for (var frameIndex = 0; frameIndex < Frames.Count; frameIndex++)
            {
                var frame = Frames[frameIndex];
                var frameScore = 0;
                var bonusScore = 0;

                // cap the roll index to 2 to avoid over-counting points if the last frame has bonus rolls
                var maxRollIndex = frame.Knocks.Count < 2 ? frame.Knocks.Count : 2;

                for (var rollIndex = 0; rollIndex < maxRollIndex; rollIndex++)
                {
                    var result = frame.Knocks[rollIndex];
                    frameScore += result;

                    // calculate bonus score for a strike
                    if (frame.IsStrike)
                    {
                        // look 2 rolls ahead
                        bonusScore += CalculateBonusScore(frameIndex, rollIndex, 2);
                        break;
                    }
                }

                // calculate bonus score for a spare
                if (frame.IsSpare)
                {
                    // look 1 roll ahead
                    bonusScore += CalculateBonusScore(frameIndex, maxRollIndex - 1, 1);
                }
                score += frameScore + bonusScore;
            }

            return score;
        }

        private int CalculateBonusScore(int frameIndex, int rollIndex, int rollCount)
        {
            var bonusPoints = 0;
            if (rollCount == 0)
            {
                return 0;
            }

            // add the next roll in the same frame, if any
            if (Frames[frameIndex].Knocks.Count > rollIndex + 1)
            {
                bonusPoints += Frames[frameIndex].Knocks[rollIndex + 1];
                bonusPoints += CalculateBonusScore(frameIndex, rollIndex + 1, rollCount - 1);
            }
            else
            {
                // add the first roll of the next frame, if any
                if (Frames.Count > frameIndex + 1)
                {
                    bonusPoints += Frames[frameIndex + 1].Knocks[0];
                    bonusPoints += CalculateBonusScore(frameIndex + 1, 0, rollCount - 1);
                }

            }

            return bonusPoints;
        }

    }
}
