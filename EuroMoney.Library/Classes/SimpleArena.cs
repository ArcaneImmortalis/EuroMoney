namespace EuroMoney.Library.Classes
{
    using EuroMoney.Library.Models;

    public class SimpleArena : AbstractArena
    {
        public SimpleArena(int xBounds, int yBounds)
        {
            this.XBounds = xBounds;
            this.YBounds = yBounds;
        }

        /// <summary>
        /// Gets a value representing the X-Axis boundary for this simple arena.
        /// </summary>
        protected int XBounds { get; }

        /// <summary>
        /// Gets a value representing the Y-Axis boundary for this simple arena.
        /// </summary>
        protected int YBounds { get; }

        /// <summary>
        /// Attempt to resolve the proposed location, assuring it complies with the layout of this arena.
        /// </summary>
        /// <param name="currentLocation">The current location of the robot being moved.</param>
        /// <param name="targetLocation">The desired location the robot wishes to move to.</param>
        /// <returns>A value indicating whether this is a valid move.</returns>
        protected override bool ResolveProposedLocation(RobotLocation currentLocation, RobotLocation targetLocation)
        {
            if (targetLocation.XLocation < 0 || targetLocation.XLocation >= this.XBounds)
            {
                // This robot has moved out of the X boundaries, cancel //
                return false;
            }
            else if (targetLocation.YLocation < 0 || targetLocation.YLocation >= this.YBounds)
            {
                // This robot has moved out of the Y boundaries, cancel //
                return false;
            }

            // This is a valid movement, return //
            return true;
        }
    }
}
