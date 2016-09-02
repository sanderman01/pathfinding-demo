using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PathfindingDemo
{
	/// <summary>
	/// Fills the map with pseudo-randomly generated planets and edges.
	/// All planets are guaranteed to form a single connected graph, before random edges are added.
	/// Random edges frequently connect to nodes far away from each other.
	/// A higher number of random edges tends to result in a more interconnected map.
	/// </summary>
	public class GalaxyMapGenerator : MonoBehaviour
	{
		[SerializeField]
		private int nPlanets = 50;

		[SerializeField]
		private float galaxyRadius = 25;

		[SerializeField]
		private int nRandomEdges = 5;

		private GalaxyMap map;

		void Start()
		{
			map = GetComponent<GalaxyMap>();
			List<StarSystem> planets = GenerateRandomPlanets(map, nPlanets, galaxyRadius);
			AddConnections(map, planets, nRandomEdges);
		}

		private static List<StarSystem> GenerateRandomPlanets(GalaxyMap map, int nPlanets, float galaxyRadius)
		{
			List<StarSystem> planets = new List<StarSystem>(nPlanets);
			for (int i = 0; i < nPlanets; ++i)
			{
				// Calculate random position, but also reduce the vertical position of all planets to get a flat-ish map.
				Vector3 pos = Random.insideUnitSphere * galaxyRadius;
				pos.y *= 0.1f;
				StarSystem newPlanet = map.CreateStar(pos);
				planets.Add(newPlanet);
			}
			return planets;
		}

		private static void AddConnections(GalaxyMap map, List<StarSystem> planets, int nRandomEdges)
		{
			// First create a spanning tree to ensure that all points are connected in one graph, then add some more edges randomly.
			// We could also do Delaunay Triangulation to get a nicer looking graph with neat proximal edges between nodes, but that's quite a bit more work.
			MakeSpanningTree(map, planets);
			AddRandomEdges(map, planets, nRandomEdges);
		}

		/// <summary>
		/// Ensure that all planets are connected in a single graph. This method will generally try to keep distances short, but will not guarantee that all distances are the shortest possible.
		/// </summary>
		private static void MakeSpanningTree(GalaxyMap map, List<StarSystem> planets)
		{
			Queue<StarSystem> unConnected = new Queue<StarSystem>(planets);
			List<StarSystem> connected = new List<StarSystem>(planets.Count);

			StarSystem newlyAdding = unConnected.Dequeue();
			connected.Add(newlyAdding);

			// for each unconnected vertex add it to the graph by finding the nearest connected vertex and making an edge between the two.
			while (unConnected.Count > 0)
			{
				newlyAdding = unConnected.Dequeue();
				StarSystem nearest = FindNearest(newlyAdding, connected);
				map.Connect(nearest, newlyAdding);
				connected.Add(newlyAdding);
			}
		}

		private static StarSystem FindNearest(StarSystem target, IEnumerable<StarSystem> planets)
		{
			float lowestDistance = float.MaxValue;
			StarSystem nearest = null;

			foreach (StarSystem p in planets)
			{
				float distance = Vector3.Distance(p.transform.position, target.transform.position);
				if (lowestDistance > distance)
				{
					lowestDistance = distance;
					nearest = p;
				}
			}
			return nearest;
		}

		private static void AddRandomEdges(GalaxyMap map, List<StarSystem> planets, int nRandomEdges)
		{
			for (int i = 0; i < nRandomEdges; ++i)
			{
				StarSystem a = planets[Random.Range(0, planets.Count)];
				StarSystem b = planets[Random.Range(0, planets.Count)];

				// if adding an edge fails, (e.g. due to being a=b or an edge between a and b already existing) then decrement then decrement the counter to ignore this iteration.
				bool result = map.Connect(a, b);
				if (!result) { i--; }
			}
		}
	}
}