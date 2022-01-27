namespace EuroMoney.Library.Classes
{
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Helpers;
    using EuroMoney.Library.Interfaces;
    using EuroMoney.Library.Models;

    public abstract class AbstractRobot<T> : ICommandableRobot<T>
    {
        public AbstractRobot()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this robot is current registered as a fighter in an arena.
        /// </summary>
        public bool IsRegistered
        {
            get { return this.ArenaInterface != null; }
        }

        /// <summary>
        /// Gets a value representing the interface this robot uses to report desired actions.
        /// </summary>
        protected IArenaInterface ArenaInterface { get; private set; }

        /// <summary>
        /// Gets or sets a value representing the last known location of this robot.
        /// </summary>
        protected RobotLocation LastLocation { get; set; }
        
        /// <summary>
        /// Set the desired state against this robot, assigning it to an arena.
        /// </summary>
        /// <param name="arenaInterface">An interface to use to communicate with the arena for information.</param>
        /// <param name="startingLocation">The starting location of this robot.</param>
        public bool SetArenaStatus(IArenaInterface arenaInterface, RobotLocation startingLocation)
        {
            if (this.IsRegistered)
            {
                // This robot is already registered in another fight, cancel //
                return false;
            }

            // This robot is now registered, set all starting state //
            this.ArenaInterface = arenaInterface;
            this.LastLocation = startingLocation;

            // All work is done, return //
            return true;
        }

        /// <summary>
        /// Attempt to execute the supplied action, updating the robots location.
        /// </summary>
        /// <param name="action">The desired action to perform.</param>
        /// <param name="newLocation">The updated location of this robot within the arena.</param>
        /// <returns>A value indicating whether this action was performed successfully.</returns>
        public bool TryExecuteAction(T action, out RobotLocation currentLocation)
        {
            currentLocation = null;

            if (!this.IsRegistered)
            {
                // This robot is not currently registered to fight, it cannot perform any actions //
                return false;
            }

            // This robot is in a fight, attempt to resolve its action //
            if (!this.TryResolveAction(action, out currentLocation))
            {
                // No action was performed, cancel //
                return false;
            }

            // This robot successfully performed its action, updated the last known location //
            this.LastLocation = currentLocation;

            // All work is done, return //
            return true;
        }

        /// <summary>
        /// Attempt to resolve the supplied command, generating the desired output.
        /// </summary>
        /// <param name="action">The action command to resolve.</param>
        /// <param name="currentLocation">The current location of the robot, after performing its action.</param>
        /// <returns>A value indicating whether this was successful.</returns>
        protected abstract bool TryResolveAction(T action, out RobotLocation currentLocation);

        /// <summary>
        /// Attempt to issue a new turn command to the arena, changing the robots heading.
        /// </summary>
        /// <param name="currentLocation">The resolved location of the robot, after this action.</param>
        /// <param name="isCounterClockwise">A value indicating whether this is a counterClockwise movement.</param>
        /// <returns>A value indicating whether this movement was successful.</returns>
        protected virtual bool GenerateHeading(out RobotLocation currentLocation, bool isCounterClockwise = false)
        {
            // Retrieve the last reported heading of this robot //
            CardinalDirection lastHeading = this.LastLocation.Heading;

            // Resolve the new heading for this robot //
            CardinalDirection newHeading = isCounterClockwise ?
                DirectionHelper.RotateCounterClockwise(lastHeading) : DirectionHelper.RotateClockwise(lastHeading);

            // Communicate with the arena, attempting to validate the change //
            return this.ArenaInterface.AttemptUpdateHeading(newHeading, out currentLocation);
        }

        /// <summary>
        /// Attempt to issue a new movement command to the arena, moving the robot forward.
        /// </summary>
        /// <param name="currentLocation">The resolved location of the robot, after this action.</param>
        /// <param name="spaces">The amount of spaces to move forward.</param>
        /// <returns>A value indicating whether this movement was successful.</returns>
        protected virtual bool GenerateMovement(out RobotLocation currentLocation, int spaces = 1)
        {
            // Generate a newly proposed location //
            RobotLocation proposedLocation = this.ResolveForwardMovement(spaces);

            // Communicate with the arena, attempting to validate the desired movement //
            return this.ArenaInterface.AttemptMovement(proposedLocation, out currentLocation);
        }

        /// <summary>
        /// Attempt to resolve a forward movement command, moving the robot the supplied amount of spaces in the last known heading.
        /// </summary>
        /// <param name="spaces">The desired amount of spaces to move.</param>
        /// <returns>A new location corresponding to the desired location.</returns>
        protected RobotLocation ResolveForwardMovement(int spaces)
        {
            // Attempt to move the robot forward according to is current heading //
            int xAxis = this.LastLocation.XLocation;
            int yAxis = this.LastLocation.YLocation;

            switch (this.LastLocation.Heading)
            {
                case CardinalDirection.North:
                    // This robot is heading north, increment the y axis //
                    yAxis += spaces;
                    break;
                case CardinalDirection.East:
                    // This robot is heading east, increment the x axis //
                    xAxis += spaces;
                    break;
                case CardinalDirection.South:
                    yAxis -= spaces;
                    // This robot is heading south, decrement the y axis //
                    break;
                case CardinalDirection.West:
                    // This robot is heading west, decrement the x axis //
                    xAxis -= spaces;
                    break;
            }

            // Generate a newly proposed location //
            return new RobotLocation(this.LastLocation.Heading, xAxis, yAxis);
        }
    }
}
