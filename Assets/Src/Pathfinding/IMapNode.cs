using System.Collections.Generic;

namespace PathfindingDemo
{
	/// <summary>
	/// Instance object of classes implementing this node interface can be traversed as a graph by a pathfinder.
	/// </summary>
	public interface IMapNode
	{
		/// <summary>
		/// All nodes directly adjacent to this one.
		/// </summary>
		IEnumerable<IMapNode> Neighbours { get; }

		/// <summary>
		/// The exact cost to move from this node to the neighbour node.
		/// In maps based on regular grids, (e.g square or hex tiles) cost might be dependent on the type of terrain in the neighbouring tile. (e.g. moving into forest or hills costs extra movement points)
		/// In more freeform node graphs, cost might be dependent on the euclidian distance between the two nodes. (e.g. the cost of traveling from one star to another depends on their distance from each other)
		/// </summary>
		float CostToNeighbour(IMapNode neighbour);

		/// <summary>
		///  The estimated cost to move from this node to another node which is not a neighbour.
		///  This cost will usually be calculated based on the number of tiles between the two nodes, or the euclidian distance between the two nodes.
		/// </summary>
		float EstimatedCostToNode(IMapNode node);
	}
}