using System.Collections.Generic;
using System.Linq;

namespace PathfindingDemo
{
	/// <summary>
	/// Pathfinder based on the famous A* algorithm.
	/// </summary>
	class AStar : IPathfinder
	{
		private static SortedList<float, IMapNode> openSet = new SortedList<float, IMapNode>();
		private static HashSet<IMapNode> closedSet = new HashSet<IMapNode>();
		private static Dictionary<IMapNode, IMapNode> cameFrom = new Dictionary<IMapNode, IMapNode>();
		private static Dictionary<IMapNode, float> gScore = new Dictionary<IMapNode, float>();
		//private static Dictionary<IMapNode, float> fScore = new Dictionary<IMapNode, float>();

		/// <summary>
		/// The algorithm is an almost direct translation from the one detailed the wiki page: https://en.wikipedia.org/wiki/A*_search_algorithm
		/// For the openSet, a SortedList is used to ensure the most promising candidates go first. (those with the lowest f-score)
		/// Since we now store f-scores as keys for ordering in the openSet, we no longer need an explicit fScore dictionary.
		/// 
		/// The SortedList approach should be more efficient than using Sort on a normal List, but it is still less efficient than a proper priority queue based on a heap datastructure.
		/// As such, further optimization could be achieved by implementing a proper heap.
		/// </summary>
		public IList<IMapNode> GetPath(IMapNode start, IMapNode goal)
		{
			// The set of nodes already evaluated.
			closedSet.Clear();
			// The set of currently discovered nodes still to be evaluated.
			// Initially, only the start node is known.
			openSet.Clear();
			openSet.Add(0, start);
			// For each node, which node it can most efficiently be reached from.
			// If a node can be reached from many nodes, cameFrom will eventually contain the
			// most efficient previous step.
			cameFrom.Clear();

			// For each node, the cost of getting from the start node to that node.
			gScore.Clear();
			// The cost of going from start to start is zero.
			gScore[start] = 0;
			// For each node, the total cost of getting from the start node to the goal
			// by passing by that node. That value is partly known, partly heuristic.
			//fScore.Clear();
			// For the first node, that value is completely heuristic.
			//fScore[start] = start.EstimatedCostToNode(goal);

			IMapNode current;
			float tentative_gScore;
			float neighbour_fScore;

			while (openSet.Count > 0)
			{
				current = openSet.First().Value;

				if (current == goal)
					return ReconstructPath(cameFrom, current);

				openSet.RemoveAt(0);
				closedSet.Add(current);

				foreach(IMapNode neighbour in current.Neighbours)
				{
					if (closedSet.Contains(neighbour))
						continue;        // Ignore the neighbor which is already evaluated.

					// The distance from start to a neighbor
					tentative_gScore = gScore[current] + current.CostToNeighbour(neighbour);
					neighbour_fScore = tentative_gScore + neighbour.EstimatedCostToNode(goal);
					if (!openSet.ContainsValue(neighbour))  // Discover a new node
						openSet.Add(neighbour_fScore, neighbour);
					else if (tentative_gScore >= gScore[neighbour])
						continue;        // This is not a better path.
					// This path is the best until now. Record it!
					cameFrom[neighbour] = current;
					gScore[neighbour] = tentative_gScore;
					//fScore[neighbour] = neighbour_fScore;
				}
			}
			return null;
		}

		private static IList<IMapNode> ReconstructPath(Dictionary<IMapNode, IMapNode> cameFrom, IMapNode current)
		{
			List<IMapNode> total_path = new List<IMapNode>();
			total_path.Add(current);

			while (cameFrom.ContainsKey(current))
			{
				current = cameFrom[current];
				total_path.Add(current);
			}
			return total_path;
		}
	}
}
