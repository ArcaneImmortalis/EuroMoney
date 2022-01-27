namespace EuroMoney.TestHarness
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EuroMoney.Library.Classes;
    using EuroMoney.Library.Enums;
    using EuroMoney.Library.Helpers;
    using EuroMoney.Library.Interfaces;
    using EuroMoney.Library.Models;

    class Program
    {
        static void Main(string[] args)
        {
            IArena arena = new SimpleArena(5, 5);

            // Attempt to resolve the first scenario //
            ResolveScenario("Scenario 1", "MLMRMMMRMMRR", arena, new RobotLocation(CardinalDirection.East, 0, 2));

            // Attempt to resolve the second scenario //
            ResolveScenario("Scenario 2", "LMLLMMLMMMRMM", arena, new RobotLocation(CardinalDirection.South, 4, 4));

            // Attempt to resolve the third scenario 
            ResolveScenario("Scenario 3", "MLMLMLM RMRMRMRM", arena, new RobotLocation(CardinalDirection.West, 2, 2));

            // Attempt to resolve the fourth scenario 
            ResolveScenario("Scenario 4", "MMLMMLMMMMM", arena, new RobotLocation(CardinalDirection.North, 1, 3));

            // All core scenarios have worked //
            Console.WriteLine("All work is done, just for fun press any key for the same scenarios but with a broken robot");
            Console.WriteLine("He has a broken right track, he cannot turn left and all M commands move it one tile to the right as it cannot go forward with one track.");

            Console.ReadLine();

            // Attempt to resolve the first scenario //
            ResolveScenario("Scenario 1 - X", "MLMRMMMRMMRR", arena, new RobotLocation(CardinalDirection.East, 0, 2), new InjuredRobot(true, false));

            // Attempt to resolve the second scenario //
            ResolveScenario("Scenario 2- X", "LMLLMMLMMMRMM", arena, new RobotLocation(CardinalDirection.South, 4, 4), new InjuredRobot(true, false));

            // Attempt to resolve the third scenario 
            ResolveScenario("Scenario 3- X", "MLMLMLM RMRMRMRM", arena, new RobotLocation(CardinalDirection.West, 2, 2), new InjuredRobot(true, false));

            // Attempt to resolve the fourth scenario 
            ResolveScenario("Scenario 4- X", "MMLMMLMMMMM", arena, new RobotLocation(CardinalDirection.North, 1, 3), new InjuredRobot(true, false));

            Console.WriteLine("All work is done, press any key to exit");

            Console.ReadLine();
        }

        protected static bool ResolveScenario(string alias, string commandString, IArena arena, RobotLocation startingLocation, ICommandableRobot<char> robot = null)
        {
            try
            {
                Console.Clear();

                Console.WriteLine($"Starting - {alias}");

                // Assure this scenario has a valid robot //
                ICommandableRobot<char> charRobot = robot ?? new SimpleCharRobot();

                if (!arena.AttemptRegistration(charRobot, startingLocation))
                {
                    // This robot not registered, cancel //
                    return false;
                }

                // This robot was registered to fight, proceed //
                Console.WriteLine($"Robot registered to fight!{Environment.NewLine}");

                // Log the actions this robot will take //
                Console.WriteLine($"Starting at: X{startingLocation.XLocation}, Y{startingLocation.YLocation}, {startingLocation.CardinalDirection}");
                Console.WriteLine($"The robot will perform: {commandString} {Environment.NewLine}");

                foreach(char command in commandString.ToCharArray())
                {
                    Console.WriteLine($"Executing Action: {command}");

                    // Attempt to execute the desired command //
                    bool success = charRobot.TryExecuteAction(command, out RobotLocation newLocation);

                    Console.WriteLine($"Action Result - {success}! The robot is at: X{newLocation.XLocation}, Y{newLocation.YLocation}, {newLocation.CardinalDirection}");
                }

                // All commands have been executed, retrieve the report from the arena //
                arena.TryRetrieveBattleReport(charRobot, out IBattleReport report);

                // All command executed, log the results //
                Console.WriteLine("Scenario complete:");
                Console.WriteLine($"The arena reports: X{report.CurrentLocation.XLocation}, Y{report.CurrentLocation.YLocation}, {report.CurrentLocation.CardinalDirection} - Total Penalities: {report.Penalties}");

                // Write out some standard formatting //
                Console.WriteLine($"{Environment.NewLine}Please press any key to move to the next action, you might have to scroll up for more details!");

                Console.ReadLine();
                Console.Clear();

                return true;
            }
            catch (Exception ex)
            {
                // Something went wrong, log the error //
                Console.WriteLine(ex);

                Console.WriteLine($"Something went wrong with {alias}");
                return false;
            }
        }
    }
}
