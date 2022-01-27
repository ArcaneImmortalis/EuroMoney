namespace EuroMoney.Library.Interfaces
{
    using EuroMoney.Library.Models;

    public interface IArena
    {
        /// <summary>
        /// Attempt to retrieve a battle report for the supplied robot, including the current location and penality counts.
        /// </summary>
        /// <param name="robot">The robot being targeted.</param>
        /// <param name="report">The report associated with the supplied robot.</param>
        /// <returns>A value indicating whether a report was located.</returns>
        bool TryRetrieveBattleReport(IRobot robot, out IBattleReport report);

        /// <summary>
        /// Attempt to register the supplied robot as a fighter within this arena.
        /// </summary>
        /// <param name="robot">The new robot to register.</param>
        /// <param name="startingLocation">The initial location of the robot within this arena.</param>
        /// <returns>A value indicating whether this robot was successfully registered with this arena.</returns>
        bool AttemptRegistration(IRobot robot, RobotLocation startingLocation);
    }
}
