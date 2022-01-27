namespace EuroMoney.Library.Interfaces
{
    using EuroMoney.Library.Models;

    /// <summary>
    /// Represents a contract that a standard robot needs to implement.
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Set the desired state against this robot, assigning it to an arena.
        /// </summary>
        /// <param name="arenaInterface">An interface to use to communicate with the arena for information.</param>
        /// <param name="startingLocation">The starting location of this robot.</param>
        /// <returns>A value indicating whether the status was successfully updated.</returns>
        bool SetArenaStatus(IArenaInterface arenaInterface, RobotLocation startingLocation);
    }
}
