namespace EuroMoney.Library.Classes
{
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Models;

    public class SimpleEnumRobot : AbstractRobot<RobotAction>
    {
        public SimpleEnumRobot()
            : base()
        {

        }

        /// <summary>
        /// Attempt to resolve the supplied command, generating the desired output.
        /// </summary>
        /// <param name="action">The action command to resolve.</param>
        /// <param name="currentLocation">The current location of the robot, after performing its action.</param>
        /// <returns>A value indicating whether this was successful.</returns>
        protected override bool TryResolveAction(RobotAction action, out RobotLocation currentLocation)
        {
            // Initalize the new location to the current known location, incase nothing changes //
            currentLocation = this.LastLocation;

            switch (action)
            {
                case RobotAction.RotateClockwise:
                    // Update the current heading of this robot, attempting to turn to the right //
                    return this.GenerateHeading(out currentLocation, false);
                case RobotAction.RotateAntiClockwise:
                    // Update the current heading of this robot, attempting to turn to the left //
                    return this.GenerateHeading(out currentLocation, true);
                case RobotAction.MoveForward:
                    // Inform the arena of this movement, assuring this is still in bounds //
                    return this.GenerateMovement(out currentLocation, 1);
                default:
                    // This is not a known action, cancel //
                    return false;
            }
        }
    }
}
