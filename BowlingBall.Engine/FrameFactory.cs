using BowlingBall.Domain;
using BowlingBall.Domain.Enum;
using System.Collections.Generic;

namespace BowlingBall.Engine
{
    public class FrameFactory
    {
        public static Frame CreateFrame(FrameType frameType, int intialPins, bool isLastFrame)
        {
            if (frameType == FrameType.Strike)
            {
                return new StrikeFrame(intialPins, isLastFrame);
            }
            else if (frameType == FrameType.Spare)
            {
                return new SpareFrame(intialPins, isLastFrame);
            }
            else
            {
                return new NormalFrame(intialPins, isLastFrame);
            }
        }
    }
}
