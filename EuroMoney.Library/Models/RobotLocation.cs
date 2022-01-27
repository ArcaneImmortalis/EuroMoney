namespace EuroMoney.Library.Models
{
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Helpers;

    public class RobotLocation
    {
        public RobotLocation(CardinalDirection heading, int xLocation, int yLocation)
        {
            this.Heading = heading;
            this.XLocation = xLocation;
            this.YLocation = yLocation;

            this.CardinalDirection = DirectionHelper.ToDirectionalChar(this.Heading);
        }

        /// <summary>
        /// Gets a value representing the current heading of this robot.
        /// </summary>
        public CardinalDirection Heading { get; }

        /// <summary>
        /// Gets a value representing the robots location along the X-axis of the arena.
        /// </summary>
        public int XLocation { get; }

        /// <summary>
        /// Gets a value representing the robots location along the Y-axis of the arena.
        /// </summary>
        public int YLocation { get; }
        
        /// <summary>
        /// Gets a value representing the heading of the robot, as a cardinal char.
        /// </summary>
        public char CardinalDirection { get; }
    }
}
