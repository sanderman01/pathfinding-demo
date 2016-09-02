using UnityEngine;

namespace PathfindingDemo
{
	/// <summary>
	/// Interface for an object that can be highlighted.
	/// </summary>
	public interface IHighlightable
	{
		/// <summary>
		/// Enable highlighting this object
		/// </summary>
		/// <param name="color"></param>
		void Highlight(Color color);

		/// <summary>
		/// Disable highlighting on this object
		/// </summary>
		void UnHighlight();
	}
}