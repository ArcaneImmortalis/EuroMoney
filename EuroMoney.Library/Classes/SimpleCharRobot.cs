namespace EuroMoney.Library.Classes
{
    using EuroMoney.Library.Models;

    public class SimpleCharRobot : AbstractRobot<char>
    {
        public SimpleCharRobot()
            : base()
        {

        }

        public int ExecuteActionChain(string commandString, out RobotLocation finalLocation)
        {
            // Default the final location to the last known location //
            finalLocation = this.LastLocation;

            // Split the supplied string into a valid collection of commands //
            char[] commands = commandString.ToCharArray();

            int successfulCommands = 0;

            // Enumerate all commands, working out the final state //
            foreach(char c in commands)
            {
                if(this.TryExecuteAction(c, out finalLocation))
                {
                    // This was a successful action, record //
                    successfulCommands++;
                }
            }

            // Return a value representing the count of all successful commands //
            return successfulCommands;
        }

        /// <summary>
        /// Attempt to resolve the supplied command, generating the desired output.
        /// </summary>
        /// <param name="action">The action command to resolve.</param>
        /// <param name="currentLocation">The current location of the robot, after performing its action.</param>
        /// <returns>A value indicating whether this was successful.</returns>
        protected override bool TryResolveAction(char action, out RobotLocation currentLocation)
        {
            // Initalize the new location to the current known location, incase nothing changes //
            currentLocation = this.LastLocation;

            switch (action)
            {
                case 'L':
                    // Update the current heading of this robot, attempting to turn to the left //
                    return this.GenerateHeading(out currentLocation, true);
                case 'R':
                    // Update the current heading of this robot, attempting to turn to the right //
                    return this.GenerateHeading(out currentLocation, false);
                case 'M':
                    // Inform the arena of this movement, assuring this is still in bounds //
                    return this.GenerateMovement(out currentLocation, 1);
                default:
                    // This is not a known action, cancel //
                    return false;
            }
        }
    }
}
