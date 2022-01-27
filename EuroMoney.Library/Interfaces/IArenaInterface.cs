namespace EuroMoney.Library.Interfaces
{
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Models;

    /// <summary>
    /// Represents a contract that an arena interface needs to implement, this is used to bridge commands between robots and the arena.
    /// </summary>
    public interface IArenaInterface
    {
        /// <summary>
        /// Attempt to perform a movement to the desired location.
        /// </summary>
        /// <param name="desiredLocation">The desired location to move towards.</param>
        /// <param name="currentLocation">The resulting location of this robot.</param>
        /// <returns>A value indicating whether this update was successful.</returns>
        bool AttemptMovement(RobotLocation desiredLocation, out RobotLocation currentLocation);

        /// <summary>
        /// Attempt to update the current heading, facing a new direction.
        /// </summary>
        /// <param name="heading">The desired direction to face.</param>
        /// <param name="currentLocation">The resulting location of this robot.</param>
        /// <returns>A value indicating whether this update was successful.</returns>
        bool AttemptUpdateHeading(CardinalDirection heading, out RobotLocation currentLocation);
    }
}
