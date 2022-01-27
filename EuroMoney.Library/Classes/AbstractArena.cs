namespace EuroMoney.Library.Classes
{
    using System.Collections.Generic;
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Interfaces;
    using EuroMoney.Library.Models;

    public abstract class AbstractArena : IArena
    {
        public AbstractArena()
        {
            this.RobotOverview = new Dictionary<IRobot, RobotBattleReport>();
        }

        /// <summary>
        /// Gets a value representing a collection of all robots that have been registered with this arena.
        /// </summary>
        protected Dictionary<IRobot, RobotBattleReport> RobotOverview { get; }

        /// <summary>
        /// Attempt to register the supplied robot as a fighter within this arena.
        /// </summary>
        /// <param name="robot">The new robot to register.</param>
        /// <param name="startingLocation">The initial location of the robot within this arena.</param>
        /// <returns>A value indicating whether this robot was successfully registered with this arena.</returns>
        public bool AttemptRegistration(IRobot robot, RobotLocation startingLocation)
        {
            lock (this.RobotOverview)
            {
                if (this.RobotOverview.TryGetValue(robot, out _))
                {
                    // This robot is already a registed fighter, cancel //
                    return false;
                }

                // Register this robot as a new contender, rembering its initial location //
                this.RobotOverview.Add(robot, new RobotBattleReport(robot, startingLocation));

                // Release the lock early, we do not care if the robot declines this registration //
            }

            // Generate a new interface for this robot, all communication will be handled though this //
            IArenaInterface arenaInterface = new SimpleArenaInterface(robot, this);

            if (!robot.SetArenaStatus(arenaInterface, startingLocation))
            {
                // This robot could not be registered with this arena, cancel //
                this.RobotOverview.Remove(robot);

                // This was not a successful registration //
                return false;
            }

            // This robot was successfully registered //
            return true;
        }

        /// <summary>
        /// Attempt to move the robot to the requested location, if possible.
        /// </summary>
        /// <param name="robot">The robot being targeted.</param>
        /// <param name="desiredLocation">The desired location to move the robot to.</param>
        /// <param name="currentLocation">The resulting location of this robot.</param>
        /// <returns>A value indicating whether this update was successful.</returns>
        public bool AttemptMovement(IRobot robot, RobotLocation desiredLocation, out RobotLocation currentLocation)
        {
            currentLocation = null;

            if (!this.RobotOverview.TryGetValue(robot, out RobotBattleReport report))
            {
                // This robot was not registered, cancel //
                return false;
            }

            // Attempt to validate the proposed movement //
            bool successfulMove = this.ResolveProposedLocation(report.CurrentLocation, desiredLocation);

            if (successfulMove)
            {
                // The proposed movement was accepted, update the last known location //
                report.UpdateLocation(desiredLocation);
            }
            else
            {
                // This update was not successful, this is a penalty //
                report.RecordPenalty();
            }

            // Set the last known location of the robot //
            currentLocation = report.CurrentLocation;

            // Return a value indicating whether the move was successful //
            return successfulMove;
        }

        /// <summary>
        /// Attempt to update the current heading, facing a new direction.
        /// </summary>
        /// <param name="robot">The robot being targeted.</param>
        /// <param name="heading">The desired direction to face.</param>
        /// <param name="currentLocation">The resulting location of this robot.</param>
        /// <returns>A value indicating whether this update was successful.</returns>
        public bool AttemptUpdateHeading(IRobot robot, CardinalDirection heading, out RobotLocation currentLocation)
        {
            currentLocation = null;

            if (!this.RobotOverview.TryGetValue(robot, out RobotBattleReport report))
            {
                // This robot was not registered, cancel //
                return false;
            }

            // Generate a new location, reflecting the updated heading //
            currentLocation = new RobotLocation(heading, report.CurrentLocation.XLocation, report.CurrentLocation.YLocation);

            // Set the last known location of the robot //
            report.UpdateLocation(currentLocation);

            // This robot has successfully changed heading, return //
            return true;
        }

        /// <summary>
        /// Attempt to retrieve a battle report for the supplied robot, including the current location and penality counts.
        /// </summary>
        /// <param name="robot">The robot being targeted.</param>
        /// <param name="report">The report associated with the supplied robot.</param>
        /// <returns>A value indicating whether a report was located.</returns>
        public bool TryRetrieveBattleReport(IRobot robot, out IBattleReport report)
        {
            // Attempt to retrieve a battle report for this robot //
            bool success = this.RobotOverview.TryGetValue(robot, out RobotBattleReport battleReport);

            // Assign the retrieved report as the output //
            report = battleReport;

            // Return a value indicating whether a report was retrieved //
            return success;
        }

        /// <summary>
        /// Attempt to resolve the proposed location, assuring it complies with the layout of this arena.
        /// </summary>
        /// <param name="currentLocation">The current location of the robot being moved.</param>
        /// <param name="targetLocation">The desired location the robot wishes to move to.</param>
        /// <returns>A value indicating whether this is a valid move.</returns>
        protected abstract bool ResolveProposedLocation(RobotLocation currentLocation, RobotLocation targetLocation);
    }
}
