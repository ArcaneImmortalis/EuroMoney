namespace EuroMoney.Library.Classes
{
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Interfaces;
    using EuroMoney.Library.Models;

    public class SimpleArenaInterface : IArenaInterface
    {
        public SimpleArenaInterface(IRobot robot, AbstractArena arena)
        {
            this.Robot = robot;
            this.Arena = arena;
        }

        /// <summary>
        /// Gets a value representing the robot this interface is conencted to.
        /// </summary>
        protected IRobot Robot { get; }

        /// <summary>
        /// Gets a value representing the arena this interface communicates with.
        /// </summary>
        protected AbstractArena Arena { get; }

        /// <summary>
        /// Attempt to perform a movement to the desired location.
        /// </summary>
        /// <param name="desiredLocation">The desired location to move towards.</param>
        /// <param name="currentLocation">The resulting location of this robot.</param>
        /// <returns>A value indicating whether this update was successful.</returns>
        public bool AttemptMovement(RobotLocation desiredLocation, out RobotLocation currentLocation)
        {
            return this.Arena.AttemptMovement(this.Robot, desiredLocation, out currentLocation);
        }

        /// <summary>
        /// Attempt to update the current heading, facing a new direction.
        /// </summary>
        /// <param name="heading">The desired direction to face.</param>
        /// <param name="currentLocation">The resulting location of this robot.</param>
        /// <returns>A value indicating whether this update was successful.</returns>
        public bool AttemptUpdateHeading(CardinalDirection heading, out RobotLocation currentLocation)
        {
            return this.Arena.AttemptUpdateHeading(this.Robot, heading, out currentLocation);
        }
    }
}
