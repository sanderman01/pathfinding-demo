using System.Collections.Generic;
using UnityEngine;

namespace PathfindingDemo
{
	/// <summary>
	/// Handles user interaction involving the selection of nodes and the visualisation of the current path.
	/// </summary>
	public class PathExplorer
	{
		private static Color PathEndColor = new Color(0.5f, 1.0f, 0.5f);
		private static Color PathColor = new Color(1.5f, 1.0f, 0.0f);

		private static IMapNode current;
		private static IMapNode previous;

		private static IList<IMapNode> currentPath;
		public static IList<IMapNode> CurrentPath { get { return currentPath; } }

		public static void SelectSystem(IMapNode system)
		{
			UnHighlight(currentPath);

			previous = current;
			current = system;

			if (current != null && previous != null)
			{
				// Calculate path
				currentPath = new AStar().GetPath(previous, current);

				if (currentPath != null)
				{
					Highlight(currentPath);
				}
			}
		}

		private static void Highlight(IList<IMapNode> path)
		{
			if (path != null && path.Count > 1)
			{
				// end nodes
				IMapNode startNode = path[0];
				IMapNode endNode = path[path.Count - 1];
				Highlight(startNode, PathEndColor);
				Highlight(endNode, PathEndColor);

				// in between nodes
				for (int i = 1; i < path.Count - 1; ++i)
				{
					IMapNode node = path[i];
					Highlight(node, PathColor);
				}
			}
		}

		private static void UnHighlight(IList<IMapNode> path)
		{
			if (path != null)
			{
				foreach (IMapNode node in path) { UnHighlight(node); }
			}
		}

		private static void Highlight(IMapNode node, Color color)
		{
			if (node is IHighlightable)
			{
				IHighlightable tile = (IHighlightable)node;
				tile.Highlight(color);
			}
			else
			{
				throw new System.Exception("I don't know how to highlight this");
			}
		}

		private static void UnHighlight(IMapNode node)
		{
			if (node is IHighlightable)
			{
				IHighlightable tile = (IHighlightable)node;
				tile.UnHighlight();
			}
			else
			{
				throw new System.Exception("I don't know how to unhighlight this");
			}
		}
	}
}