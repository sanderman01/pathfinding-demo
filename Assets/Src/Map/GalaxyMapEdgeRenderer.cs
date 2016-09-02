using System.Collections.Generic;
using UnityEngine;

namespace PathfindingDemo
{
	/// <summary>
	/// Simply renders connections between starsystems using very basic GL line drawing.
	/// Immediate mode drawing is less efficient, compared to alternatives like DrawMesh or CommandBuffer.
	/// This renderer is only intended for demonstration purposes, not for production.
	/// </summary>
	public class GalaxyMapEdgeRenderer : MonoBehaviour
	{

		[SerializeField]
		private GalaxyMap map;

		[SerializeField]
		private Color lineColor = Color.gray;

		[SerializeField]
		private Color selectedLineColor = Color.green;

		private Material mat;

		void Awake()
		{
			mat = new Material(Shader.Find("Lines"));
		}

		void OnPostRender()
		{
			mat.SetPass(0);

			GL.PushMatrix();
			GL.Begin(GL.LINES);

			// Render map edges
			GL.Color(lineColor);
			foreach (Edge edge in map.Edges)
			{
				Vector3 a = edge.a.transform.position;
				Vector3 b = edge.b.transform.position;
				GL.Vertex(a);
				GL.Vertex(b);
			}

			// Render selected path edges
			IList<IMapNode> path = PathExplorer.CurrentPath;
			if(path != null)
			{
				GL.Color(selectedLineColor);
				for (int i = 0; i < path.Count - 1; ++i)
				{
					StarSystem a = (StarSystem)path[i];
					StarSystem b = (StarSystem)path[i + 1];
					GL.Vertex(a.Position);
					GL.Vertex(b.Position);
				}
			}

			GL.End();
			GL.PopMatrix();
		}
	}
}