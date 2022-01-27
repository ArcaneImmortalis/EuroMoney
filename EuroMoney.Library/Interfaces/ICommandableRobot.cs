namespace EuroMoney.Library.Interfaces
{
    using EuroMoney.Library.Models;

    /// <summary>
    /// Represents a contract that a commandable robot needs to implement.
    /// </summary>
    /// <typeparam name="T">The type of commands this robot accepts for processing.</typeparam>
    public interface ICommandableRobot<T> : IRobot
    {
        /// <summary>
        /// Attempt to execute the supplied action, updating the robots location.
        /// </summary>
        /// <param name="action">The desired action to perform.</param>
        /// <param name="newLocation">The updated location of this robot within the arena.</param>
        /// <returns>A value indicating whether this action was performed successfully.</returns>
        bool TryExecuteAction(T action, out RobotLocation newLocation);
    }
}
