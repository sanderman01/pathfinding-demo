using System.Collections.Generic;
using UnityEngine;

namespace PathfindingDemo
{
	/// <summary>
	/// Stores starsystems and edges (e.g. wormholes/jumplanes) between starsystems.
	/// </summary>
	public class GalaxyMap : MonoBehaviour
	{
		private List<StarSystem> startSystems = new List<StarSystem>();
		private List<Edge> edges = new List<Edge>();

		public IEnumerable<StarSystem> Systems { get { return startSystems; } }
		public IEnumerable<Edge> Edges { get { return edges; } }

		private Material starMaterial;

		[SerializeField]
		private Color starColor;

		void Awake()
		{
			starMaterial = new Material(Shader.Find("Unlit/Color"));
			starMaterial.color = starColor;
		}

		public StarSystem CreateStar(Vector3 pos)
		{
			StarSystem newStar = StarSystem.CreateStar(pos, starMaterial);
			newStar.transform.SetParent(transform);
			startSystems.Add(newStar);
			return newStar;
		}

		public bool Connect(StarSystem a, StarSystem b)
		{
			if (a.Connect(b))
			{
				edges.Add(new Edge(a, b));
				return true;
			}
			else return false;
		}
	}
}