namespace EuroMoney.Library.Interfaces
{
    using EuroMoney.Library.Models;

    public interface IBattleReport
    {
        /// <summary>
        /// Gets a value representing the robot being reported on.
        /// </summary>
        IRobot Robot { get; }

        /// <summary>
        /// Gets a value representing the last known location of this robot.
        /// </summary>
        RobotLocation CurrentLocation { get; }

        /// <summary>
        /// Gets a value representing the total amount of penalties received by this robot.
        /// </summary>
        int Penalties { get; }
    }
}
