using UnityEngine;

namespace Util
{
	public static class PathfindingDemo
	{
		/// <summary>
		/// Returns the squared distance between a and b.
		/// This is a more efficient alternative to Vector3.Distance, avoiding an expensive square root operation.
		/// Useful for comparing relative distances.
		/// </summary>
		public static float DistanceSqr(this Vector3 a, Vector3 b)
		{
			return (b - a).sqrMagnitude;
		}
	}
}
