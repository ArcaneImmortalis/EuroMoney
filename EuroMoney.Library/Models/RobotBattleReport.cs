namespace EuroMoney.Library.Models
{
    using EuroMoney.Library.Interfaces;

    public class RobotBattleReport : IBattleReport
    {
        public RobotBattleReport(IRobot robot, RobotLocation initialLocation)
        {
            this.Robot = robot;
            this.CurrentLocation = initialLocation;
        }

        /// <summary>
        /// Gets a value representing the robot being reported on.
        /// </summary>
        public IRobot Robot { get; private set; }

        /// <summary>
        /// Gets or sets a value representing the last known location of this robot.
        /// </summary>
        public RobotLocation CurrentLocation { get; set; }

        /// <summary>
        /// Gets a value representing the total amount of penalties received by this robot.
        /// </summary>
        public int Penalties { get; private set; }

        /// <summary>
        /// Increment the amount of penalities received by this robot.
        /// </summary>
        /// <returns>A value representing the current count of penalties.</returns>
        public int RecordPenalty()
        {
            return ++this.Penalties;
        }

        /// <summary>
        /// Update the last known location recorded for this robot.
        /// </summary>
        /// <param name="location">An object containing the details of the current robot position.</param>
        public void UpdateLocation(RobotLocation location)
        {
            this.CurrentLocation = location;
        }
    }
}
