using EuroMoney.Library.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMoney.Library.Helpers
{
    public static class DirectionHelper
    {
        /// <summary>
        /// Rotate the supplied direction clockwise according to a compass.
        /// </summary>
        /// <param name="direction">The direction to convert.</param>
        /// <returns>The cardinal direction clockwise on the compass.</returns>
        public static CardinalDirection RotateClockwise(CardinalDirection direction)
        {
            if (direction == CardinalDirection.West)
            {
                // The compass has done a whole loop, return north //
                return CardinalDirection.North;
            }
            else
            {
                return ++direction;
            }            
        }

        /// <summary>
        /// Rotate the supplied direction counter-clockwise according to a compass.
        /// </summary>
        /// <param name="direction">The direction to convert.</param>
        /// <returns>The cardinal direction counter-clockwise on the compass.</returns>
        public static CardinalDirection RotateCounterClockwise(CardinalDirection direction)
        {
            if (direction == CardinalDirection.North)
            {
                // The compass has done a whole loop, return west //
                return CardinalDirection.West;
            }
            else
            {
                return --direction;
            }
        }

        /// <summary>
        /// Convert the supplied cardinal direction into a character representing its compass location.
        /// </summary>
        /// <param name="direction">The direction to convert.</param>
        /// <returns>A value representing the char representation of this direction.</returns>
        public static char ToDirectionalChar(CardinalDirection direction)
        {
            switch (direction)
            {
                case CardinalDirection.North:
                    return 'N';
                case CardinalDirection.East:
                    return 'E';
                case CardinalDirection.South:
                    return 'S';
                case CardinalDirection.West:
                    return 'W';
                default:
                    // This is an unknown direction, cancel //
                    return default;
            }
        }

    }
}
