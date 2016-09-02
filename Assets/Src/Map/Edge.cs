using UnityEngine;

namespace PathfindingDemo
{
	/// <summary>
	/// Represents a connection between two starsystems. These could be wormholes/jumpgates/hyperlanes or other means of transport.
	/// Spaceships are only allowed to travel from one system to another via edges. 
	/// (otherwise ships could just fly in a straight line through open space and we wouldn't need any pathfinding)
	/// </summary>
	public struct Edge
	{
		public Edge(StarSystem a, StarSystem b)
		{
			this.a = a;
			this.b = b;
			this.distance = Vector3.Distance(a.Position, b.Position);
		}

		public readonly StarSystem a;
		public readonly StarSystem b;
		public readonly float distance;
	}
}
