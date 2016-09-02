# pathfinding-demo

A demonstration project made with the Unity3D engine, including a pseudo-randomly generated map of starsystems connected by a line network, and pathfinding for the shortest route from start to goal over this network. This project is a simplified example of how pathfinding might work in something like a 4X game with procedurally generated galaxy.

The user is able to interact by selecting starsystems, after which the calculated route will be highlighted.

The difficult issues around pathfinding are frequently not in the pathfinding algorithm itself, but in the way the pathfinding system integrates with the rest of the game. Ideally, the pathfinding system should be able to do its work without requiring any knowledge of game rules other than the costs to travel between nodes. 

Interfaces are a good way to ensure this separation of responsibilities. This way there is less potential for pieces of logic to leak into places where they should not belong. As an added benefit, this approach means our existing pathfinding code can easily be re-purposed for different projects. The entire `Src/Pathfinding` directory should be usable in a tile based game without changing a line of code. e.g. in a hex grid, every tile will be/have an IMapNode with 6 neighbours.

See [Wikipedia: A* search algorithm](https://en.wikipedia.org/wiki/A*_search_algorithm) for a thorough explanation of the pathfinding algorithm used in this project.