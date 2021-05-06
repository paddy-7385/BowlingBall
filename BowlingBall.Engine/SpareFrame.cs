using BowlingBall.Domain;
using BowlingBall.Domain.Enum;

namespace BowlingBall.Engine
{   
        public class SpareFrame : Frame
        {

            public SpareFrame(int startingPinCount, bool isLastFrame = false) : base(startingPinCount, isLastFrame)
            {
                FrameTypeId = FrameType.Strike;
            }

            public override void RegisterRoll(int knockedDownPins)
            {
                ValidateRoll(knockedDownPins);
                Knocks.Add(knockedDownPins);
                RemainingPins -= knockedDownPins;
                ResetPins();
            }
        
        }

}
