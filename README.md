# EuroMoney
A Technical Test for EuroMoney

Project Overview/ Design Reasons:
I have structured this solution as a series of interchangeable components.
	
IArena, the arena is the brain of the solution, it has authority over all actions taken and is responsible for assuring the game state is correct. The arena provides all robots an interface to gather information about the state of play and what actions it can execute.
The idea is that an arena can be swapped out to add different functionality, or store game data in a different place (in a db for example), without affecting any other component.

IArenaInterface, this is used to tell a robot what information is available and what actions it may take, this could be extended to provide endpoints to gather the location of enemy bots, or to make aimed attacks.
The idea is that this can be swapped to a debug version that can log all actions sent to the arena, or maybe introduce a "delay" mechanic which causes actions to be executed slower by the interface.
	
IRobot, a robot is stupid, it only knows what it is told through the IArenaInterface. All actions are just "requests" which the arena will validate and return the latest state for that robot.
The idea is that new robots can be created, and their logic left simple as the arena does the heavy lifting. In this project I have included a robot which can have broken tracks/wheels, this impairs its ability to turn & move. The arena has no idea of this impairment as it's local only to the robot.

Please reach out to me if you have any questions or concerns,
