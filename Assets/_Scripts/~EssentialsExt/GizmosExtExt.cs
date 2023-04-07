using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public static class GizmosExtExt
	{
		public static void DrawLocation2D(Location2D location, float radius)
		{
			var (position, direction) = location;
			GizmosExt.DrawCircle(position, radius, Vector3.up);
			GizmosExt.DrawLine(position, position + direction * radius);
		}
	}
}
