using BowlingBall.Domain;
using BowlingBall.Domain.Enum;

namespace BowlingBall.Engine
{
    public class NormalFrame : Frame
    {
        public NormalFrame(int startingPinCount, bool isLastFrame = false) : base(startingPinCount,isLastFrame)
        {
            FrameTypeId = FrameType.Normal;
        }

        public override void RegisterRoll(int knockedDownPins)
        {
            ValidateRoll(knockedDownPins);
            Knocks.Add(knockedDownPins);
            RemainingPins -= knockedDownPins;
        }
    }
}
