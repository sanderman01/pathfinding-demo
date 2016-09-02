using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PathfindingDemo
{
	/// <summary>
	/// These are the individual path nodes, implementing IMapNode and also providing a scene representation of each node. Together they form a navigation graph for the pathfinder.
	/// </summary>
	public class StarSystem : MonoBehaviour, IMapNode, IHighlightable, IPointerClickHandler
	{
		List<IMapNode> neighbours = new List<IMapNode>();

		private Color color;
		private Vector3 position;

		public Vector3 Position
		{
			get { return position; }
			set { transform.position = position = value; }
		}

		public static StarSystem CreateStar(Vector3 position, Material material)
		{
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			obj.GetComponent<MeshRenderer>().material = material;
			StarSystem newStar = obj.AddComponent<StarSystem>();
			obj.AddComponent<SphereCollider>();
			newStar.Position = position;
			return newStar;
		}

		void Awake()
		{
			Renderer renderer = GetComponent<Renderer>();
			color = renderer.material.color;
		}

		public bool Connect(StarSystem other)
		{
			if (this != other && !neighbours.Contains(other))
			{
				this.neighbours.Add(other);
				other.neighbours.Add(this);
				return true;
			}
			else return false;
		}

		public IEnumerable<IMapNode> Neighbours
		{
			get { return neighbours; }
		}

		public float CostToNeighbour(IMapNode neighbour)
		{
			return EstimatedCostToNode(neighbour);
		}

		public float EstimatedCostToNode(IMapNode node)
		{
			if (node is StarSystem)
			{
				StarSystem otherSystem = (StarSystem)node;
				return Vector3.Distance(Position, otherSystem.Position);
			}
			else
			{
				throw new System.NotImplementedException();
			}
		}

		public void Highlight(Color highlightColor)
		{
			Renderer renderer = GetComponent<Renderer>();
			renderer.material.color = highlightColor;
		}

		public void UnHighlight()
		{
			Renderer renderer = GetComponent<Renderer>();
			renderer.material.color = color;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			PathExplorer.SelectSystem(this);
		}
	}
}