namespace EuroMoney.Library.Classes
{
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Helpers;
    using EuroMoney.Library.Models;

    /// <summary>
    /// This robot was created to show how a robot can override key components and issue different commands as needed. 
    /// </summary>
    public class InjuredRobot : SimpleCharRobot
    {
        public InjuredRobot(bool isLeftTrackWorking, bool isRightTrackWorking)
        {
            this.IsLeftTrackWorking = isLeftTrackWorking;
            this.IsRightTrackWorking = isRightTrackWorking;
        }

        /// <summary>
        /// Gets a value indicating the current working state of this robots left tracks. 
        /// </summary>
        protected bool IsLeftTrackWorking { get; }

        /// <summary>
        /// Gets a value indicating the current working state of this robots right tracks. 
        /// </summary>
        protected bool IsRightTrackWorking { get; }

        /// <summary>
        /// Attempt to issue a new turn command to the arena, changing the robots heading.
        /// </summary>
        /// <param name="currentLocation">The resolved location of the robot, after this action.</param>
        /// <param name="isCounterClockwise">A value indicating whether this is a counterClockwise movement.</param>
        /// <returns>A value indicating whether this movement was successful.</returns>
        protected override bool GenerateHeading(out RobotLocation currentLocation, bool isCounterClockwise = false)
        {
            // Initalize the current location as the last known position // 
            currentLocation = this.LastLocation;

            if(isCounterClockwise && !this.IsRightTrackWorking)
            {
                // The left track of this robot is busted, it cannot move counter-clockwise //
                return false;
            }
            else if(!isCounterClockwise && !this.IsLeftTrackWorking)
            {
                // The right track of this robot is busted, it cannot move clockwise //
                return false;
            }

            // This robot is able to move in the desired location, proceed //
            return base.GenerateHeading(out currentLocation, isCounterClockwise);
        }

        /// <summary>
        /// Attempt to issue a new movement command to the arena, moving the robot forward.
        /// </summary>
        /// <param name="currentLocation">The resolved location of the robot, after this action.</param>
        /// <param name="spaces">The amount of spaces to move forward.</param>
        /// <returns>A value indicating whether this movement was successful.</returns>
        protected override bool GenerateMovement(out RobotLocation currentLocation, int spaces = 1)
        {
            if(this.IsLeftTrackWorking && this.IsRightTrackWorking)
            {
                // Both tracks are working, forward movement is allowed //
                return base.GenerateMovement(out currentLocation, spaces);
            }

            // Generate a newly proposed location, with the broken track //
            RobotLocation proposedLocation = this.ResolveBrokenMovement(spaces);

            // Communicate with the arena, attempting to validate the desired movement //
            return this.ArenaInterface.AttemptMovement(proposedLocation, out currentLocation);
        }

        /// <summary>
        /// Attempt to resolve movement with a broken track, if both are broken then no movement is possible.
        /// </summary>
        /// <param name="spaces">The desired amount of spaces to move.</param>
        /// <returns>A new location corresponding to the desired location.</returns>
        protected RobotLocation ResolveBrokenMovement(int spaces)
        {
            int offset = this.IsRightTrackWorking ? 1 : -1;

            // Attempt to move the robot forward according to is current heading //
            int xAxis = this.LastLocation.XLocation;
            int yAxis = this.LastLocation.YLocation;

            switch (this.LastLocation.Heading)
            {
                case CardinalDirection.North:
                    // This robot is heading north, depending on which track is broken, it swings left or right //
                    xAxis -= spaces * offset;
                    break;
                case CardinalDirection.East:
                    // This robot is heading east, depending on which track is broken, it swings left or right //
                    yAxis += spaces * offset;
                    break;
                case CardinalDirection.South:
                    xAxis += spaces * offset;
                    // This robot is heading south, depending on which track is broken, it swings left or right //
                    break;
                case CardinalDirection.West:
                    // This robot is heading west, depending on which track is broken, it swings left or right //
                    yAxis -= spaces * offset;
                    break;
            }

            // Also generate a new heading as it could not move forward //
            CardinalDirection newHeading = this.IsRightTrackWorking ? 
                DirectionHelper.RotateCounterClockwise(this.LastLocation.Heading) : DirectionHelper.RotateClockwise(this.LastLocation.Heading);

            // Generate a newly proposed location //
            return new RobotLocation(newHeading, xAxis, yAxis);
        }
    }
}
