using System.Collections.Generic;

namespace PathfindingDemo
{
	/// <summary>
	/// General interface for pathfinding algorithms. Allows easily swapping an implementation for an optimised version or even different algorithm.
	/// </summary>
	interface IPathfinder
	{
		/// <summary>
		/// Attempts to calculate a path between start and goal. Returns null if no valid path could be found.
		/// Implementations should not be considered thread-safe, unless explicitly specified.
		/// </summary>
		IList<IMapNode> GetPath(IMapNode start, IMapNode goal);
	}
}
