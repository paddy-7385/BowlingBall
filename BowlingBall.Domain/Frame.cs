using BowlingBall.Domain.Enum;
using System;
using System.Collections.Generic;

namespace BowlingBall.Domain
{
    public abstract class Frame
    {
        #region "Public properties"

        /// <summary>
        /// How many pins have been knocked down in each roll
        /// </summary>
        public List<int> Knocks { get; } = new List<int>();

        /// <summary>
        /// No more rolls can be registered on a closed Frame
        /// </summary>
        public bool IsClosed => !isLastFrame && RemainingPins == 0 ||
                                !isLastFrame && Knocks.Count == 2 ||
                                Knocks.Count == 3;


        public bool IsStrike { get { return FrameTypeId == FrameType.Strike; } }

        public bool IsSpare { get { return FrameTypeId == FrameType.Spare; } }

        #endregion

        #region "Protected members"

        protected int RemainingPins;
        protected readonly int IntialPinCount;
        protected readonly bool isLastFrame;
        protected bool extraRollAllowed;
        protected FrameType FrameTypeId { get; set; }

        #endregion

        #region "Constructor"

        /// <summary>
        /// Create a new Frame
        /// </summary>
        /// <param name="startingPinCount">Number of pins that the Frame should start with</param>
        /// <param name="isLastFrame">Special rules apply on the last frame</param>
        public Frame(int startingPinCount, bool isLastFrame = false)
        {
            this.IntialPinCount = startingPinCount;
            RemainingPins = startingPinCount;
            this.isLastFrame = isLastFrame;
        }

        #endregion
      
        #region "private method"
       
        protected void ValidateRoll(int knockedDownPins)
        {
            if (RemainingPins == 0)
            {
                throw new InvalidOperationException("Can't roll when there are no standing pins");
            }

            if (!isLastFrame && Knocks.Count == 2 || isLastFrame && Knocks.Count == 2 && !extraRollAllowed || Knocks.Count > 2)
            {
                throw new InvalidOperationException($"Can't register more than {Knocks.Count} rolls in this frame");
            }

            if (knockedDownPins < 0 || knockedDownPins > RemainingPins)
            {
                throw new InvalidOperationException($"Can't knock down {knockedDownPins} while there are only {RemainingPins} standing pins");
            }
        }

        protected void ResetPins()
        {
            if (isLastFrame && RemainingPins == 0)
            {
                RemainingPins = IntialPinCount;
                extraRollAllowed = true;
            }
        }

        #endregion

        #region "abstract methods"

        /// <summary>
        /// Register the result of a roll
        /// </summary>
        /// <param name="knockedDownPins">How many pins have been knocked down</param>
        public abstract void RegisterRoll(int knockedDownPins);

        #endregion
    }
}
